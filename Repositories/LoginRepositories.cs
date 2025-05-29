using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementVersion2.Repositories
{
    public class LoginRepositories
    {
        public bool Login(string username, string password, out string role)
        {
            role = null;

            using (LibraryEntities db = new LibraryEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    role = user.Role;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
