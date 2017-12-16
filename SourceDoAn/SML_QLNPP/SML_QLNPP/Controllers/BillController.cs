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
        // GET: Bill
        //public ActionResult Index()
        //{
        //    //var bills = 
        //        //db.Bills.Include(b => b.DeliveryOrder).Include(b => b.Distributor).Include(b => b.Staff);
        //    //return View(bills.ToList());
        //}

        //// GET: Bill/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bill bill = db.Bills.Find(id);
        //    if (bill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bill);
        //}

        //POST: Bill/Create
        [HttpPost]
        public ActionResult Create(Bill bill)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";
            return View();
        }
        // GET: Bill/Create
        public ActionResult Create()
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Lập hóa đơn";
            return View();
        }
        //// POST: Bill/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idBill,purchase,createdDate,types,description,idDeliveryOrder,idStaff,idDistributor")] Bill bill)
        //{

        //     _billService.AddBill(bill);
        //    //ViewBag.idDeliveryOrder = new SelectList(db.DeliveryOrders, "idDeliveryOrder", "recipient", bill.idDeliveryOrder);
        //    //ViewBag.idDistributor = new SelectList(db.Distributors, "idDistributor", "name", bill.idDistributor);
        //    //ViewBag.idStaff = new SelectList(db.Staffs, "idStaff", "staffName", bill.idStaff);
        //    return View(bill);
        //}

        //// GET: Bill/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bill bill = db.Bills.Find(id);
        //    if (bill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.idDeliveryOrder = new SelectList(db.DeliveryOrders, "idDeliveryOrder", "recipient", bill.idDeliveryOrder);
        //    ViewBag.idDistributor = new SelectList(db.Distributors, "idDistributor", "name", bill.idDistributor);
        //    ViewBag.idStaff = new SelectList(db.Staffs, "idStaff", "staffName", bill.idStaff);
        //    return View(bill);
        //}

        //// POST: Bill/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idBill,purchase,createdDate,types,description,idDeliveryOrder,idStaff,idDistributor")] Bill bill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bill).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.idDeliveryOrder = new SelectList(db.DeliveryOrders, "idDeliveryOrder", "recipient", bill.idDeliveryOrder);
        //    ViewBag.idDistributor = new SelectList(db.Distributors, "idDistributor", "name", bill.idDistributor);
        //    ViewBag.idStaff = new SelectList(db.Staffs, "idStaff", "staffName", bill.idStaff);
        //    return View(bill);
        //}

        //// GET: Bill/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Bill bill = db.Bills.Find(id);
        //    if (bill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bill);
        //}

        //// POST: Bill/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Bill bill = db.Bills.Find(id);
        //    db.Bills.Remove(bill);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
