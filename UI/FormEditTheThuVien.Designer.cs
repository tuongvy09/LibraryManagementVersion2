namespace LibraryManagementVersion2.UI
{
    partial class FormEditTheThuVien
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaThe;
        private System.Windows.Forms.Label lblDocGia;
        private System.Windows.Forms.Label lblNgayCap;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblTrangThaiValue;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtMaThe;
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
            this.lblMaThe = new System.Windows.Forms.Label();
            this.lblDocGia = new System.Windows.Forms.Label();
            this.lblNgayCap = new System.Windows.Forms.Label();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblTrangThaiValue = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtMaThe = new System.Windows.Forms.TextBox();
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
            this.lblTitle.Location = new System.Drawing.Point(170, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(190, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sửa Thẻ Thư Viện";

            // 
            // lblMaThe
            // 
            this.lblMaThe.AutoSize = true;
            this.lblMaThe.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMaThe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblMaThe.Location = new System.Drawing.Point(56, 80);
            this.lblMaThe.Name = "lblMaThe";
            this.lblMaThe.Size = new System.Drawing.Size(63, 20);
            this.lblMaThe.TabIndex = 1;
            this.lblMaThe.Text = "Mã thẻ:";

            // 
            // txtMaThe
            // 
            this.txtMaThe.BackColor = System.Drawing.Color.LightGray;
            this.txtMaThe.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMaThe.Location = new System.Drawing.Point(200, 77);
            this.txtMaThe.Name = "txtMaThe";
            this.txtMaThe.ReadOnly = true;
            this.txtMaThe.Size = new System.Drawing.Size(100, 27);
            this.txtMaThe.TabIndex = 2;

            // 
            // lblDocGia
            // 
            this.lblDocGia.AutoSize = true;
            this.lblDocGia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDocGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblDocGia.Location = new System.Drawing.Point(56, 130);
            this.lblDocGia.Name = "lblDocGia";
            this.lblDocGia.Size = new System.Drawing.Size(76, 20);
            this.lblDocGia.TabIndex = 3;
            this.lblDocGia.Text = "Độc giả: *";

            // 
            // cboDocGia
            // 
            this.cboDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocGia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboDocGia.FormattingEnabled = true;
            this.cboDocGia.Location = new System.Drawing.Point(200, 127);
            this.cboDocGia.Name = "cboDocGia";
            this.cboDocGia.Size = new System.Drawing.Size(350, 28);
            this.cboDocGia.TabIndex = 4;

            // 
            // lblNgayCap
            // 
            this.lblNgayCap.AutoSize = true;
            this.lblNgayCap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblNgayCap.Location = new System.Drawing.Point(56, 180);
            this.lblNgayCap.Name = "lblNgayCap";
            this.lblNgayCap.Size = new System.Drawing.Size(88, 20);
            this.lblNgayCap.TabIndex = 5;
            this.lblNgayCap.Text = "Ngày cấp: *";

            // 
            // dtpNgayCap
            // 
            this.dtpNgayCap.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayCap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayCap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayCap.Location = new System.Drawing.Point(200, 177);
            this.dtpNgayCap.Name = "dtpNgayCap";
            this.dtpNgayCap.Size = new System.Drawing.Size(200, 27);
            this.dtpNgayCap.TabIndex = 6;

            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayHetHan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblNgayHetHan.Location = new System.Drawing.Point(56, 230);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(114, 20);
            this.lblNgayHetHan.TabIndex = 7;
            this.lblNgayHetHan.Text = "Ngày hết hạn: *";

            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(200, 227);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(200, 27);
            this.dtpNgayHetHan.TabIndex = 8;
            this.dtpNgayHetHan.ValueChanged += new System.EventHandler(this.dtpNgayHetHan_ValueChanged);

            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTrangThai.Location = new System.Drawing.Point(56, 280);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(82, 20);
            this.lblTrangThai.TabIndex = 9;
            this.lblTrangThai.Text = "Trạng thái:";

            // 
            // lblTrangThaiValue
            // 
            this.lblTrangThaiValue.AutoSize = true;
            this.lblTrangThaiValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiValue.ForeColor = System.Drawing.Color.Red;
            this.lblTrangThaiValue.Location = new System.Drawing.Point(200, 280);
            this.lblTrangThaiValue.Name = "lblTrangThaiValue";
            this.lblTrangThaiValue.Size = new System.Drawing.Size(66, 20);
            this.lblTrangThaiValue.TabIndex = 10;
            this.lblTrangThaiValue.Text = "Hết hạn";

            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(56, 330);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(101, 15);
            this.lblNote.TabIndex = 11;
            this.lblNote.Text = "* Trường bắt buộc";

            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(200, 370);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(85, 35);
            this.btnLuu.TabIndex = 12;
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
            this.btnHuy.Location = new System.Drawing.Point(325, 370);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 35);
            this.btnHuy.TabIndex = 13;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // 
            // FormEditTheThuVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblTrangThaiValue);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.dtpNgayHetHan);
            this.Controls.Add(this.lblNgayHetHan);
            this.Controls.Add(this.dtpNgayCap);
            this.Controls.Add(this.lblNgayCap);
            this.Controls.Add(this.cboDocGia);
            this.Controls.Add(this.lblDocGia);
            this.Controls.Add(this.txtMaThe);
            this.Controls.Add(this.lblMaThe);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditTheThuVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa Thẻ Thư Viện";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
