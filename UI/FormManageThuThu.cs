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

        public FormManageThuThu()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                dgvThuThu.DataSource = dbThuThu.LayThuThu();
                dgvThuThu.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Vui lòng chọn thủ thư cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa thủ thư này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int maThuThu = Convert.ToInt32(dgvThuThu.CurrentRow.Cells[0].Value);
                    bool success = dbThuThu.XoaThuThu(maThuThu, ref err);

                    if (success)
                    {
                        MessageBox.Show("Xóa thủ thư thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thủ thư thất bại: " + err, "Lỗi",
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
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                LoadData();
                return;
            }

            try
            {
                dgvThuThu.DataSource = dbThuThu.TimKiemThuThu(txtTimKiem.Text.Trim());
                dgvThuThu.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
        }

        private void dgvThuThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell click if needed
        }
    }
}