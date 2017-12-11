using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
using SML_QLNPP.Models;

namespace SML_QLNPP.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            this._orderService = orderService;
            _productService = productService;
        }
        // GET: Order
        public ActionResult List()
        {
            ViewBag.Parent = "Quản lý đặt hàng";
            ViewBag.Child = "Tìm kiếm đơn hàng";
            return View();
        }
        public ContentResult Search(string keyword, string created_date,int status)
        {
            IList<Order> rs = new List<Order>();
            if (Request.IsAjaxRequest())
            {
                rs = _orderService.SearchOrder(keyword, created_date, status);
                var list = JsonConvert.SerializeObject(rs.Select(x => new {x.idOrder, x.Distributor.name, x.Total, x.CreatedDate, x.Staff.staffName,x.Statuses }));
                return Content(list, "application/json");
            }

            return null;
        }
        public ActionResult Detail(int id)
        {
            ViewBag.Parent = "Quản lý đặt hàng";
            ViewBag.Child = "Chi tiết đơn đặt hàng";
            var model = _orderService.GetOrder(id);
            if(model == null)
            {
                return Redirect("List");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Create()
        {
            var model = new CreateOrderViewModel()
            {
                idOrder = _orderService.GenerateOrderId(),
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                        idProduct = 1,
                        quantity = 2
                    }
                }
            };
            model.Products = _productService.GetAllProducts();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateOrderViewModel model, [Bind(Prefix="OrderDetails")]List<OrderDetail> OrderDetails)
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
                }
            };
            var orderDetails = model.OrderDetails;
            var result = _orderService.CreateOrder(order, orderDetails);
            if (result == "thanh cong")
                ViewBag.success = "thanh cong";
            else
                ViewBag.fail = result;

            model.idOrder = _orderService.GenerateOrderId();
            return View(model);
        }
    }
}