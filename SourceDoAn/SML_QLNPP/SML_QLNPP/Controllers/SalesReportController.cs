using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;

namespace SML_QLNPP.Controllers
{
    public class SalesReportController : Controller
    {

        private readonly ISalesReportService _orderService;
        private readonly ISalesReportService _distributorService;

        public SalesReportController(ISalesReportService orderService)
        {
            this._orderService = orderService;
        }
        /*public SalesReportController(ISalesReportService distributorService)
        {
            this._distributorService = distributorService;
        }*/
        // GET: Promotion
        public ActionResult ListOrder()
        {
            return View();
        }
        public ActionResult ListBill()
        {
            return View();
        }
        public ActionResult ListDeliveryOrder()
        {
            return View();
        }
        public ActionResult BusinessReports()
        {
            return View();
        }
        public ActionResult CommodityAllocationReport()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            var model = _orderService.GetSalesReport(id);
            if (model == null)
            {
                return Redirect("/SalesReport/ListBill");
            }
            else
            {
                return View(model);
            }
        }
        public ContentResult Search(int idSalesReport, string startDate, string endDate)
        {
            IList<SalesReport> rs = new List<SalesReport>();
            if (Request.IsAjaxRequest())
            {
                rs = _orderService.Search(idSalesReport, startDate, endDate);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idSalesReport, x.beginDate, x.endDate }));
                return Content(list, "application/json");
            }
            return null;
        }

    }
}