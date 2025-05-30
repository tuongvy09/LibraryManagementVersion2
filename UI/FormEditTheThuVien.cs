using LibraryManagementVersion2.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormEditTheThuVien : Form
    {
        private BLTheThuVien blTheThuVien = new BLTheThuVien();
        private string err;
        private int maThe;
        private int? maDocGiaHienTai; // Lưu mã độc giả hiện tại

        public FormEditTheThuVien(int maThe)
        {
            InitializeComponent();
            this.maThe = maThe;
            InitializeCustomStyle();
            LoadTheThuVienData();
        }

        private void InitializeCustomStyle()
        {
            this.BackColor = Color.White;
        }

        private void LoadTheThuVienData()
        {
            try
            {
                var theThuVien = blTheThuVien.LayTheThuVienTheoMa(maThe);
                if (theThuVien != null)
                {
                    txtMaThe.Text = theThuVien.MaThe.ToString();
                    maDocGiaHienTai = theThuVien.MaDG;

                    // Hiển thị tên độc giả trong TextBox
                    if (theThuVien.MaDG.HasValue)
                    {
                        var docGia = blTheThuVien.LayDocGiaTheoMa(theThuVien.MaDG.Value);
                        if (docGia != null)
                        {
                            txtDocGia.Text = docGia.HoTen; // Hiển thị tên độc giả
                        }
                        else
                        {
                            txtDocGia.Text = "Độc giả không tồn tại";
                        }
                    }
                    else
                    {
                        txtDocGia.Text = "Chưa gán độc giả";
                    }

                    dtpNgayCap.Value = theThuVien.NgayCap;
                    dtpNgayHetHan.Value = theThuVien.NgayHetHan;

                    UpdateTrangThaiDisplay();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin thẻ thư viện!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thẻ thư viện: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void UpdateTrangThaiDisplay()
        {
            bool conHieuLuc = dtpNgayHetHan.Value > DateTime.Now; // Sửa logic: > thay vì <=
            lblTrangThaiValue.Text = conHieuLuc ? "Còn hiệu lực" : "Hết hạn";
            lblTrangThaiValue.ForeColor = conHieuLuc ? Color.Green : Color.Red;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                bool success = blTheThuVien.CapNhatTheThuVien(
                    maThe,
                    maDocGiaHienTai, 
                    dtpNgayCap.Value.Date,
                    dtpNgayHetHan.Value.Date,
                    ref err
                );

                if (success)
                {
                    MessageBox.Show("Cập nhật thẻ thư viện thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thẻ thư viện thất bại: " + err, "Lỗi",
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
            // Bỏ validation về độc giả vì không cho thay đổi trong form edit

            if (dtpNgayCap.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày cấp không được lớn hơn ngày hiện tại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayCap.Focus();
                return false;
            }

            if (dtpNgayHetHan.Value <= dtpNgayCap.Value)
            {
                MessageBox.Show("Ngày hết hạn phải lớn hơn ngày cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHetHan.Focus();
                return false;
            }

            return true;
        }

        private void dtpNgayHetHan_ValueChanged(object sender, EventArgs e)
        {
            UpdateTrangThaiDisplay();
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