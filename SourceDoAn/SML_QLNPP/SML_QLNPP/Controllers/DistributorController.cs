using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using DataModel;
using DataService.Interfaces;
using DataService.Dtos;
using SML_QLNPP.Models;

namespace SML_QLNPP.Controllers
{
    public class DistributorController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IDistributorService dis_Service;
        IContractService con_Service;

        public DistributorController(IDistributorService sv_Dis, IContractService sv_Con)
        {
            this.dis_Service = sv_Dis;
            this.con_Service = sv_Con;
        }

        // GET: Distributor
        
        public ActionResult Distributor()
        {
            logger.Info("Start controller to load list of distributors....");
            DistributorViewModel model = new DistributorViewModel();
            model.listDis = dis_Service.GetList(null).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult DistributorSearch(DistributorViewModel model)
        {
            logger.Info("Start controller to filter....");
            int id;
            if (model.id == null)
            {
                return RedirectToRoute("Default", new { controller = "Distributor", action = "Distributor", id = model.id });
            }
            bool check_id = int.TryParse(model.id, out id);
            if  (!check_id)
            {
                ViewBag.CheckID = false;
            }
            else
            {
                model.listDis = dis_Service.GetList(id).ToList();
            }
            model.id = null;
            return View("Distributor",model);
        }

        public ActionResult DetailedDistributor(int id)
        {
            logger.Info("Start controller to display info of distributor....");
            Distributor dis = new Distributor();
            Contract con = new Contract();
            dis = dis_Service.SearchByID(id);
            foreach(Contract _con in dis.Contracts)
            {
                if (_con.status == true)
                {
                    con = _con;
                    break;
                }
            }
            return View(con);
        }
    }
}