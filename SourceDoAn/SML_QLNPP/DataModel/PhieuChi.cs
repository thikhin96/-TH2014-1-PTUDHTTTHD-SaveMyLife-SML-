//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuChi
    {
        public int ID_PhieuChi { get; set; }
        public Nullable<decimal> TienChiTra { get; set; }
        public string LyDoChi { get; set; }
        public Nullable<System.DateTime> NgayLapPhieu { get; set; }
        public Nullable<int> NPP { get; set; }
        public Nullable<int> NhanVien { get; set; }
    
        public virtual NhanVien NhanVien1 { get; set; }
        public virtual NhaPhanPhoi NhaPhanPhoi { get; set; }
    }
}