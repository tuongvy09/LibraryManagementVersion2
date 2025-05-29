using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementVersion2.Repositories
{
    public class NXBRepository
    {
        public void AddNXB(string tenNXB)
        {
            using (var context = new LibraryEntities())
            {
                var nxb = new NXB
                {
                    TenNSB = tenNXB
                };

                context.NXBs.Add(nxb);
                context.SaveChanges();
            }
        }

        public void UpdateNXB(int maNXB, string tenNXB)
        {
            using (var context = new LibraryEntities())
            {
                var nxb = context.NXBs.Find(maNXB);
                if (nxb == null)
                    throw new Exception("NXB không tồn tại");

                nxb.TenNSB = tenNXB;
                context.SaveChanges();
            }
        }

        public void DeleteNXB(int maNXB)
        {
            using (var context = new LibraryEntities())
            {
                var nxb = context.NXBs.Find(maNXB);
                if (nxb == null)
                    throw new Exception("NXB không tồn tại");

                context.NXBs.Remove(nxb);
                context.SaveChanges();
            }
        }

        public List<NXB> SearchNXB(string tenNXB)
        {
            using (var context = new LibraryEntities())
            {
                var result = context.NXBs
                    .Where(nxb => nxb.TenNSB.Contains(tenNXB))
                    .ToList();

                return result;
            }
        }


        public List<NXB> GetAllNXB()
        {
            using (var context = new LibraryEntities())
            {
                return context.NXBs.ToList();
            }
        }
    }
}
