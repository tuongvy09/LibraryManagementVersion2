using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LibraryManagementVersion2.Repositories
{
    public class DauSachTacGiaRepository
    {
        public void AddTacGiaChoDauSach(int maDauSach, int maTacGia)
        {
            using (var context = new FinalProjectLtWinsEntities1())
            {
                var dauSach = context.DauSaches.Find(maDauSach);
                var tacGia = context.TacGias.Find(maTacGia);

                if (dauSach == null || tacGia == null)
                    throw new Exception("Đầu sách hoặc tác giả không tồn tại");

                if (!dauSach.TacGias.Contains(tacGia))
                {
                    dauSach.TacGias.Add(tacGia);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateTacGiaChoDauSach(int maDauSach, List<int> danhSachMaTacGiaMoi)
        {
            using (var context = new FinalProjectLtWinsEntities1())
            {
                var dauSach = context.DauSaches.Include(ds => ds.TacGias).FirstOrDefault(ds => ds.MaDauSach == maDauSach);

                if (dauSach == null)
                    throw new Exception("Đầu sách không tồn tại");

                // Xóa hết tác giả cũ
                dauSach.TacGias.Clear();

                // Thêm các tác giả mới (nên load từng TacGia từ DB để EF tracking)
                var tacGiasMoi = context.TacGias.Where(tg => danhSachMaTacGiaMoi.Contains(tg.MaTacGia)).ToList();

                foreach (var tg in tacGiasMoi)
                {
                    dauSach.TacGias.Add(tg);
                }

                context.SaveChanges();
            }
        }

        public List<int> LayDanhSachMaTacGiaTheoDauSach(int maDauSach)
        {
            using (var context = new FinalProjectLtWinsEntities1())
            {
                var tacGiaIds = context.DauSaches
                    .Where(ds => ds.MaDauSach == maDauSach)
                    .SelectMany(ds => ds.TacGias.Select(tg => tg.MaTacGia))
                    .ToList();

                return tacGiaIds;
            }
        }


    }
}
