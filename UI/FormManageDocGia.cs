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
        private bool isPlaceholderActive = true;
        private string placeholderText = "Nhập tên, số điện thoại hoặc CCCD...";

        public FormDocGiaManagement()
        {
            InitializeComponent();
            InitializeCustomStyle();
        }

        private void InitializeCustomStyle()
        {
            // Setup placeholder text
            SetPlaceholder();

            // Đăng ký events
            txtTimKiem.Enter += TxtTimKiem_Enter;
            txtTimKiem.Leave += TxtTimKiem_Leave;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            txtTimKiem.KeyDown += TxtTimKiem_KeyDown;

            // Đăng ký event để highlight rows sau khi data binding complete
            dgvDocGia.DataBindingComplete += (s, args) => HighlightInactiveRows();
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text) || isPlaceholderActive)
            {
                txtTimKiem.Text = placeholderText;
                txtTimKiem.ForeColor = Color.Gray;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Italic);
                isPlaceholderActive = true;
            }
        }

        private void ClearPlaceholder()
        {
            if (isPlaceholderActive)
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Regular);
                isPlaceholderActive = false;
            }
        }

        private void TxtTimKiem_Enter(object sender, EventArgs e)
        {
            ClearPlaceholder();
        }

        private void TxtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                SetPlaceholder();
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (!isPlaceholderActive && txtTimKiem.Focused)
            {
                txtTimKiem.ForeColor = Color.Black;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Regular);
            }
        }

        private void TxtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnReload_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        void LoadData()
        {
            try
            {
                // ✅ Load data với sắp xếp: Hoạt động trước, Ngừng hoạt động sau
                dgvDocGia.DataSource = dbDocGia.LayTatCaDocGia();
                dgvDocGia.AutoResizeColumns();

                // ✅ Highlight các dòng ngừng hoạt động
                HighlightInactiveRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Method để highlight các dòng ngừng hoạt động
        private void HighlightInactiveRows()
        {
            try
            {
                foreach (DataGridViewRow row in dgvDocGia.Rows)
                {
                    var trangThaiCell = row.Cells["TrangThai"];

                    if (trangThaiCell?.Value?.ToString() == "Ngừng hoạt động")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250); // Màu xám nhạt
                        row.DefaultCellStyle.ForeColor = Color.Gray; // Chữ màu xám
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi highlight rows: {ex.Message}");
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

        // ✅ UPDATED: Chức năng vô hiệu hóa/kích hoạt độc giả
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần vô hiệu hóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Lấy thông tin độc giả hiện tại
            string hoTen = dgvDocGia.CurrentRow.Cells["HoTen"].Value?.ToString() ?? "độc giả này";
            string trangThaiHienTai = dgvDocGia.CurrentRow.Cells["TrangThai"].Value?.ToString();

            // ✅ Kiểm tra trạng thái hiện tại để hiển thị thông báo phù hợp
            DialogResult result;
            string actionText;

            if (trangThaiHienTai == "Ngừng hoạt động")
            {
                result = MessageBox.Show($"Độc giả '{hoTen}' đã được vô hiệu hóa trước đó.\n\n" +
                    "Bạn có muốn kích hoạt lại tài khoản này không?",
                    "Kích hoạt lại tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                actionText = "kích hoạt lại";
            }
            else
            {
                result = MessageBox.Show($"Bạn có chắc chắn muốn vô hiệu hóa tài khoản độc giả '{hoTen}'?\n\n" +
                    "Lưu ý: Độc giả sẽ không thể mượn sách mới, nhưng dữ liệu sẽ được lưu trữ.",
                    "Xác nhận vô hiệu hóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                actionText = "vô hiệu hóa";
            }

            if (result == DialogResult.Yes)
            {
                try
                {
                    int maDocGia = Convert.ToInt32(dgvDocGia.CurrentRow.Cells["MaDocGia"].Value);
                    bool success = dbDocGia.XoaDocGia(maDocGia, ref err);

                    if (success)
                    {
                        // ✅ Thông báo phù hợp với hành động thực tế
                        MessageBox.Show($"Đã {actionText} tài khoản độc giả '{hoTen}' thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show($"Thao tác thất bại: {err}", "Lỗi",
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

        // ✅ UPDATED: Chức năng tìm kiếm độc giả với sắp xếp
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // ✅ Kiểm tra placeholder
            if (isPlaceholderActive || string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                LoadData();
                return;
            }

            try
            {
                // ✅ Tìm kiếm với sắp xếp
                dgvDocGia.DataSource = dbDocGia.TimKiemDocGiaSorted(txtTimKiem.Text.Trim());
                dgvDocGia.AutoResizeColumns();

                // ✅ Highlight các dòng ngừng hoạt động
                HighlightInactiveRows();

                // ✅ Hiển thị số kết quả tìm kiếm
                int rowCount = dgvDocGia.Rows.Count;
                if (rowCount == 0)
                {
                    MessageBox.Show($"Không tìm thấy độc giả nào với từ khóa '{txtTimKiem.Text.Trim()}'!",
                        "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.Text = $"Quản lý Độc giả - Tìm thấy {rowCount} kết quả";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ UPDATED: Chức năng reload dữ liệu
        private void btnReload_Click(object sender, EventArgs e)
        {
            SetPlaceholder();
            LoadData();
            this.Text = "Quản lý Độc giả"; // ✅ Reset title
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // ✅ NEW: Double-click để sửa nhanh
        private void dgvDocGia_DoubleClick(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }
    }
}