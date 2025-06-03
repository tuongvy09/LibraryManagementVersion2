using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementVersion2.Repositories
{
    class BLBienLai
    {
        // Lấy danh sách tất cả biên lai
        public DataTable LayBienLai()
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var bienLaiList = (from bl in context.BienLais
                                   join docgia in context.DocGias on bl.MaDocGia equals docgia.MaDocGia into dgGroup
                                   from docgia in dgGroup.DefaultIfEmpty()
                                   select new
                                   {
                                       bl.MaBienLai,
                                       TenDocGia = docgia != null ? docgia.HoTen : "Chưa xác định",
                                       SoDT = docgia != null ? docgia.SoDT : "",
                                       bl.NgayTraTT,
                                       bl.HinhThucThanhToan,
                                       bl.TienTra
                                   }).Take(1000).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã biên lai");
                dt.Columns.Add("Tên độc giả");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("Ngày thanh toán");
                dt.Columns.Add("Hình thức TT");
                dt.Columns.Add("Số tiền");

                foreach (var item in bienLaiList)
                {
                    dt.Rows.Add(
                        item.MaBienLai,
                        item.TenDocGia ?? "Chưa xác định",
                        item.SoDT ?? "",
                        item.NgayTraTT.ToString("dd/MM/yyyy"),
                        item.HinhThucThanhToan ?? "",
                        (item.TienTra).ToString("N0") + " VNĐ"
                    );
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách biên lai: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Lấy danh sách độc giả cho ComboBox
        public DataTable LayDocGiaChoComboBox()
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var docGiaList = context.DocGias
                    .Where(dg => dg.TrangThai == true)
                    .ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("MaDocGia", typeof(int));
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

        // Lấy danh sách hình thức thanh toán
        public List<string> LayHinhThucThanhToan()
        {
            return new List<string>
            {
                "Tiền mặt",
                "Chuyển khoản",
                "Thẻ tín dụng",
                "Ví điện tử"
            };
        }

        // Thêm biên lai mới
        public bool ThemBienLai(int? maDocGia, DateTime ngayTraTT, string hinhThucThanhToan, decimal tienTra, ref string err)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                // Validate input
                if (ngayTraTT > DateTime.Now)
                {
                    err = "Ngày thanh toán không được lớn hơn ngày hiện tại";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(hinhThucThanhToan))
                {
                    err = "Hình thức thanh toán không được để trống";
                    return false;
                }

                if (tienTra <= 0)
                {
                    err = "Số tiền phải lớn hơn 0";
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
                }

                // Tạo biên lai mới
                var newBienLai = new BienLai
                {
                    MaDocGia = maDocGia,
                    NgayTraTT = ngayTraTT,
                    HinhThucThanhToan = hinhThucThanhToan.Trim(),
                    TienTra = tienTra
                };

                context.BienLais.Add(newBienLai);
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

        // Cập nhật biên lai
        public bool CapNhatBienLai(int maBienLai, int? maDocGia, DateTime ngayTraTT, string hinhThucThanhToan, decimal tienTra, ref string err)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var bienLaiQuery = context.BienLais.FirstOrDefault(bl => bl.MaBienLai == maBienLai);

                if (bienLaiQuery == null)
                {
                    err = "Không tìm thấy biên lai với mã: " + maBienLai;
                    return false;
                }

                // Validate input
                if (ngayTraTT > DateTime.Now)
                {
                    err = "Ngày thanh toán không được lớn hơn ngày hiện tại";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(hinhThucThanhToan))
                {
                    err = "Hình thức thanh toán không được để trống";
                    return false;
                }

                if (tienTra <= 0)
                {
                    err = "Số tiền phải lớn hơn 0";
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
                }

                // Cập nhật thông tin
                bienLaiQuery.MaDocGia = maDocGia;
                bienLaiQuery.NgayTraTT = ngayTraTT;
                bienLaiQuery.HinhThucThanhToan = hinhThucThanhToan.Trim();
                bienLaiQuery.TienTra = tienTra;

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

        // Xóa biên lai
        public bool XoaBienLai(int maBienLai, ref string err)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var bienLai = context.BienLais.FirstOrDefault(bl => bl.MaBienLai == maBienLai);
                if (bienLai == null)
                {
                    err = "Không tìm thấy biên lai với mã: " + maBienLai;
                    return false;
                }

                context.BienLais.Remove(bienLai);
                context.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.InnerException?.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    err = "Không thể xóa biên lai vì đã có dữ liệu liên quan trong hệ thống.";
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

        // Tìm kiếm biên lai
        public DataTable TimKiemBienLai(string tuKhoa)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();

                var bienLaiList = (from bl in context.BienLais
                                   join docgia in context.DocGias on bl.MaDocGia equals docgia.MaDocGia into dgGroup
                                   from docgia in dgGroup.DefaultIfEmpty()
                                   where bl.MaBienLai.ToString().Contains(tuKhoa) ||
                                         (docgia != null && docgia.HoTen.Contains(tuKhoa)) ||
                                         (docgia != null && docgia.SoDT.Contains(tuKhoa)) ||
                                         bl.HinhThucThanhToan.Contains(tuKhoa)
                                   select new
                                   {
                                       bl.MaBienLai,
                                       TenDocGia = docgia != null ? docgia.HoTen : "Chưa xác định",
                                       SoDT = docgia != null ? docgia.SoDT : "",
                                       bl.NgayTraTT,
                                       bl.HinhThucThanhToan,
                                       bl.TienTra
                                   }).Take(500).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("Mã biên lai");
                dt.Columns.Add("Tên độc giả");
                dt.Columns.Add("Số ĐT");
                dt.Columns.Add("Ngày thanh toán");
                dt.Columns.Add("Hình thức TT");
                dt.Columns.Add("Số tiền");

                foreach (var item in bienLaiList)
                {
                    dt.Rows.Add(
                        item.MaBienLai,
                        item.TenDocGia ?? "Chưa xác định",
                        item.SoDT ?? "",
                        item.NgayTraTT.ToString("dd/MM/yyyy"),
                        item.HinhThucThanhToan ?? "",
                        (item.TienTra).ToString("N0") + " VNĐ"
                    );
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm biên lai: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }

        // Lấy thông tin biên lai theo mã
        public BienLai LayBienLaiTheoMa(int maBienLai)
        {
            LibraryManagement1Entities context = null;
            try
            {
                context = new LibraryManagement1Entities();
                return context.BienLais.FirstOrDefault(bl => bl.MaBienLai == maBienLai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin biên lai: " + ex.Message);
            }
            finally
            {
                context?.Dispose();
            }
        }
    }
}
