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
        private readonly IStaffService _staffService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PaySlipController(IPaySlipService paySlipService, IDistributorService distributorService, IStaffService staffService)
        {
            this._paySlipService = paySlipService;
            _distributorService = distributorService;
            _staffService = staffService;
        }
        // GET: PaySlip
        public ActionResult Create()
        {
            isAdminLogged();
            var currentUser = Session["admin"] as Account;
            Staff staff = _staffService.GetByAccount(currentUser.UserName);
            var model = new CreatePaySlipViewModel()
            {
                date = DateTime.Now,
                idDistributor = null,
                nameDistributor = null,
                moneyPay = null,
                reason =null,
                idPaySlip = _paySlipService.GeneratePaySlipId(),
                staffName = staff.staffName
            };
            return View(model);
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
                idStaff = _staffService.GetByAccount(currentUser.UserName).idStaff,
                AmountSpent = model.moneyPay,
                SpendingReasons = model.reason
            };
            var result = _paySlipService.CreatePaySlip(paySlip);
            if (result == "thanh cong")
                TempData["success"] = "thanh cong";
            else
                TempData["fail"] = result;
            return RedirectToAction("Create", new { PaySlipId = model.idPaySlip});
        }
    }
}