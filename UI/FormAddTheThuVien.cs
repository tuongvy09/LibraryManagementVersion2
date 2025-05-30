using LibraryManagementVersion2.Repositories;
using LibraryManagementVersion2.Utilities;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    public partial class FormAddTheThuVien : Form
    {
        private BLTheThuVien blTheThuVien = new BLTheThuVien();
        private string err;
        private bool isLoading = false;

        public FormAddTheThuVien()
        {
            InitializeComponent();
            InitializeCustomStyle();
            LoadComboBoxData();
            SetDefaultValues();
        }

        private void InitializeCustomStyle()
        {
            this.BackColor = Color.White;
        }

        private void LoadComboBoxData()
        {
            if (isLoading) return;

            try
            {
                isLoading = true;

                cboDocGia.DataSource = null;
                cboDocGia.Items.Clear();

                DataTable dtDocGia = blTheThuVien.LayDocGiaChuaCoThe();

                if (dtDocGia != null && dtDocGia.Rows.Count > 0)
                {
                    DataRow newRow = dtDocGia.NewRow();
                    newRow["MaDG"] = DBNull.Value;
                    newRow["TenDocGia"] = "-- Chọn độc giả --";
                    dtDocGia.Rows.InsertAt(newRow, 0);

                    cboDocGia.DataSource = dtDocGia;
                    cboDocGia.DisplayMember = "TenDocGia";
                    cboDocGia.ValueMember = "MaDG";
                    cboDocGia.SelectedIndex = 0;
                }
                else
                {
                    cboDocGia.Items.Add("-- Không có độc giả nào chưa có thẻ --");
                    cboDocGia.SelectedIndex = 0;

                    // Disable các control nếu không có độc giả nào
                    btnLuu.Enabled = false;
                    MessageBox.Show("Không có độc giả nào chưa có thẻ thư viện!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu ComboBox: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                cboDocGia.Items.Clear();
                cboDocGia.Items.Add("-- Lỗi tải dữ liệu --");
                cboDocGia.SelectedIndex = 0;
                btnLuu.Enabled = false;
            }
            finally
            {
                isLoading = false;
            }
        }

        private void SetDefaultValues()
        {
            dtpNgayCap.Value = DateTime.Now;
            dtpNgayHetHan.Value = DateTime.Now.AddYears(1);

            dtpNgayCap.ValueChanged += (s, e) =>
            {
                if (dtpNgayHetHan.Value <= dtpNgayCap.Value)
                {
                    dtpNgayHetHan.Value = dtpNgayCap.Value.AddYears(1);
                }
            };

            // Event khi thay đổi độc giả
            cboDocGia.SelectedIndexChanged += (s, e) =>
            {
                // Enable nút Lưu nếu đã chọn độc giả hợp lệ
                btnLuu.Enabled = (cboDocGia.SelectedIndex > 0 &&
                                 cboDocGia.SelectedValue != null &&
                                 cboDocGia.SelectedValue != DBNull.Value);
            };
        }

private void btnLuu_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
            return;

        try
        {
            int? maDocGia = null;
            if (cboDocGia.SelectedValue != null &&
                cboDocGia.SelectedValue != DBNull.Value &&
                cboDocGia.SelectedIndex > 0)
            {
                if (int.TryParse(cboDocGia.SelectedValue.ToString(), out int tempMaDocGia))
                {
                    maDocGia = tempMaDocGia;
                }
            }

            // Sử dụng method mới để lấy ID thẻ vừa tạo
            int newCardId = blTheThuVien.ThemTheThuVienVaLayMaThe(
                maDocGia,
                dtpNgayCap.Value.Date,
                dtpNgayHetHan.Value.Date,
                ref err
            );

            if (newCardId > 0)
            {
                MessageBox.Show("✅ Thêm thẻ thư viện thành công!", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Hỏi có muốn tạo QR Code không
                var result = MessageBox.Show(
                    "🔄 Bạn có muốn tạo QR Code cho thẻ này không?\n\n" +
                    "QR Code sẽ giúp quét thẻ nhanh chóng khi ra vào thư viện.",
                    "Tạo QR Code",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowQRCodeForNewCard(newCardId);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("❌ Thêm thẻ thư viện thất bại: " + err, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("❌ Lỗi: " + ex.Message, "Lỗi",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ShowQRCodeForNewCard(int newCardId)
    {
        try
        {
            var newCard = blTheThuVien.LayTheThuVienDayDuTheoMa(newCardId);

            if (newCard != null)
            {
                string qrText = QRCodeManager.CreateLibraryCardQR(newCard);
                var qrImage = QRCodeManager.GenerateQRCode(qrText);

                using (var qrDialog = new FormQRDisplay(qrImage, newCard))
                {
                    qrDialog.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("❌ Không thể lấy thông tin thẻ vừa tạo!", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"❌ Lỗi khi tạo QR Code: {ex.Message}", "Lỗi",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateInput()
        {
            if (cboDocGia.SelectedValue == null ||
                cboDocGia.SelectedValue == DBNull.Value ||
                cboDocGia.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn độc giả!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDocGia.Focus();
                return false;
            }

            if (dtpNgayCap.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày cấp không được lớn hơn ngày hiện tại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayCap.Focus();
                return false;
            }

            if (dtpNgayHetHan.Value <= dtpNgayCap.Value)
            {
                MessageBox.Show("Ngày hết hạn phải lớn hơn ngày cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHetHan.Focus();
                return false;
            }

            return true;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnLuu_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                btnHuy_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}