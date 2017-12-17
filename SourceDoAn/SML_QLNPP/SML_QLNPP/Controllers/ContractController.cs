using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataService.Interfaces;
using NLog;
using SML_QLNPP.Models;

namespace SML_QLNPP.Controllers
{
    public class ContractController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IContractService con_Service;

        public ContractController(IContractService sv_Contract)
        {
            this.con_Service = sv_Contract;
        }

        // GET: Contract
        public ActionResult Contract()
        {
            logger.Info("Start controller to load list of contract....");
            ContractViewModel model = new ContractViewModel();
            //model.listCon = con_Service.Search().ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult ContractSeacrh(ContractViewModel model)
        {
            logger.Info("Start controller to filter....");
            if (model.keyword == null)
            {
                return Redirect("Contract");
            }
            int kw;
            bool check_keywork = int.TryParse(model.keyword, out kw);
            if (!check_keywork)
            {
                ViewBag.CheckKW = false;
            }
            else
            {
                model.listCon = con_Service.Search(kw, model.criterion).ToList();
            }
            model.keyword = null;
            return View("Contract", model);
        }
    }
}