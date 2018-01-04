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
            IOrderService orderService,IBillService billService, IDeliveryOrderService dOrderService,
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
            var delivery = _dOrderService.SearchById(Int32.Parse(bill.idDeliveryOrder.ToString()));
            var order = _orderService.GetOrder(delivery.idOrder);
            // kiểm tra mã giao hàng có thuộc về npp hay không
            if (delivery.idDistributor != bill.idDistributor)
            {
                ViewBag.types = 1;
                ViewBag.msg = "Mã giao hàng và NPP không tồn tại, vui lòng kiểm tra lại!";
            } else
            {
                // TH thanh toán giao hàng
                if (bill.types ==1)
                {

                    // nếu hình thức thanh toán của đơn hàng là qua thẻ thì ko cần lập hóa đơn (paymenttype = true)
                    if (order.PaymentType == true)
                    {
                        ViewBag.types = 1;
                        ViewBag.msg = "Đã thanh toán qua thẻ, không cần lập hóa đơn thanh toán giao hàng";
                    }
                    else
                    {
                        // TH thanh toán bằng tiền mặt
                        // KT đơn giao hàng đã có hóa đơn chưa
                        var bills = delivery.Bills;
                        bool check = true; // mặc định đơn giao hàng chưa có hóa đơn
                        foreach (var item in bills)
                        {
                            if (item.idDeliveryOrder == delivery.idDeliveryOrder && item.types == 1)
                            {
                                ViewBag.types = 1;
                                ViewBag.msg = "Đơn hàng này đã lập hóa đơn thanh toán rồi, vui lòng thanh toán công nợ nếu có!";
                                check = false;
                                break;
                            }
                        }
                        // TH đơn giao hàng chưa được lập hóa đơn thanh toán
                        if (check)
                        {
                            long tienthieu = long.Parse((delivery.totalPurchase - bill.purchase).ToString());
                            // TH thu tiền ít hơn tổng tiền đơn hàng, thì tăng thêm công nợ
                            if (tienthieu > 0)
                            {
                                _distributoriervice.UpdateDebt(bill.idDistributor.Value, tienthieu);
                            }
                            ViewBag.types = 2;
                            ViewBag.msg = "Lập hóa đơn thành công";
                            _billService.AddBill(bill);
                        }
                    }
                }
                // TH2 lập hóa đơn thanh toán công nợ
                if (bill.types == 2)
                {
                    var congnohientai = delivery.Distributor.debt;
                    if (bill.purchase > congnohientai)
                    {
                        ViewBag.types = 1;
                        ViewBag.msg = "Tiền thu nhiều hơn tiền công nợ hiện tại";
                    }
                    else
                    {
                        _billService.AddBill(bill);
                        ViewBag.types = 2;
                        ViewBag.msg = "Lập hóa đơn thành công";
                    }
                }
            }
            return View(bill);
        }

        // GET: Bill/Create
        public ActionResult Create(int id)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";
            isAdminLogged();

            var user = Session["admin"] as Account;

            var dOrder = _dOrderService.SearchById(id);

            CreateBillViewModel model = new CreateBillViewModel();
            // model.idStaff = _staffService.
            model.idDeliveryOrder = id;
            model.createdDate = DateTime.Now;
            model.idDistributor = dOrder.idDistributor;
            model.nameDistributor = dOrder.Distributor.name;
            model.idDeliveryOrder = dOrder.idDeliveryOrder;
            return View(model);
        }
    }
}
