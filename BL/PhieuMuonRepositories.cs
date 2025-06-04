using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementVersion2.Repositories
{
    public class PhieuMuonRepositories
    {
        public void AddPhieuMuon(int maDocGia, int maNhanVien, DateTime ngayMuon, DateTime ngayHenTra, List<int> danhSachMaCuonSach)
        {
            using (var context = new LibraryEntities())
            {
                var phieuMuon = new PhieuMuon
                {
                    MaDocGia = maDocGia,
                    MaNhanVien = maNhanVien,
                    NgayMuon = ngayMuon,
                    NgayHenTra = ngayHenTra,
                    PhieuMuonSaches = danhSachMaCuonSach.Select(ma =>
                        new PhieuMuonSach { MaCuonSach = ma }).ToList()
                };

                context.PhieuMuons.Add(phieuMuon);
                context.SaveChanges();
            }
        }

        public void UpdatePhieuMuon(int maPhieuMuon, DateTime ngayHenTra, List<int> danhSachMaCuonSach)
        {
            using (var context = new LibraryEntities())
            {
                var phieuMuon = context.PhieuMuons
                    .Include("PhieuMuonSaches")
                    .FirstOrDefault(p => p.MaPhieuMuon == maPhieuMuon);

                if (phieuMuon == null)
                    throw new Exception("Không tìm thấy phiếu mượn.");

                phieuMuon.NgayHenTra = ngayHenTra;

                // Cập nhật lại danh sách cuốn sách
                phieuMuon.PhieuMuonSaches.Clear();

                foreach (int maCuonSach in danhSachMaCuonSach)
                {
                    phieuMuon.PhieuMuonSaches.Add(new PhieuMuonSach
                    {
                        MaCuonSach = maCuonSach
                    });
                }

                context.SaveChanges();
            }
        }

        public void DeletePhieuMuon(int maPhieuMuon)
        {
            using (var context = new LibraryEntities())
            {
                var phieuMuon = context.PhieuMuons
                    .Include(p => p.PhieuMuonSaches)
                    .FirstOrDefault(p => p.MaPhieuMuon == maPhieuMuon);

                if (phieuMuon == null)
                    throw new Exception("Không tìm thấy phiếu mượn.");

                context.PhieuMuonSaches.RemoveRange(phieuMuon.PhieuMuonSaches);
                context.PhieuMuons.Remove(phieuMuon);
                context.SaveChanges();
            }
        }

        public List<PhieuMuonViewModel> GetAllPhieuMuon()
        {
            using (var context = new LibraryEntities())
            {
                return context.PhieuMuons
                    .Include(p => p.DocGia)
                    .Include(p => p.NhanVien)
                    .Select(p => new PhieuMuonViewModel
                    {
                        MaPhieuMuon = p.MaPhieuMuon,
                        TenDocGia = p.DocGia.TenDocGia,
                        TenNhanVien = p.NhanVien.TenNhanVien,
                        NgayMuon = p.NgayMuon,
                        NgayHenTra = p.NgayHenTra,
                        SoLuongSach = p.PhieuMuonSaches.Count()
                    })
                    .ToList();
            }
        }

        public PhieuMuonDetailViewModel GetPhieuMuonDetail(int maPhieuMuon)
        {
            using (var context = new LibraryEntities())
            {
                var phieuMuon = context.PhieuMuons
                    .Include(p => p.PhieuMuonSaches.Select(pms => pms.CuonSach))
                    .FirstOrDefault(p => p.MaPhieuMuon == maPhieuMuon);

                if (phieuMuon == null)
                    return null;

                return new PhieuMuonDetailViewModel
                {
                    MaPhieuMuon = phieuMuon.MaPhieuMuon,
                    NgayMuon = phieuMuon.NgayMuon,
                    NgayHenTra = phieuMuon.NgayHenTra,
                    DanhSachCuonSach = phieuMuon.PhieuMuonSaches
                        .Select(pms => new CuonSachViewModel
                        {
                            MaCuonSach = pms.MaCuonSach,
                            TenCuonSach = pms.CuonSach.TenCuonSach
                        })
                        .ToList()
                };
            }
        }

        public class PhieuMuonViewModel
        {
            public int MaPhieuMuon { get; set; }
            public string TenDocGia { get; set; }
            public string TenNhanVien { get; set; }
            public DateTime NgayMuon { get; set; }
            public DateTime NgayHenTra { get; set; }
            public int SoLuongSach { get; set; }
        }

        public class PhieuMuonDetailViewModel
        {
            public int MaPhieuMuon { get; set; }
            public DateTime NgayMuon { get; set; }
            public DateTime NgayHenTra { get; set; }
            public List<CuonSachViewModel> DanhSachCuonSach { get; set; }
        }

        public class CuonSachViewModel
        {
            public int MaCuonSach { get; set; }
            public string TenCuonSach { get; set; }
        }
    }
}
