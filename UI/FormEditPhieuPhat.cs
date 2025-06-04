using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormEditPhieuPhat : Form
    {
        private int maPhieuPhat;
        private LibraryEntities db = new LibraryEntities();
        private List<QDP> dsTatCaQDP;

        private ComboBox cboDocGia;
        private CheckedListBox clbLoiViPham;
        private ComboBox cboTrangThai;
        private Button btnSave, btnCancel;

        public FormEditPhieuPhat(int maPhieuPhat)
        {
            this.maPhieuPhat = maPhieuPhat;

            InitCustomComponent();
            LoadDocGia();
            LoadLoiViPham();
            LoadData();
        }

        private void InitCustomComponent()
        {
            this.Text = "Sửa Phiếu Phạt";
            this.Size = new Size(450, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblDocGia = new Label() { Text = "Độc giả:", Location = new Point(20, 20), AutoSize = true };
            cboDocGia = new ComboBox() { Location = new Point(100, 17), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(lblDocGia);
            this.Controls.Add(cboDocGia);

            Label lblLoi = new Label() { Text = "Lỗi vi phạm:", Location = new Point(20, 60), AutoSize = true };
            clbLoiViPham = new CheckedListBox() { Location = new Point(100, 60), Size = new Size(300, 200) };
            this.Controls.Add(lblLoi);
            this.Controls.Add(clbLoiViPham);

            Label lblTrangThai = new Label() { Text = "Trạng thái:", Location = new Point(20, 275), AutoSize = true };
            cboTrangThai = new ComboBox() { Location = new Point(100, 272), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThai.Items.Add("Chưa thanh toán");
            cboTrangThai.Items.Add("Đã thanh toán");
            cboTrangThai.SelectedIndex = 0;
            this.Controls.Add(lblTrangThai);
            this.Controls.Add(cboTrangThai);

            btnSave = new Button() { Text = "Lưu", Location = new Point(100, 330), Size = new Size(80, 35) };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button() { Text = "Hủy", Location = new Point(200, 330), Size = new Size(80, 35) };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void LoadDocGia()
        {
            var docGias = db.DocGias.ToList();
            cboDocGia.DataSource = docGias;
            cboDocGia.DisplayMember = "HoTen";
            cboDocGia.ValueMember = "MaDocGia";
        }

        private void LoadLoiViPham()
        {
            dsTatCaQDP = db.QDPs.ToList();
            clbLoiViPham.Items.Clear();
            foreach (var qdp in dsTatCaQDP)
            {
                clbLoiViPham.Items.Add(qdp); 
            }
        }

        private void LoadData()
        {
            var phieu = db.PhieuPhats.Include("QDPs").FirstOrDefault(p => p.MaPhieuPhat == maPhieuPhat);
            if (phieu == null)
            {
                MessageBox.Show("Không tìm thấy phiếu phạt!");
                this.Close();
                return;
            }

            cboDocGia.SelectedValue = phieu.MaDG;

            if (!string.IsNullOrEmpty(phieu.TrangThai))
                cboTrangThai.SelectedItem = phieu.TrangThai;

            var qdpList = phieu.QDPs.Select(q => q.MaQDP).ToList();
            for (int i = 0; i < clbLoiViPham.Items.Count; i++)
            {
                var item = (QDP)clbLoiViPham.Items[i];
                if (qdpList.Contains(item.MaQDP))
                    clbLoiViPham.SetItemChecked(i, true);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cboDocGia.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả.");
                return;
            }

            var selectedQDPs = clbLoiViPham.CheckedItems.Cast<QDP>().ToList();
            if (!selectedQDPs.Any())
            {
                MessageBox.Show("Vui lòng chọn ít nhất một lỗi vi phạm.");
                return;
            }

            var phieuPhat = db.PhieuPhats.Include("QDPs").FirstOrDefault(p => p.MaPhieuPhat == maPhieuPhat);
            if (phieuPhat == null)
            {
                MessageBox.Show("Không tìm thấy phiếu phạt để cập nhật.");
                return;
            }

            phieuPhat.MaDG = (int)cboDocGia.SelectedValue;
            phieuPhat.TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Chưa thanh toán";

            // Cập nhật danh sách QDPs
            phieuPhat.QDPs.Clear();
            foreach (var qdp in selectedQDPs)
            {
                var entity = db.QDPs.Find(qdp.MaQDP);
                if (entity != null)
                    phieuPhat.QDPs.Add(entity);
            }

            try
            {
                db.SaveChanges();
                MessageBox.Show("Cập nhật phiếu phạt thành công.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại: " + ex.Message);
            }
        }
    }
}
