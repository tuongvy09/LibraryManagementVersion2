using LibraryManagementVersion2;
using System;
using System.Data;
using System.Linq;

namespace ThuVien_EF.BS_Layer
{
    public class BLThongKe
    {
        public DataTable GetThongKeDocGiaTheoThang(int nam)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Thang", typeof(int));
            dt.Columns.Add("Nam", typeof(int));
            dt.Columns.Add("SoLuongDocGiaMoi", typeof(int));
            dt.Columns.Add("ThangNam", typeof(string));

            try
            {
                using (LibraryEntities context = new LibraryEntities())
                {
                    var allDocGia = context.DocGias.ToList();

                    var query = allDocGia
                        .Where(dg => dg.NgayDangKy?.Year == nam)
                        .GroupBy(dg => new {
                            Thang = dg.NgayDangKy?.Month,
                            Nam = dg.NgayDangKy?.Year
                        })
                        .OrderBy(g => g.Key.Thang)
                        .Select(g => new {
                            Thang = g.Key.Thang,
                            Nam = g.Key.Nam,
                            SoLuongDocGiaMoi = g.Count()
                        });

                    foreach (var item in query)
                    {
                        DataRow row = dt.NewRow();
                        row["Thang"] = item.Thang;
                        row["Nam"] = item.Nam;
                        row["SoLuongDocGiaMoi"] = item.SoLuongDocGiaMoi;
                        row["ThangNam"] = $"{item.Thang:00}/{item.Nam}";
                        dt.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetThongKeDocGiaTheoThang: {ex.Message}");
            }

            return dt;
        }

        public DataTable GetThongKeDoanhThuTheoThang(int nam)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Thang", typeof(int));
            dt.Columns.Add("Nam", typeof(int));
            dt.Columns.Add("TongDoanhThu", typeof(decimal));
            dt.Columns.Add("SoGiaoDich", typeof(int));
            dt.Columns.Add("ThangNam", typeof(string));
            dt.Columns.Add("DoanhThuDisplay", typeof(string));

            try
            {
                using (LibraryEntities context = new LibraryEntities())
                {
                    var allBienLai = context.BienLais.ToList();

                    var query = allBienLai
                        .Where(bl => bl.NgayTraTT.Year == nam && bl.TienTra > 0)
                        .GroupBy(bl => new {
                            Thang = bl.NgayTraTT.Month,
                            Nam = bl.NgayTraTT.Year
                        })
                        .OrderBy(g => g.Key.Thang)
                        .Select(g => new {
                            Thang = g.Key.Thang,
                            Nam = g.Key.Nam,
                            TongDoanhThu = g.Sum(x => x.TienTra),
                            SoGiaoDich = g.Count()
                        });

                    foreach (var item in query)
                    {
                        DataRow row = dt.NewRow();
                        row["Thang"] = item.Thang;
                        row["Nam"] = item.Nam;
                        row["TongDoanhThu"] = item.TongDoanhThu;
                        row["SoGiaoDich"] = item.SoGiaoDich;
                        row["ThangNam"] = $"{item.Thang:00}/{item.Nam}";
                        row["DoanhThuDisplay"] = item.TongDoanhThu.ToString("N0") + " VNĐ";
                        dt.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi GetThongKeDoanhThuTheoThang: {ex.Message}");
            }

            return dt;
        }
    }
}