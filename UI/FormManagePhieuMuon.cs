<<<<<<< HEAD
﻿using System;
=======
﻿using LibraryManagementVersion2.BL;
using System;
>>>>>>> eb23ac5d69ab744d67b2ca0dbae35ff27525f6ce
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
<<<<<<< HEAD
=======
using static LibraryManagementVersion2.BL.BLPhieuMuon;
>>>>>>> eb23ac5d69ab744d67b2ca0dbae35ff27525f6ce

namespace LibraryManagementVersion2.UI
{
    public partial class FormManagePhieuMuon : Form
    {
<<<<<<< HEAD
        public FormManagePhieuMuon()
        {
            InitializeComponent();
=======
        BLPhieuMuon _phieuMuonDAO = new BLPhieuMuon();
        private List<PhieuMuon> currentData;

        public FormManagePhieuMuon()
        {
            InitializeComponent();
            InitializeCustomStyle();

            this.Load += FormManagePhieuMuon_Load;
        }

        private void FormManagePhieuMuon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void InitializeCustomStyle()
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Nhập mã phiếu, tên độc giả hoặc tên cuốn sách...";
                txtSearch.ForeColor = Color.Gray;
            }

            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == "Nhập mã phiếu, tên độc giả hoặc tên cuốn sách...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Nhập mã phiếu, tên độc giả hoặc tên cuốn sách...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    btnSearch.PerformClick();
            };
        }

        private void LoadData()
        {
            try
            {
                currentData = _phieuMuonDAO.GetAllPhieuMuon();

                dgvPhieuMuon.DataSource = currentData.Select(x => new
                {
                    x.MaMuonSach,
                    x.TenDocGia,
                    x.TenCuonSach,
                    NgayMuon = x.NgayMuon.ToString("dd/MM/yyyy"),
                    NgayTra = x.NgayTra.ToString("dd/MM/yyyy"),
                    x.TrangThaiM,
                    x.GiaMuon,
                    x.SoNgayMuon,
                    x.TienCoc
                }).ToList();

                SetupColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupColumnHeaders()
        {
            dgvPhieuMuon.Columns["MaMuonSach"].HeaderText = "Mã phiếu mượn";
            dgvPhieuMuon.Columns["TenDocGia"].HeaderText = "Tên độc giả";
            dgvPhieuMuon.Columns["TenCuonSach"].HeaderText = "Tên cuốn sách";
            dgvPhieuMuon.Columns["NgayMuon"].HeaderText = "Ngày mượn";
            dgvPhieuMuon.Columns["NgayTra"].HeaderText = "Ngày trả";
            dgvPhieuMuon.Columns["TrangThaiM"].HeaderText = "Trạng thái";
            dgvPhieuMuon.Columns["GiaMuon"].HeaderText = "Giá mượn";
            dgvPhieuMuon.Columns["SoNgayMuon"].HeaderText = "Số ngày mượn";
            dgvPhieuMuon.Columns["TienCoc"].HeaderText = "Tiền cọc";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText) || searchText == "nhập mã phiếu, tên độc giả hoặc tên cuốn sách...")
            {
                LoadData();
                return;
            }

            var filtered = currentData.Where(p =>
                p.MaMuonSach.ToString().Contains(searchText) ||
                (p.TenDocGia != null && p.TenDocGia.ToLower().Contains(searchText)) ||
                (p.TenCuonSach != null && p.TenCuonSach.ToLower().Contains(searchText))
            ).ToList();

            dgvPhieuMuon.DataSource = filtered.Select(x => new
            {
                x.MaMuonSach,
                x.TenDocGia,
                x.TenCuonSach,
                NgayMuon = x.NgayMuon.ToString("dd/MM/yyyy"),
                NgayTra = x.NgayTra.ToString("dd/MM/yyyy"),
                x.TrangThaiM,
                x.GiaMuon,
                x.SoNgayMuon,
                x.TienCoc
            }).ToList();

            SetupColumnHeaders();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var formAdd = new FormAddPhieuMuon())
            {
                if (formAdd.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPhieuMuon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa phiếu mượn này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int maMuonSach = Convert.ToInt32(dgvPhieuMuon.CurrentRow.Cells["MaMuonSach"].Value);
                    if (_phieuMuonDAO.DeletePhieuMuon(maMuonSach))
                    {
                        MessageBox.Show("Xóa phiếu mượn thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPhieuMuon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPhieuMuon.CurrentRow != null)
            {
                var row = dgvPhieuMuon.CurrentRow;

                var chiTiet = new PhieuMuon
                {
                    MaMuonSach = Convert.ToInt32(row.Cells["MaMuonSach"].Value),
                    TenDocGia = row.Cells["TenDocGia"].Value?.ToString(),
                    TenCuonSach = row.Cells["TenCuonSach"].Value?.ToString(),
                    NgayMuon = DateTime.ParseExact(row.Cells["NgayMuon"].Value.ToString(), "dd/MM/yyyy", null),
                    NgayTra = DateTime.ParseExact(row.Cells["NgayTra"].Value.ToString(), "dd/MM/yyyy", null),
                    TrangThaiM = row.Cells["TrangThaiM"].Value?.ToString(),
                    GiaMuon = Convert.ToDecimal(row.Cells["GiaMuon"].Value),
                    SoNgayMuon = Convert.ToInt32(row.Cells["SoNgayMuon"].Value),
                    TienCoc = Convert.ToDecimal(row.Cells["TienCoc"].Value)
                };

                using (var form = new FormChiTietPhieuMuon(chiTiet))
                {
                    form.ShowDialog();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Nhập mã phiếu, tên độc giả hoặc tên cuốn sách...";
            txtSearch.ForeColor = Color.Gray;
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPhieuMuon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMuonSach = Convert.ToInt32(dgvPhieuMuon.CurrentRow.Cells["MaMuonSach"].Value);
            using (var formEdit = new FormEditPhieuMuon(maMuonSach))
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
>>>>>>> eb23ac5d69ab744d67b2ca0dbae35ff27525f6ce
        }
    }
}
