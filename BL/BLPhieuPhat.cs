using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementVersion2.BL
{
    public class BLPhieuPhat
    {
        private LibraryEntities db = new LibraryEntities();

        public List<PhieuPhatDTO> GetAllPhieuPhat()
        {
            return db.PhieuPhats
                     .Select(pp => new PhieuPhatDTO
                     {
                         MaPhieuPhat = pp.MaPhieuPhat,
                         MaDocGia = pp.MaDG ?? 0,
                         TenDocGia = pp.DocGia.HoTen,
                         DanhSachLyDo = pp.QDPs.Select(q => q.Lydo).ToList(),
                         SoTienPhat = pp.QDPs.Sum(q => q.TienPhat ?? 0)
                     }).ToList();
        }

        // Thêm phiếu phạt mới
        public bool AddPhieuPhat(PhieuPhatDTO phieuPhat, List<int> danhSachMaQDP)
        {
            try
            {
                var pp = new PhieuPhat
                {
                    MaDG = phieuPhat.MaDocGia
                };
                db.PhieuPhats.Add(pp);
                db.SaveChanges(); // Lưu để có MaPhieuPhat

                foreach (var maQDP in danhSachMaQDP)
                {
                    var qdp = db.QDPs.Find(maQDP);
                    if (qdp != null)
                        pp.QDPs.Add(qdp);
                }

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Cập nhật phiếu phạt (không cập nhật QDPs ở đây)
        public bool UpdatePhieuPhat(PhieuPhatDTO dto)
        {
            try
            {
                var pp = db.PhieuPhats.Find(dto.MaPhieuPhat);
                if (pp == null) return false;

                pp.MaDG = dto.MaDocGia;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Xóa phiếu phạt (cần clear liên kết QDPs)
        public bool DeletePhieuPhat(int maPhieuPhat)
        {
            try
            {
                var pp = db.PhieuPhats.Find(maPhieuPhat);
                if (pp == null) return false;

                pp.QDPs.Clear();
                db.PhieuPhats.Remove(pp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Tìm kiếm theo mã hoặc tên độc giả
        public List<PhieuPhatDTO> SearchPhieuPhat(string keyword)
        {
            return db.PhieuPhats
                     .Where(p => p.MaPhieuPhat.ToString().Contains(keyword) ||
                                 p.DocGia.HoTen.Contains(keyword))
                     .Select(p => new PhieuPhatDTO
                     {
                         MaPhieuPhat = p.MaPhieuPhat,
                         MaDocGia = p.MaDG ?? 0,
                         TenDocGia = p.DocGia.HoTen,
                         DanhSachLyDo = p.QDPs.Select(q => q.Lydo).ToList(),
                         SoTienPhat = p.QDPs.Sum(q => q.TienPhat ?? 0)
                     }).ToList();
        }

        // Chi tiết một phiếu phạt
        public List<PhieuPhatDTO> GetChiTietPhieuPhat(int maPhieuPhat)
        {
            return db.PhieuPhats
                     .Where(p => p.MaPhieuPhat == maPhieuPhat)
                     .Select(p => new PhieuPhatDTO
                     {
                         MaPhieuPhat = p.MaPhieuPhat,
                         MaDocGia = p.MaDG ?? 0,
                         TenDocGia = p.DocGia.HoTen,
                         DanhSachLyDo = p.QDPs.Select(q => q.Lydo).ToList(),
                         SoTienPhat = p.QDPs.Sum(q => q.TienPhat ?? 0)
                     }).ToList();
        }

        // Lấy chi tiết QDP theo phiếu
        public List<int> GetDanhSachMaQDPByMaPhieu(int maPhieuPhat)
        {
            return db.PhieuPhats
                     .Where(p => p.MaPhieuPhat == maPhieuPhat)
                     .SelectMany(p => p.QDPs.Select(q => q.MaQDP))
                     .ToList();
        }

        // Lấy phiếu phạt theo mã
        public PhieuPhatDTO GetPhieuPhatById(int maPhieuPhat)
        {
            return db.PhieuPhats
                     .Where(p => p.MaPhieuPhat == maPhieuPhat)
                     .Select(p => new PhieuPhatDTO
                     {
                         MaPhieuPhat = p.MaPhieuPhat,
                         MaDocGia = p.MaDG ?? 0,
                         TenDocGia = p.DocGia.HoTen,
                         DanhSachLyDo = p.QDPs.Select(q => q.Lydo).ToList(),
                         SoTienPhat = p.QDPs.Sum(q => q.TienPhat ?? 0)
                     }).FirstOrDefault();
        }

        // Lấy mã độc giả từ tên
        public int? GetMaDocGiaFromName(string tenDocGia)
        {
            return db.DocGias.FirstOrDefault(d => d.HoTen == tenDocGia)?.MaDocGia;
        }

        // Lấy danh sách mã QDP từ lý do
        public List<int> GetDanhSachMaQDP(string lyDo)
        {
            return db.QDPs
                     .Where(q => q.Lydo == lyDo)
                     .Select(q => q.MaQDP)
                     .ToList();
        }

        // DTO
        public class PhieuPhatDTO
        {
            public int MaPhieuPhat { get; set; }
            public int MaDocGia { get; set; }
            public string TenDocGia { get; set; }
            public List<string> DanhSachLyDo { get; set; } = new List<string>();
            public List<string> LoiViPhams { get; set; }
            public decimal SoTienPhat { get; set; }
        }
    }
}
