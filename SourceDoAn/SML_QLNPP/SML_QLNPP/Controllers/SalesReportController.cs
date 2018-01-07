using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SML_QLNPP.Models;
using NLog.Internal;
using System.Collections;

namespace SML_QLNPP.Controllers
{
    public class SalesReportController : BaseController
    {
        // GET: SalesReport
        private readonly ISalesReportService _orderService;
        private readonly IBillService _billService;
        private readonly IDistributorService _distributorService;
        private readonly IOrderService _oService;

        public object DataRepository { get; private set; }

        public SalesReportController(ISalesReportService orderService, IOrderService oService, IBillService bService, IDistributorService distributorService)
        {
            this._orderService = orderService;
            this._oService = oService;
            this._billService = bService;
            this._distributorService = distributorService;

        }
        //private void GetYears()
        //{
        //    List<int> Years = new List<int>();
        //    DateTime startYear = DateTime.Now;
        //    while (startYear.Year <= DateTime.Now.AddYears(3).Year)
        //    {
        //        Years.Add(startYear.Year);
        //        startYear = startYear.AddYears(1);
        //    }
        //    ViewBag.Years = Years;
        //}
        [HttpGet]
        public ActionResult ListBill()
        {
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Hóa Đơn";
            //SalesReportViewModel model = new SalesReportViewModel();
            //model.Bill = _billService.SearchListBill(0, 0, "");
            return View();
        }
        [HttpGet]
        public ActionResult ListOrder()
        {
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Đơn Đặt Hàng";
            SelectList list = new SelectList(_oService.GetOrderByDistributor(), "idDistributor", "name");
            ViewBag.DistributorName = list;
            return View();
        }
        //[HttpPost]
        //public ContentResult GetAllOrder()
        //{
        //    IList<Order> rs = new List<Order>();
        //    if (Request.IsAjaxRequest())
        //    {
        //        var OrderList = rs.Select(x => new
        //        {
        //            idOrder = x.idOrder,
        //            CreatedDate =x.CreatedDate,
        //            idDistributor = x.idDistributor,
        //            NameDistributor = x.Distributor.name,
        //            idStaff = x.idStaff,
        //            Total = x.Total
        //        }).ToList();
        //        return Content(OrderList, "application/json");
        //    }

        //    return null;
        //}
        //public ActionResult LoadData()
        //{
        //    var Items = g(0, "", 0);
        //    return Json(new { data = Items }, JsonRequestBehavior.AllowGet);
        //}

        

        public ActionResult ListDeliveryOrder()
        {
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Đơn Giao Hàng";
            return View();
        }
        public ActionResult BusinessReport()
        {
            ViewBag.Parent = "Báo Cáo Kinh Doanh";
            ViewBag.Distributor = new SelectList(_distributorService.GetList(), "idDistributor", "name");
            return View();
        }
        

        public ActionResult CommodityAllocationReport()
        {
            ViewBag.Parent = "Báo Cáo Phân Bổ Hàng Hóa";
            return View();
        }
        public ActionResult Statistic()
        {
            ViewBag.Parent = "Thống Kê";
            ViewBag.Child = "Tổng Quan";
            return View();
        }
        public ActionResult StatisticCompare()
        {
            ViewBag.Parent = "Thống Kê";
            ViewBag.Child = "So Sánh";
            return View();
        }
        
        /*public ActionResult Detail(int id)
        {
            ViewBag.Parent = "Báo Cáo Kinh Doanh";
            ViewBag.Child = "Chi tiết khuyến mãi";
            var model = _orderService.GetSalesReport(id);
            if (model == null)
            {
                return Redirect("/Order/List");
            }
            else
            {
                return View(model);
            }
        }*/
        
        //public ContentResult SearchListBill(int idBill, int idDistributor, string CreateDate)
        //{
        //    IList<Bill> rs = new List<Bill>();
        //    if (Request.IsAjaxRequest())
        //    {
        //        rs = _orderService.SearchListOrder(idBill, idDistributor, CreateDate);
        //        var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idSalesReport, x.beginDate, x.endDate }));
        //        return Content(list, "application/json");
        //    }
        //    return null;
        //}
    }
}