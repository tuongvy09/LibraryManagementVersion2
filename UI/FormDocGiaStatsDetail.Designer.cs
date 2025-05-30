using System.Drawing;
using System.Windows.Forms;

namespace ThuVien_EF.Forms
{
    partial class FormDocGiaStatsDetail
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblDocGiaInfo;
        private Panel panelStats;
        private Label lblTienMuon;
        private Label lblTienPhat;
        private Label lblTongCong;
        private Label lblSoLanMuon;
        private Label lblLanMuonGanNhat;
        private Button btnClose;
        private Button btnExportDetail;

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
            this.lblTitle = new Label();
            this.lblDocGiaInfo = new Label();
            this.panelStats = new Panel();
            this.lblTienMuon = new Label();
            this.lblTienPhat = new Label();
            this.lblTongCong = new Label();
            this.lblSoLanMuon = new Label();
            this.lblLanMuonGanNhat = new Label();
            this.btnClose = new Button();
            this.btnExportDetail = new Button();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(250, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 THỐNG KÊ CHI TIẾT";
            // 
            // lblDocGiaInfo
            // 
            this.lblDocGiaInfo.AutoSize = true;
            this.lblDocGiaInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblDocGiaInfo.Location = new Point(20, 60);
            this.lblDocGiaInfo.Name = "lblDocGiaInfo";
            this.lblDocGiaInfo.Size = new Size(200, 21);
            this.lblDocGiaInfo.TabIndex = 1;
            this.lblDocGiaInfo.Text = "Độc giả: [Tên] (Mã: [ID])";
            // 
            // panelStats
            // 
            this.panelStats.BorderStyle = BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.lblTienMuon);
            this.panelStats.Controls.Add(this.lblTienPhat);
            this.panelStats.Controls.Add(this.lblTongCong);
            this.panelStats.Controls.Add(this.lblSoLanMuon);
            this.panelStats.Controls.Add(this.lblLanMuonGanNhat);
            this.panelStats.Location = new Point(20, 100);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new Size(460, 220);
            this.panelStats.TabIndex = 2;
            // 
            // lblTienMuon
            // 
            this.lblTienMuon.AutoSize = true;
            this.lblTienMuon.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblTienMuon.Location = new Point(15, 20);
            this.lblTienMuon.Name = "lblTienMuon";
            this.lblTienMuon.Size = new Size(250, 20);
            this.lblTienMuon.TabIndex = 0;
            this.lblTienMuon.Text = "💰 Tổng tiền mượn sách: 0 VNĐ";
            // 
            // lblTienPhat
            // 
            this.lblTienPhat.AutoSize = true;
            this.lblTienPhat.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblTienPhat.Location = new Point(15, 55);
            this.lblTienPhat.Name = "lblTienPhat";
            this.lblTienPhat.Size = new Size(200, 20);
            this.lblTienPhat.TabIndex = 1;
            this.lblTienPhat.Text = "⚠️ Tổng tiền phạt: 0 VNĐ";
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblTongCong.ForeColor = Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTongCong.Location = new Point(15, 90);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new Size(180, 21);
            this.lblTongCong.TabIndex = 2;
            this.lblTongCong.Text = "💵 Tổng cộng: 0 VNĐ";
            // 
            // lblSoLanMuon
            // 
            this.lblSoLanMuon.AutoSize = true;
            this.lblSoLanMuon.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLanMuon.Location = new Point(15, 130);
            this.lblSoLanMuon.Name = "lblSoLanMuon";
            this.lblSoLanMuon.Size = new Size(160, 20);
            this.lblSoLanMuon.TabIndex = 3;
            this.lblSoLanMuon.Text = "📚 Số lần mượn: 0 lần";
            // 
            // lblLanMuonGanNhat
            // 
            this.lblLanMuonGanNhat.AutoSize = true;
            this.lblLanMuonGanNhat.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblLanMuonGanNhat.Location = new Point(15, 165);
            this.lblLanMuonGanNhat.Name = "lblLanMuonGanNhat";
            this.lblLanMuonGanNhat.Size = new Size(280, 20);
            this.lblLanMuonGanNhat.TabIndex = 4;
            this.lblLanMuonGanNhat.Text = "📅 Lần mượn gần nhất: Chưa mượn";
            // 
            // btnExportDetail
            // 
            this.btnExportDetail.BackColor = Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExportDetail.FlatStyle = FlatStyle.Flat;
            this.btnExportDetail.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnExportDetail.ForeColor = Color.White;
            this.btnExportDetail.Location = new Point(160, 340);
            this.btnExportDetail.Name = "btnExportDetail";
            this.btnExportDetail.Size = new Size(120, 35);
            this.btnExportDetail.TabIndex = 3;
            this.btnExportDetail.Text = "📄 Xuất chi tiết";
            this.btnExportDetail.UseVisualStyleBackColor = false;
            this.btnExportDetail.Click += new System.EventHandler(this.btnExportDetail_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(300, 340);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "✖️ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormDocGiaStatsDetail
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(500, 400);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportDetail);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.lblDocGiaInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDocGiaStatsDetail";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thống kê chi tiết độc giả";
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
