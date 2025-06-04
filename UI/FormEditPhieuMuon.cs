﻿using LibraryManagementVersion2.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryManagementVersion2.BL.BLPhieuMuon;

namespace LibraryManagementVersion2.UI
{
    public partial class FormEditPhieuMuon : Form
    {
        private LibraryEntities dbContext = new LibraryEntities();
        private PhieuMuon phieuMuonToEdit;

        private Label lblTitle, lblMaPhieuMuon, lblDocGia, lblSach, lblNgayMuon, lblNgayTra, lblTrangThai;
        private TextBox txtMaPhieuMuon, txtTenDocGia, txtTenCuonSach, txtGiaMuon, txtTienCoc;
        private DateTimePicker dtpNgayMuon, dtpNgayTra;
        private Label lblTrangThaiValue;
        private ComboBox cbTrangThai;
        private Button btnSave, btnCancel;


        public FormEditPhieuMuon(int maPhieuMuon)
        {
            InitializeComponentForm();
            InitializeCustomStyle();
            LoadPhieuMuonData(maPhieuMuon);
        }

        private void InitializeComponentForm()
        {
            this.Text = "Sửa Phiếu Mượn";
            this.Size = new Size(550, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeCustomStyle()
        {
            Color mainColor = ColorTranslator.FromHtml("#739a4f");
            this.BackColor = Color.White;
            lblTitle = new Label()
            {
                Text = "Sửa Phiếu Mượn",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = mainColor,
                Location = new Point(200, 15),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            lblMaPhieuMuon = new Label()
            {
                Text = "Mã phiếu mượn:",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblMaPhieuMuon);

            txtMaPhieuMuon = new TextBox()
            {
                Location = new Point(160, 57),
                Width = 100,
                ReadOnly = false,
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(txtMaPhieuMuon);

            lblDocGia = new Label()
            {
                Text = "Tên độc giả:",
                Location = new Point(20, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblDocGia);

            txtTenDocGia = new TextBox()
            {
                Location = new Point(160, 97),
                Width = 300,
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.LightGray
            };
            this.Controls.Add(txtTenDocGia);

            lblSach = new Label()
            {
                Text = "Tên sách:",
                Location = new Point(20, 140),
                AutoSize = false,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblSach);

            txtTenCuonSach = new TextBox()
            {
                Location = new Point(160, 137),
                Width = 300,
                Font = new Font("Segoe UI", 10),
                ReadOnly = false,
                BackColor = Color.LightGray
            };
            this.Controls.Add(txtTenCuonSach);

            lblNgayMuon = new Label()
            {
                Text = "Ngày mượn: *",
                Location = new Point(20, 180),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblNgayMuon);

            dtpNgayMuon = new DateTimePicker()
            {
                Location = new Point(160, 177),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(dtpNgayMuon);

            lblNgayTra = new Label()
            {
                Text = "Ngày trả: *",
                Location = new Point(20, 220),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblNgayTra);

            dtpNgayTra = new DateTimePicker()
            {
                Location = new Point(160, 217),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10)
            };
            dtpNgayTra.ValueChanged += DtpNgayTra_ValueChanged;
            this.Controls.Add(dtpNgayTra);

            lblTrangThai = new Label()
            {
                Text = "Trạng thái:",
                Location = new Point(20, 260),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = mainColor
            };
            this.Controls.Add(lblTrangThai);

            cbTrangThai = new ComboBox()
            {
                Location = new Point(160, 257),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
            };
            cbTrangThai.Items.AddRange(new string[] { "Chua tra", "Da tra" });
            this.Controls.Add(cbTrangThai);

            lblTrangThaiValue = new Label()
            {
                Location = new Point(370, 257),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Red,
                Text = "Chưa trả"
            };
            this.Controls.Add(lblTrangThaiValue);

            Label lblGiaMuon = CreateLabel("Giá mượn:", 20, 305, mainColor);
            this.Controls.Add(lblGiaMuon);

            txtGiaMuon = new TextBox()
            {
                Location = new Point(160, 300),
                Width = 200,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(txtGiaMuon);
            txtGiaMuon.Leave += TxtGiaMuon_Leave;

            Label lblTienCoc = CreateLabel("Tiền cọc:", 20, 350, mainColor);
            this.Controls.Add(lblTienCoc);

            txtTienCoc = new TextBox()
            {
                Location = new Point(160, 345),
                Width = 200,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(txtTienCoc);
            txtTienCoc.Leave += TxtTienCoc_Leave;

            btnSave = new Button()
            {
                Text = "Lưu",
                BackColor = mainColor,
                ForeColor = Color.White,
                Location = new Point(160, 400),
                Size = new Size(80, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button()
            {
                Text = "Hủy",
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Location = new Point(270, 400),
                Size = new Size(80, 35),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);

            Label lblNote = new Label()
            {
                Text = "* Trường bắt buộc",
                Location = new Point(20, 405),
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.Red
            };
            this.Controls.Add(lblNote);
        }
        private Label CreateLabel(string text, int x, int y, Color foreColor)
        {
            return new Label()
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = foreColor
            };
        }

        private void LoadPhieuMuonData(int maPhieuMuon)
        {
            try
            {
                var blPhieuMuon = new BLPhieuMuon();
                phieuMuonToEdit = blPhieuMuon.GetPhieuMuonById(maPhieuMuon);

                if (phieuMuonToEdit != null)
                {
                    txtMaPhieuMuon.Text = phieuMuonToEdit.MaMuonSach.ToString();
                    txtTenDocGia.Text = phieuMuonToEdit.TenDocGia;
                    txtTenCuonSach.Text = phieuMuonToEdit.TenCuonSach;
                    dtpNgayMuon.Value = phieuMuonToEdit.NgayMuon;
                    dtpNgayTra.Value = phieuMuonToEdit.NgayTra;
                    cbTrangThai.SelectedItem = phieuMuonToEdit.TrangThaiM;
                    txtGiaMuon.Text = phieuMuonToEdit.GiaMuon.ToString("N0");
                    txtTienCoc.Text = phieuMuonToEdit.TienCoc.ToString("N0");
                    UpdateTrangThaiDisplay();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phiếu mượn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải phiếu mượn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTrangThaiDisplay()
        {
            bool daTra = dtpNgayTra.Value.Date <= DateTime.Now.Date &&
                         dtpNgayTra.Value.Date >= dtpNgayMuon.Value.Date;

            lblTrangThaiValue.Text = daTra ? "Đã trả" : "Chưa trả";
            lblTrangThaiValue.ForeColor = daTra ? Color.Green : Color.Red;
        }

        private void DtpNgayTra_ValueChanged(object sender, EventArgs e)
        {
            UpdateTrangThaiDisplay();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                phieuMuonToEdit.NgayMuon = dtpNgayMuon.Value.Date;
                phieuMuonToEdit.NgayTra = dtpNgayTra.Value.Date;
                phieuMuonToEdit.TrangThaiM = cbTrangThai.SelectedItem?.ToString() ?? "Chua tra";
                phieuMuonToEdit.SoNgayMuon = (dtpNgayTra.Value - dtpNgayMuon.Value).Days;

                if (!decimal.TryParse(txtGiaMuon.Text, out decimal giaMuon) ||
                    !decimal.TryParse(txtTienCoc.Text, out decimal tienCoc))
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng số cho giá mượn và tiền cọc.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                phieuMuonToEdit.GiaMuon = giaMuon;
                phieuMuonToEdit.TienCoc = tienCoc;

                dbContext.SaveChanges(); // Lưu vào EF

                MessageBox.Show("Cập nhật phiếu mượn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (dtpNgayTra.Value.Date < dtpNgayMuon.Value.Date)
            {
                MessageBox.Show("Ngày trả không được nhỏ hơn ngày mượn!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayTra.Focus();
                return false;
            }
            return true;
        }

        private void TxtGiaMuon_Leave(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtGiaMuon.Text, out decimal giaMuon) || giaMuon < 0)
            {
                MessageBox.Show("Giá mượn phải là số hợp lệ không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaMuon.Focus();
            }
        }

        private void TxtTienCoc_Leave(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtTienCoc.Text, out decimal tienCoc) || tienCoc < 0)
            {
                MessageBox.Show("Tiền cọc phải là số hợp lệ không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTienCoc.Focus();
            }
        }
    }
} 