using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace LibraryManagementVersion2.Repositories
{
    public class TheLoaiRepository
    {
        public void AddTheLoai(int qdSoTuoi, string tenTheLoai)
        {
            using (var context = new LibraryEntities())
            {
                try
                {
                    var theLoai = new TheLoai
                    {
                        QDSoTuoi = qdSoTuoi,
                        TenTheLoai = tenTheLoai
                    };

                    context.TheLoais.Add(theLoai);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể thêm thể loại.\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void UpdateTheLoai(int maTheLoai, int qdSoTuoi)
        {
            using (var context = new LibraryEntities())
            {
                try
                {
                    var theLoai = context.TheLoais.Find(maTheLoai);
                    if (theLoai != null)
                    {
                        theLoai.QDSoTuoi = qdSoTuoi;
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy thể loại với mã: {maTheLoai}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật thể loại: {ex.Message}");
                }
            }
        }
        public bool DeleteTheLoai(int maTheLoai, out string errorMessage)
        {
            errorMessage = "";
            using (var context = new LibraryEntities())
            {
                try
                {
                    var theLoai = context.TheLoais.Include(t => t.DauSaches).FirstOrDefault(t => t.MaTheLoai == maTheLoai);

                    if (theLoai == null)
                    {
                        errorMessage = $"Không tìm thấy thể loại với mã: {maTheLoai}";
                        return false;
                    }

                    if (theLoai.DauSaches.Any())
                    {
                        errorMessage = "Thể loại này đang được sử dụng trong danh sách đầu sách, không thể xóa.";
                        return false;
                    }

                    context.TheLoais.Remove(theLoai);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = $"Lỗi khi xóa thể loại: {ex.Message}";
                    return false;
                }
            }
        }
        public List<TheLoai> SearchTheLoai(string keyword)
        {
            using (var context = new LibraryEntities())
            {
                var query = context.TheLoais.AsQueryable();

                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(tl => tl.TenTheLoai.Contains(keyword));
                }

                return query.ToList();
            }
        }
        public List<TheLoai> GetAllTheLoai()
        {
            using (var context = new LibraryEntities())
            {
                return context.TheLoais.ToList();
            }
        }

    }
}
