using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
namespace SML_QLNPP.Controllers
{
    public class DeliveryOrderController : Controller
    {
        private readonly IDeliveryOrderService _deliveryOrderService;

        public DeliveryOrderController(IDeliveryOrderService deliveryOrderService)
        {
            this._deliveryOrderService = deliveryOrderService;
        }
        // GET: Order
        public ActionResult List()
        {
            return View();
        }
        public ContentResult Search(string keyword, string created_date,int status)
        {
            IList<DeliveryOrder> rs = new List<DeliveryOrder>();
            if (Request.IsAjaxRequest())
            {
                rs = _deliveryOrderService.GetAll();
                var list = JsonConvert.SerializeObject(rs.Select(x => new {x.idOrder, x.Distributor.name, x.totalPurchase, x.deliveryDate, x.Staff.staffName,x.status }));
                return Content(list, "application/json");
            }

            return null;
        }
        public ActionResult Detail(int id)
        {
            var model = _deliveryOrderService.SearchById(id);
            if(model == null)
            {
                return Redirect("/DeliveryOrder/List");
            }
            else
            {
                return View(model);
            }
        }
    }
}