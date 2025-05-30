//using OfficeOpenXml;
//using System;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;
//using LibraryManagementVersion2;
//using System.ComponentModel;

//namespace LibraryManagement.Services
//{
//    public class ExcelImporter
//    {
//        public void ImportExcelToDatabase(string filePath)
//        {
//            ExcelPackage.License.SetNonCommercialPersonal("Library Management App");

//            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
//            {
//                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
//                int rowCount = worksheet.Dimension.Rows;

//                using (var context = new FinalProjectLtWinsEntities1()) // Tên DbContext từ EDMX
//                {
//                    for (int row = 2; row <= rowCount; row++)
//                    {
//                        string tenCuonSach = worksheet.Cells[row, 2].Text.Trim();
//                        string tenDauSach = worksheet.Cells[row, 3].Text.Trim();
//                        string trangThai = worksheet.Cells[row, 4].Text.Trim();
//                        string tenTheLoai = worksheet.Cells[row, 5].Text.Trim();
//                        int quyDinhTuoi = int.TryParse(worksheet.Cells[row, 6].Text.Trim(), out var tuoi) ? tuoi : 0;
//                        string tenNXB = worksheet.Cells[row, 7].Text.Trim();
//                        string tenTacGia = worksheet.Cells[row, 8].Text.Trim();

//                        // Tìm hoặc thêm thể loại
//                        var theLoai = context.TheLoais.FirstOrDefault(t => t.TenTheLoai == tenTheLoai);
//                        if (theLoai == null)
//                        {
//                            theLoai = new TheLoai { TenTheLoai = tenTheLoai, QDSoTuoi = quyDinhTuoi };
//                            context.TheLoais.Add(theLoai);
//                        }

//                        // Tìm hoặc thêm NXB
//                        var nxb = context.NXBs.FirstOrDefault(n => n.TenNSB == tenNXB);
//                        if (nxb == null)
//                        {
//                            nxb = new NXB { TenNSB = tenNXB };
//                            context.NXBs.Add(nxb);
//                        }

//                        // Tìm hoặc thêm Tác giả
//                        var tacGia = context.TacGias.FirstOrDefault(tg => tg.TenTG == tenTacGia);
//                        if (tacGia == null)
//                        {
//                            tacGia = new TacGia { TenTG = tenTacGia };
//                            context.TacGias.Add(tacGia);
//                        }

//                        // Lưu để có mã
//                        context.SaveChanges();

//                        // Tìm hoặc thêm đầu sách
//                        var dauSach = context.DauSaches.FirstOrDefault(ds => ds.TenDauSach == tenDauSach);
//                        if (dauSach == null)
//                        {
//                            dauSach = new DauSach
//                            {
//                                TenDauSach = tenDauSach,
//                                MaTheLoai = theLoai.MaTheLoai,
//                                MaNXB = nxb.MaNXB
//                            };
//                            dauSach.TacGias.Add(tacGia); // Thêm mối quan hệ nhiều-nhiều
//                            context.DauSaches.Add(dauSach);
//                        }
//                        else if (!dauSach.TacGias.Any(tg => tg.MaTacGia == tacGia.MaTacGia))
//                        {
//                            dauSach.TacGias.Add(tacGia); // Nếu đã có đầu sách, thêm tác giả nếu chưa có
//                        }

//                        // Lưu để có mã đầu sách
//                        context.SaveChanges();

//                        // Thêm cuốn sách
//                        var cuonSach = new CuonSach
//                        {
//                            TenCuonSach = tenCuonSach,
//                            TrangThaiSach = trangThai,
//                            MaDauSach = dauSach.MaDauSach
//                        };
//                        context.CuonSaches.Add(cuonSach);

//                        // Lưu lần cuối
//                        context.SaveChanges();
//                    }
//                }

//                MessageBox.Show("Import thành công!");
//            }
//        }
//    }
//}
