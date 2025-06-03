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
            LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

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

        public DataTable LayThuThuSorted()
        {
            LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

            var thuThus = from tt in qltvEntity.ThuThus
                          orderby tt.TrangThai descending, tt.TenThuThu ascending
                          select new
                          {
                              tt.MaThuThu,
                              tt.TenThuThu,
                              tt.Email,
                              tt.SoDienThoai,
                              tt.DiaChi,
                              tt.NgaySinh,
                              tt.NgayBatDauLam,
                              tt.GioiTinh,
                              tt.TrangThai
                          };

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã thủ thư");
            dt.Columns.Add("Tên thủ thư");
            dt.Columns.Add("Email");
            dt.Columns.Add("Số điện thoại");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Ngày sinh");
            dt.Columns.Add("Ngày bắt đầu làm");
            dt.Columns.Add("Giới tính");
            dt.Columns.Add("Trạng thái");

            foreach (var tt in thuThus)
            {
                dt.Rows.Add(
                    tt.MaThuThu,
                    tt.TenThuThu,
                    tt.Email ?? "Chưa cập nhật",
                    tt.SoDienThoai ?? "Chưa cập nhật",
                    tt.DiaChi ?? "Chưa cập nhật",
                    tt.NgaySinh?.ToString("dd/MM/yyyy") ?? "",
                    tt.NgayBatDauLam?.ToString("dd/MM/yyyy") ?? "",
                    ConvertGioiTinh(tt.GioiTinh),
                    tt.TrangThai == true ? "Hoạt động" : "Ngừng hoạt động"
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
                LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

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
                LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

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
                LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

                var thuThuQuery = (from tt in qltvEntity.ThuThus
                                   where tt.MaThuThu == maThuThu
                                   select tt).SingleOrDefault();

                if (thuThuQuery != null)
                {
                    // Soft delete - toggle trạng thái
                    thuThuQuery.TrangThai = !thuThuQuery.TrangThai;
                    thuThuQuery.NgayCapNhat = DateTime.Now;
                    qltvEntity.SaveChanges();
                }

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
            LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

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

        public DataTable TimKiemThuThuSorted(string tuKhoa)
        {
            LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

            var thuThus = from tt in qltvEntity.ThuThus
                          where tt.TenThuThu.Contains(tuKhoa) ||
                                tt.Email.Contains(tuKhoa) ||
                                tt.SoDienThoai.Contains(tuKhoa)
                          orderby tt.TrangThai descending, tt.TenThuThu ascending
                          select new
                          {
                              tt.MaThuThu,
                              tt.TenThuThu,
                              tt.Email,
                              tt.SoDienThoai,
                              tt.DiaChi,
                              tt.NgaySinh,
                              tt.NgayBatDauLam,
                              tt.GioiTinh,
                              tt.TrangThai
                          };

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã thủ thư");
            dt.Columns.Add("Tên thủ thư");
            dt.Columns.Add("Email");
            dt.Columns.Add("Số điện thoại");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Ngày sinh");
            dt.Columns.Add("Ngày bắt đầu làm");
            dt.Columns.Add("Giới tính");
            dt.Columns.Add("Trạng thái");

            foreach (var tt in thuThus)
            {
                dt.Rows.Add(
                    tt.MaThuThu,
                    tt.TenThuThu,
                    tt.Email ?? "Chưa cập nhật",
                    tt.SoDienThoai ?? "Chưa cập nhật",
                    tt.DiaChi ?? "Chưa cập nhật",
                    tt.NgaySinh?.ToString("dd/MM/yyyy") ?? "",
                    tt.NgayBatDauLam?.ToString("dd/MM/yyyy") ?? "",
                    ConvertGioiTinh(tt.GioiTinh),
                    tt.TrangThai == true ? "Hoạt động" : "Ngừng hoạt động"
                );
            }
            return dt;
        }

        // Lấy thông tin thủ thư theo mã
        public ThuThu LayThuThuTheoMa(int maThuThu)
        {
            LibraryManagement1Entities qltvEntity = new LibraryManagement1Entities();

            return qltvEntity.ThuThus.FirstOrDefault(tt => tt.MaThuThu == maThuThu);
        }

        private string ConvertGioiTinh(string gioiTinh)
        {
            if (string.IsNullOrEmpty(gioiTinh))
                return "Chưa xác định";

            switch (gioiTinh.ToUpper())
            {
                case "M":
                case "MALE":
                    return "Nam";
                case "F":
                case "FEMALE":
                    return "Nữ";
                case "NAM":
                    return "Nam";
                case "NỮ":
                case "NU":
                    return "Nữ";
                default:
                    return gioiTinh;
            }
        }

    }
}
