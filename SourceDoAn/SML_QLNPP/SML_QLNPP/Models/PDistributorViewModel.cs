using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;

namespace SML_QLNPP.Models
{
    public class PDistributorViewModel
    {
        public byte status { get; set; }
        public IList<PotentialDistributor> listPDistributor { get; set; }
    }
}