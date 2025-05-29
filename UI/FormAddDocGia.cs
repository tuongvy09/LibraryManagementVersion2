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
        private bool isLoading = false; // Flag để tránh recursive loading

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
                    // Thêm dòng "-- Chọn loại độc giả --"
                    DataRow newRow = dtLoaiDG.NewRow();
                    newRow["MaLoaiDG"] = DBNull.Value;
                    newRow["TenLoaiDG"] = "-- Chọn loại độc giả --";
                    dtLoaiDG.Rows.InsertAt(newRow, 0);

                    cboLoaiDocGia.DataSource = dtLoaiDG;
                    cboLoaiDocGia.DisplayMember = "TenLoaiDG";
                    cboLoaiDocGia.ValueMember = "MaLoaiDG";
                    cboLoaiDocGia.SelectedIndex = 0;
                }
                else
                {
                    // Nếu không có dữ liệu, thêm item mặc định
                    cboLoaiDocGia.Items.Add("-- Chọn loại độc giả --");
                    cboLoaiDocGia.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu ComboBox: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Fallback: thêm item mặc định
                cboLoaiDocGia.Items.Clear();
                cboLoaiDocGia.Items.Add("-- Chọn loại độc giả --");
                cboLoaiDocGia.SelectedIndex = 0;
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
                string gioiTinh = "";
                if (cboGioiTinh.SelectedIndex == 0)
                    gioiTinh = "M"; // Nam
                else if (cboGioiTinh.SelectedIndex == 1)
                    gioiTinh = "F"; // Nữ

                int? maLoaiDG = null;
                if (cboLoaiDocGia.SelectedValue != null &&
                    cboLoaiDocGia.SelectedValue != DBNull.Value &&
                    cboLoaiDocGia.SelectedIndex > 0)
                {
                    if (int.TryParse(cboLoaiDocGia.SelectedValue.ToString(), out int tempMaLoaiDG))
                    {
                        maLoaiDG = tempMaLoaiDG;
                    }
                }

                bool success = blDocGia.ThemDocGia(
                    txtHoTen.Text.Trim(),
                    Convert.ToInt32(txtTuoi.Text.Trim()),
                    txtSoDT.Text.Trim(),
                    txtCCCD.Text.Trim(),
                    gioiTinh,
                    txtEmail.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    maLoaiDG,
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

            if (!string.IsNullOrWhiteSpace(txtCCCD.Text) && txtCCCD.Text.Trim().Length != 12)
            {
                MessageBox.Show("CCCD phải có 12 chữ số!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Focus();
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

        private void txtTuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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