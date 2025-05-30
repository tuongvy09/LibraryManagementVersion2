using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementVersion2.Repositories
{
    public class TheThuVienQRData
    {
        public int MaThe { get; set; }
        public int? MaDG { get; set; }
        public DateTime NgayCap { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string TenDocGia { get; set; }
        public string SoDT { get; set; }

        public bool TrangThaiThe
        {
            get { return NgayHetHan > DateTime.Now; }
        }
    }
    class BLTheThuVien
    {
        // Lấy danh sách tất cả thẻ thư viện
        public DataTable LayTheThuVien()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var theThuVienList = (from ttv in context.TheThuViens
                                      join docgia in context.DocGias on ttv.MaDG equals docgia.MaDocGia into dgGroup
                                      from docgia in dgGroup.DefaultIfEmpty()
                                      select new
                                      {
                                          ttv.MaThe,
                                          ttv.MaDG,
                                          ttv.NgayCap,
                                          ttv.NgayHetHan,
                                          TenDocGia = docgia != null ? docgia.HoTen : "Chưa gán",
                                          SoDT = docgia != null ? docgia.SoDT : "",
                                          TrangThai = ttv.NgayHetHan >= DateTime.Now ? "Hết hạn" : "Hết hạn"
                                      }).Take(1000).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã thẻ");
                dt.Columns.Add("Tên độc giả");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("Ngày cấp");
                dt.Columns.Add("Ngày hết hạn");
                dt.Columns.Add("Trạng thái");

                foreach (var item in theThuVienList)
                {
                    string trangThai = item.NgayHetHan > DateTime.Now ? "Còn hiệu lực" : "Hết hạn";

                    dt.Rows.Add(
                        item.MaThe,
                        item.TenDocGia ?? "Chưa gán",
                        item.SoDT ?? "",
                        item.NgayCap.ToString("dd/MM/yyyy"),
                        item.NgayHetHan.ToString("dd/MM/yyyy"),
                        trangThai
                    );
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thẻ thư viện: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Lấy danh sách độc giả cho ComboBox
        public DataTable LayDocGiaChoComboBox()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var docGiaList = context.DocGias
                    .Where(dg => dg.TrangThai == true)
                    .ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("MaDG", typeof(int));
                dt.Columns.Add("TenDocGia", typeof(string));

                foreach (var docgia in docGiaList)
                {
                    dt.Rows.Add(docgia.MaDocGia, docgia.HoTen ?? "");
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách độc giả: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Thêm thẻ thư viện mới
        public bool ThemTheThuVien(int? maDocGia, DateTime ngayCap, DateTime ngayHetHan, ref string err)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                // Validate input
                if (ngayCap > DateTime.Now)
                {
                    err = "Ngày cấp không được lớn hơn ngày hiện tại";
                    return false;
                }

                if (ngayHetHan <= ngayCap)
                {
                    err = "Ngày hết hạn phải lớn hơn ngày cấp";
                    return false;
                }

                // Kiểm tra độc giả có tồn tại không (nếu có)
                if (maDocGia.HasValue)
                {
                    var docGiaExists = context.DocGias.Any(dg => dg.MaDocGia == maDocGia.Value && dg.TrangThai == true);
                    if (!docGiaExists)
                    {
                        err = "Độc giả không tồn tại hoặc đã bị vô hiệu hóa";
                        return false;
                    }

                    // Kiểm tra độc giả đã có thẻ thư viện chưa
                    var hasCard = context.TheThuViens.Any(ttv => ttv.MaDG == maDocGia.Value);
                    if (hasCard)
                    {
                        err = "Độc giả đã có thẻ thư viện";
                        return false;
                    }
                }

                // Tạo thẻ mới
                var newTheThuVien = new TheThuVien
                {
                    MaDG = maDocGia,
                    NgayCap = ngayCap,
                    NgayHetHan = ngayHetHan
                };

                context.TheThuViens.Add(newTheThuVien);
                context.SaveChanges();

                // Cập nhật MaThe trong bảng DocGia nếu có
                if (maDocGia.HasValue)
                {
                    var docGia = context.DocGias.FirstOrDefault(dg => dg.MaDocGia == maDocGia.Value);
                    if (docGia != null)
                    {
                        docGia.MaThe = newTheThuVien.MaThe;
                        docGia.NgayCapNhat = DateTime.Now;
                        context.SaveChanges();
                    }
                }

                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var validationErrors = string.Join("; ",
                    ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors)
                    .Select(e => e.PropertyName + ": " + e.ErrorMessage));
                err = "Lỗi validation: " + validationErrors;
                return false;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx)
                {
                    switch (sqlEx.Number)
                    {
                        case 2627:
                        case 2601:
                            err = "Dữ liệu đã tồn tại (trùng lặp)";
                            break;
                        case 547:
                            err = "Vi phạm ràng buộc khóa ngoại";
                            break;
                        default:
                            err = "Lỗi cập nhật database: " + sqlEx.Message;
                            break;
                    }
                }
                else
                {
                    err = "Lỗi cập nhật database: " + ex.InnerException?.Message ?? ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                err = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Cập nhật thẻ thư viện
        // Cập nhật thẻ thư viện
        public bool CapNhatTheThuVien(int maThe, int? maDocGia, DateTime ngayCap, DateTime ngayHetHan, ref string err)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var theThuVienQuery = context.TheThuViens.FirstOrDefault(ttv => ttv.MaThe == maThe);

                if (theThuVienQuery == null)
                {
                    err = "Không tìm thấy thẻ thư viện với mã: " + maThe;
                    return false;
                }

                // Validate input
                if (ngayCap > DateTime.Now)
                {
                    err = "Ngày cấp không được lớn hơn ngày hiện tại";
                    return false;
                }

                if (ngayHetHan <= ngayCap)
                {
                    err = "Ngày hết hạn phải lớn hơn ngày cấp";
                    return false;
                }

                // LƯU MÃ ĐỘC GIẢ CŨ TRƯỚC KHI CẬP NHẬT
                int? oldMaDocGia = theThuVienQuery.MaDG;

                // CHỈ KIỂM TRA VALIDATION ĐỘC GIẢ KHI THỰC SỰ THAY ĐỔI ĐỘC GIẢ
                if (maDocGia != oldMaDocGia)
                {
                    // Kiểm tra độc giả có tồn tại không (nếu có và khác với độc giả cũ)
                    if (maDocGia.HasValue)
                    {
                        var docGiaExists = context.DocGias.Any(dg => dg.MaDocGia == maDocGia.Value && dg.TrangThai == true);
                        if (!docGiaExists)
                        {
                            err = "Độc giả không tồn tại hoặc đã bị vô hiệu hóa";
                            return false;
                        }

                        // Kiểm tra độc giả đã có thẻ khác chưa (trừ thẻ hiện tại)
                        var hasOtherCard = context.TheThuViens.Any(ttv => ttv.MaDG == maDocGia.Value && ttv.MaThe != maThe);
                        if (hasOtherCard)
                        {
                            err = "Độc giả đã có thẻ thư viện khác";
                            return false;
                        }
                    }
                }

                // Cập nhật thông tin (chỉ cập nhật ngày nếu không thay đổi độc giả)
                if (maDocGia == oldMaDocGia)
                {
                    // CHỈ CẬP NHẬT NGÀY, KHÔNG THAY ĐỔI ĐỘC GIẢ
                    theThuVienQuery.NgayCap = ngayCap;
                    theThuVienQuery.NgayHetHan = ngayHetHan;
                }
                else
                {
                    // CẬP NHẬT CẢ ĐỘC GIẢ VÀ NGÀY
                    theThuVienQuery.MaDG = maDocGia;
                    theThuVienQuery.NgayCap = ngayCap;
                    theThuVienQuery.NgayHetHan = ngayHetHan;

                    // Cập nhật MaThe trong bảng DocGia khi thay đổi độc giả
                    // Xóa liên kết cũ nếu có
                    if (oldMaDocGia.HasValue)
                    {
                        var oldDocGia = context.DocGias.FirstOrDefault(dg => dg.MaDocGia == oldMaDocGia.Value);
                        if (oldDocGia != null)
                        {
                            oldDocGia.MaThe = null;
                            oldDocGia.NgayCapNhat = DateTime.Now;
                        }
                    }

                    // Tạo liên kết mới nếu có
                    if (maDocGia.HasValue)
                    {
                        var newDocGia = context.DocGias.FirstOrDefault(dg => dg.MaDocGia == maDocGia.Value);
                        if (newDocGia != null)
                        {
                            newDocGia.MaThe = maThe;
                            newDocGia.NgayCapNhat = DateTime.Now;
                        }
                    }
                }

                context.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var validationErrors = string.Join("; ",
                    ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors)
                    .Select(e => e.PropertyName + ": " + e.ErrorMessage));
                err = "Lỗi validation: " + validationErrors;
                return false;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx)
                {
                    switch (sqlEx.Number)
                    {
                        case 2627:
                        case 2601:
                            err = "Dữ liệu đã tồn tại (trùng lặp)";
                            break;
                        case 547:
                            err = "Vi phạm ràng buộc khóa ngoại";
                            break;
                        default:
                            err = "Lỗi cập nhật database: " + sqlEx.Message;
                            break;
                    }
                }
                else
                {
                    err = "Lỗi cập nhật database: " + ex.InnerException?.Message ?? ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                err = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Xóa thẻ thư viện
        // Thêm vào class BLTheThuVien

        // Lấy thẻ còn hiệu lực
        public DataTable LayTheThuVienConHieuLuc()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var theThuVienList = (from ttv in context.TheThuViens
                                      join docgia in context.DocGias on ttv.MaDG equals docgia.MaDocGia into dgGroup
                                      from docgia in dgGroup.DefaultIfEmpty()
                                      where ttv.NgayHetHan > DateTime.Now // CHỈ LẤY CÒNG HIỆU LỰC
                                      select new
                                      {
                                          ttv.MaThe,
                                          ttv.MaDG,
                                          ttv.NgayCap,
                                          ttv.NgayHetHan,
                                          TenDocGia = docgia != null ? docgia.HoTen : "Chưa gán",
                                          SoDT = docgia != null ? docgia.SoDT : ""
                                      }).OrderBy(x => x.NgayHetHan).ToList();

                return ConvertToDataTable(theThuVienList, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thẻ còn hiệu lực: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Lấy thẻ hết hạn
        public DataTable LayTheThuVienHetHan()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var theThuVienList = (from ttv in context.TheThuViens
                                      join docgia in context.DocGias on ttv.MaDG equals docgia.MaDocGia into dgGroup
                                      from docgia in dgGroup.DefaultIfEmpty()
                                      where ttv.NgayHetHan <= DateTime.Now // CHỈ LẤY HẾT HẠN
                                      select new
                                      {
                                          ttv.MaThe,
                                          ttv.MaDG,
                                          ttv.NgayCap,
                                          ttv.NgayHetHan,
                                          TenDocGia = docgia != null ? docgia.HoTen : "Chưa gán",
                                          SoDT = docgia != null ? docgia.SoDT : ""
                                      }).OrderByDescending(x => x.NgayHetHan).ToList();

                return ConvertToDataTable(theThuVienList, false);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thẻ hết hạn: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Helper method để convert
        private DataTable ConvertToDataTable(dynamic theThuVienList, bool conHieuLuc)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã thẻ");
            dt.Columns.Add("Tên độc giả");
            dt.Columns.Add("Số ĐT");
            dt.Columns.Add("Ngày cấp");
            dt.Columns.Add("Ngày hết hạn");
            if (!conHieuLuc) // Chỉ hiển thị cột "Số ngày hết hạn" cho tab hết hạn
            {
                dt.Columns.Add("Hết hạn từ");
            }

            foreach (var item in theThuVienList)
            {
                if (conHieuLuc)
                {
                    dt.Rows.Add(
                        item.MaThe,
                        item.TenDocGia ?? "Chưa gán",
                        item.SoDT ?? "",
                        item.NgayCap.ToString("dd/MM/yyyy"),
                        item.NgayHetHan.ToString("dd/MM/yyyy")
                    );
                }
                else
                {
                    int soNgayHetHan = (DateTime.Now - item.NgayHetHan).Days;
                    dt.Rows.Add(
                        item.MaThe,
                        item.TenDocGia ?? "Chưa gán",
                        item.SoDT ?? "",
                        item.NgayCap.ToString("dd/MM/yyyy"),
                        item.NgayHetHan.ToString("dd/MM/yyyy"),
                        $"{soNgayHetHan} ngày"
                    );
                }
            }
            return dt;
        }

        // Tìm kiếm thẻ thư viện
        public DataTable TimKiemTheThuVien(string tuKhoa)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var theThuVienList = (from ttv in context.TheThuViens
                                      join docgia in context.DocGias on ttv.MaDG equals docgia.MaDocGia into dgGroup
                                      from docgia in dgGroup.DefaultIfEmpty()
                                      where ttv.MaThe.ToString().Contains(tuKhoa) ||
                                            (docgia != null && (docgia.HoTen.Contains(tuKhoa) ||
                                                               docgia.SoDT.Contains(tuKhoa)))
                                      select new
                                      {
                                          ttv.MaThe,
                                          ttv.MaDG,
                                          ttv.NgayCap,
                                          ttv.NgayHetHan,
                                          TenDocGia = docgia != null ? docgia.HoTen : "Chưa gán",
                                          SoDT = docgia != null ? docgia.SoDT : ""
                                      }).Take(500).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã thẻ");
                dt.Columns.Add("Tên độc giả");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("Ngày cấp");
                dt.Columns.Add("Ngày hết hạn");
                dt.Columns.Add("Trạng thái");

                foreach (var item in theThuVienList)
                {
                    string trangThai = item.NgayHetHan >= DateTime.Now ? "Còn hiệu lực" : "Hết hạn";

                    dt.Rows.Add(
                        item.MaThe,
                        item.TenDocGia ?? "Chưa gán",
                        item.SoDT ?? "",
                        item.NgayCap.ToString("dd/MM/yyyy"),
                        item.NgayHetHan.ToString("dd/MM/yyyy"),
                        trangThai
                    );
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm thẻ thư viện: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Lấy thông tin thẻ thư viện theo mã
        public TheThuVien LayTheThuVienTheoMa(int maThe)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();
                return context.TheThuViens.FirstOrDefault(ttv => ttv.MaThe == maThe);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin thẻ thư viện: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Thêm method mới vào class BLTheThuVien
        public DataTable LayDocGiaChuaCoThe()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                // Lấy các độc giả chưa có thẻ thư viện (MaThe = null)
                var docGiaList = context.DocGias
                    .Where(dg => dg.TrangThai == true && dg.MaThe == null) // Chưa có thẻ
                    .OrderBy(dg => dg.HoTen)
                    .ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("MaDG", typeof(int));
                dt.Columns.Add("TenDocGia", typeof(string));

                foreach (var docgia in docGiaList)
                {
                    dt.Rows.Add(docgia.MaDocGia, docgia.HoTen ?? "");
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách độc giả chưa có thẻ: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Thêm method lấy thông tin độc giả theo mã
        public DocGia LayDocGiaTheoMa(int maDocGia)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();
                return context.DocGias.FirstOrDefault(dg => dg.MaDocGia == maDocGia);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin độc giả: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        public TheThuVienQRData LayTheThuVienDayDuTheoMa(int maThe)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                // SQL JOIN 2 bảng TheThuVien và DocGia
                var theThuVienInfo = (from tt in context.TheThuViens
                                      join dg in context.DocGias on tt.MaDG equals dg.MaDocGia into dgGroup
                                      from dg in dgGroup.DefaultIfEmpty()
                                      where tt.MaThe == maThe
                                      select new
                                      {
                                          tt.MaThe,
                                          tt.MaDG,
                                          tt.NgayCap,
                                          tt.NgayHetHan,
                                          TenDocGia = dg != null ? dg.HoTen : "Chưa gán",
                                          SoDT = dg != null ? dg.SoDT : ""
                                      }).FirstOrDefault();

                if (theThuVienInfo != null)
                {
                    return new TheThuVienQRData
                    {
                        MaThe = theThuVienInfo.MaThe,
                        MaDG = theThuVienInfo.MaDG,
                        NgayCap = theThuVienInfo.NgayCap,
                        NgayHetHan = theThuVienInfo.NgayHetHan,
                        TenDocGia = theThuVienInfo.TenDocGia,
                        SoDT = theThuVienInfo.SoDT
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin đầy đủ thẻ thư viện: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Method để insert thẻ và return ID (cần cho QR generation sau khi add)
        public int ThemTheThuVienVaLayMaThe(int? maDocGia, DateTime ngayCap, DateTime ngayHetHan, ref string err)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                // Validate input
                if (ngayCap > DateTime.Now)
                {
                    err = "Ngày cấp không được lớn hơn ngày hiện tại";
                    return 0;
                }

                if (ngayHetHan <= ngayCap)
                {
                    err = "Ngày hết hạn phải lớn hơn ngày cấp";
                    return 0;
                }

                // Kiểm tra độc giả có tồn tại không (nếu có)
                if (maDocGia.HasValue)
                {
                    var docGiaExists = context.DocGias.Any(dg => dg.MaDocGia == maDocGia.Value && dg.TrangThai == true);
                    if (!docGiaExists)
                    {
                        err = "Độc giả không tồn tại hoặc đã bị vô hiệu hóa";
                        return 0;
                    }

                    // Kiểm tra độc giả đã có thẻ thư viện chưa
                    var hasCard = context.TheThuViens.Any(ttv => ttv.MaDG == maDocGia.Value);
                    if (hasCard)
                    {
                        err = "Độc giả đã có thẻ thư viện";
                        return 0;
                    }
                }

                // Tạo thẻ mới
                var newTheThuVien = new TheThuVien
                {
                    MaDG = maDocGia,
                    NgayCap = ngayCap,
                    NgayHetHan = ngayHetHan
                };

                context.TheThuViens.Add(newTheThuVien);
                context.SaveChanges();

                // Cập nhật MaThe trong bảng DocGia nếu có
                if (maDocGia.HasValue)
                {
                    var docGia = context.DocGias.FirstOrDefault(dg => dg.MaDocGia == maDocGia.Value);
                    if (docGia != null)
                    {
                        docGia.MaThe = newTheThuVien.MaThe;
                        docGia.NgayCapNhat = DateTime.Now;
                        context.SaveChanges();
                    }
                }

                return newTheThuVien.MaThe; // Return ID của thẻ vừa tạo
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var validationErrors = string.Join("; ",
                    ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors)
                    .Select(e => e.PropertyName + ": " + e.ErrorMessage));
                err = "Lỗi validation: " + validationErrors;
                return 0;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx)
                {
                    switch (sqlEx.Number)
                    {
                        case 2627:
                        case 2601:
                            err = "Dữ liệu đã tồn tại (trùng lặp)";
                            break;
                        case 547:
                            err = "Vi phạm ràng buộc khóa ngoại";
                            break;
                        default:
                            err = "Lỗi cập nhật database: " + sqlEx.Message;
                            break;
                    }
                }
                else
                {
                    err = "Lỗi cập nhật database: " + ex.InnerException?.Message ?? ex.Message;
                }
                return 0;
            }
            catch (Exception ex)
            {
                err = "Lỗi hệ thống: " + ex.Message;
                return 0;
            }
            finally
            {
                context?.Dispose();
            }
        }
    }
}
