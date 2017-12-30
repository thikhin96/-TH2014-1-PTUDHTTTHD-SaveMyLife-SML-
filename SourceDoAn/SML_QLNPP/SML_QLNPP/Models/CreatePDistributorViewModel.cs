using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;

namespace SML_QLNPP.Models
{
    public class CreatePDistributorViewModel
    {
        public int idDistributor { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<byte> status { get; set; }
        public int idRepresentative { get; set; }
        public string rep_name { get; set; }
        public string rep_phone { get; set; }
        public string rep_email { get; set; }
        public string title { get; set; }
        public Nullable<int> PDistributor { get; set; }
    }
}