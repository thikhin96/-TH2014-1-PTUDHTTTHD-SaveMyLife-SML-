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
    
    public partial class PhanCongTraoDoi
    {
        public int ID_NV { get; set; }
        public int ID_DoiTac { get; set; }
        public Nullable<System.DateTime> ThoiGian { get; set; }
        public string DiaDiem { get; set; }
        public Nullable<bool> HoanThanh { get; set; }
        public string KetQua { get; set; }
    
        public virtual DoiTac DoiTac { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}