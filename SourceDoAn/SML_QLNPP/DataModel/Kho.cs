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
    
    public partial class Kho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kho()
        {
            this.DonYCDoiTras = new HashSet<DonYCDoiTra>();
            this.PhieuDoiTras = new HashSet<PhieuDoiTra>();
        }
    
        public int ID_Kho { get; set; }
        public string SoNha_Duong { get; set; }
        public string PhuongXa { get; set; }
        public string QuanHuyen { get; set; }
        public string ThanhPho { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> NPP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonYCDoiTra> DonYCDoiTras { get; set; }
        public virtual NhaPhanPhoi NhaPhanPhoi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDoiTra> PhieuDoiTras { get; set; }
    }
}