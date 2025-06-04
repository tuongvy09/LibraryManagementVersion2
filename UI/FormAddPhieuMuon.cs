using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using static LibraryManagementVersion2.BL.BLPhieuMuon;

namespace LibraryManagementVersion2.UI
{
    public partial class FormAddPhieuMuon : Form
    {
        private ComboBox cbTenDocGia, cbTenCuonSach;
        private DateTimePicker dtpNgayMuon, dtpNgayTra;
        private TextBox txtGiaMuon, txtTienCoc;
        private Label lblTrangThaiValue;
        private Button btnSave, btnCancel;

        private Dictionary<string, int> docGiaDict = new Dictionary<string, int>();
        private Dictionary<string, int> cuonSachDict = new Dictionary<string, int>();

        public FormAddPhieuMuon()
        {
            InitializeComponent();
            InitializeCustomComponent();
            LoadComboBoxData();
            UpdateTrangThai();
        }

        private void InitializeCustomComponent()
        {
            this.Text = "Thêm Phiếu Mượn";
            this.Size = new Size(500, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            Color mainColor = ColorTranslator.FromHtml("#739a4f");

            Label lblTitle = new Label()
            {
                Text = "THÊM PHIẾU MƯỢN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = mainColor,
                AutoSize = true,
                Location = new Point(30, 20)
            };
            this.Controls.Add(lblTitle);

            int y = 70;
            AddLabel("Tên Độc Giả:", 30, y, mainColor);
            cbTenDocGia = AddComboBox(180, y); y += 40;

            AddLabel("Tên Cuốn Sách:", 30, y, mainColor);
            cbTenCuonSach = AddComboBox(180, y); y += 40;

            AddLabel("Ngày Mượn:", 30, y, mainColor);
            dtpNgayMuon = AddDateTimePicker(180, y); y += 40;

            AddLabel("Ngày Trả:", 30, y, mainColor);
            dtpNgayTra = AddDateTimePicker(180, y);
            dtpNgayTra.ValueChanged += (s, e) => UpdateTrangThai();
            y += 40;

            AddLabel("Trạng Thái:", 30, y, mainColor);
            lblTrangThaiValue = new Label()
            {
                Location = new Point(180, y),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = true
            };
            this.Controls.Add(lblTrangThaiValue);
            y += 40;

            AddLabel("Giá Mượn:", 30, y, mainColor);
            txtGiaMuon = AddTextBox(180, y); y += 40;

            AddLabel("Tiền Cọc:", 30, y, mainColor);
            txtTienCoc = AddTextBox(180, y); y += 50;

            btnSave = new Button()
            {
                Text = "Lưu",
                Location = new Point(130, y),
                Size = new Size(100, 35),
                BackColor = mainColor,
                ForeColor = Color.White
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button()
            {
                Text = "Hủy",
                Location = new Point(250, y),
                Size = new Size(100, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White
            };
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);
        }

        private void LoadComboBoxData()
        {
            using (var context = new LibraryEntities())
            {
                var docGias = context.DocGias.ToList();
                foreach (var dg in docGias)
                {
                    cbTenDocGia.Items.Add(dg.HoTen);
                    docGiaDict[dg.HoTen] = dg.MaDocGia;
                }

                var cuonSachs = context.CuonSaches.Include(c => c.DauSach).ToList();
                foreach (var cs in cuonSachs)
                {
                    string tenHienThi = cs.TenCuonSach; // hoặc lấy từ cs.DauSach.TenDauSach
                    cbTenCuonSach.Items.Add(tenHienThi);
                    cuonSachDict[tenHienThi] = cs.MaCuonSach;
                }
            }

            if (cbTenDocGia.Items.Count > 0) cbTenDocGia.SelectedIndex = 0;
            if (cbTenCuonSach.Items.Count > 0) cbTenCuonSach.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int maDocGia = docGiaDict[cbTenDocGia.SelectedItem.ToString()];
            int maCuonSach = cuonSachDict[cbTenCuonSach.SelectedItem.ToString()];

            decimal.TryParse(txtGiaMuon.Text.Trim(), out decimal giaMuon);
            decimal.TryParse(txtTienCoc.Text.Trim(), out decimal tienCoc);

            using (var context = new LibraryEntities())
            {
                var pms = new PhieuMuonSach
                {
                    MaDocGia = maDocGia
                };
                context.PhieuMuonSaches.Add(pms);
                context.SaveChanges();

                var ms = new MuonSach
                {
                    MaPhieu = pms.MaPhieu,
                    NgayMuon = dtpNgayMuon.Value.Date,
                    NgayTra = dtpNgayTra.Value.Date,
                    TrangThaiM = dtpNgayTra.Value.Date <= DateTime.Now ? "Da tra" : "Chua tra",
                    GiaMuon = giaMuon,
                    SoNgayMuon = (dtpNgayTra.Value.Date - dtpNgayMuon.Value.Date).Days,
                    TienCoc = tienCoc
                };
                context.MuonSaches.Add(ms);

                // Optionally gắn thêm vào bảng liên kết nếu có

                MessageBox.Show("Thêm phiếu mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void UpdateTrangThai()
        {
            bool daTra = dtpNgayTra.Value.Date <= DateTime.Now.Date && dtpNgayTra.Value.Date >= dtpNgayMuon.Value.Date;
            lblTrangThaiValue.Text = daTra ? "Đã trả" : "Chưa trả";
            lblTrangThaiValue.ForeColor = daTra ? Color.Green : Color.Red;
        }

        private bool ValidateInput()
        {
            if (cbTenDocGia.SelectedIndex < 0 || cbTenCuonSach.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn độc giả và sách.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpNgayTra.Value.Date < dtpNgayMuon.Value.Date)
            {
                MessageBox.Show("Ngày trả không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtGiaMuon.Text.Trim(), out _) || !decimal.TryParse(txtTienCoc.Text.Trim(), out _))
            {
                MessageBox.Show("Giá mượn và tiền cọc phải là số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void AddLabel(string text, int x, int y, Color color)
        {
            this.Controls.Add(new Label()
            {
                Text = text,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10),
                ForeColor = color,
                AutoSize = true
            });
        }

        private ComboBox AddComboBox(int x, int y)
        {
            var cb = new ComboBox()
            {
                Location = new Point(x, y),
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(cb);
            return cb;
        }

        private DateTimePicker AddDateTimePicker(int x, int y)
        {
            var dtp = new DateTimePicker()
            {
                Location = new Point(x, y),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10),
                Value = DateTime.Now
            };
            this.Controls.Add(dtp);
            return dtp;
        }

        private TextBox AddTextBox(int x, int y)
        {
            var tb = new TextBox()
            {
                Location = new Point(x, y),
                Width = 250,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(tb);
            return tb;
        }
    }
}
