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
    public partial class FormManageDocGia : Form
    {
        BLDocGia dbDocGia = new BLDocGia();
        string err;

        public FormManageDocGia()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                dgvDocGia.DataSource = dbDocGia.LayDocGia();
                dgvDocGia.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormManageDocGia_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FormAddDocGia frmAdd = new FormAddDocGia();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maDocGia = Convert.ToInt32(dgvDocGia.CurrentRow.Cells[0].Value);
            FormEditDocGia frmEdit = new FormEditDocGia(maDocGia);
            if (frmEdit.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa độc giả này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int maDocGia = Convert.ToInt32(dgvDocGia.CurrentRow.Cells[0].Value);
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

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell click if needed
        }
    }
}