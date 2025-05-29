using LibraryManagementVersion2.Repositories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormAddThuThu : Form
    {
        private BLThuThu blThuThu = new BLThuThu();
        private string err;

        public FormAddThuThu()
        {
            InitializeComponent();
            InitializeCustomStyle();
        }

        private void InitializeCustomStyle()
        {
            Color mainColor = ColorTranslator.FromHtml("#739a4f");
            this.BackColor = Color.White;

            // Set các giá trị mặc định
            dtpNgaySinh.Value = DateTime.Now.AddYears(-25);
            dtpNgayBatDauLam.Value = DateTime.Now;
            chkTrangThai.Checked = true;
            cboGioiTinh.SelectedIndex = -1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                string gioiTinh = cboGioiTinh.SelectedIndex == 0 ? "M" :
                                 (cboGioiTinh.SelectedIndex == 1 ? "F" : "");

                bool success = blThuThu.ThemThuThu(
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
                    MessageBox.Show("Thêm thủ thư thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm thủ thư thất bại: " + err, "Lỗi",
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
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenThuThu.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thủ thư!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenThuThu.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtSoDienThoai.Text) && !IsValidPhoneNumber(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
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
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^0\d{9,10}$");
        }
    }
}