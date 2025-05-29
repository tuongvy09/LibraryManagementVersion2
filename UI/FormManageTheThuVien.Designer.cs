using System.Windows.Forms;

namespace LibraryManagementVersion2.UI
{
    partial class FormManageTheThuVien
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvTheThuVien;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnTaoQR;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelButtons;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTheThuVien = new System.Windows.Forms.DataGridView();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnTaoQR = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheThuVien)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTheThuVien
            // 
            this.dgvTheThuVien.AllowUserToAddRows = false;
            this.dgvTheThuVien.AllowUserToDeleteRows = false;
            this.dgvTheThuVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTheThuVien.BackgroundColor = System.Drawing.Color.White;
            this.dgvTheThuVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTheThuVien.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTheThuVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTheThuVien.ColumnHeadersHeight = 40;
            this.dgvTheThuVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(213)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTheThuVien.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTheThuVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTheThuVien.EnableHeadersVisualStyles = false;
            this.dgvTheThuVien.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.dgvTheThuVien.Location = new System.Drawing.Point(0, 130);
            this.dgvTheThuVien.MultiSelect = false;
            this.dgvTheThuVien.Name = "dgvTheThuVien";
            this.dgvTheThuVien.ReadOnly = true;
            this.dgvTheThuVien.RowHeadersVisible = false;
            this.dgvTheThuVien.RowHeadersWidth = 51;
            this.dgvTheThuVien.RowTemplate.Height = 35;
            this.dgvTheThuVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTheThuVien.Size = new System.Drawing.Size(1000, 420);
            this.dgvTheThuVien.TabIndex = 3;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(20, 55);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 30);
            this.txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(346, 47);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(137, 30);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(20, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(90, 30);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(120, 10);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(60, 30);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(190, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(60, 30);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.Location = new System.Drawing.Point(260, 10);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(80, 30);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Làm mới";
            this.btnReload.UseVisualStyleBackColor = false;
            // 
            // btnTaoQR
            // 
            this.btnTaoQR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnTaoQR.FlatAppearance.BorderSize = 0;
            this.btnTaoQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoQR.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTaoQR.ForeColor = System.Drawing.Color.White;
            this.btnTaoQR.Location = new System.Drawing.Point(350, 10);
            this.btnTaoQR.Name = "btnTaoQR";
            this.btnTaoQR.Size = new System.Drawing.Size(133, 30);
            this.btnTaoQR.TabIndex = 4;
            this.btnTaoQR.Text = "Tạo QR";
            this.btnTaoQR.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(154)))), ((int)(((byte)(79)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ THẺ THƯ VIỆN";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.txtTimKiem);
            this.panelTop.Controls.Add(this.btnTimKiem);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 80);
            this.panelTop.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnThem);
            this.panelButtons.Controls.Add(this.btnSua);
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Controls.Add(this.btnReload);
            this.panelButtons.Controls.Add(this.btnTaoQR);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 80);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1000, 50);
            this.panelButtons.TabIndex = 2;
            // 
            // FormManageTheThuVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.dgvTheThuVien);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormManageTheThuVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Thẻ Thư Viện";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheThuVien)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
