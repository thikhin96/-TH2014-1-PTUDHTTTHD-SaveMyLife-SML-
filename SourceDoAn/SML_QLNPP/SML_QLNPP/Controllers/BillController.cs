using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using SML_QLNPP.Models;
namespace SML_QLNPP.Controllers
{
    public class BillController : BaseController
    {
        private readonly IBillService _billService;
        private readonly IDeliveryOrderService _dOrderService;
        private readonly IDistributorService _distributoriervice;
        private readonly IOrderService _orderService;
        private readonly IStaffService _staffService;
        private readonly IAccountService _accountService;
        public BillController(IAccountService accountService, IStaffService staffService,
            IOrderService orderService, IBillService billService, IDeliveryOrderService dOrderService,
            IDistributorService distributorBservice)
        {
            this._billService = billService;
            this._dOrderService = dOrderService;
            this._distributoriervice = distributorBservice;
            this._orderService = orderService;
            this._staffService = staffService;
            this._accountService = accountService;
        }
        //POST: Bill/Create
        [HttpPost]
        public ActionResult Create(CreateBillViewModel model)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";
            isAdminLogged();
            var bill = new Bill();
            bill.idDeliveryOrder = model.idDeliveryOrder;
            bill.idDistributor = model.idDistributor;
            bill.idStaff = model.idStaff;
            bill.purchase = model.purchase;
            bill.description = model.description;
            bill.createdDate = model.createdDate;
            var delivery = _dOrderService.SearchById(bill.idDeliveryOrder.Value);
            var order = _orderService.GetOrder(delivery.idOrder.Value);
            // TH thanh toán giao hàng
            // nếu hình thức thanh toán của đơn hàng là qua thẻ thì ko cần lập hóa đơn (paymenttype = true)
            if (order.PaymentType == true)
            {
                ViewBag.types = 1;
                ViewBag.msg = "Đã thanh toán qua thẻ, không cần lập hóa đơn thanh toán giao hàng";
            }
            else
            {
                // TH thanh toán bằng tiền mặt
                var bills = delivery.Bills;
                // TH Đơn đã có hóa đơn rồi
                if(bills.FirstOrDefault() != null)
                {
                    ViewBag.types = 1;
                    ViewBag.msg = "Đơn hàng này đã lập hóa đơn thanh toán rồi, vui lòng thanh toán công nợ nếu có!";            
                }
                else
                {
                    // TH đơn chưa có hóa đơn
                    decimal tienthieu = delivery.totalPurchase.Value - bill.purchase.Value;
                    if(tienthieu < 0)
                    {
                        ViewBag.types = 1;
                        ViewBag.msg = "Lập thất bại, tiền đơn hàng chỉ có " + string.Format("{0:0,0}", delivery.totalPurchase.Value) + " VNĐ.";
                    }
                    else
                    {
                        // TH thu tiền ít hơn tổng tiền đơn hàng, thì tăng thêm công nợ
                        if (tienthieu > 0)
                        {
                            var dis = order.Distributor;
                            dis.debt += tienthieu;
                            _distributoriervice.UpdateDebt(bill.idDistributor.Value, dis.debt.Value);
                        }
                        _billService.AddBill(bill);
                    }
                }
            }
            ViewBag.types = 2;
            ViewBag.msg = "Lập hoá đơn thành công, bạn đã thanh toán " + string.Format("{0:0,0}", delivery.totalPurchase.Value) + " VNĐ.";
            return View(model);
        }

        // GET: Bill/Create
        public ActionResult Create(int idDelivery)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";
            isAdminLogged();
            CreateBillViewModel model = new CreateBillViewModel();
            var user = Session["admin"] as Account;
            Staff staff = _staffService.GetByAccount(user.UserName);
            var dOrder = _dOrderService.SearchById(idDelivery);

            model.idStaff = staff.idStaff;
            model.idDeliveryOrder = idDelivery;
            model.createdDate = DateTime.Now;
            model.idDistributor = dOrder.idDistributor;
            model.nameDistributor = dOrder.Distributor.name;
            model.idDeliveryOrder = dOrder.idDeliveryOrder;
            return View(model);
        }
    }
}
