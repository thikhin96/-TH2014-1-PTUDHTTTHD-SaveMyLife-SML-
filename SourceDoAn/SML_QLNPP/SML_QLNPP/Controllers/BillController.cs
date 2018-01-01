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

namespace SML_QLNPP.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillService _billService;
        private readonly IDeliveryOrderService _dOrderService;
        private readonly IDistributorService _distributoriervice;
        public BillController(IBillService billService, IDeliveryOrderService dOrderService, IDistributorService distributorBservice)
        {
            this._billService = billService;
            this._dOrderService = dOrderService;
            this._distributoriervice = distributorBservice;
        }
        //POST: Bill/Create
        [HttpPost]
        public ActionResult Create(Bill bill)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";

            var delivery = _dOrderService.SearchById(Int32.Parse(bill.idDeliveryOrder.ToString()));
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
                    if (delivery.Order.PaymentType == true)
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
        public ActionResult Create()
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";
            return View();
        }
    }
}
