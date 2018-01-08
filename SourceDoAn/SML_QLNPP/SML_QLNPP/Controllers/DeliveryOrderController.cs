using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
using System.Collections;
using SML_QLNPP.Models;

namespace SML_QLNPP.Controllers
{
    public class DeliveryOrderController : BaseController
    {
        private readonly IDeliveryOrderService _deliveryOrderService;
        private readonly IOrderService _orderService;
        private readonly IStaffService _staffService;
        private readonly IPromotionService _promotionService;
        private readonly IProductService _productService;
        private readonly IDistributorService _disService;
        public DeliveryOrderController(IDistributorService disService, IProductService productService,
            IPromotionService promotionService, IDeliveryOrderService deliveryOrderService,
            IOrderService orderService, IStaffService staffService)
        {
            this._deliveryOrderService = deliveryOrderService;
            this._orderService = orderService;
            this._staffService = staffService;
            this._promotionService = promotionService;
            this._productService = productService;
            this._disService = disService;
        }

        //GET:
        [HttpGet]
        public ActionResult Create(int id)
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập đơn giao hàng";
            // kt nếu đơn đặt hàng đã lập đơn giao rồi thì thông báo, điều hướng đến trang chi tiết giao hàng
            var order = _orderService.GetOrder(id);
            var firdOrder = order.DeliveryOrders.FirstOrDefault();
            if (firdOrder != null)
            {
                return RedirectToAction("Details", new { id = firdOrder.idDeliveryOrder });
            }
            else
            {
                var contractList = order.Distributor.Contracts.ToList();
                var nowContract = contractList.OrderByDescending(i => i.expiredDate).FirstOrDefault();
                var commission = 0;
                if (nowContract.status == true)
                {
                    commission = nowContract.commission.Value;
                }
                var model = new CreateDeliveryOrderViewModel
                {
                    idOrder = order.idOrder,
                    description = "Vừa lập đơn giao, nv giao nhanh chóng kiểm kê và đi giao",
                    idDistributor = order.idDistributor,
                    distributorName = order.Distributor.name,
                    DeliveryType = order.DeliveryType,
                    PaymentType = order.PaymentType,
                    estimateDateOfDelivery = order.EstimateDateOfDelivery,
                    recipient = order.Consignee.Name,
                    recipientPhone = order.Consignee.PhoneNumber,
                    totalPurchase = 0,
                    deliveryDate = DateTime.Now,
                    commission = commission
                };

                // lấy km tốt nhất của order
                int nPromotion = 0;
                Promotion promotion = getPromotionOrder(order, out nPromotion);

                List<DetailedDeliveryOrder> listddOrder = new List<DetailedDeliveryOrder>();
                foreach (var item in order.OrderDetails)
                {
                    var ddOrder = new DetailedDeliveryOrder
                    {
                        idDeliveryOrder = model.idDeliveryOrder,
                        idProduct = item.idProduct,
                        quantity = item.quantity,
                        Product = item.Product,
                        promoQuantity = 0,
                        //idDeliveryOrder =1
                    };
                    listddOrder.Add(ddOrder);
                    model.totalPurchase += ddOrder.quantity * ddOrder.Product.Price;
                }
                // đưa sp km vào listddOrder
                foreach (var item in promotion.PromotionGifts)
                {
                    bool check = true;
                    foreach (var item1 in listddOrder)
                    {
                        if (item.idProduct == item1.idProduct)
                        {
                            listddOrder.Remove(item1);
                            item1.promoQuantity = item.quantity * nPromotion;
                            item1.note = "SLKM " + item.quantity * nPromotion + " từ CTKM số " + item.idPromotion;
                            listddOrder.Add(item1);
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        var ddOrder = new DetailedDeliveryOrder
                        {
                            idProduct = item.idProduct,
                            quantity = 0,
                            promoQuantity = item.quantity * nPromotion,
                            Product = item.Product,
                            note = "SLKM " + item.quantity * nPromotion + " từ CTKM số " + item.idPromotion
                            //idDeliveryOrder =1
                        };
                        listddOrder.Add(ddOrder);
                    }
                }
                model.DetailedDeliveryOrder = listddOrder;
                // những thuộc tính cần chọn khi lập đơn giao hàng
                ViewBag.Storage = order.Distributor.Storages.ToList();
                ViewBag.Staff = _staffService.GetAll().ToList();
                //  tính chiết khấu
                model.totalPurchase = model.totalPurchase * (1 - model.commission / 100);
                return View(model);
            }
        }
        // 
        public Promotion getPromotionOrder(Order order, out int nPromotion)
        {
            var promotion = _promotionService.Search(0, "", "");
            int idBestPromotion = -1;
            decimal valueBestPromotion = -1;
            int nBestPromotion = -1;
            // lấy ds km
            foreach (var item in promotion)
            {
                int count = 0;
                decimal value = 0;
                // list lưu các bội số của km
                List<int> narray = new List<int>();
                // kt order có thỏa dk km hay không
                if (item.startDate <= order.CreatedDate && item.endDate >= order.CreatedDate)
                {
                    foreach (var item1 in item.PromotionProducts)
                    {
                        foreach (var item2 in order.OrderDetails)
                        {
                            // kt mã sp mua và sl của nó
                            // tính bội số của sp khuyến mãi
                            int temp = Int16.Parse((item2.quantity / item1.Quantity).ToString());
                            if (item1.idProduct == item2.idProduct && temp >= 1)
                            {
                                count++;
                                narray.Add(temp);
                                break;
                            }
                        }
                    }
                }
                // tính tổng giá trị của km này nếu có
                foreach (var item3 in item.PromotionGifts)
                {
                    value += Decimal.Parse((item3.Product.Price * item3.quantity).ToString());
                }

                // nếu order đủ dk của km, và đây là km tốt nhất thì lấy km này
                if (count == item.PromotionProducts.Count && count != 0)
                {
                    int n = narray.Max();
                    if (valueBestPromotion < value * n)
                    {
                        idBestPromotion = item.idPromotion;
                        valueBestPromotion = value;
                        // lấy bội số km lớn nhất
                        nBestPromotion = n;
                    }
                }
            }
            nPromotion = nBestPromotion;
            return _promotionService.GetPromotion(idBestPromotion);
        }
        [HttpPost]
        public ActionResult Create(CreateDeliveryOrderViewModel model)
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập đơn giao hàng";

            // những thuộc tính cần lựa chọn khi lập đơn giao hàng
            ViewBag.Storage = _disService.SearchByID(model.idDistributor.Value).Storages.ToList();
            ViewBag.Staff = _staffService.GetAll().ToList();

            DeliveryOrder dOrder = new DeliveryOrder();
            dOrder.deliveryAdd = model.deliveryAdd;
            dOrder.description = model.description;
            dOrder.deliveryDate = model.deliveryDate;
            dOrder.idOrder = model.idOrder;
            dOrder.idDistributor = model.idDistributor;
            dOrder.idStaff = model.idStaff;
            dOrder.recipient = model.recipient;
            dOrder.recipientPhone = model.recipientPhone;
            dOrder.status = 5;
            dOrder.updateDate = DateTime.Now;
            dOrder.deliveryDate = DateTime.Now;
            dOrder.totalPurchase = model.totalPurchase;
            //dOrder.idDeliveryOrder = _deliveryOrderService.GenerateDOrderId();

            // lits product lưu giữ sp để cập nhật số lượng sau khi lập giao hàng
            List<Product> ListProduct = new List<Product>();
            // mặc định sl trong kho đủ giao
            bool checkStorage = true;
            foreach (var item in model.DetailedDeliveryOrder)
            {
                // add chi tiết giao vào đơn giao hàng
                //item.idDeliveryOrder = dOrder.idDeliveryOrder;
                dOrder.DetailedDeliveryOrders.Add(item);
                // kiểm tra lượng tồn kho
                var p = _productService.GetProduct(item.idProduct);
                int sl = p.Quantity.Value - item.quantity.Value - item.promoQuantity.Value;
                if (sl > 0)
                {
                    p.Quantity = sl;
                    ListProduct.Add(p);
                }
                else
                {
                    ViewBag.msg = "Sản phẩm " + p.ProductName + " chỉ còn " + p.Quantity;
                    ViewBag.types = 1;
                    checkStorage = false;
                }
            }
            // nếu trong kho đủ sl
            if (checkStorage)
            {
                bool rs = true;
                rs = _deliveryOrderService.AddDeliveryOrder(dOrder);
                if (rs)
                {
                    // cập nhật sl sp trong kho
                    foreach (var item in ListProduct)
                    {
                        _productService.UpdateProduct(item);
                    }
                    // lập thành công chuyển qua trang chi tiết
                    return RedirectToAction("Details", new { id = dOrder.idDeliveryOrder });
                }
                ViewBag.msg = "Lập thất bại";
                ViewBag.types = 1;
            }
            // lập thất bại, chèn Product và chi tiết(ngoài view cần lấy name, price)
            model.DetailedDeliveryOrder.Clear();
            foreach (var item in dOrder.DetailedDeliveryOrders)
            {
                item.Product = _productService.GetProduct(item.idProduct);
                model.DetailedDeliveryOrder.Add(item);
            }
            return View(model);
        }
        //GET: DeliveryOrder
        public ActionResult List(DeliveryOrderViewModel model)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Tìm kiếm";
            return View(model);
        }

        // GET: DeliveryOrder
        [HttpGet]
        public ActionResult Search(DeliveryOrderViewModel model)
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Tìm kiếm";
            try
            {
                if (model.idDeliveryOrder > 0)
                {
                    var t = _deliveryOrderService.SearchById(model.idDeliveryOrder);
                    if (t != null)
                    {
                        IList<DeliveryOrder> temp = new List<DeliveryOrder>();
                        temp.Add(t);
                        model.listDeliveryOrder = temp.ToList();
                    }
                    return View("List", model);
                }
                else if (model.deliveryDate != null)
                {
                    DateTime delivery_date = Convert.ToDateTime(model.deliveryDate);
                    model.listDeliveryOrder = _deliveryOrderService.SearchByDeliveryDate(delivery_date);
                    return View("List", model);
                }
                else if (model.status != 0)
                {
                    //byte status = Convert.ToByte(dOrder.status);
                    model.listDeliveryOrder = _deliveryOrderService.SearchByStatus(model.status);
                    View("List", model);
                }
                else
                {
                    model.listDeliveryOrder = _deliveryOrderService.GetAll();
                    return View("List", model);
                }
            }
            catch (FormatException e)
            {
                throw;
            }
            return View("List");
        }

        // GET: DeliveryOrder/Details/5. xem thông tin chi tiết của một đơn giao hàng
        public ActionResult Details(int id)
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Chi tiết";
            try
            {
                var dOrder = _deliveryOrderService.SearchById(id);
                if (dOrder == null)
                {
                    return Redirect("/DeliveryOrder/List");
                }
                else
                {
                    DetailedDeliveryOrderViewModel ddOrderVM = new DetailedDeliveryOrderViewModel();
                    ddOrderVM.deliveryType = dOrder.Order.DeliveryType;
                    ddOrderVM.estimateDateOfDelivery = dOrder.Order.EstimateDateOfDelivery;
                    ddOrderVM.idDeliveryOrder = dOrder.idDeliveryOrder;
                    ddOrderVM.idDistributor = dOrder.idDistributor;
                    ddOrderVM.idOrder = dOrder.idOrder.Value;
                    ddOrderVM.idStaff = dOrder.idStaff;
                    ddOrderVM.nameDistributor = dOrder.Distributor.name;
                    ddOrderVM.nameStaff = dOrder.Staff.staffName;
                    ddOrderVM.paymentType = dOrder.Order.PaymentType;
                    ddOrderVM.recipient = dOrder.recipient;
                    ddOrderVM.recipientPhone = dOrder.recipientPhone;
                    ddOrderVM.DetailedDeliveryOrders = dOrder.DetailedDeliveryOrders.ToList();
                    ddOrderVM.status = dOrder.status;
                    ddOrderVM.description = dOrder.description;
                    ddOrderVM.deliveryDate = dOrder.deliveryDate;
                    ddOrderVM.updateDate = dOrder.updateDate;
                    ddOrderVM.totalPurchase = dOrder.totalPurchase;
                    ddOrderVM.deliveryAdd = dOrder.deliveryAdd;
                    return View(ddOrderVM);
                }
            }
            catch
            {
                throw;
            }
        }

        // POST: xác nhận, ghi nhận trạng trạng thái đơn giao hàng trên view details
        [HttpPost]
        public ActionResult Details(DetailedDeliveryOrderViewModel model)
        {
            try
            {
                isAdminLogged();
                ViewBag.Parent = "Quản lý giao hàng";
                ViewBag.Child = "Chi tiết";
                ViewBag.msg = "Cập nhật thành công";
                ViewBag.types = 2;
                // lấy Delivery từ db dựa vào model.idDelivery
                var delivery = _deliveryOrderService.SearchById(model.idDeliveryOrder);
                model.DetailedDeliveryOrders = delivery.DetailedDeliveryOrders.ToList();

                // cập nhật status, description, updateDate, deliveryDate
                delivery.status = model.status;
                delivery.description = model.description;
                delivery.updateDate = DateTime.Now;
                delivery.deliveryDate = model.deliveryDate;
                delivery.deliveryAdd = model.deliveryAdd;
                delivery.recipient = model.recipient;
                delivery.recipientPhone = model.recipientPhone;
                //delivery.deliveryDate = DateTime.ParseExact(Request["deliveryDate"].ToString(), "dd/MM/yyyy", null);
                // lưu cập nhật xuốn db
                bool result = _deliveryOrderService.UpdateDeliveryOrder(delivery);
                // kiểm tra kq cập nhật và trả về view
                if (!result)
                {
                    ViewBag.msg = "Cập nhật thất bại";
                    ViewBag.types = 1;
                }
                return View("Details", model);
            }
            catch
            {
                throw;
            }
        }

        //[HttpPost]
        ////   [AllowAnonymous]
        //public JsonResult UpdateStatus(int idDeliveryOrder, int status, string description)
        //{
        //    bool result = true;
        //    var rs = "";
        //    var delivery = _deliveryOrderService.SearchById(idDeliveryOrder);
        //    delivery.status = Convert.ToByte(status);
        //    delivery.description = description;
        //    delivery.updateDate = DateTime.Now;
        //    result = _deliveryOrderService.UpdateDeliveryOrder(delivery);
        //    if (result)
        //    {
        //        rs = "true";
        //    }
        //    else
        //    {
        //        rs = "false";
        //    }

        //    return Json(new
        //    {
        //        success = rs
        //    });
        //}
    }
}
