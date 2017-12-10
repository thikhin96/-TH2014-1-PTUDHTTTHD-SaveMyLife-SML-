using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class CreateProductViewModel
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public int Unit { get; set; }
        public Nullable<decimal> Price { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public List<Unit> Units { get; set; }
        public bool IsDisabled { get; set; }
        
    }
}