using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryManagementVersion2.Forms;

namespace LibraryManagementVersion2
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Tạo logo đơn giản
            CreateSimpleLogo();

            // Khởi tạo menu buttons
            InitializeMenuButtons();

            // Hiển thị nội dung chào mừng
            ShowWelcomeContent();
        }

        private void ShowWelcomeContent()
        {
            contentPanel.Controls.Clear();

            Label lblWelcome = new Label()
            {
                Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN\n" +
                       "Version 2 - Entity Framework Database First\n\n" +
                       "📚 Chào mừng đến với hệ thống quản lý thư viện!\n" +
                       "Chọn chức năng từ menu bên trái để bắt đầu.\n\n" 
                      ,
                Font = new Font("Segoe UI", 14F, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#5c7d3d"),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            contentPanel.Controls.Add(lblWelcome);
        }

        private void ShowContent(string item)
        {
            contentPanel.Controls.Clear(); // Xóa nội dung cũ

            Form newForm = null;

            switch (item)
            {
                case "Quản lý Thủ thư":
                    newForm = new FormThuThu();
                    break;

                case "Quản lý Độc giả":
                    ShowNotImplemented("Quản lý Độc giả");
                    return;

                case "Quản lý Thẻ thư viện":
                    ShowNotImplemented("Quản lý Thẻ thư viện");
                    return;

                case "Biên lai":
                    ShowNotImplemented("Biên lai");
                    return;

                case "Thống kê Độc giả":
                    ShowNotImplemented("Thống kê Độc giả");
                    return;

                default:
                    ShowNotImplemented(item);
                    return;
            }

            if (newForm != null)
            {
                // Hiển thị form trong panel
                ShowFormInPanel(newForm);
            }
        }

        private void ShowNotImplemented(string functionName)
        {
            Label lblNotImplemented = new Label()
            {
                Text = $"🚧 CHỨC NĂNG '{functionName.ToUpper()}'\n\n" +
                       "Đang được phát triển...\n\n" +
                       "Hiện tại chỉ có 'Quản lý Thủ thư' đã hoàn thành.\n" +
                       "Các chức năng khác sẽ được bổ sung sau.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            contentPanel.Controls.Add(lblNotImplemented);
        }

        private void ShowFormInPanel(Form form)
        {
            try
            {
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.WindowState = FormWindowState.Normal;
                contentPanel.Controls.Add(form);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Fallback về welcome screen
                ShowWelcomeContent();
            }
        }
    }
}
