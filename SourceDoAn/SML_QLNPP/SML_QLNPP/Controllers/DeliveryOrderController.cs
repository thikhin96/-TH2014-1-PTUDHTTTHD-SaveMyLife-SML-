using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
namespace SML_QLNPP.Controllers
{
    public class DeliveryOrderController : Controller
    {
        private readonly IDeliveryOrderService _deliveryOrderService;
        public DeliveryOrderController(IDeliveryOrderService deliveryOrderService)
        {
            this._deliveryOrderService = deliveryOrderService;
        }
        // GET: DeliveryOrder
        public ContentResult Search()
        {
            IList<DeliveryOrder> rs = new List<DeliveryOrder>();
            if (Request.IsAjaxRequest())
            {
                rs = _deliveryOrderService.GetAll();
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idDeliveryOrder, x.Distributor.name, x.totalPurchase, x.deliveryDate, x.Staff.staffName, x.status }));
                return Content(list, "application/json");
            }
            return null;
        }

        // GET: DeliveryOrder
        public ActionResult List()
        {
            var model = _deliveryOrderService.GetAll();
            return View(model);
        }

        // GET: DeliveryOrder/Details/5
        public ActionResult Details(int id)
        {
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
        /*
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
