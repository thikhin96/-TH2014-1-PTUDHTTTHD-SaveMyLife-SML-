using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;

namespace SML_QLNPP.Models
{
    public class CreateReturnRequestViewModel
    {
        public int idReturnRequest { get; set; }
        public Nullable<int> status { get; set; }
        public string note { get; set; }
        public Nullable<bool> modeOfReturn { get; set; }
        public Nullable<int> idDistributor { get; set; }
        public Nullable<int> idStorage { get; set; }
        public Storage Storage { get; set; }
        public Nullable<int> idStaff { get; set; }
        public List<ReturnRequestDetail> ReturnRequestDetails { get; set; }
        public List<Product> Products { get; set; }
        public List<Storage> Storages { get; set; }
        public string Action { get; set; }
    }
}