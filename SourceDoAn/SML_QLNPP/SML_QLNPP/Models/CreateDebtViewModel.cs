using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class CreateDebtViewModel
    {
        public int idDebt { get; set; }
        public int idDistributor { get; set; }
        public int idStaff { get; set; }
        public string nameDistributor { get; set; }
        public decimal debt { get; set; }
        public DateTime createdDate { get; set; }
        public decimal moneyDebt { get; set; }
        public string staffName { get; set; }
    }
}