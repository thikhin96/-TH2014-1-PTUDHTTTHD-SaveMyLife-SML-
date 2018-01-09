using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;
using System.ComponentModel.DataAnnotations;

namespace SML_QLNPP.Models
{
    public class DetailedDeliveryOrderViewModel
    {
        public int idDeliveryOrder { get; set; }
        public int idOrder { get; set; }
        public Nullable<int> idStaff { get; set; }
        public string nameStaff { get; set; }
        public Nullable<int> idDistributor { get; set; }
        public string nameDistributor { get; set; }
        public decimal debtDistributor { get; set; }
        public string recipient { get; set; }
        public string recipientPhone { get; set; }
        public Nullable<DateTime> estimateDateOfDelivery { get; set; }
        public Nullable<bool> paymentType { get; set; }
        public Nullable<bool> deliveryType { get; set; }
        public List<DetailedDeliveryOrder> DetailedDeliveryOrders { get; set; }
        public Nullable<byte> status { get; set; }
        public string description { get; set; }
        public Nullable<DateTime> deliveryDate { get; set; }
        public string deliveryAdd { get; set; }
        public Nullable<DateTime> updateDate { get; set; }
        public Nullable<decimal> totalPurchase { get; set; }
    }
}