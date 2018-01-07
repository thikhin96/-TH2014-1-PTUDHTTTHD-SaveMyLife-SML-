using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
using SML_QLNPP.Models;
using NLog;

namespace SML_QLNPP.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public OrderController(IOrderService orderService, IProductService productService)
        {
            this._orderService = orderService;
            _productService = productService;
        }
        // GET: Order
        public ActionResult List()
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý đặt hàng";
            ViewBag.Child = "Tìm kiếm đơn hàng";
            return View();
        }
        public ContentResult Search(string keyword, string created_date, int status)
        {
            isAdminLogged();
            IList<Order> rs = new List<Order>();
            if (Request.IsAjaxRequest())
            {
                rs = _orderService.SearchOrder(keyword, created_date, status);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idOrder, x.Distributor.name, x.Total, x.CreatedDate, x.Staff.staffName, x.Statuses }));
                return Content(list, "application/json");
            }

            return null;
        }
        public ActionResult Detail(int id)
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý đặt hàng";
            ViewBag.Child = "Chi tiết đơn đặt hàng";
            var model = _orderService.GetOrder(id);
            if (model == null)
            {
                return Redirect("List");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Update(int id)
        {
            _logger.Info("Start Update(GET) - OrderController");
            CheckForAuthorization();
            ViewBag.Title = "Chỉnh sửa đơn đặt hàng | Nhà phân phối sữa Vitamilk | Nhà phân phối sữa hàng đầu Việt Nam";
            var order = _orderService.GetOrder(id);
            var model = new CreateOrderViewModel();
            if (order != null) 
            {
                model = new CreateOrderViewModel
                {
                    idOrder = order.idOrder,
                    idDistributor = order.idDistributor,
                    distributorName = order.Distributor.name,
                    Total = order.Total,
                    PaymentType = order.PaymentType.ToString(),
                    DeliveryType = order.DeliveryType.ToString(),
                    EstimateDateOfDelivery = order.EstimateDateOfDelivery,
                    OrderDetails = order.OrderDetails.ToList(),
                    Consignee = order.Consignee,
                    Action = "Update",
                    Products = _productService.GetAllProducts(),
                    Statuses = order.Statuses
                };
                _logger.Info("Success: Complete Update(GET) - OrderController");
            }
            else
            {
                _logger.Info("Error: Update(GET) - OrderController | Can not find any order with given id!");
            }
            return View("Create", model);

        }

        [HttpPost]
        public ActionResult Update(CreateOrderViewModel model, [Bind(Prefix = "OrderDetails")]List<OrderDetail> OrderDetails)
        {
            _logger.Info("Start Update(POST) - OrderController");
            if (GetCurrentUser() != null)
            {
                var order = new Order
                {
                    idOrder = model.idOrder,
                    idDistributor = model.idDistributor,
                    Total = model.Total,
                    DeliveryType = Convert.ToBoolean(model.DeliveryType),
                    PaymentType = Convert.ToBoolean(model.PaymentType),
                    EstimateDateOfDelivery = model.EstimateDateOfDelivery,
                    Statuses = model.Statuses,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Consignee = model.Consignee,
                    idConsignee = model.Consignee.idConsignee,
                    OrderDetails = model.OrderDetails,
                    idStaff = model.idStaff
                };
                var result = _orderService.UpdateOrder(order);
                if (result == "thanh cong")
                    TempData["success"] = "thanh cong";
                else
                    TempData["fail"] = result;
                return RedirectToAction("Update", new { id = model.idOrder });
            }
            _logger.Info("Success: Complete Update(POST) - OrderController");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            _logger.Info("Start Create(GET) - OrderController");
            CheckForAuthorization();
            ViewBag.Title = "Thêm đơn đặt hàng | Nhà phân phối sữa Vitamilk | Nhà phân phối sữa hàng đầu Việt Nam";
            var model = new CreateOrderViewModel()
            {
                idOrder = _orderService.GenerateOrderId(),
                OrderDetails = new List<OrderDetail>(),
                Action = "Create"
            };
            model.Products = _productService.GetAllProducts();
            _logger.Info("Success: Complete Create(GET) - OrderController");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateOrderViewModel model, [Bind(Prefix = "OrderDetails")]List<OrderDetail> OrderDetails)
        {
            _logger.Info("Start Create(POST) - OrderController");
            var loggedUser = GetCurrentUser() as Account;
            if (loggedUser != null)
            {
                model.Products = _productService.GetAllProducts();
                var order = new Order
                {
                    idDistributor = model.idDistributor,
                    Total = model.Total,
                    DeliveryType = Convert.ToBoolean(model.DeliveryType),
                    PaymentType = Convert.ToBoolean(model.PaymentType),
                    EstimateDateOfDelivery = model.EstimateDateOfDelivery,
                    Statuses = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Consignee = new Consignee
                    {
                        Name = model.Consignee.Name,
                        PhoneNumber = model.Consignee.PhoneNumber,
                        idDistributor = model.idDistributor
                    },
                    OrderDetails = OrderDetails,
                    idStaff = loggedUser.idUser
                };
                var result = _orderService.CreateOrder(order);
                if (result == "thanh cong")
                {
                    TempData["success"] = "thanh cong";
                    model = new CreateOrderViewModel();
                }
                else
                    TempData["fail"] = result;
                return RedirectToAction("Create");
            }
            _logger.Info("Success: Complete Create(POST) - OrderController");
            return RedirectToAction("Index", "Home");
        }
    }
}