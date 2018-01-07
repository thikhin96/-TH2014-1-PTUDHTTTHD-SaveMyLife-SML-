using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class ReturnDetailViewModel
    {
        public List<ReturnDetail> ReturnDetails { get; set; }
        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public Storage Storage { get; set; }
        public int? ReturnRequestId { get; set; }
        public int ReturnId { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ReturnForm { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string StaffName { get; set; }
        public Nullable<int> idStorage { get; set; }
    }
}