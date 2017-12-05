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
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        // GET: Order
        public ActionResult List()
        {
            return View();
        }
        public ContentResult Search(string keyword, string created_date,int status)
        {
            IList<Order> rs = new List<Order>();
            if (Request.IsAjaxRequest())
            {
                rs = _orderService.SearchOrder(keyword, created_date, status);
                var list = JsonConvert.SerializeObject(rs.Select(x => new {x.idOrder, x.Distributor.name, x.Total, x.CreatedDate, x.Staff.staffName,x.Statuses }));
                return Content(list, "application/json");
            }

            return null;
        }
        public ActionResult Detail(int id)
        {
            var model = _orderService.GetOrder(id);
            if(model == null)
            {
                return Redirect("/Order/List");
            }
            else
            {
                return View(model);
            }
        }
    }
}