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

        ContractController (IContractService sv_Contract)
        {
            this.con_Service = sv_Contract;
        }

        // GET: Contract
        public ActionResult Contract()
        {
            logger.Info("Start controller to load list of contract....");
            //ContractViewModel model = new ContractViewModel();
            //model.listCon = con_Service.Search().ToList();
            return View();
        }
/*
        [HttpGet]
        public ActionResult ContractSeacrh(ContractViewModel model)
        {
            logger.Info("Start controller to filter....");
            if (model.keywork == null)
            {
                return Redirect("Contract");
            }
            int keywork;
            bool check_keywork = int.TryParse(model.keywork, out keywork);
            if (!check_keywork)
            {
                ViewBag.CheckKW = false;
            }
            else
            {
                model.listCon = this.con_Service.Search(keywork, model.criterion).ToList();
            }
            model.keywork = null;
            return View("Contract", model);
        }*/
    }
}