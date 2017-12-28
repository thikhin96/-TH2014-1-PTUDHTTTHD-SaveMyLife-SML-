using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;

namespace SML_QLNPP.Models
{
    public class DetailedDeliveryOrderViewModel
    {
        public int idDeliveryOrder { get; set; }
        public int idOrder { get; set; }
        public int idStaff { get; set; }
        public string nameStaff { get; set; }
        public int idDistributor { get; set; }
        public string nameDistributor { get; set; }
        public string debtDistributor { get; set; }
        public int deliveryTypes { get; set; }
        public int paymentTypes { get; set; }
    }
}