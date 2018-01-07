using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class ReturnRequestDetailViewModel
    {
        public int idReturnRequest { get; set; }
        public string status { get; set; }
        public string note { get; set; }
        public string modeOfReturn { get; set; }
        public Nullable<int> idDistributor { get; set; }
        public string dName { get; set; }
        public Storage Storage { get; set; }
        public string Staff { get; set; }
        public List<ReturnRequestDetail> ReturnRequestDetails { get; set; }
        public List<Storage> Storages { get; set; }
        public string Action { get; set; }
    }
}