using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementVersion2.Repositories
{
    class BLDocGia
    {
        // Lấy danh sách tất cả độc giả - FIXED VARIABLE CONFLICT
        public DataTable LayDocGia()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var docGiaList = (from docgia in context.DocGias  // Đổi từ 'dg' thành 'docgia'
                                  join loaidg in context.LoaiDocGias on docgia.MaLoaiDG equals loaidg.MaLoaiDG into ldgGroup
                                  from loaidg in ldgGroup.DefaultIfEmpty()
                                  select new
                                  {
                                      docgia.MaDocGia,
                                      docgia.HoTen,
                                      docgia.Tuoi,
                                      docgia.SoDT,
                                      docgia.CCCD,
                                      docgia.Email,
                                      docgia.DiaChi,
                                      docgia.NgayDangKy,
                                      docgia.TienNo,
                                      docgia.TrangThai,
                                      docgia.GioiTinh,
                                      TenLoaiDG = loaidg != null ? loaidg.TenLoaiDG : "Chưa phân loại"
                                  }).Take(1000).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã ĐG");
                dt.Columns.Add("Họ tên");
                dt.Columns.Add("Tuổi");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("CCCD");
                dt.Columns.Add("Email");
                dt.Columns.Add("Địa chỉ");
                dt.Columns.Add("Loại ĐG");
                dt.Columns.Add("Ngày đăng ký");
                dt.Columns.Add("Tiền nợ");
                dt.Columns.Add("Trạng thái");

                foreach (var item in docGiaList)  // Đổi từ 'dg' thành 'item'
                {
                    dt.Rows.Add(
                        item.MaDocGia,
                        item.HoTen ?? "",
                        item.Tuoi,
                        item.SoDT ?? "",
                        item.CCCD ?? "",
                        item.Email ?? "",
                        item.DiaChi ?? "",
                        item.TenLoaiDG ?? "Chưa phân loại",
                        item.NgayDangKy?.ToString("dd/MM/yyyy") ?? "",
                        (item.TienNo ?? 0).ToString("N0") + " VNĐ",
                        item.TrangThai == true ? "Hoạt động" : "Không hoạt động"
                    );
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

        // Lấy danh sách loại độc giả cho ComboBox
        public DataTable LayLoaiDocGia()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var loaiDGList = context.LoaiDocGias.ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("MaLoaiDG", typeof(int));
                dt.Columns.Add("TenLoaiDG", typeof(string));

                foreach (var loaidg in loaiDGList)  // Đổi từ 'ldg' thành 'loaidg'
                {
                    dt.Rows.Add(loaidg.MaLoaiDG, loaidg.TenLoaiDG ?? "");
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy loại độc giả: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Thêm độc giả mới
        public bool ThemDocGia(string hoTen, int tuoi, string soDT, string cccd,
                              string gioiTinh, string email, string diaChi,
                              int? maLoaiDG, bool trangThai, ref string err)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                // Validate input trước khi thêm
                if (string.IsNullOrWhiteSpace(hoTen))
                {
                    err = "Họ tên không được để trống";
                    return false;
                }

                if (tuoi <= 0 || tuoi > 150)
                {
                    err = "Tuổi phải từ 1 đến 150";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(soDT))
                {
                    err = "Số điện thoại không được để trống";
                    return false;
                }

                // Kiểm tra trùng lặp số điện thoại
                var existingByPhone = context.DocGias.FirstOrDefault(docgia => docgia.SoDT == soDT);
                if (existingByPhone != null)
                {
                    err = "Số điện thoại đã tồn tại trong hệ thống";
                    return false;
                }

                // Kiểm tra trùng lặp CCCD (nếu có)
                if (!string.IsNullOrWhiteSpace(cccd))
                {
                    var existingByCCCD = context.DocGias.FirstOrDefault(docgia => docgia.CCCD == cccd);
                    if (existingByCCCD != null)
                    {
                        err = "CCCD đã tồn tại trong hệ thống";
                        return false;
                    }
                }

                // Kiểm tra MaLoaiDG có tồn tại không (nếu có)
                if (maLoaiDG.HasValue)
                {
                    var loaiDGExists = context.LoaiDocGias.Any(loaidg => loaidg.MaLoaiDG == maLoaiDG.Value);
                    if (!loaiDGExists)
                    {
                        err = "Loại độc giả không tồn tại";
                        return false;
                    }
                }

                // Tạo độc giả mới
                var newDocGia = new DocGia
                {
                    HoTen = hoTen.Trim(),
                    Tuoi = tuoi,
                    SoDT = soDT.Trim(),
                    CCCD = string.IsNullOrWhiteSpace(cccd) ? null : cccd.Trim(),
                    GioiTinh = string.IsNullOrWhiteSpace(gioiTinh) ? null : gioiTinh,
                    Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(diaChi) ? null : diaChi.Trim(),
                    MaLoaiDG = maLoaiDG,
                    NgayDangKy = DateTime.Now,
                    TienNo = 0,
                    TrangThai = trangThai,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = null,
                    MaThe = null
                };

                context.DocGias.Add(newDocGia);
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
                            err = "Dữ liệu đã tồn tại (trùng lặp khóa chính hoặc unique constraint)";
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

        // Cập nhật thông tin độc giả
        public bool CapNhatDocGia(int maDocGia, string hoTen, int tuoi, string soDT,
                                 string cccd, string gioiTinh, string email, string diaChi,
                                 int? maLoaiDG, bool trangThai, ref string err)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var docGiaQuery = context.DocGias.FirstOrDefault(docgia => docgia.MaDocGia == maDocGia);

                if (docGiaQuery == null)
                {
                    err = "Không tìm thấy độc giả với mã: " + maDocGia;
                    return false;
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(hoTen))
                {
                    err = "Họ tên không được để trống";
                    return false;
                }

                if (tuoi <= 0 || tuoi > 150)
                {
                    err = "Tuổi phải từ 1 đến 150";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(soDT))
                {
                    err = "Số điện thoại không được để trống";
                    return false;
                }

                // Kiểm tra trùng lặp số điện thoại (trừ chính nó)
                var existingByPhone = context.DocGias.FirstOrDefault(docgia => docgia.SoDT == soDT && docgia.MaDocGia != maDocGia);
                if (existingByPhone != null)
                {
                    err = "Số điện thoại đã tồn tại trong hệ thống";
                    return false;
                }

                // Kiểm tra trùng lặp CCCD (nếu có, trừ chính nó)
                if (!string.IsNullOrWhiteSpace(cccd))
                {
                    var existingByCCCD = context.DocGias.FirstOrDefault(docgia => docgia.CCCD == cccd && docgia.MaDocGia != maDocGia);
                    if (existingByCCCD != null)
                    {
                        err = "CCCD đã tồn tại trong hệ thống";
                        return false;
                    }
                }

                // Kiểm tra MaLoaiDG có tồn tại không (nếu có)
                if (maLoaiDG.HasValue)
                {
                    var loaiDGExists = context.LoaiDocGias.Any(loaidg => loaidg.MaLoaiDG == maLoaiDG.Value);
                    if (!loaiDGExists)
                    {
                        err = "Loại độc giả không tồn tại";
                        return false;
                    }
                }

                // Cập nhật thông tin
                docGiaQuery.HoTen = hoTen.Trim();
                docGiaQuery.Tuoi = tuoi;
                docGiaQuery.SoDT = soDT.Trim();
                docGiaQuery.CCCD = string.IsNullOrWhiteSpace(cccd) ? null : cccd.Trim();
                docGiaQuery.GioiTinh = string.IsNullOrWhiteSpace(gioiTinh) ? null : gioiTinh;
                docGiaQuery.Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
                docGiaQuery.DiaChi = string.IsNullOrWhiteSpace(diaChi) ? null : diaChi.Trim();
                docGiaQuery.MaLoaiDG = maLoaiDG;
                docGiaQuery.TrangThai = trangThai;
                docGiaQuery.NgayCapNhat = DateTime.Now;

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

        // Xóa độc giả
        public bool XoaDocGia(int maDocGia, ref string err)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var docGia = context.DocGias.FirstOrDefault(docgia => docgia.MaDocGia == maDocGia);
                if (docGia == null)
                {
                    err = "Không tìm thấy độc giả với mã: " + maDocGia;
                    return false;
                }

                // Kiểm tra ràng buộc trước khi xóa
                var hasLibraryCard = context.TheThuViens.Any(ttv => ttv.MaDG == maDocGia);
                if (hasLibraryCard)
                {
                    err = "Không thể xóa độc giả vì đã có thẻ thư viện. Vui lòng xóa thẻ trước.";
                    return false;
                }

                var hasBorrowRecords = context.PhieuMuonSaches.Any(pms => pms.MaDocGia == maDocGia);
                if (hasBorrowRecords)
                {
                    err = "Không thể xóa độc giả vì đã có lịch sử mượn sách.";
                    return false;
                }

                var hasFineRecords = context.PhieuPhats.Any(pp => pp.MaDG == maDocGia);
                if (hasFineRecords)
                {
                    err = "Không thể xóa độc giả vì đã có phiếu phạt.";
                    return false;
                }

                var hasReceiptRecords = context.BienLais.Any(bl => bl.MaDocGia == maDocGia);
                if (hasReceiptRecords)
                {
                    err = "Không thể xóa độc giả vì đã có biên lai thanh toán.";
                    return false;
                }

                context.DocGias.Remove(docGia);
                context.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    err = "Không thể xóa độc giả vì đã có dữ liệu liên quan trong hệ thống.";
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

        // Tìm kiếm độc giả - FIXED VARIABLE CONFLICT
        public DataTable TimKiemDocGia(string tuKhoa)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();

                var docGiaList = (from docgia in context.DocGias  // Đổi từ 'dg' thành 'docgia'
                                  join loaidg in context.LoaiDocGias on docgia.MaLoaiDG equals loaidg.MaLoaiDG into ldgGroup
                                  from loaidg in ldgGroup.DefaultIfEmpty()
                                  where docgia.HoTen.Contains(tuKhoa) ||
                                        docgia.SoDT.Contains(tuKhoa) ||
                                        (docgia.Email != null && docgia.Email.Contains(tuKhoa)) ||
                                        (docgia.CCCD != null && docgia.CCCD.Contains(tuKhoa))
                                  select new
                                  {
                                      docgia.MaDocGia,
                                      docgia.HoTen,
                                      docgia.Tuoi,
                                      docgia.SoDT,
                                      docgia.CCCD,
                                      docgia.Email,
                                      docgia.DiaChi,
                                      docgia.NgayDangKy,
                                      docgia.TienNo,
                                      docgia.TrangThai,
                                      TenLoaiDG = loaidg != null ? loaidg.TenLoaiDG : "Chưa phân loại"
                                  }).Take(500).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã ĐG");
                dt.Columns.Add("Họ tên");
                dt.Columns.Add("Tuổi");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("CCCD");
                dt.Columns.Add("Email");
                dt.Columns.Add("Địa chỉ");
                dt.Columns.Add("Loại ĐG");
                dt.Columns.Add("Ngày đăng ký");
                dt.Columns.Add("Tiền nợ");
                dt.Columns.Add("Trạng thái");

                foreach (var item in docGiaList)  // Đổi từ 'dg' thành 'item'
                {
                    dt.Rows.Add(
                        item.MaDocGia,
                        item.HoTen ?? "",
                        item.Tuoi,
                        item.SoDT ?? "",
                        item.CCCD ?? "",
                        item.Email ?? "",
                        item.DiaChi ?? "",
                        item.TenLoaiDG ?? "Chưa phân loại",
                        item.NgayDangKy?.ToString("dd/MM/yyyy") ?? "",
                        (item.TienNo ?? 0).ToString("N0") + " VNĐ",
                        item.TrangThai == true ? "Hoạt động" : "Không hoạt động"
                    );
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm độc giả: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Lấy thông tin độc giả theo mã
        public DocGia LayDocGiaTheoMa(int maDocGia)
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();
                return context.DocGias.FirstOrDefault(docgia => docgia.MaDocGia == maDocGia);
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

        // Test connection
        public DataTable TestConnection()
        {
            LibraryEntities context = null;
            try
            {
                context = new LibraryEntities();
                var count = context.DocGias.Count();

                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                dt.Rows.Add("Kết nối thành công! Có " + count + " độc giả.");

                return dt;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Error");
                dt.Rows.Add("Lỗi: " + ex.Message);
                return dt;
            }
            finally
            {
                context?.Dispose();
            }
        }
    }
}