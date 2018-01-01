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
    public class DeliveryOrderController : Controller
    {
        private readonly IDeliveryOrderService _deliveryOrderService;
        private readonly IOrderService _orderService;
        private readonly IStaffService _staffService;
        private readonly IPromotionService _promotionService;
        public DeliveryOrderController(IPromotionService promotionService, IDeliveryOrderService deliveryOrderService, IOrderService orderService, IStaffService staffService)
        {
            this._deliveryOrderService = deliveryOrderService;
            this._orderService = orderService;
            this._staffService = staffService;
            this._promotionService = promotionService;
        }

        //GET:
        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập đơn giao hàng";
            // kt nếu đơn đặt hàng đã lập đơn giao rồi thì thông báo, điều hướng đến trang chi tiết giao hàng
            var order = _orderService.GetOrder(id);
            if (order.DeliveryOrder !=null)
            {
                return Redirect("/DeliveryOrder/Details/"+order.DeliveryOrder.idDeliveryOrder);
            }
            else
            {
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
                    totalPurchase = 0
                };

                List<DetailedDeliveryOrder> listddOrder = new List<DetailedDeliveryOrder>();
                foreach (var item in order.OrderDetails)
                {
                    var ddOrder = new DetailedDeliveryOrder
                    {
                        idProduct = item.idProduct,
                        quantity = item.quantity,
                        Product = item.Product,
                        //idDeliveryOrder =1
                    };
                    listddOrder.Add(ddOrder);
                    model.totalPurchase += ddOrder.quantity * ddOrder.Product.Price;
                }
                model.DetailedDeliveryOrder = listddOrder;
                    
                
               // model.DetailedDeliveryOrder.
               // model.OrderDetails = order.OrderDetails.ToList();
                

                // những thuộc tính cần chọn khi lập đơn giao hàng
                model.Storage = order.Distributor.Storages.ToList();
                model.Staff = _staffService.GetAll().ToList();
                // lấy km tốt nhất của order
                int nPromotion = 0;
                Promotion promotion = getPromotionOrder(order, out nPromotion);
                //
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
                    foreach(var item1 in item.PromotionProducts)
                    {
                        foreach(var item2 in order.OrderDetails)
                        {
                            // kt mã sp mua và sl của nó
                            // tính bội số của sp khuyến mãi
                            int temp = Int16.Parse((item2.quantity / item1.Quantity).ToString());
                            if (item1.idProduct == item2.idProduct && temp >=1 )
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
                if (count == item.PromotionProducts.Count && count !=0)
                {
                    int n = narray.Max();
                    if (valueBestPromotion < value * n)
                    {
                        idBestPromotion = item.idPromotion;
                        valueBestPromotion = value;
                        // lấy bội số km lớn lớn
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
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập đơn giao hàng";
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
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child =  "Tìm kiếm";
            
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
                Console.WriteLine(e.Message);
            }
            return View("List");
        }

        // GET: DeliveryOrder/Details/5. xem thông tin chi tiết của một đơn giao hàng
        public ActionResult Details(int id)
        {
            ViewBag.Parent ="Quản lý giao hàng";
            ViewBag.Child = "Chi tiết";
            var model = _deliveryOrderService.SearchById(id);
            if (model == null)
            {
                return Redirect("/DeliveryOrder/List");
            }
            else
            {
                return View(model);
            }
        }

        // POST: xác nhận, ghi nhận trạng trạng thái đơn giao hàng trên view details
        [HttpPost]
        public ActionResult Details(DeliveryOrder model)
        {
            try
            {
                ViewBag.Parent = "Quản lý giao hàng";
                ViewBag.Child = "Chi tiết";
                // lấy Delivery từ db dựa vào model.idDelivery
                var delivery = _deliveryOrderService.SearchById(model.idDeliveryOrder);

                if (model.status == delivery.status)
                {
                    ViewBag.Status = "Chưa thay đổi tình trạng!";
                    ViewBag.types = 1;
                    return View("Details", delivery);
                }

                // cập nhật satus, description, updateDate
                delivery.status = model.status;
                delivery.description = model.description;
                delivery.updateDate = DateTime.Now;
                // lưu cập nhật xuốn db
                bool result = _deliveryOrderService.UpdateDeliveryOrder(delivery);
                // kiểm tra kq cập nhật và trả về view
                if (result)
                {
                    ViewBag.Status = "Cập nhật thành công";
                    ViewBag.types = 2;
                    return View("Details", delivery);
                }
                else
                {
                    return View("List");
                }  
            }
            catch
            {
                return View();
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
