using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class DistributorBase
    {
        public int idDistributor { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
        public string note { get; set; }
    }
}
