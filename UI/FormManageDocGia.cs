using LibraryManagementVersion2.Repositories;
using LibraryManagementVersion2.UI;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ThuVien_EF.BS_Layer;

namespace ThuVien_EF.Forms
{
    public partial class FormDocGiaManagement : Form
    {
        BLDocGia dbDocGia = new BLDocGia();
        string err;

        public FormDocGiaManagement()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                dgvDocGia.DataSource = dbDocGia.LayTatCaDocGia();
                dgvDocGia.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDocGiaManagement_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // ✅ RESTORED: Chức năng thêm độc giả
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Tạo FormAddDocGia cho Entity Framework version
                FormAddDocGia frmAdd = new FormAddDocGia();
                if (frmAdd.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Thêm độc giả thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Fallback nếu FormAddDocGia chưa tồn tại
                MessageBox.Show("Chức năng thêm độc giả đang được phát triển!\n\n" +
                    "Lỗi: " + ex.Message, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ✅ RESTORED: Chức năng sửa độc giả
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maDocGia = Convert.ToInt32(dgvDocGia.CurrentRow.Cells["MaDocGia"].Value);

                // TODO: Tạo FormEditDocGia cho Entity Framework version
                FormEditDocGia frmEdit = new FormEditDocGia(maDocGia);
                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Cập nhật thông tin độc giả thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Fallback nếu FormEditDocGia chưa tồn tại
                MessageBox.Show("Chức năng sửa độc giả đang được phát triển!\n\n" +
                    "Lỗi: " + ex.Message, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ✅ RESTORED: Chức năng xóa độc giả
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = dgvDocGia.CurrentRow.Cells["HoTen"].Value?.ToString() ?? "N/A";
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa độc giả '{hoTen}'?\n\nLưu ý: Thao tác này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int maDocGia = Convert.ToInt32(dgvDocGia.CurrentRow.Cells["MaDocGia"].Value);
                    bool success = dbDocGia.XoaDocGia(maDocGia, ref err);

                    if (success)
                    {
                        MessageBox.Show("Xóa độc giả thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa độc giả thất bại: " + err, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ✅ RESTORED: Chức năng tìm kiếm độc giả
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                LoadData();
                return;
            }

            try
            {
                dgvDocGia.DataSource = dbDocGia.TimKiemDocGia(txtTimKiem.Text.Trim());
                dgvDocGia.AutoResizeColumns();

                // Hiển thị số kết quả tìm được
                int rowCount = dgvDocGia.Rows.Count;
                this.Text = $"Quản lý Độc giả - Tìm thấy {rowCount} kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ RESTORED: Chức năng reload dữ liệu
        private void btnReload_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            this.Text = "Quản lý Độc giả - Entity Framework"; // Reset title
        }

        // ✅ NEW: Chức năng xem thống kê tiền mượn + phạt
        private void btnViewStats_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một độc giả để xem thống kê chi tiết!\n\nHoặc vào mục 'Thống kê báo cáo' để xem tổng quan!",
                    "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int maDocGia = Convert.ToInt32(dgvDocGia.CurrentRow.Cells["MaDocGia"].Value);
                string hoTen = dgvDocGia.CurrentRow.Cells["HoTen"].Value?.ToString() ?? "N/A";

                ShowDocGiaStats(maDocGia, hoTen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NEW: Hiển thị form thống kê chi tiết
        private void ShowDocGiaStats(int maDocGia, string hoTen)
        {
            try
            {
                DataTable statsData = dbDocGia.LayChiTietTienMuonDocGia(maDocGia);

                if (statsData.Rows.Count == 0)
                {
                    MessageBox.Show($"Không có dữ liệu thống kê cho độc giả {hoTen}!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Lấy dữ liệu từ DataTable
                DataRow row = statsData.Rows[0];

                // Tạo form hiển thị thống kê
                using (var formStats = new FormDocGiaStatsDetail(row))
                {
                    formStats.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Optional: Handle cell click
        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Có thể thêm logic xử lý click vào cell nếu cần
        }

        // ✅ NEW: Double-click để xem thống kê nhanh
        private void dgvDocGia_DoubleClick(object sender, EventArgs e)
        {
            btnViewStats_Click(sender, e);
        }

        // ✅ NEW: Enter key trên textbox tìm kiếm
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem_Click(sender, e);
            }
        }

        // ✅ NEW: Xử lý placeholder text cho textbox tìm kiếm
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.ForeColor == System.Drawing.Color.Gray)
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Nhập tên, số điện thoại hoặc CCCD...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Setup placeholder text
            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Nhập tên, số điện thoại hoặc CCCD...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }

            // Add event handlers cho placeholder
            txtTimKiem.Enter += txtTimKiem_Enter;
            txtTimKiem.Leave += txtTimKiem_Leave;
        }

    }
}
