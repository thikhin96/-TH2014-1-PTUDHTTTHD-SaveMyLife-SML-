using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;

namespace SML_QLNPP.Models
{
    public class PDistributorDetailViewModel
    {
        public int idDistributor { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
        public Nullable<byte> status { get; set; }
        public string note { get; set; }
        public string rep_name { get; set; }
        public string rep_phone { get; set; }
        public string rep_email { get; set; }
        public string title { get; set; }
        public string place { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string result { get; set; }
        public int staffAssigment { get; set; }
        public Nullable<bool> isComplete { get; set; }
        public List<Staff> allStaff { get; set; }
    }
}