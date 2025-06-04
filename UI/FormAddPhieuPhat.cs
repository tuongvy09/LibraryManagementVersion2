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
    public partial class FormAddPhieuPhat : Form
    {
        private LibraryEntities dbContext = new LibraryEntities();

        private Label lblTitle, lblDocGia, lblLoiViPham;
        private ComboBox cboDocGia;
        private CheckedListBox clbLoiViPham;
        private Button btnSave, btnCancel;

        public FormAddPhieuPhat()
        {
            InitializeComponentForm();
            InitializeCustomStyle();
            LoadComboBoxData();
            LoadLoiViPhamData();
        }

        private void InitializeComponentForm()
        {
            this.Text = "Thêm Phiếu Phạt";
            this.Size = new Size(500, 450);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeCustomStyle()
        {
            Color mainColor = ColorTranslator.FromHtml("#739a4f");
            this.BackColor = Color.White;

            lblTitle = new Label()
            {
                Text = "Thêm Phiếu Phạt",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = mainColor,
                Location = new Point(170, 15),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            lblDocGia = new Label()
            {
                Text = "Độc giả: *",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblDocGia);

            cboDocGia = new ComboBox()
            {
                Location = new Point(150, 57),
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(cboDocGia);

            lblLoiViPham = new Label()
            {
                Text = "Lỗi vi phạm: *",
                Location = new Point(20, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblLoiViPham);

            clbLoiViPham = new CheckedListBox()
            {
                Location = new Point(150, 100),
                Width = 300,
                Height = 180,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(clbLoiViPham);

            btnSave = new Button()
            {
                Text = "Lưu",
                BackColor = mainColor,
                ForeColor = Color.White,
                Location = new Point(150, 300),
                Size = new Size(80, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button()
            {
                Text = "Hủy",
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Location = new Point(250, 300),
                Size = new Size(80, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);

            Label lblNote = new Label()
            {
                Text = "* Trường bắt buộc",
                Location = new Point(20, 350),
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.Red
            };
            this.Controls.Add(lblNote);
        }

        private void LoadComboBoxData()
        {
            try
            {
                var docGias = dbContext.DocGias.ToList();
                cboDocGia.DataSource = docGias;
                cboDocGia.DisplayMember = "HoTen";
                cboDocGia.ValueMember = "MaDocGia";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Độc giả: " + ex.Message);
            }
        }

        private void LoadLoiViPhamData()
        {
            try
            {
                var loiViPhams = dbContext.QDPs.ToList(); 
                clbLoiViPham.DataSource = loiViPhams;
                clbLoiViPham.DisplayMember = "Lydo"; 
                clbLoiViPham.ValueMember = "MaQDP";   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Lỗi vi phạm: " + ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cboDocGia.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clbLoiViPham.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một lỗi vi phạm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedDocGia = cboDocGia.SelectedItem as DocGia;

                var phieuPhat = new PhieuPhat
                {
                    MaDG = selectedDocGia.MaDocGia,
                    TrangThai = "Chưa thanh toán"
                };

                foreach (var item in clbLoiViPham.CheckedItems)
                {
                    if (item is QDP qdp)
                    {
                        dbContext.QDPs.Attach(qdp);
                        phieuPhat.QDPs.Add(qdp); 
                    }
                }

                dbContext.PhieuPhats.Add(phieuPhat);
                dbContext.SaveChanges();

                MessageBox.Show("Thêm phiếu phạt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm phiếu phạt thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
