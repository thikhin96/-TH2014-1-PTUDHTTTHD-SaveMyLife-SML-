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
        public BillController(IBillService billService)
        {
            this._billService = billService;
        }
        //POST: Bill/Create
        [HttpPost]
        public ActionResult Create(Bill bill)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";
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
