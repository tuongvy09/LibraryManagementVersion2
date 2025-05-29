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
            SetupEventHandlers(); // Thêm dòng này
        }

        private void SetupEventHandlers()
        {
            // Wire up all event handlers
            this.Load += FormManageTheThuVien_Load;
            this.txtTimKiem.KeyPress += txtTimKiem_KeyPress;
            this.btnTimKiem.Click += btnTimKiem_Click;
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;
            this.btnReload.Click += btnReload_Click;
            this.btnTaoQR.Click += btnTaoQR_Click;
            this.dgvTheThuVien.CellClick += dgvTheThuVien_CellClick;
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
                    // Đặt width cho các cột
                    dgvTheThuVien.Columns[0].Width = 80;  // Mã thẻ
                    dgvTheThuVien.Columns[1].Width = 200; // Tên độc giả
                    dgvTheThuVien.Columns[2].Width = 120; // Số ĐT
                    dgvTheThuVien.Columns[3].Width = 120; // Ngày cấp
                    dgvTheThuVien.Columns[4].Width = 120; // Ngày hết hạn
                    dgvTheThuVien.Columns[5].Width = 120; // Trạng thái

                    // Center align cho một số cột
                    dgvTheThuVien.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvTheThuVien.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvTheThuVien.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvTheThuVien.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvTheThuVien.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Tô màu cho cột trạng thái
                    foreach (DataGridViewRow row in dgvTheThuVien.Rows)
                    {
                        if (row.Cells[5].Value != null)
                        {
                            string trangThai = row.Cells[5].Value.ToString();
                            if (trangThai == "Hết hạn")
                            {
                                row.Cells[5].Style.ForeColor = Color.Red;
                                row.Cells[5].Style.Font = new Font(dgvTheThuVien.Font, FontStyle.Bold);
                            }
                            else if (trangThai == "Còn hiệu lực")
                            {
                                row.Cells[5].Style.ForeColor = Color.Green;
                                row.Cells[5].Style.Font = new Font(dgvTheThuVien.Font, FontStyle.Bold);
                            }
                        }
                    }
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

            try
            {
                string tenDocGia = dgvTheThuVien.CurrentRow.Cells[1].Value?.ToString() ?? "N/A";
                string maThe = dgvTheThuVien.CurrentRow.Cells[0].Value?.ToString() ?? "N/A";

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa thẻ thư viện?\n\n" +
                    $"Mã thẻ: {maThe}\n" +
                    $"Độc giả: {tenDocGia}\n\n" +
                    $"⚠️ Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int maTheInt = Convert.ToInt32(dgvTheThuVien.CurrentRow.Cells[0].Value);
                    bool success = dbTheThuVien.XoaTheThuVien(maTheInt, ref err);

                    if (success)
                    {
                        MessageBox.Show("Xóa thẻ thư viện thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thẻ thư viện thất bại: " + err, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();

                // Kiểm tra nếu là placeholder text thì xem như tìm kiếm rỗng
                if (tuKhoa == "Nhập mã thẻ, tên độc giả hoặc số điện thoại..." || string.IsNullOrWhiteSpace(tuKhoa))
                {
                    LoadData();
                    return;
                }

                dgvTheThuVien.DataSource = dbTheThuVien.TimKiemTheThuVien(tuKhoa);
                dgvTheThuVien.AutoResizeColumns();
                CustomizeDataGridView();

                // Hiển thị số kết quả tìm được
                int soKetQua = dgvTheThuVien.Rows.Count;
                lblTitle.Text = $"QUẢN LÝ THẺ THƯ VIỆN - Tìm thấy {soKetQua} kết quả";
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

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter key
            {
                btnTimKiem_Click(sender, e);
            }
        }

        private void dgvTheThuVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể thêm logic xử lý khi click vào cell nếu cần
            if (e.RowIndex >= 0 && dgvTheThuVien.Rows[e.RowIndex].Cells[0].Value != null)
            {
                string trangThai = dgvTheThuVien.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? "";
                // Có thể thêm logic tùy chỉnh ở đây
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
