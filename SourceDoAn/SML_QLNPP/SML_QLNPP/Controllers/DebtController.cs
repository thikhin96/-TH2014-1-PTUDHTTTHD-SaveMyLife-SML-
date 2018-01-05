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
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public DebtController(IDebtService debtService, IDistributorService distributorService)
        {
            this._debtService = debtService;
            _distributorService = distributorService;
        }
        // GET: Debt
        public ActionResult Create(int ReturnId)
        {
            isAdminLogged();
            var request = _debtService.GetReturn(ReturnId);
            var model = new CreateDebtViewModel();
            if (request != null)
            {
                model = new CreateDebtViewModel()
                {
                        idDebt = _debtService.GenerateDebtId(),
                        idDistributor = request.Distributor.idDistributor,
                        nameDistributor = request.Distributor.name,
                        //debt = request.Distributor.debt
                };
                return View(model);
            }
            else
            {
                return Redirect("/Account/Login");
            }

        }
    }
}