//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagementVersion2
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocGia()
        {
            this.BienLais = new HashSet<BienLai>();
            this.PhieuMuonSaches = new HashSet<PhieuMuonSach>();
            this.PhieuPhats = new HashSet<PhieuPhat>();
            this.TheThuViens = new HashSet<TheThuVien>();
        }
    
        public int MaDocGia { get; set; }
        public Nullable<int> MaLoaiDG { get; set; }
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public string SoDT { get; set; }
        public string CCCD { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public Nullable<System.DateTime> NgayDangKy { get; set; }
        public Nullable<decimal> TienNo { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<int> MaThe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienLai> BienLais { get; set; }
        public virtual LoaiDocGia LoaiDocGia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuonSach> PhieuMuonSaches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuPhat> PhieuPhats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheThuVien> TheThuViens { get; set; }
        public virtual TheThuVien TheThuVien { get; set; }
    }
}
