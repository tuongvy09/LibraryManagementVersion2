using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementVersion2.Repositories
{
    public class BLDauSach
    {
        public List<DauSach> GetAllMaDauSach()
        {
            using (var context = new LibraryManagement1Entities())
            {
                return context.DauSaches.ToList();
            }
        }

        public int AddDauSach(string tenDauSach, int maTheLoai, int maNXB, int? namXuatBan, decimal? giaTien, int? soTrang, string ngonNgu, string mota)
        {
            using (var context = new LibraryManagement1Entities())
            {
                var newDauSach = new DauSach
                {
                    TenDauSach = tenDauSach,
                    MaTheLoai = maTheLoai,
                    MaNXB = maNXB,
                    NamXuatBan = namXuatBan,
                    GiaTien = giaTien,
                    SoTrang = soTrang,
                    NgonNgu = ngonNgu,
                    MoTa = mota
                };

                context.DauSaches.Add(newDauSach);
                context.SaveChanges();

                return newDauSach.MaDauSach;
            }
        }

        public void UpdateDauSach(int maDauSach, int maTheLoai, int maNXB, int? namXuatBan, decimal? giaTien, int? soTrang, string ngonNgu, string mota)
        {
            using (var context = new LibraryManagement1Entities())
            {
                var dauSach = context.DauSaches.SingleOrDefault(ds => ds.MaDauSach == maDauSach);
                if (dauSach != null)
                {
                    dauSach.MaTheLoai = maTheLoai;
                    dauSach.MaNXB = maNXB;
                    dauSach.NamXuatBan = namXuatBan;
                    dauSach.GiaTien = giaTien;
                    dauSach.SoTrang = soTrang;
                    dauSach.NgonNgu = ngonNgu;
                    dauSach.MoTa = mota;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Không tìm thấy DauSach với MaDauSach = {maDauSach}");
                }
            }
        }

        public void DeleteDauSach(int maDauSach)
        {
            using (var context = new LibraryManagement1Entities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var dauSach = context.DauSaches
                                            .Include(ds => ds.TacGias) 
                                            .SingleOrDefault(ds => ds.MaDauSach == maDauSach);

                        if (dauSach == null)
                            throw new Exception($"Không tìm thấy DauSach với MaDauSach = {maDauSach}");
                        dauSach.TacGias.Clear();

                        context.DauSaches.Remove(dauSach);

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<DauSachDTO> SearchDauSach(string keyword)
        {
            using (var context = new LibraryManagement1Entities())
            {
                // Load đầy đủ dữ liệu liên quan trước khi xử lý
                var dsList = context.DauSaches
                    .Include(ds => ds.TheLoai)
                    .Include(ds => ds.NXB)
                    .Include(ds => ds.TacGias)
                    .Where(ds => ds.TenDauSach.Contains(keyword))
                    .ToList();

                var list = dsList.Select(ds => new DauSachDTO
                {
                    MaDauSach = ds.MaDauSach,
                    TenDauSach = ds.TenDauSach,
                    MaTheLoai = ds.MaTheLoai,
                    TenTheLoai = ds.TheLoai != null ? ds.TheLoai.TenTheLoai : "",
                    MaNXB = ds.MaNXB,
                    TenNXB = ds.NXB != null ? ds.NXB.TenNSB : "",
                    NamXuatBan = ds.NamXuatBan,
                    GiaTien = ds.GiaTien,
                    SoTrang = ds.SoTrang,
                    NgonNgu = ds.NgonNgu,
                    MoTa = ds.MoTa,
                    TacGia = ds.TacGias != null && ds.TacGias.Any()
                             ? string.Join(", ", ds.TacGias.Select(tg => tg.TenTG))
                             : ""
                }).OrderBy(ds => ds.TenDauSach).ToList();

                return list;
            }
        }

        public List<DauSachDTO> GetAllDauSachFullInfo()
        {
            using (var context = new LibraryManagement1Entities())
            {
                var dsList = context.DauSaches
    .Include(ds => ds.TheLoai)
    .Include(ds => ds.NXB)
    .ToList();

                var list = dsList.Select(ds => new DauSachDTO
                {
                    MaDauSach = ds.MaDauSach,
                    TenDauSach = ds.TenDauSach,
                    MaTheLoai = ds.MaTheLoai,
                    TenTheLoai = ds.TheLoai != null ? ds.TheLoai.TenTheLoai : "",
                    MaNXB = ds.MaNXB,
                    TenNXB = ds.NXB != null ? ds.NXB.TenNSB : "",
                    NamXuatBan = ds.NamXuatBan,
                    GiaTien = ds.GiaTien,
                    SoTrang = ds.SoTrang,
                    NgonNgu = ds.NgonNgu,
                    MoTa = ds.MoTa,
                    TacGia = ds.TacGias != null && ds.TacGias.Any() ? string.Join(", ", ds.TacGias.Select(tg => tg.TenTG)) : ""
                }).OrderBy(ds => ds.TenDauSach).ToList();
                return list;
            }
        }

        public class DauSachDTO
        {
            public int MaDauSach { get; set; }
            public string TenDauSach { get; set; }
            public int MaTheLoai { get; set; }
            public string TenTheLoai { get; set; }
            public int? MaNXB { get; set; }
            public string TenNXB { get; set; }
            public int? NamXuatBan { get; set; }
            public decimal? GiaTien { get; set; }
            public int? SoTrang { get; set; }
            public string NgonNgu { get; set; }
            public string MoTa { get; set; }
            public string TacGia { get; set; }  // string list TacGia gộp lại
        }
    }
}
