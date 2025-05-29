namespace LibraryManagementVersion2.UI
{
    partial class FormAddThuThu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTenThuThu, lblEmail, lblSoDienThoai, lblDiaChi;
        private System.Windows.Forms.Label lblNgayBatDauLam, lblNgaySinh, lblGioiTinh;
        private System.Windows.Forms.TextBox txtTenThuThu, txtEmail, txtSoDienThoai, txtDiaChi;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDauLam, dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnLuu, btnHuy;

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
            this.lblTenThuThu = new System.Windows.Forms.Label();
            this.txtTenThuThu = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblNgayBatDauLam = new System.Windows.Forms.Label();
            this.dtpNgayBatDauLam = new System.Windows.Forms.DateTimePicker();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTitle.Location = new System.Drawing.Point(220, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(132, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm Thủ thư";
            // 
            // lblTenThuThu
            // 
            this.lblTenThuThu.AutoSize = true;
            this.lblTenThuThu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenThuThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTenThuThu.Location = new System.Drawing.Point(50, 70);
            this.lblTenThuThu.Name = "lblTenThuThu";
            this.lblTenThuThu.Size = new System.Drawing.Size(90, 19);
            this.lblTenThuThu.TabIndex = 1;
            this.lblTenThuThu.Text = "Tên thủ thư: *";
            // 
            // txtTenThuThu
            // 
            this.txtTenThuThu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenThuThu.Location = new System.Drawing.Point(210, 67);
            this.txtTenThuThu.Name = "txtTenThuThu";
            this.txtTenThuThu.Size = new System.Drawing.Size(300, 25);
            this.txtTenThuThu.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblEmail.Location = new System.Drawing.Point(50, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 19);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(210, 107);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 25);
            this.txtEmail.TabIndex = 4;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoDienThoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblSoDienThoai.Location = new System.Drawing.Point(50, 150);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(99, 19);
            this.lblSoDienThoai.TabIndex = 5;
            this.lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(210, 147);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(300, 25);
            this.txtSoDienThoai.TabIndex = 6;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblDiaChi.Location = new System.Drawing.Point(50, 190);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(54, 19);
            this.lblDiaChi.TabIndex = 7;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new System.Drawing.Point(210, 187);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(300, 60);
            this.txtDiaChi.TabIndex = 8;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblNgaySinh.Location = new System.Drawing.Point(50, 270);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(77, 19);
            this.lblNgaySinh.TabIndex = 9;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(210, 267);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(140, 25);
            this.dtpNgaySinh.TabIndex = 10;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioiTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblGioiTinh.Location = new System.Drawing.Point(370, 270);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(68, 19);
            this.lblGioiTinh.TabIndex = 11;
            this.lblGioiTinh.Text = "Giới tính:";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cboGioiTinh.Location = new System.Drawing.Point(444, 267);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(66, 25);
            this.cboGioiTinh.TabIndex = 12;
            // 
            // lblNgayBatDauLam
            // 
            this.lblNgayBatDauLam.AutoSize = true;
            this.lblNgayBatDauLam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayBatDauLam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblNgayBatDauLam.Location = new System.Drawing.Point(50, 310);
            this.lblNgayBatDauLam.Name = "lblNgayBatDauLam";
            this.lblNgayBatDauLam.Size = new System.Drawing.Size(126, 19);
            this.lblNgayBatDauLam.TabIndex = 13;
            this.lblNgayBatDauLam.Text = "Ngày bắt đầu làm:";
            // 
            // dtpNgayBatDauLam
            // 
            this.dtpNgayBatDauLam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayBatDauLam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDauLam.Location = new System.Drawing.Point(210, 307);
            this.dtpNgayBatDauLam.Name = "dtpNgayBatDauLam";
            this.dtpNgayBatDauLam.Size = new System.Drawing.Size(140, 25);
            this.dtpNgayBatDauLam.TabIndex = 14;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.chkTrangThai.Location = new System.Drawing.Point(210, 350);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(87, 23);
            this.chkTrangThai.TabIndex = 15;
            this.chkTrangThai.Text = "Hoạt động";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(210, 390);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 35);
            this.btnLuu.TabIndex = 16;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(310, 390);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 35);
            this.btnHuy.TabIndex = 17;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FormAddThuThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 450);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.dtpNgayBatDauLam);
            this.Controls.Add(this.lblNgayBatDauLam);
            this.Controls.Add(this.cboGioiTinh);
            this.Controls.Add(this.lblGioiTinh);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtTenThuThu);
            this.Controls.Add(this.lblTenThuThu);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormAddThuThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Thủ thư";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}