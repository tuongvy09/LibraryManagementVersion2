using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementVersion2.BL
{
    public class BLPhieuMuon
    {
        private LibraryEntities db = new LibraryEntities();

        public List<PhieuMuon> GetAllPhieuMuon()
        {
            var query = from ms in db.MuonSaches
                        join pms in db.PhieuMuonSaches on ms.MaPhieu equals pms.MaPhieu
                        join dg in db.DocGias on pms.MaDocGia equals dg.MaDocGia
                        from cs in pms.CuonSaches
                        select new PhieuMuon
                        {
                            MaMuonSach = ms.MaMuonSach,
                            TenDocGia = dg.HoTen,
                            TenCuonSach = cs.TenCuonSach,
                            NgayMuon = ms.NgayMuon,
                            NgayTra = ms.NgayTra,
                            TrangThaiM = ms.TrangThaiM,
                            GiaMuon = ms.GiaMuon ?? 0,
                            SoNgayMuon = ms.SoNgayMuon ?? 0,
                            TienCoc = ms.TienCoc ?? 0
                        };

            return query.ToList();
        }

        public bool AddPhieuMuon(PhieuMuon phieuMuon, List<int> danhSachMaCuonSach)
        {
            try
            {
                var pms = new PhieuMuonSach
                {
                    MaDocGia = phieuMuon.MaDocGia
                };
                db.PhieuMuonSaches.Add(pms);
                db.SaveChanges();

                var ms = new MuonSach
                {
                    MaPhieu = pms.MaPhieu,
                    NgayMuon = phieuMuon.NgayMuon,
                    NgayTra = phieuMuon.NgayTra,
                    TrangThaiM = phieuMuon.TrangThaiM,
                    GiaMuon = phieuMuon.GiaMuon,
                    SoNgayMuon = phieuMuon.SoNgayMuon,
                    TienCoc = phieuMuon.TienCoc
                };
                db.MuonSaches.Add(ms);
                db.SaveChanges();

                foreach (var maCuonSach in danhSachMaCuonSach)
                {
                    var cuonSach = db.CuonSaches.Find(maCuonSach);
                    if (cuonSach != null)
                    {
                        pms.CuonSaches.Add(cuonSach);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePhieuMuon(PhieuMuon phieuMuon)
        {
            try
            {
                var ms = db.MuonSaches.FirstOrDefault(m => m.MaMuonSach == phieuMuon.MaMuonSach);
                if (ms == null) return false;

                ms.NgayMuon = phieuMuon.NgayMuon;
                ms.NgayTra = phieuMuon.NgayTra;
                ms.TrangThaiM = phieuMuon.TrangThaiM;
                ms.GiaMuon = phieuMuon.GiaMuon;
                ms.SoNgayMuon = phieuMuon.SoNgayMuon;
                ms.TienCoc = phieuMuon.TienCoc;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePhieuMuon(int maMuonSach)
        {
            try
            {
                var ms = db.MuonSaches.FirstOrDefault(m => m.MaMuonSach == maMuonSach);
                if (ms == null) return false;

                var pms = db.PhieuMuonSaches.FirstOrDefault(p => p.MaPhieu == ms.MaPhieu);
                if (pms == null) return false;

                pms.CuonSaches.Clear();
                db.MuonSaches.Remove(ms);
                db.PhieuMuonSaches.Remove(pms);

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PhieuMuon> GetChiTietPhieu(int maPhieu)
        {
            var query = from ms in db.MuonSaches
                        join pms in db.PhieuMuonSaches on ms.MaPhieu equals pms.MaPhieu
                        join dg in db.DocGias on pms.MaDocGia equals dg.MaDocGia
                        from cs in pms.CuonSaches
                        where pms.MaPhieu == maPhieu
                        select new PhieuMuon
                        {
                            MaMuonSach = ms.MaMuonSach,
                            TenDocGia = dg.HoTen,
                            TenCuonSach = cs.TenCuonSach,
                            NgayMuon = ms.NgayMuon,
                            NgayTra = ms.NgayTra,
                            TrangThaiM = ms.TrangThaiM,
                            GiaMuon = ms.GiaMuon ?? 0,
                            SoNgayMuon = ms.SoNgayMuon ?? 0,
                            TienCoc = ms.TienCoc ?? 0
                        };

            return query.ToList();
        }

        public List<int> GetDanhSachMaCuonSachByMaPhieu(int maPhieu)
        {
            var pms = db.PhieuMuonSaches.FirstOrDefault(p => p.MaPhieu == maPhieu);
            return pms?.CuonSaches.Select(c => c.MaCuonSach).ToList() ?? new List<int>();
        }

        public PhieuMuon GetPhieuMuonById(int maMuonSach)
        {
            var query = from ms in db.MuonSaches
                        join pms in db.PhieuMuonSaches on ms.MaPhieu equals pms.MaPhieu
                        join dg in db.DocGias on pms.MaDocGia equals dg.MaDocGia
                        from cs in pms.CuonSaches
                        where ms.MaMuonSach == maMuonSach
                        select new PhieuMuon
                        {
                            MaMuonSach = ms.MaMuonSach,
                            TenDocGia = dg.HoTen,
                            TenCuonSach = cs.TenCuonSach,
                            NgayMuon = ms.NgayMuon,
                            NgayTra = ms.NgayTra,
                            TrangThaiM = ms.TrangThaiM,
                            GiaMuon = ms.GiaMuon ?? 0,
                            SoNgayMuon = ms.SoNgayMuon ?? 0,
                            TienCoc = ms.TienCoc ?? 0
                        };

            return query.FirstOrDefault();
        }

        public int? GetMaDocGiaFromName(string tenDocGia)
        {
            var docGia = db.DocGias.FirstOrDefault(d => d.HoTen == tenDocGia);
            return docGia?.MaDocGia;
        }

        public List<int> GetDanhSachMaCuonSach(string tenDauSach)
        {
            return db.CuonSaches
                     .Where(cs => cs.DauSach.TenDauSach == tenDauSach)
                     .Select(cs => cs.MaCuonSach)
                     .ToList();
        }

        public class PhieuMuon
        {
            public int MaMuonSach { get; set; }
            public int MaPhieu { get; set; }
            public int MaDocGia { get; set; }
            public string TenDocGia { get; set; }
            public string TenCuonSach { get; set; }
            public List<string> DanhSachTenCuonSach { get; set; }
            public DateTime NgayMuon { get; set; }
            public DateTime NgayTra { get; set; }
            public string TrangThaiM { get; set; }
            public decimal GiaMuon { get; set; }
            public int SoNgayMuon { get; set; }
            public decimal TienCoc { get; set; }
        }
    }
}
