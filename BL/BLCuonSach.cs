using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LibraryManagementVersion2.Repositories
{
    public class BLCuonSach
    {
        public void AddCuonSach(int maDauSach, string trangThaiSach, string tenCuonSach)
        {
            using (var context = new LibraryEntities())
            {
                CuonSach newCuonSach = new CuonSach
                {
                    MaDauSach = maDauSach,
                    TrangThaiSach = trangThaiSach,
                    TenCuonSach = tenCuonSach
                };

                context.CuonSaches.Add(newCuonSach);
                context.SaveChanges();
            }
        }

        public void UpdateCuonSach(
            int maCuonSach,
            string tenDauSach,
            int maDauSach,
            int maTheLoai,
            int maNXB,
            string trangThai,
            List<int> danhSachMaTacGia)
            {
            using (var context = new LibraryEntities())
            {
                // 1. Lấy cuốn sách theo mã
                var cuonSach = context.CuonSaches
                    .Include("DauSach")
                    .FirstOrDefault(cs => cs.MaCuonSach == maCuonSach);

                if (cuonSach == null)
                {
                    throw new Exception("Không tìm thấy cuốn sách.");
                }

                var dauSach = cuonSach.DauSach;

                if (dauSach == null)
                {
                    throw new Exception("Không tìm thấy đầu sách.");
                }

                // 2. Cập nhật thông tin đầu sách
                dauSach.TenDauSach = tenDauSach;
                dauSach.MaTheLoai = maTheLoai;
                dauSach.MaNXB = maNXB;

                // 3. Cập nhật trạng thái cuốn sách
                cuonSach.TrangThaiSach = trangThai;

                // 4. Cập nhật danh sách tác giả (quan hệ nhiều-nhiều)
                // Xóa hết tác giả hiện tại
                dauSach.TacGias.Clear();

                // Lấy danh sách tác giả mới từ CSDL và gán lại
                var tacGiasMoi = context.TacGias
                    .Where(tg => danhSachMaTacGia.Contains(tg.MaTacGia))
                    .ToList();

                foreach (var tg in tacGiasMoi)
                {
                    dauSach.TacGias.Add(tg);
                }

                // 5. Lưu thay đổi
                context.SaveChanges();
            }
        }

        private void UpdateTacGia_DauSach(int maDauSach, List<int> danhSachMaTacGia)
        {
            using (var context = new LibraryEntities())
            {
                var dauSach = context.DauSaches
                    .Include("TacGias")
                    .FirstOrDefault(ds => ds.MaDauSach == maDauSach);

                if (dauSach == null)
                {
                    throw new Exception("Không tìm thấy đầu sách.");
                }

                dauSach.TacGias.Clear();

                var tacGiasMoi = context.TacGias
                    .Where(tg => danhSachMaTacGia.Contains(tg.MaTacGia))
                    .ToList();

                foreach (var tacGia in tacGiasMoi)
                {
                    dauSach.TacGias.Add(tacGia);
                }

                context.SaveChanges();
            }
        }

        public void DeleteCuonSach(int maCuonSach)
        {   
            using (var context = new LibraryEntities())
            {
                var cuonSach = context.CuonSaches
                    .Include(cs => cs.PhieuMuonSaches) // Phải include để EF tải danh sách phiếu mượn liên quan
                    .FirstOrDefault(cs => cs.MaCuonSach == maCuonSach);

                if (cuonSach == null)
                {
                    throw new Exception("Không tìm thấy cuốn sách cần xóa.");
                }

                // Kiểm tra nếu cuốn sách có phiếu mượn thì không cho xóa
                if (cuonSach.PhieuMuonSaches.Any())
                {
                    throw new Exception("Cuốn sách này đang được sử dụng trong phiếu mượn và không thể xóa.");
                }

                context.CuonSaches.Remove(cuonSach);
                context.SaveChanges();
            }
        }

        public List<CuonSachDetailModel> GetAllCuonSachDetails()
        {
            using (var context = new LibraryEntities())
            {
                var result = context.CuonSaches
                    .Include("DauSach")
                    .Include("DauSach.TheLoai")
                    .Include("DauSach.NXB")
                    .Include("DauSach.TacGias")
                    .Select(cs => new CuonSachDetailModel
                    {
                        MaCuonSach = cs.MaCuonSach,
                        TenCuonSach = cs.TenCuonSach,
                        TrangThaiSach = cs.TrangThaiSach,
                        MaDauSach = cs.DauSach.MaDauSach,
                        TenDauSach = cs.DauSach.TenDauSach,
                        TenTheLoai = cs.DauSach.TheLoai.TenTheLoai,
                        QDSoTuoi = cs.DauSach.TheLoai.QDSoTuoi ?? 0,
                        TenNSB = cs.DauSach.NXB.TenNSB,
                        TacGias = cs.DauSach.TacGias
                                    .Select(tg => tg.TenTG)
                                    .ToList()
                    })
                    .ToList();

                return result;
            }
        }

        public List<CuonSachDetailModel> SearchCuonSach(string keyword)
        {
            using (var context = new LibraryEntities())
            {
                var query = context.CuonSaches
                    .Where(cs => cs.TenCuonSach.Contains(keyword) ||
                                 cs.DauSach.TacGias.Any(tg => tg.TenTG.Contains(keyword)) ||
                                 (cs.DauSach.NXB != null && cs.DauSach.NXB.TenNSB.Contains(keyword)))
                    .Select(cs => new
                    {
                        cs.MaCuonSach,
                        cs.TenCuonSach,
                        cs.TrangThaiSach,
                        MaDauSach = cs.DauSach.MaDauSach,
                        TenTheLoai = cs.DauSach.TheLoai.TenTheLoai,
                        QDSoTuoi = cs.DauSach.TheLoai.QDSoTuoi ?? 0,
                        TenNSB = cs.DauSach.NXB.TenNSB,
                        TenDauSach = cs.DauSach.TenDauSach,
                        TacGias = cs.DauSach.TacGias.Select(tg => tg.TenTG).ToList()
                    })
                    .ToList();

                var result = query.Select(x => new CuonSachDetailModel
                {
                    MaCuonSach = x.MaCuonSach,
                    TenCuonSach = x.TenCuonSach,
                    TrangThaiSach = x.TrangThaiSach,
                    MaDauSach = x.MaDauSach, 
                    TenTheLoai = x.TenTheLoai,
                    QDSoTuoi = x.QDSoTuoi,
                    TenNSB = x.TenNSB,
                    TenDauSach = x.TenDauSach,
                    TacGias = x.TacGias
                }).ToList();

                return result;
            }
        }

        public CuonSachDetailModel GetCuonSachById(int maCuonSach)
        {
            using (var context = new LibraryEntities())
            {
                var cs = context.CuonSaches
                    .Where(c => c.MaCuonSach == maCuonSach)
                    .Select(c => new
                    {
                        c.TenCuonSach,
                        c.TrangThaiSach,
                        c.DauSach.TenDauSach,
                        c.DauSach.MaDauSach,
                        TenTheLoai = c.DauSach.TheLoai.TenTheLoai,
                        QDSoTuoi = c.DauSach.TheLoai.QDSoTuoi ?? 0,
                        TenNSB = c.DauSach.NXB.TenNSB,
                        TacGias = c.DauSach.TacGias.Select(tg => tg.TenTG).ToList()
                    })
                    .FirstOrDefault();

                if (cs == null)
                    return null;

                return new CuonSachDetailModel
                {
                    TenCuonSach = cs.TenCuonSach,
                    TrangThaiSach = cs.TrangThaiSach,
                    TenDauSach = cs.TenDauSach,
                    MaDauSach = cs.MaDauSach,
                    TenTheLoai = cs.TenTheLoai,
                    QDSoTuoi = cs.QDSoTuoi,
                    TenNSB = cs.TenNSB,
                    TacGias = cs.TacGias
                };
            }
        }

        public List<SachHot> GetTop10CuonSachHot()
        {
            using (var context = new LibraryEntities())
            {
                var query = context.CuonSaches
                    .Select(cs => new SachHot
                    {
                        TenCuonSach = cs.TenCuonSach,
                        TenDauSach = cs.DauSach.TenDauSach,
                        SoLuongMuon = cs.PhieuMuonSaches.Count()
                    })
                    .OrderByDescending(sh => sh.SoLuongMuon)
                    .Take(10)
                    .ToList();

                return query;
            }
        }

        public class CuonSachDetailModel
        {
            public int MaCuonSach { get; set; }
            public int MaDauSach { get; set; }
            public string TenDauSach { get; set; }
            public string TenCuonSach { get; set; }
            public string TrangThaiSach { get; set; }
            public string TenTheLoai { get; set; }
            public int QDSoTuoi { get; set; }
            public string TenNSB { get; set; }

            public List<string> TacGias { get; set; }
        }

        public class SachHot
        {
            public string TenCuonSach { get; set; }
            public string TenDauSach { get; set; }
            public int SoLuongMuon { get; set; }
        }

    }
}
