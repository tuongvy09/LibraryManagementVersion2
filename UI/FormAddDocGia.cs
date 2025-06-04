using LibraryManagementVersion2.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormAddDocGia : Form
    {
        private BLDocGia blDocGia = new BLDocGia();
        private string err;
        private bool isLoading = false;

        public FormAddDocGia()
        {
            InitializeComponent();
            InitializeCustomStyle();
            LoadComboBoxData();
        }

        private void InitializeCustomStyle()
        {
            this.BackColor = Color.White;

            // Set các giá trị mặc định
            chkTrangThai.Checked = true;
            cboGioiTinh.SelectedIndex = -1;

            // ✅ Set title cho form
            this.Text = "Thêm Độc giả";
        }

        private void LoadComboBoxData()
        {
            if (isLoading) return; // Tránh recursive loading

            try
            {
                isLoading = true;

                // Clear ComboBox trước khi load
                cboLoaiDocGia.DataSource = null;
                cboLoaiDocGia.Items.Clear();

                // Load loại độc giả
                DataTable dtLoaiDG = blDocGia.LayLoaiDocGia();

                if (dtLoaiDG != null && dtLoaiDG.Rows.Count > 0)
                {
                    // ✅ Không thêm dòng "-- Chọn loại độc giả --" nữa
                    // Vì sẽ gây lỗi khóa ngoại khi MaLoaiDG = null

                    cboLoaiDocGia.DataSource = dtLoaiDG;
                    cboLoaiDocGia.DisplayMember = "TenLoaiDG";
                    cboLoaiDocGia.ValueMember = "MaLoaiDG";

                    // ✅ Chọn item đầu tiên (loại độc giả hợp lệ)
                    if (dtLoaiDG.Rows.Count > 0)
                    {
                        cboLoaiDocGia.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Không có loại độc giả nào trong hệ thống!\nVui lòng thêm loại độc giả trước.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu ComboBox: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                // ✅ Xử lý giới tính
                string gioiTinh = null;
                if (cboGioiTinh.SelectedIndex == 0)
                    gioiTinh = "Nam";
                else if (cboGioiTinh.SelectedIndex == 1)
                    gioiTinh = "Nữ";

                // ✅ Xử lý MaLoaiDG - PHẢI có giá trị hợp lệ
                int maLoaiDG;
                if (cboLoaiDocGia.SelectedValue == null ||
                    !int.TryParse(cboLoaiDocGia.SelectedValue.ToString(), out maLoaiDG))
                {
                    MessageBox.Show("Vui lòng chọn loại độc giả hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboLoaiDocGia.Focus();
                    return;
                }

                // ✅ Validate CCCD và Email trước khi gọi method
                string cccd = string.IsNullOrWhiteSpace(txtCCCD.Text) ? null : txtCCCD.Text.Trim();
                string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string diaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim();

                bool success = blDocGia.ThemDocGia(
                    txtHoTen.Text.Trim(),
                    Convert.ToInt32(txtTuoi.Text.Trim()),
                    txtSoDT.Text.Trim(),
                    cccd,
                    gioiTinh,
                    email,
                    diaChi,
                    maLoaiDG, // ✅ Truyền int thay vì int?
                    chkTrangThai.Checked,
                    ref err
                );

                if (success)
                {
                    MessageBox.Show("Thêm độc giả thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm độc giả thất bại: " + err, "Lỗi",
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTuoi.Text))
            {
                MessageBox.Show("Vui lòng nhập tuổi!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuoi.Focus();
                return false;
            }

            if (!int.TryParse(txtTuoi.Text, out int tuoi) || tuoi <= 0 || tuoi > 150)
            {
                MessageBox.Show("Tuổi phải là số từ 1 đến 150!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuoi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSoDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDT.Focus();
                return false;
            }

            // ✅ Validate số điện thoại theo cấu trúc database (char(10))
            if (txtSoDT.Text.Trim().Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 chữ số!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDT.Focus();
                return false;
            }

            if (!IsValidPhoneNumber(txtSoDT.Text.Trim()))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! (Định dạng: 0xxxxxxxxx)", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDT.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // ✅ Validate CCCD theo cấu trúc database (char(12))
            if (!string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                if (txtCCCD.Text.Trim().Length != 12)
                {
                    MessageBox.Show("CCCD phải có đúng 12 chữ số!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCCCD.Focus();
                    return false;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(txtCCCD.Text.Trim(), @"^\d{12}$"))
                {
                    MessageBox.Show("CCCD chỉ được chứa các chữ số!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCCCD.Focus();
                    return false;
                }
            }

            // ✅ Validate loại độc giả
            if (cboLoaiDocGia.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại độc giả!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiDocGia.Focus();
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
            // ✅ Validate số điện thoại Việt Nam 10 số bắt đầu bằng 0
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^0\d{9}$");
        }

        private void txtTuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ✅ Giới hạn 10 ký tự cho số điện thoại
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && ((sender as TextBox).Text.Length >= 10))
            {
                e.Handled = true;
            }
        }

        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ✅ Giới hạn 12 ký tự cho CCCD
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && ((sender as TextBox).Text.Length >= 12))
            {
                e.Handled = true;
            }
        }

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