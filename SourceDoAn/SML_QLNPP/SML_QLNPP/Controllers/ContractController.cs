using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataService.Interfaces;
using NLog;
using SML_QLNPP.Models;
using DataModel;

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
            model.listCon = con_Service.Search().ToList();
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

        public ActionResult DetailedContract(int id)
        {
            logger.Info("Start controller to display info of contract....");
            Contract con = con_Service.Get(id);
            return View(con);
        }

        [HttpPost]
        public ActionResult CancelContract(Contract con)
        {
            logger.Info("Start controller to cancel a contract...");
            bool result = con_Service.CancelContract(con.idContract, con.note);
            if (!result)
            {
                TempData["message"] = "Thất bại";
            }
            else
            {
                TempData["message"] = "Thành công";
            }
            //thang` detailcontract nay no yeu cau Model la Contract, ma minh truyen qua ID la int no bao loi la dung roi con gi
            // aief v dc 
            return RedirectToAction("DetailedContract", new { id = con.idContract });
        }

        [HttpPost]
        public ActionResult CreateContract(Contract con)
        {
            
            return View();
        }
    }
}