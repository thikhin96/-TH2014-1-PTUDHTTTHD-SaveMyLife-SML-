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
        public DeliveryOrderController(IDeliveryOrderService deliveryOrderService)
        {
            this._deliveryOrderService = deliveryOrderService;
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
