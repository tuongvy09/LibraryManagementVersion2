﻿using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace LibraryManagementVersion2
{
    partial class Home
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.FlowLayoutPanel menuPanel;
        private System.Windows.Forms.Panel contentPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.menuPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.sidebarPanel.SuspendLayout();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.Controls.Add(this.menuPanel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(330, 800);
            this.sidebarPanel.TabIndex = 1;
            this.sidebarPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sidebarPanel_Paint);
            // 
            // menuPanel
            // 
            this.menuPanel.AutoScroll = true;
            this.menuPanel.BackColor = System.Drawing.Color.Transparent;
            this.menuPanel.Controls.Add(this.logoPictureBox);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Padding = new System.Windows.Forms.Padding(10);
            this.menuPanel.Size = new System.Drawing.Size(330, 800);
            this.menuPanel.TabIndex = 0;
            this.menuPanel.WrapContents = false;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Location = new System.Drawing.Point(35, 30);
            this.logoPictureBox.Margin = new System.Windows.Forms.Padding(25, 20, 25, 10);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(220, 120);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 0;
            this.logoPictureBox.TabStop = false;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(330, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1551, 800);
            this.contentPanel.TabIndex = 0;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1881, 800);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "Home";
            this.Text = "Hệ thống Quản lý Thư viện - Version 2 (Entity Framework)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Home_Load);
            this.sidebarPanel.ResumeLayout(false);
            this.menuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        // Tạo logo đơn giản nếu không có ảnh
        private void CreateSimpleLogo()
        {
            try
            {
                Bitmap logo = new Bitmap(220, 120);
                using (Graphics g = Graphics.FromImage(logo))
                {
                    // Vẽ background gradient
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        new Rectangle(0, 0, 220, 120),
                        ColorTranslator.FromHtml("#5c7d3d"),
                        ColorTranslator.FromHtml("#8cbf5f"),
                        LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, 0, 0, 220, 120);
                    }

                    // Vẽ text
                    Font font = new Font("Segoe UI", 16, FontStyle.Bold);
                    string text = "THƯ VIỆN\nV2.0 EF";
                    SizeF textSize = g.MeasureString(text, font);
                    PointF textPos = new PointF((220 - textSize.Width) / 2, (120 - textSize.Height) / 2);

                    g.DrawString(text, font, Brushes.White, textPos);

                    // Vẽ border
                    g.DrawRectangle(new Pen(Color.White, 2), 1, 1, 218, 118);
                }

                logoPictureBox.Image = logo;
            }
            catch
            {
                // Fallback simple background
                logoPictureBox.BackColor = ColorTranslator.FromHtml("#5c7d3d");
            }
        }

        // Tạo menu buttons
        private void InitializeMenuButtons()
        {
            // Danh sách menu chỉ cho phần AT (bỏ role)
            List<string> menuItems = new List<string>()
            {
                "Quản lý Thủ thư",
                "Quản lý Độc giả",
                "Quản lý Thẻ thư viện",
                "Biên lai",
                "Thống kê Độc giả"
            };

            // Dictionary cho icons (sử dụng fallback nếu không có ảnh)
            Dictionary<string, Image> menuIcons = new Dictionary<string, Image>()
            {
                { "Quản lý Thủ thư", CreateDefaultIcon() },
                { "Quản lý Độc giả", CreateDefaultIcon() },
                { "Quản lý Thẻ thư viện", CreateDefaultIcon() },
                { "Biên lai", CreateDefaultIcon() },
                { "Thống kê Độc giả", CreateDefaultIcon() }
            };

            foreach (var item in menuItems)
            {
                RoundedButton btn = new RoundedButton()
                {
                    Text = item,
                    Width = 280,
                    Height = 45,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = ColorTranslator.FromHtml("#5c7d3d"),
                    Cursor = Cursors.Hand,
                    Margin = new Padding(10, 3, 10, 3),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(50, 0, 10, 0),
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText
                };

                if (menuIcons.ContainsKey(item))
                {
                    btn.Image = new Bitmap(menuIcons[item], new Size(24, 24));
                    btn.Padding = new Padding(10, 0, 10, 0);
                }

                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#8cbf5f");

                btn.MouseEnter += (s, e) => { btn.BackColor = ColorTranslator.FromHtml("#7eb957"); };
                btn.MouseLeave += (s, e) => { btn.BackColor = ColorTranslator.FromHtml("#5c7d3d"); };

                btn.Click += (s, e) => { ShowContent(item); };

                this.menuPanel.Controls.Add(btn);
            }
        }

        // Tạo icon mặc định
        private Image CreateDefaultIcon()
        {
            Bitmap bmp = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.Gray, 0, 0, 24, 24);
                g.DrawRectangle(Pens.White, 1, 1, 22, 22);
            }
            return bmp;
        }

        // Vẽ nền sidebar
        private void sidebarPanel_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ gradient background (fallback nếu không có ảnh)
            using (LinearGradientBrush brush = new LinearGradientBrush(
                sidebarPanel.ClientRectangle,
                ColorTranslator.FromHtml("#3a5a2a"),
                ColorTranslator.FromHtml("#5c7d3d"),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, sidebarPanel.ClientRectangle);
            }
        }
    }

    // RoundedButton class
    public class RoundedButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, 20, 20, 180, 90);
            path.AddArc(bounds.Right - 20, bounds.Y, 20, 20, 270, 90);
            path.AddArc(bounds.Right - 20, bounds.Bottom - 20, 20, 20, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - 20, 20, 20, 90, 90);
            path.CloseAllFigures();

            this.Region = new Region(path);

            base.OnPaint(pevent);
        }
    }
}
