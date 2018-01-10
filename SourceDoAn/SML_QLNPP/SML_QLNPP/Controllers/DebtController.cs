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
    public class DebtController : BaseController
    {
        private readonly IDebtService _debtService;
        private readonly IDistributorService _distributorService;
        private readonly IStaffService _staffService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public DebtController(IStaffService staffService, IDebtService debtService, IDistributorService distributorService)
        {
            this._debtService = debtService;
            _distributorService = distributorService;
            _staffService = staffService;
        }
        [HttpGet]
        public ActionResult Create(int idDistributor)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Thanh Toán Công nợ";
            isAdminLogged();
            CreateDebtViewModel model = new CreateDebtViewModel();
            var user = Session["admin"] as Account;
            Staff staff = _staffService.GetByAccount(user.UserName);
            var distributor = _distributorService.GetDistributor(idDistributor);

            model.idDebt = _debtService.GenerateDebtId();
            model.idStaff = staff.idStaff;
            model.staffName = staff.staffName;
            model.idDistributor = distributor.idDistributor;
            model.nameDistributor = distributor.name;
            model.createdDate = DateTime.Now;
            model.debt = distributor.debt.Value;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CreateDebtViewModel model)
        {
            ViewBag.Parent = "Quản lý giao hàng";
            ViewBag.Child = "Thanh Toán Công nợ";
            isAdminLogged();
            var user = Session["admin"] as Account;
            Staff staff = _staffService.GetByAccount(user.UserName);
            var debt = new Debt
            {
                CreatedDate = DateTime.Now,
                Purchase = model.moneyDebt,
                idDistributor = model.idDistributor,
                idStaff = staff.idStaff,
            };
            var result = _debtService.CreateDebt(debt);
            if (result == "thanh cong")
            {
                var dis = _distributorService.GetDistributor(model.idDistributor);
                dis.debt -= model.moneyDebt;
                _distributorService.UpdateDebt(dis.idDistributor, dis.debt.Value);
                model.debt = dis.debt.Value;
                model.idDebt = debt.idDebt;
                ViewBag.types = 2;
                ViewBag.msg = "Lập phiếu công nợ thành công";
                return RedirectToAction("Create", new { idDistributor = model.idDistributor });
            }
            else
            {
                ViewBag.types = 1;
                ViewBag.msg = "Lập phiếu công nợ thất bại";
                return View(model);
            }
        }
        //// GET: Debt
        //public ActionResult Create(int ReturnId)
        //{
        //    isAdminLogged();
        //    var currentUser = Session["admin"] as Account;
        //    var request = _returnService.GetReturnCardDetail(ReturnId);
        //    var dis = _distributorService.SearchByID(request.Distributor.idDistributor);
        //    var model = new CreateDebtViewModel();
        //    if (request != null)
        //    {

        //        model = new CreateDebtViewModel()
        //        {
        //            idDebt = _debtService.GenerateDebtId(),
        //            idDistributor = request.Distributor.idDistributor,
        //            nameDistributor = request.Distributor.name,
        //            debt = dis.debt.GetValueOrDefault(),
        //            staffName = request.Staff.staffName
        //        };
        //        if (dis.debt.GetValueOrDefault() >= request.Total)
        //        {
        //            model.moneyDebt = Convert.ToDecimal(request.Total);
        //            model.newDebt = dis.debt.GetValueOrDefault() - Convert.ToDecimal(request.Total);
        //        }    
        //        else
        //        {
        //            model.moneyDebt = dis.debt.GetValueOrDefault();
        //            model.newDebt = 0;
        //        }
        //        return View(model);
        //    }
        //    else
        //    {
        //        return Redirect("/Account/Login");
        //    }
        //}
        //[HttpPost]
        //public ActionResult Create(CreateDebtViewModel model)
        //{
        //    isAdminLogged();
        //    var currentUser = Session["admin"] as Account;
        //    var debt = new Debt
        //    {
        //        CreatedDate = DateTime.Now,
        //        Purchase = model.moneyDebt,
        //        idDistributor = model.idDistributor,
        //        idStaff = currentUser.idUser
        //    };
        //    var result = _debtService.CreateDebt(debt); 
        //    if (result == "thanh cong")
        //    {
        //        var dis = _distributorService.SearchByID(model.idDistributor);
        //        dis.debt -= model.moneyDebt;
        //        TempData["success"] = "thanh cong";
        //    }    
        //    else
        //        TempData["fail"] = result;
        //    return Redirect("/PaySlip/Create?DebtId=" + model.idDebt);
        //}
    }
}