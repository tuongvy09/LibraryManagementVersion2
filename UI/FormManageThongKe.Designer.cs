using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ThuVien_EF.Forms
{
    partial class FormManageThongKe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ChartArea chartArea1 = new ChartArea();
            ChartArea chartArea2 = new ChartArea();
            this.lblTitle = new Label();
            this.panelFilters = new Panel();
            this.btnExport = new Button();
            this.cboThang = new ComboBox();
            this.lblThang = new Label();
            this.cboNam = new ComboBox();
            this.lblNam = new Label();
            this.tabControl = new TabControl();
            this.tabDocGia = new TabPage();
            this.chartDocGia = new Chart();
            this.tabDoanhthu = new TabPage();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chartDoanhThu = new Chart();
            this.tabTop10SachMuonNhieuNhat = new System.Windows.Forms.TabPage();
            this.dgvBestSeller = new System.Windows.Forms.DataGridView();
            this.panelFilters.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDocGia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDocGia)).BeginInit();
            this.tabDoanhthu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTitle.Location = new Point(20, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(324, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 THỐNG KÊ BÁO CÁO";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelFilters.BorderStyle = BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.btnExport);
            this.panelFilters.Controls.Add(this.cboThang);
            this.panelFilters.Controls.Add(this.lblThang);
            this.panelFilters.Controls.Add(this.cboNam);
            this.panelFilters.Controls.Add(this.lblNam);
            this.panelFilters.Location = new Point(20, 61);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new Size(1150, 49);
            this.panelFilters.TabIndex = 1;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = FlatStyle.Flat;
            this.btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = Color.White;
            this.btnExport.Location = new Point(300, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(131, 31);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "📋 Xuất báo cáo";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // cboThang
            // 
            this.cboThang.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboThang.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Items.AddRange(new object[] {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"});
            this.cboThang.Location = new Point(210, 12);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new Size(68, 28);
            this.cboThang.TabIndex = 3;
            this.cboThang.SelectedIndexChanged += new System.EventHandler(this.CboThang_SelectedIndexChanged);
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.ForeColor = Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblThang.Location = new Point(150, 15);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new Size(57, 20);
            this.lblThang.TabIndex = 2;
            this.lblThang.Text = "Tháng:";
            // 
            // cboNam
            // 
            this.cboNam.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboNam.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Items.AddRange(new object[] {
            "2020", "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030"});
            this.cboNam.Location = new Point(52, 12);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new Size(84, 28);
            this.cboNam.TabIndex = 1;
            this.cboNam.SelectedIndexChanged += new System.EventHandler(this.CboNam_SelectedIndexChanged);
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblNam.ForeColor = Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblNam.Location = new Point(11, 15);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new Size(47, 20);
            this.lblNam.TabIndex = 0;
            this.lblNam.Text = "Năm:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDocGia);
            this.tabControl.Controls.Add(this.tabDoanhthu);
            this.tabControl.Controls.Add(this.tabTop10SachMuonNhieuNhat);
            this.tabControl.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new Point(20, 122);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(1150, 401);
            this.tabControl.TabIndex = 2;
            // 
            // tabDocGia
            // 
            this.tabDocGia.Controls.Add(this.chartDocGia);
            this.tabDocGia.Location = new Point(4, 29);
            this.tabDocGia.Name = "tabDocGia";
            this.tabDocGia.Padding = new Padding(3);
            this.tabDocGia.Size = new Size(1142, 368);
            this.tabDocGia.TabIndex = 0;
            this.tabDocGia.Text = "📈 Độc giả mới theo tháng";
            this.tabDocGia.UseVisualStyleBackColor = true;
            // 
            // chartDocGia
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDocGia.ChartAreas.Add(chartArea1);
            this.chartDocGia.Dock = DockStyle.Fill;
            this.chartDocGia.Location = new Point(3, 3);
            this.chartDocGia.Name = "chartDocGia";
            this.chartDocGia.Size = new Size(1136, 362);
            this.chartDocGia.TabIndex = 0;
            this.chartDocGia.Text = "chartDocGia";
            // 
            // tabDoanhthu
            // 
            this.tabDoanhthu.Controls.Add(this.chartDoanhThu);
            this.tabDoanhthu.Location = new Point(4, 29);
            this.tabDoanhthu.Name = "tabDoanhthu";
            this.tabDoanhthu.Padding = new Padding(3);
            this.tabDoanhthu.Size = new Size(1142, 368);
            this.tabDoanhthu.TabIndex = 1;
            this.tabDoanhthu.Text = "💰 Doanh thu theo tháng";
            this.tabDoanhthu.UseVisualStyleBackColor = true;
            // 
            // chartDoanhThu
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea2);
            this.chartDoanhThu.Dock = DockStyle.Fill;
            this.chartDoanhThu.Location = new Point(3, 3);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new Size(1136, 362);
            this.chartDoanhThu.TabIndex = 0;
            this.chartDoanhThu.Text = "chartDoanhThu";
            // 
            // tabTop10SachMuonNhieuNhat
            // 
            this.tabTop10SachMuonNhieuNhat.Controls.Add(this.dgvBestSeller);
            this.tabTop10SachMuonNhieuNhat.Location = new System.Drawing.Point(4, 29);
            this.tabTop10SachMuonNhieuNhat.Name = "tabTop10SachMuonNhieuNhat";
            this.tabTop10SachMuonNhieuNhat.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabTop10SachMuonNhieuNhat.Size = new System.Drawing.Size(1142, 368);
            this.tabTop10SachMuonNhieuNhat.TabIndex = 0;
            this.tabTop10SachMuonNhieuNhat.Text = "📚 Top 10 sách mượn nhiều";
            this.tabTop10SachMuonNhieuNhat.UseVisualStyleBackColor = true;
            // 
            // dgvBestSeller
            // 
            this.dgvBestSeller.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBestSeller.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvBestSeller.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBestSeller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBestSeller.EnableHeadersVisualStyles = false;
            this.dgvBestSeller.Location = new System.Drawing.Point(3, 3);
            this.dgvBestSeller.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvBestSeller.Name = "dgvBestSeller";
            this.dgvBestSeller.ReadOnly = true;
            this.dgvBestSeller.Size = new System.Drawing.Size(1136, 362);
            this.dgvBestSeller.TabIndex = 0;

            // 
            // FormManageThongKe
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1200, 544);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormManageThongKe";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Thống Kê - Entity Framework";
            this.Load += new System.EventHandler(this.FormManageThongKe_Load);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabDocGia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDocGia)).EndInit();
            this.tabDoanhthu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblTitle;
        private Panel panelFilters;
        private Label lblNam;
        private ComboBox cboNam;
        private Label lblThang;
        private ComboBox cboThang;
        private Button btnExport;
        private TabControl tabControl;
        private TabPage tabDocGia;
        private Chart chartDocGia;
        private TabPage tabDoanhthu;
        private Chart chartDoanhThu;

        private System.Windows.Forms.TabPage tabTop10SachMuonNhieuNhat;
        private System.Windows.Forms.DataGridView dgvBestSeller;
    }
}
