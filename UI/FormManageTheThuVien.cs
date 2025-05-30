using LibraryManagementVersion2.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibraryManagementVersion2.Utilities;

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
            InitializeDataGridViews();
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

            // Setup style cho form
            this.BackColor = Color.White;
        }

        private void InitializeDataGridViews()
        {
            try
            {
                // Khởi tạo DataTable rỗng để tránh null reference
                DataTable emptyTable = new DataTable();
                emptyTable.Columns.Add("Mã thẻ");
                emptyTable.Columns.Add("Tên độc giả");
                emptyTable.Columns.Add("Số ĐT");
                emptyTable.Columns.Add("Ngày cấp");
                emptyTable.Columns.Add("Ngày hết hạn");

                dgvConHieuLuc.DataSource = emptyTable.Copy();

                // Cho tab hết hạn thêm cột "Hết hạn từ"
                DataTable emptyTableHetHan = emptyTable.Copy();
                emptyTableHetHan.Columns.Add("Hết hạn từ");
                dgvHetHan.DataSource = emptyTableHetHan;

                // Style cho DataGridViews
                SetupDataGridViewStyle(dgvConHieuLuc);
                SetupDataGridViewStyle(dgvHetHan);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing DataGridViews: " + ex.Message);
            }
        }

        private void SetupDataGridViewStyle(DataGridView dgv)
        {
            try
            {
                // Style cho DataGridView
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(115, 154, 79);
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.GridColor = Color.FromArgb(221, 221, 221);
                dgv.BorderStyle = BorderStyle.Fixed3D;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting up DataGridView style: " + ex.Message);
            }
        }

        void LoadData()
        {
            try
            {
                // Load data cho 2 tab
                dgvConHieuLuc.DataSource = dbTheThuVien.LayTheThuVienConHieuLuc();
                dgvHetHan.DataSource = dbTheThuVien.LayTheThuVienHetHan();

                dgvConHieuLuc.AutoResizeColumns();
                dgvHetHan.AutoResizeColumns();

                UpdateTabTitles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTabTitles()
        {
            try
            {
                // Cập nhật title với số lượng
                int conHieuLuc = dgvConHieuLuc?.Rows?.Count ?? 0;
                int hetHan = dgvHetHan?.Rows?.Count ?? 0;

                if (tabConHieuLuc != null)
                    tabConHieuLuc.Text = $"Còn hiệu lực ({conHieuLuc})";

                if (tabHetHan != null)
                    tabHetHan.Text = $"Hết hạn ({hetHan})";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating tab titles: " + ex.Message);
            }
        }

        private DataGridView GetActiveDataGridView()
        {
            return tabControl.SelectedTab == tabConHieuLuc ? dgvConHieuLuc : dgvHetHan;
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
            var activeGrid = GetActiveDataGridView();
            if (activeGrid.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thẻ thư viện cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maThe = Convert.ToInt32(activeGrid.CurrentRow.Cells[0].Value);
                FormEditTheThuVien frmEdit = new FormEditTheThuVien(maThe);
                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form sửa thẻ thư viện: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Tìm kiếm trên cả 2 tab
                var ketQuaTimKiem = dbTheThuVien.TimKiemTheThuVien(txtTimKiem.Text.Trim());

                dgvConHieuLuc.DataSource = LayKetQuaTheoTrangThai(ketQuaTimKiem, true);
                dgvHetHan.DataSource = LayKetQuaTheoTrangThai(ketQuaTimKiem, false);

                dgvConHieuLuc.AutoResizeColumns();
                dgvHetHan.AutoResizeColumns();

                UpdateTabTitles();

                // Hiển thị số kết quả tìm được
                int tongKetQua = ketQuaTimKiem?.Rows?.Count ?? 0;
                if (tongKetQua == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable LayKetQuaTheoTrangThai(DataTable ketQua, bool conHieuLuc)
        {
            if (ketQua == null) return new DataTable();

            DataTable result = ketQua.Clone();

            try
            {
                foreach (DataRow row in ketQua.Rows)
                {
                    if (row == null) continue;

                    string trangThai = row["Trạng thái"]?.ToString() ?? "";
                    bool isConHieuLuc = trangThai == "Còn hiệu lực";

                    if (isConHieuLuc == conHieuLuc)
                    {
                        result.ImportRow(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error filtering results: " + ex.Message);
            }

            return result;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset lại textbox tìm kiếm với placeholder
                txtTimKiem.Text = "Nhập mã thẻ, tên độc giả hoặc số điện thoại...";
                txtTimKiem.ForeColor = Color.Gray;

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
            var activeGrid = GetActiveDataGridView();
            if (activeGrid.CurrentRow == null)
            {
                MessageBox.Show("❗ Vui lòng chọn thẻ thư viện cần tạo QR Code!", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maThe = Convert.ToInt32(activeGrid.CurrentRow.Cells[0].Value);
                var theThuVien = dbTheThuVien.LayTheThuVienDayDuTheoMa(maThe);

                if (theThuVien != null)
                {
                    // Check if card is still valid
                    bool isValid = theThuVien.NgayHetHan > DateTime.Now;
                    if (!isValid)
                    {
                        var continueResult = MessageBox.Show(
                            "⚠️ Thẻ này đã hết hạn!\n\nBạn có muốn tiếp tục tạo QR Code không?",
                            "Thẻ hết hạn",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (continueResult == DialogResult.No)
                            return;
                    }

                    // Generate QR Code
                    string qrText = QRCodeManager.CreateLibraryCardQR(theThuVien);
                    var qrImage = QRCodeManager.GenerateQRCode(qrText);

                    // Show QR Code Dialog
                    using (var qrDialog = new FormQRDisplay(qrImage, theThuVien))
                    {
                        qrDialog.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("❌ Không tìm thấy thông tin thẻ thư viện!", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi tạo QR Code: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvConHieuLuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell click for valid cards if needed
        }

        private void dgvHetHan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell click for expired cards if needed
        }

        public void RefreshData()
        {
            LoadData();
        }

        // Override ProcessCmdKey để xử lý phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F3:
                    btnTimKiem_Click(null, null);
                    return true;
                case Keys.F5:
                    btnReload_Click(null, null);
                    return true;
                case Keys.F2:
                    btnThem_Click(null, null);
                    return true;
                case Keys.F4:
                    btnSua_Click(null, null);
                    return true;
                case Keys.Escape:
                    this.Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
