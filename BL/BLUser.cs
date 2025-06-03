using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementVersion2.Repositories
{
    public class BLUser
    {
        public User GetUserByUsername(string username)
        {
            using (var context = new LibraryManagement1Entities())
            {
                try
                {
                    return context.Users.FirstOrDefault(u => u.Username == username);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi truy vấn User: {ex.Message}");
                    return null;
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (var context = new LibraryManagement1Entities())
            {
                try
                {
                    return context.Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi truy vấn danh sách User: {ex.Message}");
                    return new List<User>();
                }
            }
        }
        public bool AddUser(string username, string password, string role)
        {
            using (var context = new LibraryManagement1Entities())
            {
                try
                {
                    var user = new User
                    {
                        Username = username,
                        Password = password, // Có thể mã hóa trước khi lưu
                        Role = role
                    };

                    context.Users.Add(user);
                    context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm User: {ex.Message}");
                    return false;
                }
            }
        }
        public bool UpdateUser(int userId, string username, string password, string role)
        {
            using (var context = new LibraryManagement1Entities())
            {
                try
                {
                    var user = context.Users.Find(userId);
                    if (user == null)
                    {
                        MessageBox.Show("Không tìm thấy User.");
                        return false;
                    }

                    user.Username = username;
                    user.Password = password; // Có thể mã hóa
                    user.Role = role;

                    context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật User: {ex.Message}");
                    return false;
                }
            }
        }
        public bool DeleteUser(int userId)
        {
            using (var context = new LibraryManagement1Entities())
            {
                try
                {
                    var user = context.Users.Find(userId);
                    if (user == null)
                    {
                        MessageBox.Show("Không tìm thấy User.");
                        return false;
                    }

                    context.Users.Remove(user);
                    context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa User: {ex.Message}");
                    return false;
                }
            }
        }

    }
}
