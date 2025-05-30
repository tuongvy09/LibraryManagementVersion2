using LibraryManagementVersion2.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormManageBienLai : Form
    {
        BLBienLai dbBienLai = new BLBienLai();
        string err;

        public FormManageBienLai()
        {
            InitializeComponent();
            InitializeCustomStyle();
        }

        private void InitializeCustomStyle()
        {
            // Thêm placeholder text cho textbox tìm kiếm
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập mã biên lai, tên độc giả hoặc hình thức thanh toán...";
                txtTimKiem.ForeColor = Color.Gray;
            }

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Nhập mã biên lai, tên độc giả hoặc hình thức thanh toán...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Nhập mã biên lai, tên độc giả hoặc hình thức thanh toán...";
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
            // Style cho DataGridView
            dgvBienLai.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvBienLai.DefaultCellStyle.SelectionBackColor = Color.FromArgb(115, 154, 79);
            dgvBienLai.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBienLai.GridColor = Color.FromArgb(221, 221, 221);
            dgvBienLai.BorderStyle = BorderStyle.Fixed3D;
            dgvBienLai.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        void LoadData()
        {
            try
            {
                dgvBienLai.DataSource = dbBienLai.LayBienLai();
                dgvBienLai.AutoResizeColumns();

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
                if (dgvBienLai.Columns.Count > 0)
                {
                    // Căn trái tất cả các cột
                    foreach (DataGridViewColumn column in dgvBienLai.Columns)
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error but don't show to user
                Console.WriteLine("Error customizing DataGridView: " + ex.Message);
            }
        }

        private void FormManageBienLai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text) ||
                txtTimKiem.Text == "Nhập mã biên lai, tên độc giả hoặc hình thức thanh toán...")
            {
                LoadData();
                return;
            }

            try
            {
                dgvBienLai.DataSource = dbBienLai.TimKiemBienLai(txtTimKiem.Text.Trim());
                dgvBienLai.AutoResizeColumns();
                CustomizeDataGridView();

                // Hiển thị số kết quả tìm được
                int soKetQua = dgvBienLai.Rows.Count;
                if (soKetQua == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblTitle.Text = $"QUẢN LÝ BIÊN LAI - Tìm thấy {soKetQua} kết quả";
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
                txtTimKiem.Text = "Nhập mã biên lai, tên độc giả hoặc hình thức thanh toán...";
                txtTimKiem.ForeColor = Color.Gray;

                // Reset lại title
                lblTitle.Text = "QUẢN LÝ BIÊN LAI";

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

        private void dgvBienLai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell click if needed
        }

        // Method để refresh form từ bên ngoài
        public void RefreshData()
        {
            LoadData();
        }

        // Override ProcessCmdKey để xử lý phím tắt (chỉ giữ lại search và reload)
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
