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
            InitializeDataGridViews();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing DataGridViews: " + ex.Message);
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
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
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
            txtTimKiem.Clear();
            LoadData();
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
    }
}
