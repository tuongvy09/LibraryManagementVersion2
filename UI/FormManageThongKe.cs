using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ThuVien_EF.BS_Layer;

namespace ThuVien_EF.Forms
{
    public partial class FormManageThongKe : Form
    {
        private readonly BLThongKe blThongKe = new BLThongKe();
        private bool dataLoaded = false;
        private bool isLoading = false;

        public FormManageThongKe()
        {
            InitializeComponent();
            InitializeControls();
            SetupChartStyles();
        }

        private void InitializeControls()
        {
            // Khởi tạo ComboBox values
            string currentYear = DateTime.Now.Year.ToString();
            string currentMonth = DateTime.Now.Month.ToString();

            if (cboNam.Items.Contains(currentYear))
                cboNam.SelectedItem = currentYear;
            else
                cboNam.SelectedIndex = 5; // Default 2025

            if (cboThang.Items.Contains(currentMonth))
                cboThang.SelectedItem = currentMonth;
            else
                cboThang.SelectedIndex = 4; // Default tháng 5
        }

        private void SetupChartStyles()
        {
            // Style cho chart Độc giả
            chartDocGia.BackColor = Color.Transparent;
            chartDocGia.ChartAreas[0].BackColor = Color.White;
            chartDocGia.ChartAreas[0].BorderColor = Color.LightGray;
            chartDocGia.ChartAreas[0].BorderWidth = 1;
            chartDocGia.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartDocGia.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartDocGia.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9F);
            chartDocGia.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9F);

            // Style cho chart Doanh thu
            chartDoanhThu.BackColor = Color.Transparent;
            chartDoanhThu.ChartAreas[0].BackColor = Color.White;
            chartDoanhThu.ChartAreas[0].BorderColor = Color.LightGray;
            chartDoanhThu.ChartAreas[0].BorderWidth = 1;
            chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9F);
            chartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9F);
        }

        private void FormManageThongKe_Load(object sender, EventArgs e)
        {
            if (!dataLoaded)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (isLoading) return;

            try
            {
                isLoading = true;
                btnExport.Enabled = false;
                btnExport.Text = "Đang tải...";

                if (!int.TryParse(cboNam.SelectedItem?.ToString(), out int nam))
                {
                    MessageBox.Show("Vui lòng chọn năm hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nam < 2020 || nam > 2030)
                {
                    MessageBox.Show("Năm phải từ 2020-2030!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoadThongKeDocGiaChart(nam);
                LoadThongKeDoanhThuChart(nam);

                dataLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
                btnExport.Enabled = true;
                btnExport.Text = "📋 Xuất báo cáo";
            }
        }

        private void LoadThongKeDocGiaChart(int nam)
        {
            try
            {
                DataTable data = blThongKe.GetThongKeDocGiaTheoThang(nam);

                chartDocGia.Series.Clear();
                Series series = new Series("Độc giả mới");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(115, 154, 79);
                series.BorderWidth = 2;
                series.Font = new Font("Segoe UI", 9F);

                // Add data for all 12 months
                for (int i = 1; i <= 12; i++)
                {
                    // Tìm data cho tháng i
                    DataRow[] monthRows = data.Select($"Thang = {i}");
                    int count = 0;

                    if (monthRows.Length > 0)
                    {
                        count = Convert.ToInt32(monthRows[0]["SoLuongDocGiaMoi"]);
                    }

                    var point = series.Points.AddXY($"T{i}", count);
                    series.Points[point].Label = count.ToString();
                    series.Points[point].LabelForeColor = Color.Black;
                }

                chartDocGia.Series.Add(series);
                chartDocGia.ChartAreas[0].AxisX.Title = "Tháng";
                chartDocGia.ChartAreas[0].AxisY.Title = "Số lượng độc giả";
                chartDocGia.ChartAreas[0].AxisX.TitleFont = new Font("Segoe UI", 10F, FontStyle.Bold);
                chartDocGia.ChartAreas[0].AxisY.TitleFont = new Font("Segoe UI", 10F, FontStyle.Bold);

                chartDocGia.Titles.Clear();
                var title = chartDocGia.Titles.Add($"Thống kê độc giả mới năm {nam}");
                title.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                title.ForeColor = Color.FromArgb(115, 154, 79);

                // Add legend
                chartDocGia.Legends.Clear();
                Legend legend = new Legend();
                legend.Font = new Font("Segoe UI", 9F);
                chartDocGia.Legends.Add(legend);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải biểu đồ độc giả: {ex.Message}", "Lỗi");
            }
        }

        private void LoadThongKeDoanhThuChart(int nam)
        {
            try
            {
                DataTable data = blThongKe.GetThongKeDoanhThuTheoThang(nam);

                System.Diagnostics.Debug.WriteLine($"🔍 Loaded {data.Rows.Count} rows cho năm {nam}");

                chartDoanhThu.Series.Clear();
                chartDoanhThu.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;

                // Series cho doanh thu (cột)
                Series seriesDoanhThu = new Series("Doanh thu");
                seriesDoanhThu.ChartType = SeriesChartType.Column;
                seriesDoanhThu.Color = Color.FromArgb(40, 167, 69);
                seriesDoanhThu.BorderWidth = 0;
                seriesDoanhThu.IsValueShownAsLabel = true;

                // Series cho số giao dịch (đường)
                Series seriesGiaoDich = new Series("Số giao dịch");
                seriesGiaoDich.ChartType = SeriesChartType.Line;
                seriesGiaoDich.Color = Color.FromArgb(255, 193, 7);
                seriesGiaoDich.BorderWidth = 3;
                seriesGiaoDich.MarkerStyle = MarkerStyle.Circle;
                seriesGiaoDich.MarkerSize = 8;
                seriesGiaoDich.YAxisType = AxisType.Secondary;
                seriesGiaoDich.IsValueShownAsLabel = true;

                decimal maxDoanhThu = 0;
                int maxGiaoDich = 0;

                // Add data for all 12 months
                for (int i = 1; i <= 12; i++)
                {
                    // Tìm data cho tháng i
                    DataRow[] monthRows = data.Select($"Thang = {i}");
                    decimal doanhThu = 0;
                    int soGiaoDich = 0;

                    if (monthRows.Length > 0)
                    {
                        doanhThu = Convert.ToDecimal(monthRows[0]["TongDoanhThu"]);
                        soGiaoDich = Convert.ToInt32(monthRows[0]["SoGiaoDich"]);
                    }

                    System.Diagnostics.Debug.WriteLine($"📊 Tháng {i}: Doanh thu = {doanhThu:N0}, Giao dịch = {soGiaoDich}");

                    // Track max values
                    if (doanhThu > maxDoanhThu) maxDoanhThu = doanhThu;
                    if (soGiaoDich > maxGiaoDich) maxGiaoDich = soGiaoDich;

                    // Thêm điểm
                    seriesDoanhThu.Points.AddXY($"T{i}", (double)doanhThu);
                    seriesGiaoDich.Points.AddXY($"T{i}", soGiaoDich);
                }

                chartDoanhThu.Series.Add(seriesDoanhThu);
                chartDoanhThu.Series.Add(seriesGiaoDich);

                // Thiết lập trục
                chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
                chartDoanhThu.ChartAreas[0].AxisY2.Title = "Số giao dịch";
                chartDoanhThu.ChartAreas[0].AxisX.TitleFont = new Font("Segoe UI", 10F, FontStyle.Bold);
                chartDoanhThu.ChartAreas[0].AxisY.TitleFont = new Font("Segoe UI", 10F, FontStyle.Bold);
                chartDoanhThu.ChartAreas[0].AxisY2.TitleFont = new Font("Segoe UI", 10F, FontStyle.Bold);

                // Format Y-axis
                chartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N0";

                // Set axis ranges
                if (maxDoanhThu > 0)
                {
                    chartDoanhThu.ChartAreas[0].AxisY.Minimum = 0;
                    chartDoanhThu.ChartAreas[0].AxisY.Maximum = (double)(maxDoanhThu * 1.2m);
                }

                if (maxGiaoDich > 0)
                {
                    chartDoanhThu.ChartAreas[0].AxisY2.Minimum = 0;
                    chartDoanhThu.ChartAreas[0].AxisY2.Maximum = maxGiaoDich * 1.2;
                }

                // Thiết lập tiêu đề
                chartDoanhThu.Titles.Clear();
                var title = chartDoanhThu.Titles.Add($"Thống kê doanh thu năm {nam}");
                title.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                title.ForeColor = Color.FromArgb(115, 154, 79);

                // Thêm legend
                chartDoanhThu.Legends.Clear();
                Legend legend = new Legend();
                legend.Font = new Font("Segoe UI", 9F);
                legend.Docking = Docking.Top;
                chartDoanhThu.Legends.Add(legend);

                chartDoanhThu.Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi LoadThongKeDoanhThuChart: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải biểu đồ doanh thu: {ex.Message}", "Lỗi");
            }
        }

        private void CboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataLoaded && !isLoading)
            {
                LoadData();
            }
        }

        private void CboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataLoaded && !isLoading)
            {
                LoadData();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Chức năng xuất báo cáo Excel đang được phát triển!\n\nDữ liệu hiện tại có thể copy từ biểu đồ.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
