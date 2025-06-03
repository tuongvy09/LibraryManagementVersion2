using LibraryManagementVersion2.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormManageThuThu : Form
    {
        BLThuThu dbThuThu = new BLThuThu();
        string err;
        private bool isPlaceholderActive = true;
        private string placeholderText = "Nhập tên thủ thư, email hoặc số điện thoại...";

        public FormManageThuThu()
        {
            InitializeComponent();
            InitializeCustomStyle();
        }

        private void InitializeCustomStyle()
        {
            // Setup placeholder text
            SetPlaceholder();

            txtTimKiem.Enter += TxtTimKiem_Enter;
            txtTimKiem.Leave += TxtTimKiem_Leave;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;

            // Thêm KeyPress event cho txtTimKiem
            txtTimKiem.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)13) // Enter key
                {
                    btnTimKiem_Click(s, e);
                }
                else if (e.KeyChar == (char)27) // Escape key
                {
                    btnReload_Click(s, e);
                }
            };
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

        void LoadData()
        {
            try
            {
                // ✅ Load data với sắp xếp: Hoạt động trước, Ngừng hoạt động sau
                dgvThuThu.DataSource = dbThuThu.LayThuThuSorted();
                dgvThuThu.AutoResizeColumns();

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
                foreach (DataGridViewRow row in dgvThuThu.Rows)
                {
                    // Giả sử cột trạng thái là cột cuối cùng hoặc có tên "Trạng thái"
                    var trangThaiCell = row.Cells["Trạng thái"] ?? row.Cells[row.Cells.Count - 1];

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

        private void FormManageThuThu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FormAddThuThu frmAdd = new FormAddThuThu();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvThuThu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thủ thư cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maThuThu = Convert.ToInt32(dgvThuThu.CurrentRow.Cells[0].Value);
            FormEditThuThu frmEdit = new FormEditThuThu(maThuThu);
            if (frmEdit.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvThuThu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thủ thư cần vô hiệu hóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Lấy thông tin thủ thư hiện tại
            string tenThuThu = dgvThuThu.CurrentRow.Cells["Tên thủ thư"]?.Value?.ToString() ??
                              dgvThuThu.CurrentRow.Cells[1]?.Value?.ToString() ?? "thủ thư này";

            string trangThaiHienTai = dgvThuThu.CurrentRow.Cells["Trạng thái"]?.Value?.ToString() ??
                                     dgvThuThu.CurrentRow.Cells[dgvThuThu.CurrentRow.Cells.Count - 1]?.Value?.ToString();

            // ✅ Kiểm tra trạng thái hiện tại để hiển thị thông báo phù hợp
            DialogResult result;
            string actionText;

            if (trangThaiHienTai == "Ngừng hoạt động")
            {
                result = MessageBox.Show($"Thủ thư '{tenThuThu}' đã được vô hiệu hóa trước đó.\n\n" +
                    "Bạn có muốn kích hoạt lại tài khoản này không?",
                    "Kích hoạt lại tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                actionText = "kích hoạt lại";
            }
            else
            {
                result = MessageBox.Show($"Bạn có chắc chắn muốn vô hiệu hóa tài khoản thủ thư '{tenThuThu}'?\n\n" +
                    "Lưu ý: Thủ thư sẽ không thể truy cập hệ thống, nhưng dữ liệu sẽ được lưu trữ.",
                    "Xác nhận vô hiệu hóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                actionText = "vô hiệu hóa";
            }

            if (result == DialogResult.Yes)
            {
                try
                {
                    int maThuThu = Convert.ToInt32(dgvThuThu.CurrentRow.Cells[0].Value);
                    bool success = dbThuThu.XoaThuThu(maThuThu, ref err);

                    if (success)
                    {
                        // ✅ Thông báo phù hợp với hành động thực tế
                        MessageBox.Show($"Đã {actionText} tài khoản thủ thư '{tenThuThu}' thành công!",
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
                dgvThuThu.DataSource = dbThuThu.TimKiemThuThuSorted(txtTimKiem.Text.Trim());
                dgvThuThu.AutoResizeColumns();

                // ✅ Highlight các dòng ngừng hoạt động
                HighlightInactiveRows();

                // ✅ Hiển thị số kết quả tìm kiếm
                int soKetQua = dgvThuThu.Rows.Count;
                if (soKetQua == 0)
                {
                    MessageBox.Show($"Không tìm thấy thủ thư nào với từ khóa '{txtTimKiem.Text.Trim()}'!",
                        "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.Text = $"Quản lý thủ thư - Tìm thấy {soKetQua} kết quả";
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
            SetPlaceholder();
            LoadData();
            this.Text = "Quản lý thủ thư"; // ✅ Reset title
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvThuThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell click if needed
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Đã xử lý trong TxtTimKiem_TextChanged method ở trên
        }

        // ✅ Override để xử lý DataBindingComplete event
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Đăng ký event để highlight rows sau khi data binding complete
            dgvThuThu.DataBindingComplete += (s, args) => HighlightInactiveRows();
        }
    }
}