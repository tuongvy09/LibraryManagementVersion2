using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementVersion2;
using LibraryManagementVersion2.Repositories;

namespace LibraryManagementVersion2
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = txtTen.Text.Trim();
            string password = txtPassWord.Text.Trim();

            BLLogin loginRepo = new BLLogin();
            string role;

            if (loginRepo.Login(username, password, out role))
            {
                MessageBox.Show("Đăng nhập thành công với vai trò: " + role, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                (new Home(role)).Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Login_Load_1(object sender, EventArgs e)
        {

        }
    }
}
