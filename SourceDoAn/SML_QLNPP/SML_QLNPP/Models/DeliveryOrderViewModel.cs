using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataService.Dtos;
using DataModel;

namespace SML_QLNPP.Models
{
    public class DeliveryOrderViewModel
    {
        public int idDeliveryOrder { get; set; }
        public string deliveryDate { get; set; }
        public byte status { get; set; }
        public IList<DeliveryOrder> listDeliveryOrder { set; get; }
    }
}