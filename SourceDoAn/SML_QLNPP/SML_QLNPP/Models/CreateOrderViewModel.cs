using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SML_QLNPP.Models
{
    public class CreateOrderViewModel
    {
        public int idOrder { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string DeliveryType { get; set; }
        public string PaymentType { get; set; }
        public Nullable<DateTime> EstimateDateOfDelivery { get; set; }
        public Nullable<byte> Statuses { get; set; }
        public Nullable<int> idDistributor { get; set; }
        public Nullable<int> idConsignee { get; set; }
        public Nullable<int> idStaff { get; set; }
        public Consignee Consignee { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Product> Products { get; set; }
    }
}