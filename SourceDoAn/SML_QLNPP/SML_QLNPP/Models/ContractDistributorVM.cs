using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class ContractViewModel
    {
        public int idContract { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime expiredDate { get; set; }
        public decimal minOrderTotalValue { get; set; }
        public decimal maxDebt { get; set; }
        public byte commission { get; set; }
        public bool disType { get; set; }
        public string area { get; set; }
        public bool status { get; set; }
        public string note { get; set; }
    }

    public class DistributorViewModel
    {
        public int idDistributor { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public string note { get; set; }
        public string debt { get; set; }
    }

    public class RepresentativeViewModel
    {
        public int idRepresentative { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string title { get; set; }
    }

    public class PdisRepViewModel 
    {
        public DistributorViewModel pDis { get; set; }
        public RepresentativeViewModel rep { get; set; }

        public PdisRepViewModel()
        {
            pDis = new DistributorViewModel();
            rep = new RepresentativeViewModel();
        }
    }

    public class ContractDistributorVM
    {
        public ContractViewModel con { get; set; }
        public DistributorViewModel dis { get; set; }
        public RepresentativeViewModel rep { get; set; }
        public IList<DistributorViewModel> oldDis { get; set; }
        public IList<PdisRepViewModel> pDis { get; set; }
    }
}