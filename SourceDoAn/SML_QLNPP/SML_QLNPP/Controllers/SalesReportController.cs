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
        private readonly IDeliveryOrderService _DeOrService;

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
            isAdminLogged();
            SalesReportViewModel model = new SalesReportViewModel();
            model.Distributors = _distributorService.GetAllDistributor();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Hóa Đơn";
            
            return View(model);
        }
        //<th>Mã Hóa Đơn</th>
        //                            <th>Mã Nhân Viên</th>
        //                            <th>Mã Nhà Phân Phối</th>
        //                            <th>Ngày Lập</th>
        //                            <th>Tổng Tiền</th>
        //                            <th>Loại Hóa Đơn</th>
        //                            <th>Mô Tả</th>
        public ContentResult SearchListBill(int month, int quartar, int year, int idDistributor)
        {
            IList<Bill> rs = new List<Bill>();
            if (Request.IsAjaxRequest())
            {
                rs = _billService.SearchListBill(month, quartar, year, idDistributor);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idBill, x.idStaff, x.idDistributor, x.createdDate, x.purchase, x.types, x.description }));
                return Content(list, "application/json");
            }
            return null;
        }

        [HttpGet]
        public ActionResult ListOrder()
        {
            isAdminLogged();
            SalesReportViewModel model = new SalesReportViewModel();
            model.Distributors = _distributorService.GetAllDistributor();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Đơn Đặt Hàng";
            return View(model);
        }
        public ContentResult SearchListOrder(int month, int quartar, int year, int idDistributor)
        {
            IList<Order> rs = new List<Order>();
            if (Request.IsAjaxRequest())
            {
                rs = _oService.SearchListOrder(month, quartar, year, idDistributor);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idOrder, x.CreatedDate, x.idDistributor, x.idStaff, x.Total }));
                return Content(list, "application/json");
            }
            return null;
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
            isAdminLogged();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Đơn Giao Hàng";
            return View();
        }
        public ActionResult BusinessReport()
        {
            isAdminLogged();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Báo Cáo Kinh Doanh";
            //ViewBag.Distributor = new SelectList(_distributorService.GetList(), "idDistributor", "name");
            return View();
        }
        

        public ActionResult CommodityAllocationReport()
        {
            isAdminLogged();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Báo Cáo Phân Bổ Hàng Hóa";
            return View();
        }
        public ActionResult Statistic()
        {
            isAdminLogged();
            ViewBag.Parent = "Thống Kê";
            ViewBag.Child = "Tổng Quan";
            return View();
        }
        public ActionResult StatisticCompare()
        {
            isAdminLogged();
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