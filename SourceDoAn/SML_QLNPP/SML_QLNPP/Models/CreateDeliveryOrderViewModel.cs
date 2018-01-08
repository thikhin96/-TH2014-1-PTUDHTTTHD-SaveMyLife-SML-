using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class CreateDeliveryOrderViewModel
    {
        public int idOrder { get; set; }
        public int idDeliveryOrder { get; set; }
        public Nullable<int> idStaff { get; set; }
        public List<DetailedDeliveryOrder> DetailedDeliveryOrder { get; set; }
        public Nullable<int> idDistributor { get; set; }
        public string distributorName { get; set; }
        public string deliveryAdd { get; set; }
        public string recipient { get; set; }
        public string recipientPhone { get; set; }
        public Nullable<decimal> totalPurchase { get; set; }
        public Nullable<bool> DeliveryType { get; set; }
        public Nullable<bool> PaymentType { get; set; }
        public Nullable<DateTime> estimateDateOfDelivery { get; set; }
        public Nullable<DateTime> deliveryDate { get; set; }
        public string description { get; set; }
        public int commission { get; set; }
    }
}