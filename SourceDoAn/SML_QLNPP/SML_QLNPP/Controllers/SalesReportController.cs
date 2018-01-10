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

        public SalesReportController(ISalesReportService orderService, IOrderService oService, IBillService bService, IDistributorService distributorService, IDeliveryOrderService DeOrService)
        {
            this._orderService = orderService;
            this._oService = oService;
            this._billService = bService;
            this._distributorService = distributorService;
            this._DeOrService = DeOrService;

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

        [HttpGet]
        public ActionResult ListDeliveryOrder()
        {
            isAdminLogged();
            SalesReportViewModel model = new SalesReportViewModel();
            model.Distributors = _distributorService.GetAllDistributor();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Đơn Giao Hàng";
            return View(model);
        }
        public ContentResult SearchListDeliveryOrder(int month, int quartar, int year, int idDistributor)
        {
            IList<DeliveryOrder> rs = new List<DeliveryOrder>();
            if (Request.IsAjaxRequest())
            {
                rs = _DeOrService.SearchListDeliveryOrder(month, quartar, year, idDistributor);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idDeliveryOrder, x.deliveryDate, x.idDistributor, x.idStaff, x.totalPurchase }));
                return Content(list, "application/json");
            }
            return null;
        }

        [HttpGet]
        public ActionResult BusinessReport()
        {
            isAdminLogged();
            SalesReportViewModel model = new SalesReportViewModel();
            model.Distributors = _distributorService.GetAllDistributor();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Báo Cáo Kinh Doanh";
            return View(model);
        }

        [HttpGet]
        public ActionResult CommodityAllocationReport()
        {
            isAdminLogged();
            SalesReportViewModel model = new SalesReportViewModel();
            model.Distributors = _distributorService.GetAllDistributor();
            ViewBag.Parent = "Báo Cáo Doanh Thu";
            ViewBag.Child = "Báo Cáo Phân Bổ Hàng Hóa";
            return View(model);
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
        
        
        
    }
}