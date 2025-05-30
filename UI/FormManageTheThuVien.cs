using LibraryManagementVersion2.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormManageTheThuVien : Form
    {
        BLTheThuVien dbTheThuVien = new BLTheThuVien();
        string err;

        public FormManageTheThuVien()
        {
            InitializeComponent();
            InitializeCustomStyle();
        }

        private void InitializeCustomStyle()
        {
            // Thêm placeholder text cho textbox tìm kiếm
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập mã thẻ, tên độc giả hoặc số điện thoại...";
                txtTimKiem.ForeColor = Color.Gray;
            }

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Nhập mã thẻ, tên độc giả hoặc số điện thoại...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Nhập mã thẻ, tên độc giả hoặc số điện thoại...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };

            // Thêm KeyPress event cho txtTimKiem
            txtTimKiem.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)13) // Enter key
                {
                    btnTimKiem_Click(s, e);
                }
            };

            // Setup DataGridView style
            SetupDataGridViewStyle();
        }

        private void SetupDataGridViewStyle()
        {
            // Style cho DataGridView giống FormManageThuThu
            dgvTheThuVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvTheThuVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(115, 154, 79);
            dgvTheThuVien.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTheThuVien.GridColor = Color.FromArgb(221, 221, 221);
            dgvTheThuVien.BorderStyle = BorderStyle.Fixed3D;
            dgvTheThuVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Event để format cell colors cho trạng thái
            dgvTheThuVien.CellFormatting += DgvTheThuVien_CellFormatting;
        }

        private void DgvTheThuVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTheThuVien.Columns.Count > 5 && e.ColumnIndex == 5) // Cột trạng thái
            {
                if (e.Value?.ToString() == "Còn hiệu lực")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (e.Value?.ToString() == "Hết hạn")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        void LoadData()
        {
            try
            {
                dgvTheThuVien.DataSource = dbTheThuVien.LayTheThuVien();
                dgvTheThuVien.AutoResizeColumns();

                // Tùy chỉnh header và style cho DataGridView
                CustomizeDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            try
            {
                if (dgvTheThuVien.Columns.Count > 0)
                {
                    // Center align cho một số cột
                    dgvTheThuVien.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Mã thẻ
                    if (dgvTheThuVien.Columns.Count > 2)
                        dgvTheThuVien.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Số ĐT
                    if (dgvTheThuVien.Columns.Count > 3)
                        dgvTheThuVien.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Ngày cấp
                    if (dgvTheThuVien.Columns.Count > 4)
                        dgvTheThuVien.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Ngày hết hạn
                    if (dgvTheThuVien.Columns.Count > 5)
                        dgvTheThuVien.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Trạng thái
                }
            }
            catch (Exception ex)
            {
                // Log error but don't show to user
                Console.WriteLine("Error customizing DataGridView: " + ex.Message);
            }
        }

        private void FormManageTheThuVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                FormAddTheThuVien frmAdd = new FormAddTheThuVien();
                if (frmAdd.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Đã thêm thẻ thư viện mới thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form thêm thẻ thư viện: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTheThuVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thẻ thư viện cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maThe = Convert.ToInt32(dgvTheThuVien.CurrentRow.Cells[0].Value);
                FormEditTheThuVien frmEdit = new FormEditTheThuVien(maThe);
                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Đã cập nhật thẻ thư viện thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form sửa thẻ thư viện: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTheThuVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thẻ thư viện cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa thẻ thư viện này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int maThe = Convert.ToInt32(dgvTheThuVien.CurrentRow.Cells[0].Value);
                    bool success = dbTheThuVien.XoaTheThuVien(maThe, ref err);

                    if (success)
                    {
                        MessageBox.Show("Xóa thẻ thư viện thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thẻ thư viện thất bại: " + err, "Lỗi",
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text) ||
                txtTimKiem.Text == "Nhập mã thẻ, tên độc giả hoặc số điện thoại...")
            {
                LoadData();
                return;
            }

            try
            {
                dgvTheThuVien.DataSource = dbTheThuVien.TimKiemTheThuVien(txtTimKiem.Text.Trim());
                dgvTheThuVien.AutoResizeColumns();
                CustomizeDataGridView();

                // Hiển thị số kết quả tìm được
                int soKetQua = dgvTheThuVien.Rows.Count;
                if (soKetQua == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblTitle.Text = $"QUẢN LÝ THẺ THƯ VIỆN - Tìm thấy {soKetQua} kết quả";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset lại textbox tìm kiếm
                txtTimKiem.Text = "Nhập mã thẻ, tên độc giả hoặc số điện thoại...";
                txtTimKiem.ForeColor = Color.Gray;

                // Reset lại title
                lblTitle.Text = "QUẢN LÝ THẺ THƯ VIỆN";

                // Load lại dữ liệu
                LoadData();

                MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm mới dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaoQR_Click(object sender, EventArgs e)
        {
            if (dgvTheThuVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thẻ thư viện để tạo QR Code!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maThe = dgvTheThuVien.CurrentRow.Cells[0].Value?.ToString() ?? "";
                string tenDocGia = dgvTheThuVien.CurrentRow.Cells[1].Value?.ToString() ?? "";

                // TODO: Implement QR Code generation
                MessageBox.Show($"Tính năng tạo QR Code sẽ được triển khai sau!\n\n" +
                               $"Thông tin thẻ:\n" +
                               $"Mã thẻ: {maThe}\n" +
                               $"Độc giả: {tenDocGia}",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTheThuVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell click if needed
        }

        // Method để refresh form từ bên ngoài
        public void RefreshData()
        {
            LoadData();
        }

        // Override ProcessCmdKey để xử lý phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    btnThem_Click(null, null);
                    return true;
                case Keys.F2:
                    btnSua_Click(null, null);
                    return true;
                case Keys.F3:
                    btnTimKiem_Click(null, null);
                    return true;
                case Keys.F5:
                    btnReload_Click(null, null);
                    return true;
                case Keys.Delete:
                    btnXoa_Click(null, null);
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Method để thiết lập focus khi form được mở
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtTimKiem.Focus();
        }
    }
}
