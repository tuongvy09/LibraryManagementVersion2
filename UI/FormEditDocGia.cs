using LibraryManagementVersion2.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormEditDocGia : Form
    {
        private BLDocGia blDocGia = new BLDocGia();
        private string err;
        private int maDocGia;
        private bool isLoading = false;

        public FormEditDocGia(int maDocGia)
        {
            InitializeComponent();
            this.maDocGia = maDocGia;
            InitializeCustomStyle();
            LoadComboBoxData();
            LoadDocGiaData();
        }

        private void InitializeCustomStyle()
        {
            this.BackColor = Color.White;
            this.Text = "Sửa Độc giả"; // ✅ Set title

            // ✅ Setup ComboBox giới tính
            SetupGioiTinhComboBox();
        }

        // ✅ Method để setup ComboBox giới tính
        private void SetupGioiTinhComboBox()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = -1; // No selection by default
        }

        private void LoadComboBoxData()
        {
            if (isLoading) return;

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
                    // ✅ Không thêm dòng "-- Chọn loại độc giả --" cho form edit
                    // Vì phải có giá trị hợp lệ
                    cboLoaiDocGia.DataSource = dtLoaiDG;
                    cboLoaiDocGia.DisplayMember = "TenLoaiDG";
                    cboLoaiDocGia.ValueMember = "MaLoaiDG";
                }
                else
                {
                    MessageBox.Show("Không có loại độc giả nào trong hệ thống!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
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

        private void LoadDocGiaData()
        {
            try
            {
                var docGia = blDocGia.LayDocGiaTheoMa(maDocGia);
                if (docGia != null)
                {
                    txtHoTen.Text = docGia.HoTen ?? "";
                    txtTuoi.Text = docGia.Tuoi.ToString();
                    txtSoDT.Text = docGia.SoDT ?? "";
                    txtCCCD.Text = docGia.CCCD ?? "";
                    txtEmail.Text = docGia.Email ?? "";
                    txtDiaChi.Text = docGia.DiaChi ?? "";

                    // ✅ Set giới tính theo format mới
                    if (!string.IsNullOrEmpty(docGia.GioiTinh))
                    {
                        switch (docGia.GioiTinh.Trim())
                        {
                            case "Nam":
                            case "M":
                            case "Male":
                                cboGioiTinh.SelectedIndex = 0; // Nam
                                break;
                            case "Nữ":
                            case "F":
                            case "Female":
                                cboGioiTinh.SelectedIndex = 1; // Nữ
                                break;
                            default:
                                cboGioiTinh.SelectedIndex = -1;
                                break;
                        }
                    }
                    else
                    {
                        cboGioiTinh.SelectedIndex = -1;
                    }

                    // ✅ Set loại độc giả - đảm bảo có giá trị hợp lệ
                    if (docGia.MaLoaiDG.HasValue)
                    {
                        cboLoaiDocGia.SelectedValue = docGia.MaLoaiDG.Value;
                    }
                    else
                    {
                        // Nếu không có MaLoaiDG, chọn item đầu tiên
                        if (cboLoaiDocGia.Items.Count > 0)
                        {
                            cboLoaiDocGia.SelectedIndex = 0;
                        }
                    }

                    // Set trạng thái
                    chkTrangThai.Checked = docGia.TrangThai ?? true;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin độc giả!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu độc giả: " + ex.Message, "Lỗi",
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
                // ✅ Xử lý giới tính theo format mới
                string gioiTinh = null;
                if (cboGioiTinh.SelectedIndex >= 0)
                {
                    switch (cboGioiTinh.SelectedIndex)
                    {
                        case 0:
                            gioiTinh = "Nam";
                            break;
                        case 1:
                            gioiTinh = "Nữ";
                            break;
                        default:
                            gioiTinh = null;
                            break;
                    }
                }

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

                // ✅ Xử lý các trường có thể null
                string cccd = string.IsNullOrWhiteSpace(txtCCCD.Text) ? null : txtCCCD.Text.Trim();
                string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string diaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim();

                bool success = blDocGia.CapNhatDocGia(
                    maDocGia,
                    txtHoTen.Text.Trim(),
                    Convert.ToInt32(txtTuoi.Text.Trim()),
                    txtSoDT.Text.Trim(),
                    cccd,
                    gioiTinh, // ✅ Pass "Nam" or "Nữ" or null
                    email,
                    diaChi,
                    maLoaiDG, // ✅ Pass int instead of int?
                    chkTrangThai.Checked,
                    ref err
                );

                if (success)
                {
                    MessageBox.Show("Cập nhật độc giả thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật độc giả thất bại: " + err, "Lỗi",
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

        // ✅ Enhanced validation methods
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

        // ✅ Event handlers with enhanced validation
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

        private void txtHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ✅ Cho phép chữ cái, số, khoảng trắng và một số ký tự đặc biệt thường dùng trong tên
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != ' ' && e.KeyChar != '.' && e.KeyChar != '-')
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

        private void FormEditDocGia_Load(object sender, EventArgs e)
        {
            txtHoTen.Focus();
        }
    }
}