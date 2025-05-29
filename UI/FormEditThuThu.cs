using LibraryManagementVersion2.Repositories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormEditThuThu : Form
    {
        private BLThuThu blThuThu = new BLThuThu();
        private string err;
        private int maThuThu;

        public FormEditThuThu(int maThuThu)
        {
            InitializeComponent();
            this.maThuThu = maThuThu;
            InitializeCustomStyle();
            LoadThuThuData();
        }

        private void InitializeCustomStyle()
        {
            Color mainColor = ColorTranslator.FromHtml("#739a4f");
            this.BackColor = Color.White;

            // Set focus vào textbox đầu tiên
            this.ActiveControl = txtTenThuThu;
        }

        private void LoadThuThuData()
        {
            try
            {
                var thuThu = blThuThu.LayThuThuTheoMa(maThuThu);
                if (thuThu != null)
                {
                    txtTenThuThu.Text = thuThu.TenThuThu ?? "";
                    txtEmail.Text = thuThu.Email ?? "";
                    txtSoDienThoai.Text = thuThu.SoDienThoai ?? "";
                    txtDiaChi.Text = thuThu.DiaChi ?? "";

                    // Xử lý ngày sinh
                    if (thuThu.NgaySinh.HasValue)
                        dtpNgaySinh.Value = thuThu.NgaySinh.Value;
                    else
                        dtpNgaySinh.Value = DateTime.Now.AddYears(-25);

                    // Xử lý ngày bắt đầu làm
                    if (thuThu.NgayBatDauLam.HasValue)
                        dtpNgayBatDauLam.Value = thuThu.NgayBatDauLam.Value;
                    else
                        dtpNgayBatDauLam.Value = DateTime.Now;

                    // Xử lý giới tính
                    if (thuThu.GioiTinh == "M")
                        cboGioiTinh.SelectedIndex = 0; // Nam
                    else if (thuThu.GioiTinh == "F")
                        cboGioiTinh.SelectedIndex = 1; // Nữ
                    else
                        cboGioiTinh.SelectedIndex = -1; // Không chọn

                    // Xử lý trạng thái
                    chkTrangThai.Checked = thuThu.TrangThai ?? true;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin thủ thư!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                // Xác định giới tính
                string gioiTinh = "";
                if (cboGioiTinh.SelectedIndex == 0)
                    gioiTinh = "M"; // Nam
                else if (cboGioiTinh.SelectedIndex == 1)
                    gioiTinh = "F"; // Nữ

                bool success = blThuThu.CapNhatThuThu(
                    maThuThu,
                    txtTenThuThu.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtSoDienThoai.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    dtpNgayBatDauLam.Value,
                    dtpNgaySinh.Value,
                    gioiTinh,
                    chkTrangThai.Checked,
                    ref err
                );

                if (success)
                {
                    MessageBox.Show("Cập nhật thủ thư thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thủ thư thất bại: " + err, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn hủy các thay đổi?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            // Kiểm tra tên thủ thư (bắt buộc)
            if (string.IsNullOrWhiteSpace(txtTenThuThu.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thủ thư!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenThuThu.Focus();
                return false;
            }

            // Kiểm tra độ dài tên
            if (txtTenThuThu.Text.Trim().Length > 100)
            {
                MessageBox.Show("Tên thủ thư không được vượt quá 100 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenThuThu.Focus();
                return false;
            }

            // Kiểm tra email (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }

                if (txtEmail.Text.Trim().Length > 100)
                {
                    MessageBox.Show("Email không được vượt quá 100 ký tự!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            // Kiểm tra số điện thoại (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                if (!IsValidPhoneNumber(txtSoDienThoai.Text.Trim()))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! (Định dạng: 0xxxxxxxxx)", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoDienThoai.Focus();
                    return false;
                }

                if (txtSoDienThoai.Text.Trim().Length > 15)
                {
                    MessageBox.Show("Số điện thoại không được vượt quá 15 ký tự!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoDienThoai.Focus();
                    return false;
                }
            }

            // Kiểm tra địa chỉ
            if (!string.IsNullOrWhiteSpace(txtDiaChi.Text) && txtDiaChi.Text.Trim().Length > 255)
            {
                MessageBox.Show("Địa chỉ không được vượt quá 255 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }

            // Kiểm tra ngày sinh hợp lý
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không thể là ngày trong tương lai!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // Kiểm tra tuổi hợp lý (từ 18 đến 65 tuổi)
            int tuoi = DateTime.Now.Year - dtpNgaySinh.Value.Year;
            if (DateTime.Now.DayOfYear < dtpNgaySinh.Value.DayOfYear)
                tuoi--;

            if (tuoi < 18 || tuoi > 65)
            {
                MessageBox.Show("Tuổi thủ thư phải từ 18 đến 65 tuổi!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // Kiểm tra ngày bắt đầu làm
            if (dtpNgayBatDauLam.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày bắt đầu làm không thể là ngày trong tương lai!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayBatDauLam.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Kiểm tra số điện thoại Việt Nam (10-11 số, bắt đầu bằng 0)
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^0\d{9,10}$");
        }

        // Event handlers cho các controls (nếu cần)
        private void txtTenThuThu_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép chữ cái, số và khoảng trắng
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FormEditThuThu_Load(object sender, EventArgs e)
        {
            // Set focus vào textbox đầu tiên
            txtTenThuThu.Focus();
        }

        // Override để xử lý phím Enter
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnLuu_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                btnHuy_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
