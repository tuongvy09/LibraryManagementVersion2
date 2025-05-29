namespace LibraryManagementVersion2.UI
{
    partial class FormAddTheThuVien
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDocGia;
        private System.Windows.Forms.Label lblNgayCap;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.ComboBox cboDocGia;
        private System.Windows.Forms.DateTimePicker dtpNgayCap;
        private System.Windows.Forms.DateTimePicker dtpNgayHetHan;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDocGia = new System.Windows.Forms.Label();
            this.lblNgayCap = new System.Windows.Forms.Label();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.cboDocGia = new System.Windows.Forms.ComboBox();
            this.dtpNgayCap = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTitle.Location = new System.Drawing.Point(150, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(206, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm Thẻ Thư Viện";

            // 
            // lblDocGia
            // 
            this.lblDocGia.AutoSize = true;
            this.lblDocGia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDocGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblDocGia.Location = new System.Drawing.Point(43, 80);
            this.lblDocGia.Name = "lblDocGia";
            this.lblDocGia.Size = new System.Drawing.Size(76, 20);
            this.lblDocGia.TabIndex = 1;
            this.lblDocGia.Text = "Độc giả: *";

            // 
            // cboDocGia
            // 
            this.cboDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocGia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboDocGia.FormattingEnabled = true;
            this.cboDocGia.Location = new System.Drawing.Point(200, 77);
            this.cboDocGia.Name = "cboDocGia";
            this.cboDocGia.Size = new System.Drawing.Size(350, 28);
            this.cboDocGia.TabIndex = 2;

            // 
            // lblNgayCap
            // 
            this.lblNgayCap.AutoSize = true;
            this.lblNgayCap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblNgayCap.Location = new System.Drawing.Point(43, 130);
            this.lblNgayCap.Name = "lblNgayCap";
            this.lblNgayCap.Size = new System.Drawing.Size(88, 20);
            this.lblNgayCap.TabIndex = 3;
            this.lblNgayCap.Text = "Ngày cấp: *";

            // 
            // dtpNgayCap
            // 
            this.dtpNgayCap.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayCap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayCap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayCap.Location = new System.Drawing.Point(200, 127);
            this.dtpNgayCap.Name = "dtpNgayCap";
            this.dtpNgayCap.Size = new System.Drawing.Size(200, 27);
            this.dtpNgayCap.TabIndex = 4;

            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayHetHan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblNgayHetHan.Location = new System.Drawing.Point(43, 180);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(114, 20);
            this.lblNgayHetHan.TabIndex = 5;
            this.lblNgayHetHan.Text = "Ngày hết hạn: *";

            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(200, 177);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(200, 27);
            this.dtpNgayHetHan.TabIndex = 6;

            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(43, 230);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(101, 15);
            this.lblNote.TabIndex = 7;
            this.lblNote.Text = "* Trường bắt buộc";

            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(200, 270);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(85, 35);
            this.btnLuu.TabIndex = 8;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(325, 270);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 35);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // 
            // FormAddTheThuVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 350);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.dtpNgayHetHan);
            this.Controls.Add(this.lblNgayHetHan);
            this.Controls.Add(this.dtpNgayCap);
            this.Controls.Add(this.lblNgayCap);
            this.Controls.Add(this.cboDocGia);
            this.Controls.Add(this.lblDocGia);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddTheThuVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Thẻ Thư Viện";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
