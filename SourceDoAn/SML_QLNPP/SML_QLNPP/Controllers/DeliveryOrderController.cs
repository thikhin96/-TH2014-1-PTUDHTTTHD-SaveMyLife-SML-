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
        [HttpPost]
        public ActionResult List(FormCollection form)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Tìm kiếm";
            
            string str_idDelivery = Request.Form["idDelivery"];
            string str_status = Request.Form["status"];
            string str_delivery_date = Request.Form["delivery_date"];

            //int idDelivery = Convert.ToInt32();
            //byte status = Convert.ToByte();
            //DateTime delivery_date = Convert.ToDateTime();

            try
            {
                if (str_idDelivery != "")
                {
                    int idDelivery = Convert.ToInt32(str_idDelivery);
                    var model = _deliveryOrderService.SearchById(idDelivery);
                    IList<DeliveryOrder> list = new List<DeliveryOrder>();
                    list.Add(model);
                    return View("List", list);
                }
                else if (str_delivery_date != "")
                {
                    DateTime delivery_date= Convert.ToDateTime(str_delivery_date);
                    var model = _deliveryOrderService.SearchByDeliveryDate(delivery_date);
                    return View("List", model);
                }
                else if (str_status !="0")
                {
                    byte status = Convert.ToByte(str_status);
                    var model = _deliveryOrderService.SearchByStatus(status);
                    return View("List", model);
                }else
                {
                    var model = _deliveryOrderService.GetAll();
                    return View("List", model);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return View();
        }
        // GET: DeliveryOrder/Details/5
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

        [HttpPost]
        //   [AllowAnonymous]
        public JsonResult UpdateStatus(int idDeliveryOrder, int status, string description)
        {
            bool result = true;
            var rs = "";
            var delivery = _deliveryOrderService.SearchById(idDeliveryOrder);
            delivery.status = Convert.ToByte(status);
            delivery.description = description;
            delivery.updateDate = DateTime.Now;
            result = _deliveryOrderService.UpdateDeliveryOrder(delivery);
            if (result)
            {
                rs = "true";
            }
            else
            {
                rs = "false";
            }

            return Json(new
            {
                success = rs
            });
        }
        // GET: DeliveryOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryOrder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DeliveryOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeliveryOrder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /*
// GET: DeliveryOrder/Delete/5
public ActionResult Delete(int id)
{
    return View();
}

// POST: DeliveryOrder/Delete/5
[HttpPost]
public ActionResult Delete(int id, FormCollection collection)
{
    try
    {
        // TODO: Add delete logic here

        return RedirectToAction("Index");
    }
    catch
    {
        return View();
    }
}
*/
    }
}
