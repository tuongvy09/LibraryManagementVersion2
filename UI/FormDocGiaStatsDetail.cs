using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ThuVien_EF.Forms
{
    public partial class FormDocGiaStatsDetail : Form
    {
        private DataRow statsData;

        public FormDocGiaStatsDetail(DataRow data)
        {
            InitializeComponent();
            this.statsData = data;
            LoadStatsData();
        }

        private void LoadStatsData()
        {
            try
            {
                if (statsData != null)
                {
                    // Cập nhật tiêu đề form
                    this.Text = $"Thống kê chi tiết - {statsData["HoTen"]}";

                    // Hiển thị thông tin lên các label
                    lblDocGiaInfo.Text = $"Độc giả: {statsData["HoTen"]} (Mã: {statsData["MaDocGia"]})";

                    decimal tongTienMuon = Convert.ToDecimal(statsData["TongTienMuon"]);
                    decimal tongTienPhat = Convert.ToDecimal(statsData["TongTienPhat"]);
                    decimal tongCong = Convert.ToDecimal(statsData["TongCong"]);
                    int soLanMuon = Convert.ToInt32(statsData["SoLanMuon"]);
                    DateTime lanMuonGanNhat = Convert.ToDateTime(statsData["LanMuonGanNhat"]);

                    lblTienMuon.Text = $"💰 Tổng tiền mượn sách: {tongTienMuon:N0} VNĐ";
                    lblTienPhat.Text = $"⚠️ Tổng tiền phạt: {tongTienPhat:N0} VNĐ";
                    lblTongCong.Text = $"💵 Tổng cộng: {tongCong:N0} VNĐ";
                    lblSoLanMuon.Text = $"📚 Số lần mượn: {soLanMuon} lần";
                    lblLanMuonGanNhat.Text = $"📅 Lần mượn gần nhất: {(lanMuonGanNhat != DateTime.MinValue ? lanMuonGanNhat.ToString("dd/MM/yyyy") : "Chưa mượn")}";

                    // Highlight tổng cộng nếu > 0
                    if (tongCong > 0)
                    {
                        lblTongCong.ForeColor = Color.Red;
                        lblTongCong.Font = new Font(lblTongCong.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
