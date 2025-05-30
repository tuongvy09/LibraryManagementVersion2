using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementVersion2.Repositories
{
    class BLThuThu
    {
        // Lấy danh sách tất cả thủ thư
        public DataTable LayThuThu()
        {
            LibraryEntities qltvEntity = new LibraryEntities();

            var thuThuList = from tt in qltvEntity.ThuThus
                             select tt;

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Thủ Thư");
            dt.Columns.Add("Tên Thủ Thư");
            dt.Columns.Add("Email");
            dt.Columns.Add("Số Điện Thoại");
            dt.Columns.Add("Địa Chỉ");
            dt.Columns.Add("Ngày Bắt Đầu Làm");
            dt.Columns.Add("Ngày Sinh");
            dt.Columns.Add("Giới Tính");
            dt.Columns.Add("Trạng Thái");

            foreach (var tt in thuThuList)
            {
                dt.Rows.Add(
                    tt.MaThuThu,
                    tt.TenThuThu,
                    tt.Email,
                    tt.SoDienThoai,
                    tt.DiaChi,
                    tt.NgayBatDauLam?.ToString("dd/MM/yyyy"),
                    tt.NgaySinh?.ToString("dd/MM/yyyy"),
                    tt.GioiTinh == "M" ? "Nam" : (tt.GioiTinh == "F" ? "Nữ" : ""),
                    tt.TrangThai == true ? "Hoạt động" : "Không hoạt động"
                );
            }
            return dt;
        }

        // Thêm thủ thư mới
        public bool ThemThuThu(string tenThuThu, string email, string soDienThoai,
                              string diaChi, DateTime? ngayBatDauLam, DateTime? ngaySinh,
                              string gioiTinh, bool trangThai, ref string err)
        {
            try
            {
                LibraryEntities qltvEntity = new LibraryEntities();

                ThuThu tt = new ThuThu();
                tt.TenThuThu = tenThuThu;
                tt.Email = email;
                tt.SoDienThoai = soDienThoai;
                tt.DiaChi = diaChi;
                tt.NgayBatDauLam = ngayBatDauLam;
                tt.NgaySinh = ngaySinh;
                tt.GioiTinh = gioiTinh;
                tt.TrangThai = trangThai;
                tt.NgayTao = DateTime.Now;

                qltvEntity.ThuThus.Add(tt);
                qltvEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        // Cập nhật thông tin thủ thư
        public bool CapNhatThuThu(int maThuThu, string tenThuThu, string email,
                                 string soDienThoai, string diaChi, DateTime? ngayBatDauLam,
                                 DateTime? ngaySinh, string gioiTinh, bool trangThai, ref string err)
        {
            try
            {
                LibraryEntities qltvEntity = new LibraryEntities();

                var thuThuQuery = (from tt in qltvEntity.ThuThus
                                   where tt.MaThuThu == maThuThu
                                   select tt).SingleOrDefault();

                if (thuThuQuery != null)
                {
                    thuThuQuery.TenThuThu = tenThuThu;
                    thuThuQuery.Email = email;
                    thuThuQuery.SoDienThoai = soDienThoai;
                    thuThuQuery.DiaChi = diaChi;
                    thuThuQuery.NgayBatDauLam = ngayBatDauLam;
                    thuThuQuery.NgaySinh = ngaySinh;
                    thuThuQuery.GioiTinh = gioiTinh;
                    thuThuQuery.TrangThai = trangThai;
                    thuThuQuery.NgayCapNhat = DateTime.Now;

                    qltvEntity.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        // Xóa thủ thư
        public bool XoaThuThu(int maThuThu, ref string err)
        {
            try
            {
                LibraryEntities qltvEntity = new LibraryEntities();

                ThuThu tt = new ThuThu();
                tt.MaThuThu = maThuThu;

                qltvEntity.ThuThus.Attach(tt);
                qltvEntity.ThuThus.Remove(tt);
                qltvEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        // Tìm kiếm thủ thư theo tên
        public DataTable TimKiemThuThu(string tenThuThu)
        {   
            LibraryEntities qltvEntity = new LibraryEntities();

            var thuThuList = from tt in qltvEntity.ThuThus
                             where tt.TenThuThu.Contains(tenThuThu)
                             select tt;

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã Thủ Thư");
            dt.Columns.Add("Tên Thủ Thư");
            dt.Columns.Add("Email");
            dt.Columns.Add("Số Điện Thoại");
            dt.Columns.Add("Địa Chỉ");
            dt.Columns.Add("Ngày Bắt Đầu Làm");
            dt.Columns.Add("Ngày Sinh");
            dt.Columns.Add("Giới Tính");
            dt.Columns.Add("Trạng Thái");

            foreach (var tt in thuThuList)
            {
                dt.Rows.Add(
                    tt.MaThuThu,
                    tt.TenThuThu,
                    tt.Email,
                    tt.SoDienThoai,
                    tt.DiaChi,
                    tt.NgayBatDauLam?.ToString("dd/MM/yyyy"),
                    tt.NgaySinh?.ToString("dd/MM/yyyy"),
                    tt.GioiTinh == "M" ? "Nam" : (tt.GioiTinh == "F" ? "Nữ" : ""),
                    tt.TrangThai == true ? "Hoạt động" : "Không hoạt động"
                );
            }
            return dt;
        }

        // Lấy thông tin thủ thư theo mã
        public ThuThu LayThuThuTheoMa(int maThuThu)
        {
            LibraryEntities qltvEntity = new LibraryEntities();

            return qltvEntity.ThuThus.FirstOrDefault(tt => tt.MaThuThu == maThuThu);
        }
    }
}
