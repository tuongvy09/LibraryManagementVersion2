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
        // ✅ Method gốc (giữ nguyên để tương thích)
        public DataTable LayDocGia()
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var docGiaList = (from docgia in context.DocGias
                                  join loaidg in context.LoaiDocGias on docgia.MaLoaiDG equals loaidg.MaLoaiDG into ldgGroup
                                  from loaidg in ldgGroup.DefaultIfEmpty()
                                  select new
                                  {
                                      docgia.MaDocGia,
                                      docgia.HoTen,
                                      docgia.Tuoi,
                                      docgia.GioiTinh,
                                      docgia.SoDT,
                                      docgia.CCCD,
                                      docgia.Email,
                                      docgia.DiaChi,
                                      docgia.NgayDangKy,
                                      TongTienNo = docgia.PhieuPhats
                                        .SelectMany(pp => pp.QDPs)
                                        .Sum(qdp => (decimal?)qdp.TienPhat) ?? 0,
                                      docgia.TrangThai,
                                      TenLoaiDG = loaidg != null ? loaidg.TenLoaiDG : "Chưa phân loại"
                                  }).Take(1000).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã ĐG");
                dt.Columns.Add("Họ tên");
                dt.Columns.Add("Tuổi");
                dt.Columns.Add("Giới tính");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("CCCD");
                dt.Columns.Add("Email");
                dt.Columns.Add("Địa chỉ");
                dt.Columns.Add("Loại ĐG");
                dt.Columns.Add("Ngày đăng ký");
                dt.Columns.Add("Tiền nợ");
                dt.Columns.Add("Trạng thái");

                foreach (var item in docGiaList)
                {
                    dt.Rows.Add(
                        item.MaDocGia,
                        item.HoTen ?? "",
                        item.Tuoi,
                        ConvertGioiTinh(item.GioiTinh),
                        item.SoDT ?? "",
                        item.CCCD ?? "",
                        item.Email ?? "",
                        item.DiaChi ?? "",
                        item.TenLoaiDG ?? "Chưa phân loại",
                        item.NgayDangKy?.ToString("dd/MM/yyyy") ?? "",
                        item.TongTienNo.ToString("N0") + " VNĐ",
                        item.TrangThai == true ? "Hoạt động" : "Ngừng hoạt động"
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

        // ✅ NEW - Method với sắp xếp: Hoạt động trước, Ngừng hoạt động sau
        public DataTable LayDocGiaSorted()
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var docGiaList = (from docgia in context.DocGias
                                  join loaidg in context.LoaiDocGias on docgia.MaLoaiDG equals loaidg.MaLoaiDG into ldgGroup
                                  from loaidg in ldgGroup.DefaultIfEmpty()
                                  orderby docgia.TrangThai descending, docgia.HoTen ascending
                                  select new
                                  {
                                      docgia.MaDocGia,
                                      docgia.HoTen,
                                      docgia.Tuoi,
                                      docgia.GioiTinh,
                                      docgia.SoDT,
                                      docgia.CCCD,
                                      docgia.Email,
                                      docgia.DiaChi,
                                      docgia.NgayDangKy,
                                      TongTienNo = docgia.PhieuPhats
                                        .SelectMany(pp => pp.QDPs)
                                        .Sum(qdp => (decimal?)qdp.TienPhat) ?? 0,
                                      docgia.TrangThai,
                                      TenLoaiDG = loaidg != null ? loaidg.TenLoaiDG : "Chưa phân loại"
                                  }).Take(1000).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã ĐG");
                dt.Columns.Add("Họ tên");
                dt.Columns.Add("Tuổi");
                dt.Columns.Add("Giới tính");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("CCCD");
                dt.Columns.Add("Email");
                dt.Columns.Add("Địa chỉ");
                dt.Columns.Add("Loại ĐG");
                dt.Columns.Add("Ngày đăng ký");
                dt.Columns.Add("Tiền nợ");
                dt.Columns.Add("Trạng thái");

                foreach (var item in docGiaList)
                {
                    dt.Rows.Add(
                        item.MaDocGia,
                        item.HoTen ?? "",
                        item.Tuoi,
                        ConvertGioiTinh(item.GioiTinh),
                        item.SoDT ?? "",
                        item.CCCD ?? "",
                        item.Email ?? "",
                        item.DiaChi ?? "",
                        item.TenLoaiDG ?? "Chưa phân loại",
                        item.NgayDangKy?.ToString("dd/MM/yyyy") ?? "",
                        item.TongTienNo.ToString("N0") + " VNĐ",
                        item.TrangThai == true ? "Hoạt động" : "Ngừng hoạt động"
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
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var loaiDGList = context.LoaiDocGias.ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("MaLoaiDG", typeof(int));
                dt.Columns.Add("TenLoaiDG", typeof(string));

                foreach (var loaidg in loaiDGList)
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
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

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
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

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

        // ✅ UPDATED - Xóa độc giả (thực chất là toggle trạng thái)
        public bool XoaDocGia(int maDocGia, ref string err)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var docGia = context.DocGias.FirstOrDefault(docgia => docgia.MaDocGia == maDocGia);
                if (docGia == null)
                {
                    err = "Không tìm thấy độc giả với mã: " + maDocGia;
                    return false;
                }

                // ✅ Soft delete - toggle trạng thái thay vì xóa thật
                docGia.TrangThai = !docGia.TrangThai;
                docGia.NgayCapNhat = DateTime.Now;

                context.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    err = "Không thể thay đổi trạng thái độc giả vì có dữ liệu liên quan.";
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

        // ✅ Method gốc (giữ nguyên để tương thích)
        public DataTable TimKiemDocGia(string tuKhoa)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var docGiaList = (from docgia in context.DocGias
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
                                      docgia.GioiTinh,
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
                dt.Columns.Add("Giới tính");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("CCCD");
                dt.Columns.Add("Email");
                dt.Columns.Add("Địa chỉ");
                dt.Columns.Add("Loại ĐG");
                dt.Columns.Add("Ngày đăng ký");
                dt.Columns.Add("Tiền nợ");
                dt.Columns.Add("Trạng thái");

                foreach (var item in docGiaList)
                {
                    dt.Rows.Add(
                        item.MaDocGia,
                        item.HoTen ?? "",
                        item.Tuoi,
                        ConvertGioiTinh(item.GioiTinh),
                        item.SoDT ?? "",
                        item.CCCD ?? "",
                        item.Email ?? "",
                        item.DiaChi ?? "",
                        item.TenLoaiDG ?? "Chưa phân loại",
                        item.NgayDangKy?.ToString("dd/MM/yyyy") ?? "",
                        (item.TienNo ?? 0).ToString("N0") + " VNĐ",
                        item.TrangThai == true ? "Hoạt động" : "Ngừng hoạt động"
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

        // ✅ NEW - Method tìm kiếm với sắp xếp
        public DataTable TimKiemDocGiaSorted(string tuKhoa)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var docGiaList = (from docgia in context.DocGias
                                  join loaidg in context.LoaiDocGias on docgia.MaLoaiDG equals loaidg.MaLoaiDG into ldgGroup
                                  from loaidg in ldgGroup.DefaultIfEmpty()
                                  where docgia.HoTen.Contains(tuKhoa) ||
                                        docgia.SoDT.Contains(tuKhoa) ||
                                        (docgia.Email != null && docgia.Email.Contains(tuKhoa)) ||
                                        (docgia.CCCD != null && docgia.CCCD.Contains(tuKhoa))
                                  orderby docgia.TrangThai descending, docgia.HoTen ascending
                                  select new
                                  {
                                      docgia.MaDocGia,
                                      docgia.HoTen,
                                      docgia.Tuoi,
                                      docgia.GioiTinh,
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
                dt.Columns.Add("Giới tính");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("CCCD");
                dt.Columns.Add("Email");
                dt.Columns.Add("Địa chỉ");
                dt.Columns.Add("Loại ĐG");
                dt.Columns.Add("Ngày đăng ký");
                dt.Columns.Add("Tiền nợ");
                dt.Columns.Add("Trạng thái");

                foreach (var item in docGiaList)
                {
                    dt.Rows.Add(
                        item.MaDocGia,
                        item.HoTen ?? "",
                        item.Tuoi,
                        ConvertGioiTinh(item.GioiTinh),
                        item.SoDT ?? "",
                        item.CCCD ?? "",
                        item.Email ?? "",
                        item.DiaChi ?? "",
                        item.TenLoaiDG ?? "Chưa phân loại",
                        item.NgayDangKy?.ToString("dd/MM/yyyy") ?? "",
                        (item.TienNo ?? 0).ToString("N0") + " VNĐ",
                        item.TrangThai == true ? "Hoạt động" : "Ngừng hoạt động"
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
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();
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
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();
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

        public DataTable LayChiTietTienMuonDocGia(int maDocGia)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaDocGia", typeof(int));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("TongTienMuon", typeof(decimal));
            dt.Columns.Add("TongTienPhat", typeof(decimal));
            dt.Columns.Add("TongCong", typeof(decimal));
            dt.Columns.Add("SoLanMuon", typeof(int));
            dt.Columns.Add("LanMuonGanNhat", typeof(DateTime));

            try
            {
                using (LibraryManagement1Entities context = new LibraryManagement1Entities())
                {
                    // Lấy thông tin độc giả
                    var docGia = context.DocGias.FirstOrDefault(dg => dg.MaDocGia == maDocGia);
                    if (docGia == null) return dt;

                    decimal tongTienMuon = 0;
                    int soLanMuon = 0;
                    DateTime? lanMuonGanNhat = null;

                    var allPhieuMuon = context.PhieuMuonSaches.ToList()
                        .Where(pms => pms.MaDocGia == maDocGia);

                    foreach (var phieu in allPhieuMuon)
                    {
                        // Lấy chi phí từ LoaiPhieuMuon
                        var loaiPhieu = context.LoaiPhieuMuons.FirstOrDefault(lpm => lpm.MaLPhieuMuon == phieu.MaPhieu);
                        if (loaiPhieu != null && loaiPhieu.ChiPhi.HasValue)
                        {
                            tongTienMuon += loaiPhieu.ChiPhi.Value;
                        }

                        soLanMuon++;

                        // Lấy ngày mượn gần nhất
                        var muonSach = context.MuonSaches.FirstOrDefault(ms => ms.MaPhieu == phieu.MaPhieu);
                        if (muonSach != null)
                        {
                            if (lanMuonGanNhat == null || muonSach.NgayMuon > lanMuonGanNhat)
                            {
                                lanMuonGanNhat = muonSach.NgayMuon;
                            }
                        }
                    }

                    decimal tongTienPhat = 0;

                    // Lấy tất cả phiếu phạt của độc giả
                    var allPhieuPhat = context.PhieuPhats
                        .Where(pp => pp.MaDG == maDocGia)
                        .ToList();

                    foreach (var phieuPhat in allPhieuPhat)
                    {
                        if (phieuPhat.QDPs != null && phieuPhat.QDPs.Any())
                        {
                            foreach (var qdp in phieuPhat.QDPs)
                            {
                                if (qdp.TienPhat.HasValue)
                                {
                                    tongTienPhat += qdp.TienPhat.Value;
                                }
                            }
                        }
                    }

                    // Thêm dữ liệu vào DataTable
                    dt.Rows.Add(
                        docGia.MaDocGia,
                        docGia.HoTen,
                        tongTienMuon,
                        tongTienPhat,
                        tongTienMuon + tongTienPhat,
                        soLanMuon,
                        lanMuonGanNhat ?? DateTime.MinValue
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LayChiTietTienMuonDocGia: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            return dt;
        }

        // ✅ UPDATED - Method LayTatCaDocGia với sắp xếp
        public DataTable LayTatCaDocGia()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaDocGia", typeof(int));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("Tuoi", typeof(int));
            dt.Columns.Add("GioiTinh", typeof(string));
            dt.Columns.Add("SoDT", typeof(string));
            dt.Columns.Add("CCCD", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("DiaChi", typeof(string));
            dt.Columns.Add("TenLoaiDG", typeof(string));
            dt.Columns.Add("NgayDangKy", typeof(string));
            dt.Columns.Add("TienNo", typeof(string));
            dt.Columns.Add("TrangThai", typeof(string));

            try
            {
                using (LibraryManagement1Entities context = new LibraryManagement1Entities())
                {
                    // ✅ Sắp xếp: Hoạt động trước, Ngừng hoạt động sau
                    var allDocGia = context.DocGias
                        .OrderByDescending(dg => dg.TrangThai)
                        .ThenBy(dg => dg.HoTen)
                        .ToList();

                    foreach (var dg in allDocGia)
                    {
                        // Lấy tên loại độc giả
                        string tenLoaiDG = "Chưa phân loại";
                        if (dg.MaLoaiDG.HasValue)
                        {
                            var loaiDG = context.LoaiDocGias.FirstOrDefault(ldg => ldg.MaLoaiDG == dg.MaLoaiDG.Value);
                            tenLoaiDG = loaiDG?.TenLoaiDG ?? "Chưa phân loại";
                        }

                        string ngayDangKyStr;
                        if (dg.NgayDangKy.HasValue)
                        {
                            ngayDangKyStr = dg.NgayDangKy.Value.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            ngayDangKyStr = "Chưa có";
                        }

                        string tienNoStr;
                        if (dg.TienNo.HasValue)
                        {
                            tienNoStr = dg.TienNo.Value.ToString("N0") + " VNĐ";
                        }
                        else
                        {
                            tienNoStr = "0 VNĐ";
                        }

                        string trangThaiStr;
                        if (dg.TrangThai.HasValue)
                        {
                            trangThaiStr = dg.TrangThai.Value ? "Hoạt động" : "Ngừng hoạt động";
                        }
                        else
                        {
                            trangThaiStr = "Không xác định";
                        }

                        dt.Rows.Add(
                            dg.MaDocGia,
                            dg.HoTen ?? "",
                            dg.Tuoi,
                            ConvertGioiTinh(dg.GioiTinh),
                            dg.SoDT ?? "",
                            dg.CCCD ?? "",
                            dg.Email ?? "",
                            dg.DiaChi ?? "",
                            tenLoaiDG,
                            ngayDangKyStr,
                            tienNoStr,
                            trangThaiStr
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LayTatCaDocGia: {ex.Message}");
            }

            return dt;
        }

        // ✅ Helper method để convert giới tính
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