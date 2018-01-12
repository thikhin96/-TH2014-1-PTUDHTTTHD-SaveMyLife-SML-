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
        private readonly IBillService _billService;
        private readonly IDistributorService _distributorService;
        private readonly IOrderService _oService;
        private readonly IDeliveryOrderService _DeOrService;

        public object DataRepository { get; private set; }

        public SalesReportController(IOrderService oService, IBillService bService, IDistributorService distributorService, IDeliveryOrderService DeOrService)
        {
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
        public ContentResult SearchListBill(int month, int quartar, int year, int idDistributor)
        {
            IList<Bill> rs = new List<Bill>();
            if (Request.IsAjaxRequest())
            {
                rs = _billService.SearchListBill(month, quartar, year, idDistributor);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idBill, x.Staff.staffName, x.Distributor.name, x.createdDate, x.purchase, x.types, x.description }));
                return Content(list, "application/json");
            }
            return null;
        }


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
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idOrder, x.CreatedDate, x.Distributor.name, x.Staff.staffName, x.Total }));
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
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idDeliveryOrder, x.deliveryDate, x.Distributor.name, x.Staff.staffName, x.totalPurchase }));
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
        public ContentResult SearchListOrderDetail(int month, int quartar, int year, int idDistributor)
        {
            IList<Order> rs = new List<Order>();
            IList<OrderDetail> rs1 = new List<OrderDetail>();
            if (Request.IsAjaxRequest())
            {
                rs1 = _oService.SearchListOrderDetail(month, quartar, year, idDistributor);
                var list = JsonConvert.SerializeObject(rs1.Select(x => new { x.idOrder, x.Order.CreatedDate, x.Order.Distributor.name, x.quantity, x.Order.Total }));
                return Content(list, "application/json");
            }
            return null;
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
        public ContentResult SearchListDetailDeliveryOrder(int month, int quartar, int year, int idDistributor)
        {
            IList<DeliveryOrder> rs1 = new List<DeliveryOrder>();
            IList<DetailedDeliveryOrder> rs = new List<DetailedDeliveryOrder>();
            if (Request.IsAjaxRequest())
            {
                rs = _DeOrService.SearchListDetailDeliveryOrder(month, quartar, year, idDistributor);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idDeliveryOrder, x.DeliveryOrder.deliveryDate, x.DeliveryOrder.Distributor.name, x.DeliveryOrder.Staff.staffName, x.quantity, x.DeliveryOrder.totalPurchase }));
                return Content(list, "application/json");
            }
            return null;
        }
        public ActionResult Statistic()
        {
            isAdminLogged();
            SalesReportViewModel model = new SalesReportViewModel();
            model.Distributors = _distributorService.GetAllDistributor();
            ViewBag.Parent = "Thống Kê";
            ViewBag.Child = "Tổng Quan";
            return View(model);
        }
        public ActionResult StatisticCompare()
        {
            isAdminLogged();
            SalesReportViewModel model = new SalesReportViewModel();
            model.Distributors = _distributorService.GetAllDistributor();
            ViewBag.Parent = "Thống Kê";
            ViewBag.Child = "So Sánh";
            return View(model);
        }
        
        
        
    }
}