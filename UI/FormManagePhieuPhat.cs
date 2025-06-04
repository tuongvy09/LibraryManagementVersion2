using LibraryManagementVersion2.BL;
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
    public partial class FormManagePhieuPhat : Form
    {
        private readonly BLPhieuPhat _phieuPhatService = new BLPhieuPhat();
        private List<BLPhieuPhat.PhieuPhatDTO> currentData;

        public FormManagePhieuPhat()
        {
            InitializeComponent();
            InitializeCustomEvents();
            LoadPhieuPhatData();

            txtSearch.Text = "Nhập mã phiếu hoặc tên độc giả...";
            txtSearch.ForeColor = Color.Gray;
        }

        private void InitializeCustomEvents()
        {
            dgvPhieuPhat.DoubleClick += dgvPhieuPhat_DoubleClick;
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.KeyDown += TxtSearch_KeyDown;

            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void LoadPhieuPhatData()
        {
            try
            {
                currentData = _phieuPhatService.GetAllPhieuPhat();

                dgvPhieuPhat.DataSource = currentData.Select(p => new
                {
                    p.MaPhieuPhat,
                    p.TenDocGia,
                    TongTien = p.SoTienPhat.ToString("N0") + " đ",
                    LoiViPham = string.Join(", ", p.DanhSachLyDo ?? new List<string>())
                }).ToList();

                SetupColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupColumnHeaders()
        {
            dgvPhieuPhat.Columns["MaPhieuPhat"].HeaderText = "Mã phiếu phạt";
            dgvPhieuPhat.Columns["TenDocGia"].HeaderText = "Tên độc giả";
            dgvPhieuPhat.Columns["TongTien"].HeaderText = "Tổng tiền phạt";
            dgvPhieuPhat.Columns["LoiViPham"].HeaderText = "Lỗi vi phạm";
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập mã phiếu hoặc tên độc giả...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Nhập mã phiếu hoặc tên độc giả...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword) || keyword == "Nhập mã phiếu hoặc tên độc giả...")
            {
                LoadPhieuPhatData();
                return;
            }

            var filtered = _phieuPhatService.SearchPhieuPhat(keyword);

            dgvPhieuPhat.DataSource = filtered.Select(p => new
            {
                p.MaPhieuPhat,
                p.TenDocGia,
                TongTien = p.SoTienPhat.ToString("N0") + " đ",
                LoiViPham = string.Join(", ", p.DanhSachLyDo ?? new List<string>())
            }).ToList();

            SetupColumnHeaders();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Nhập mã phiếu hoặc tên độc giả...";
            txtSearch.ForeColor = Color.Gray;
            LoadPhieuPhatData();
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPhieuPhat.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu phạt cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu phạt này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int maPhieuPhat = Convert.ToInt32(dgvPhieuPhat.CurrentRow.Cells["MaPhieuPhat"].Value);
                if (_phieuPhatService.DeletePhieuPhat(maPhieuPhat))
                {
                    MessageBox.Show("Xóa phiếu phạt thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPhieuPhatData();
                }
                else
                {
                    MessageBox.Show("Xóa phiếu phạt thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPhieuPhat_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPhieuPhat.CurrentRow != null)
            {
                int maPhieuPhat = Convert.ToInt32(dgvPhieuPhat.CurrentRow.Cells["MaPhieuPhat"].Value);
                var chiTiet = currentData.FirstOrDefault(p => p.MaPhieuPhat == maPhieuPhat);

                /*if (chiTiet != null)
                {
                    var form = new FormChiTietPhieuPhat(chiTiet);
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết phiếu phạt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            using (var form = new FormAddPhieuPhat())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadPhieuPhatData();
                }
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (dgvPhieuPhat.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu phạt cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maPhieuPhat = Convert.ToInt32(dgvPhieuPhat.CurrentRow.Cells["MaPhieuPhat"].Value);
            using (var form = new FormEditPhieuPhat(maPhieuPhat))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadPhieuPhatData();
                }
            }
        }
    }
}
