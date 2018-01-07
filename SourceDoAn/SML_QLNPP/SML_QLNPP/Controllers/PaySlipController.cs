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
    public class PaySlipController : BaseController
    {
        private readonly IPaySlipService _paySlipService;
        private readonly IDistributorService _distributorService;
        private readonly IDebtService _debtService;
        private readonly IReturnService _returnService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PaySlipController(IPaySlipService paySlipService, IDistributorService distributorService, IDebtService debtService)
        {
            this._paySlipService = paySlipService;
            _distributorService = distributorService;
            _debtService = debtService;
        }
        // GET: PaySlip
        public ActionResult Create(int DebtId)
        {
            isAdminLogged();
            var currentUser = Session["admin"] as Account;
            var request = _debtService.GetDebt(DebtId);
            //var rq = _returnService.GetReturnCardDetail(ReturnId);
            //var dis = _distributorService.SearchByID(request.Distributor.idDistributor);
            var model = new CreatePaySlipViewModel();
            if (request != null)
            {

                model = new CreatePaySlipViewModel()
                {
                    idPaySlip = _paySlipService.GeneratePaySlipId(),
                    idDistributor = request.Distributor.idDistributor,
                    nameDistributor = request.Distributor.name,
                    //moneyPay = rq.Total.GetValueOrDefault() - dis.debt.GetValueOrDefault(),
                    staffName = request.Staff.staffName
                };
                return View(model);
            }
            else
            {
                return Redirect("/Account/ALogin");
            }
        }
        [HttpPost]
        public ActionResult Create(CreatePaySlipViewModel model)
        {
            isAdminLogged();
            var currentUser = Session["admin"] as Account;
            var paySlip = new PaySlip
            {
                CreatedDate = DateTime.Now,
                idDistributor = model.idDistributor,
                idStaff = currentUser.idUser,
                AmountSpent = model.moneyPay,
                SpendingReasons = model.reason
            };
            var result = _paySlipService.CreatePaySlip(paySlip);
            if (result == "thanh cong")
                TempData["success"] = "thanh cong";
            else
                TempData["fail"] = result;
            return RedirectToAction("ReturnRequest/List");
        }
    }
}