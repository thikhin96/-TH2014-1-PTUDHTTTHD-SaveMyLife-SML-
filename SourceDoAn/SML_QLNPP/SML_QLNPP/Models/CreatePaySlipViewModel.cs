using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class CreatePaySlipViewModel
    {
        public int idPaySlip { get; set; }
        public int idDistributor { get; set; }
        public string nameDistributor { get; set; }
        public DateTime date { get; set; }
        public decimal moneyPay { get; set; }
        public string reason { get; set; }
        public string staffName { get; set; }
    }
}