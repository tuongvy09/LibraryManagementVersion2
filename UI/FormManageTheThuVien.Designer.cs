namespace LibraryManagementVersion2.UI
{
    partial class FormManageTheThuVien
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabConHieuLuc;
        private System.Windows.Forms.TabPage tabHetHan;
        private System.Windows.Forms.DataGridView dgvConHieuLuc;
        private System.Windows.Forms.DataGridView dgvHetHan;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnTaoQR;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Label lblTitle;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabConHieuLuc = new System.Windows.Forms.TabPage();
            this.tabHetHan = new System.Windows.Forms.TabPage();
            this.dgvConHieuLuc = new System.Windows.Forms.DataGridView();
            this.dgvHetHan = new System.Windows.Forms.DataGridView();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnTaoQR = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();

            this.tabControl.SuspendLayout();
            this.tabConHieuLuc.SuspendLayout();
            this.tabHetHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConHieuLuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHetHan)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(364, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ THẺ THƯ VIỆN";

            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTimKiem.ForeColor = System.Drawing.Color.Black;
            this.lblTimKiem.Location = new System.Drawing.Point(15, 55);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(69, 19);
            this.lblTimKiem.TabIndex = 1;
            this.lblTimKiem.Text = "Tìm kiếm:";

            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.Black;
            this.txtTimKiem.Location = new System.Drawing.Point(90, 52);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(250, 25);
            this.txtTimKiem.TabIndex = 2;

            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(350, 50);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(80, 30);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(18, 90);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(80, 35);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(110, 90);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 35);
            this.btnSua.TabIndex = 5;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.Location = new System.Drawing.Point(202, 90);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(80, 35);
            this.btnReload.TabIndex = 6;
            this.btnReload.Text = "Làm mới";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);

            // 
            // btnTaoQR
            // 
            this.btnTaoQR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnTaoQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoQR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoQR.ForeColor = System.Drawing.Color.White;
            this.btnTaoQR.Location = new System.Drawing.Point(294, 90);
            this.btnTaoQR.Name = "btnTaoQR";
            this.btnTaoQR.Size = new System.Drawing.Size(80, 35);
            this.btnTaoQR.TabIndex = 7;
            this.btnTaoQR.Text = "Tạo QR";
            this.btnTaoQR.UseVisualStyleBackColor = false;
            this.btnTaoQR.Click += new System.EventHandler(this.btnTaoQR_Click);

            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabConHieuLuc);
            this.tabControl.Controls.Add(this.tabHetHan);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabControl.Location = new System.Drawing.Point(18, 135);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(950, 400);
            this.tabControl.TabIndex = 8;

            // 
            // tabConHieuLuc
            // 
            this.tabConHieuLuc.BackColor = System.Drawing.Color.White;
            this.tabConHieuLuc.Controls.Add(this.dgvConHieuLuc);
            this.tabConHieuLuc.ForeColor = System.Drawing.Color.Black;
            this.tabConHieuLuc.Location = new System.Drawing.Point(4, 26);
            this.tabConHieuLuc.Name = "tabConHieuLuc";
            this.tabConHieuLuc.Padding = new System.Windows.Forms.Padding(3);
            this.tabConHieuLuc.Size = new System.Drawing.Size(942, 370);
            this.tabConHieuLuc.TabIndex = 0;
            this.tabConHieuLuc.Text = "Còn hiệu lực";

            // 
            // tabHetHan
            // 
            this.tabHetHan.BackColor = System.Drawing.Color.White;
            this.tabHetHan.Controls.Add(this.dgvHetHan);
            this.tabHetHan.ForeColor = System.Drawing.Color.Black;
            this.tabHetHan.Location = new System.Drawing.Point(4, 26);
            this.tabHetHan.Name = "tabHetHan";
            this.tabHetHan.Padding = new System.Windows.Forms.Padding(3);
            this.tabHetHan.Size = new System.Drawing.Size(942, 370);
            this.tabHetHan.TabIndex = 1;
            this.tabHetHan.Text = "Hết hạn";

            // 
            // dgvConHieuLuc
            // 
            this.dgvConHieuLuc.AllowUserToAddRows = false;
            this.dgvConHieuLuc.AllowUserToDeleteRows = false;
            this.dgvConHieuLuc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConHieuLuc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConHieuLuc.BackgroundColor = System.Drawing.Color.White;
            // CHỈ IN ĐẬM HEADER - NỘI DUNG MÀU ĐEN BÌNH THƯỜNG
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConHieuLuc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConHieuLuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConHieuLuc.EnableHeadersVisualStyles = false;
            // NỘI DUNG MÀU ĐEN BÌNH THƯỜNG
            this.dgvConHieuLuc.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvConHieuLuc.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvConHieuLuc.Location = new System.Drawing.Point(3, 3);
            this.dgvConHieuLuc.MultiSelect = false;
            this.dgvConHieuLuc.Name = "dgvConHieuLuc";
            this.dgvConHieuLuc.ReadOnly = true;
            this.dgvConHieuLuc.RowHeadersVisible = false;
            this.dgvConHieuLuc.RowHeadersWidth = 51;
            this.dgvConHieuLuc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConHieuLuc.Size = new System.Drawing.Size(936, 364);
            this.dgvConHieuLuc.TabIndex = 0;
            this.dgvConHieuLuc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConHieuLuc_CellClick);

            // 
            // dgvHetHan
            // 
            this.dgvHetHan.AllowUserToAddRows = false;
            this.dgvHetHan.AllowUserToDeleteRows = false;
            this.dgvHetHan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHetHan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHetHan.BackgroundColor = System.Drawing.Color.White;
            // CHỈ IN ĐẬM HEADER - NỘI DUNG MÀU ĐEN BÌNH THƯỜNG
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHetHan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHetHan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHetHan.EnableHeadersVisualStyles = false;
            // NỘI DUNG MÀU ĐEN BÌNH THƯỜNG
            this.dgvHetHan.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvHetHan.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvHetHan.Location = new System.Drawing.Point(3, 3);
            this.dgvHetHan.MultiSelect = false;
            this.dgvHetHan.Name = "dgvHetHan";
            this.dgvHetHan.ReadOnly = true;
            this.dgvHetHan.RowHeadersVisible = false;
            this.dgvHetHan.RowHeadersWidth = 51;
            this.dgvHetHan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHetHan.Size = new System.Drawing.Size(936, 364);
            this.dgvHetHan.TabIndex = 0;
            this.dgvHetHan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHetHan_CellClick);

            // 
            // FormManageTheThuVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnTaoQR);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormManageTheThuVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Thẻ thư viện";
            this.Load += new System.EventHandler(this.FormManageTheThuVien_Load);

            this.tabControl.ResumeLayout(false);
            this.tabConHieuLuc.ResumeLayout(false);
            this.tabHetHan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConHieuLuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHetHan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
