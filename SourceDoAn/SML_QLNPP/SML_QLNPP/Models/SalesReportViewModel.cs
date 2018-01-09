using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel;
using System.ComponentModel.DataAnnotations;

namespace SML_QLNPP.Models
{
    public class SalesReportViewModel
    {

        public List<Order> Orders { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
        public IList<Product> Products { get; set; }
        public IList<DeliveryOrder> DeliveryOrder { get; set; }
        public IList<Bill> Bill { get; set; }
        public int idDistributor { get; set; }
        //public string NameDistributor { get; set; }
        public int Distributor { get; set; }
        public List<Distributor> Distributors { get; set; }

    }
}