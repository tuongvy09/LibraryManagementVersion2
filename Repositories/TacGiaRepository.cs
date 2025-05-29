using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementVersion2.Repositories
{
    public class TacGiaRepository
    {
        public void AddTacGia(string tenTG)
        {
            using (var context = new LibraryEntities())
            {
                try
                {
                    var tacGia = new TacGia
                    {
                        TenTG = tenTG
                    };
                    context.TacGias.Add(tacGia);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm Tác giả: " + ex.Message);
                }
            }
        }

        public void UpdateTacGia(int maTacGia, string tenTG)
        {
            using (var context = new LibraryEntities())
            {
                try
                {
                    var tacGia = context.TacGias.Find(maTacGia);
                    if (tacGia != null)
                    {
                        tacGia.TenTG = tenTG;
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy tác giả với mã: {maTacGia}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật Tác giả: " + ex.Message);
                }
            }
        }

        public void DeleteTacGia(int maTacGia)
        {
            using (var context = new LibraryEntities())
            {
                try
                {
                    var tacGia = context.TacGias.Include("DauSaches").FirstOrDefault(tg => tg.MaTacGia == maTacGia);

                    if (tacGia != null)
                    {
                        tacGia.DauSaches.Clear();

                        context.TacGias.Remove(tacGia);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy tác giả với mã: {maTacGia}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa Tác giả: " + ex.Message);
                }
            }
        }

        public List<TacGia> GetAllTacGia()
        {
            using (var context = new LibraryEntities())
            {
                return context.TacGias.ToList();
            }
        }

        public List<TacGia> GetTacGias(string searchTerm = "")
        {
            using (var context = new LibraryEntities())
            {
                var query = context.TacGias.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(tg => tg.TenTG.Contains(searchTerm));
                }

                return query.ToList();
            }
        }

    }
}
