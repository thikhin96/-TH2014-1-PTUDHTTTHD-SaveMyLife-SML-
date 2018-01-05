using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataService.Interfaces;
using DataService.Dtos;
using NLog;
using SML_QLNPP.Models;
using DataModel;

namespace SML_QLNPP.Controllers
{
    public class ContractController : BaseController
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IContractService con_Service;
        IDistributorService dis_Service;
        IStaffService staff_Service;
        IPDistributorService pDis_Service;
        IRepresentativeService rep_Service;

        public ContractController(IContractService _serviceContract, IDistributorService _serviceDistributor, IStaffService _serviceStaff, IPDistributorService _servicePDis, IRepresentativeService _serviceRep)
        {
            this.con_Service = _serviceContract;
            this.dis_Service = _serviceDistributor;
            this.staff_Service = _serviceStaff;
            this.pDis_Service = _servicePDis;
            this.rep_Service = _serviceRep;
        }

        // GET: Contract
        public ActionResult Contract()
        {
            isAdminLogged();
            logger.Info("Start controller to load list of contract....");
            ContractListViewModel model = new ContractListViewModel();
            model.listCon = con_Service.Search().ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult ContractSeacrh(ContractListViewModel model)
        {
            logger.Info("Start controller to filter....");
            isAdminLogged();
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
            isAdminLogged();
            Contract con = con_Service.Get(id);
            return View(con);
        }

        [HttpPost]
        public ActionResult CancelContract(Contract con)
        {
            logger.Info("Start controller to cancel a contract...");
            isAdminLogged();
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

        
        public ActionResult CreateContract()
        {
            isAdminLogged();
            ContractDistributorVM model = new ContractDistributorVM();
            
            // Get List of old distributors
            IList<DistributorList> oldDisList = dis_Service.GetList(null, false);
            IList<DistributorViewModel> _oldDisList = new List<DistributorViewModel>();
            DistributorViewModel _oldDis;
            if (oldDisList.Count != 0)
            {
                foreach (DistributorList dis in oldDisList)
                {
                    _oldDis = new DistributorViewModel();
                    _oldDis.address = dis.Dis.address;
                    _oldDis.Email = dis.Dis.Email;
                    _oldDis.idDistributor = dis.Dis.idDistributor;
                    _oldDis.name = dis.Dis.name;
                    _oldDis.phone = dis.Dis.phone;
                    _oldDisList.Add(_oldDis);
                }
                model.oldDis = _oldDisList;
            }

            // Get List of approved potential distributor
            IList<PotentialDistributor> pDisList = pDis_Service.SearchByStatus(3);
            IList<PdisRepViewModel> pDisRepList = new List<PdisRepViewModel>();
            PdisRepViewModel pDisRep;
            if (pDisList.Count != 0)
            {
                foreach( PotentialDistributor item in pDisList)
                {
                    pDisRep = new PdisRepViewModel();
                    pDisRep.pDis.address = item.address;
                    pDisRep.pDis.Email = item.Email;
                    pDisRep.pDis.idDistributor = item.idDistributor;
                    pDisRep.pDis.name = item.name;
                    pDisRep.pDis.phone = item.phone;

                    foreach (Representative rep in item.Representatives)
                    {
                        if (rep.PotentialDistributor.idDistributor == item.idDistributor)
                        {
                            pDisRep.rep.email = rep.email;
                            pDisRep.rep.idRepresentative = rep.idRepresentative;
                            pDisRep.rep.name = rep.name;
                            pDisRep.rep.phone = rep.phone;
                            pDisRep.rep.title = rep.title;
                        }
                    }
                    pDisRepList.Add(pDisRep);
                }
                model.pDis = pDisRepList;
            }

            return View(model);
        }
        
        [HttpPost]
        public ActionResult CreateContract(ContractDistributorVM model, [Bind(Prefix = "ContractViewModel")]ContractViewModel con, [Bind(Prefix = "DistributorViewModel")]DistributorViewModel dis, [Bind(Prefix = "RepresentativeViewModel")]RepresentativeViewModel rep)
        {
            isAdminLogged();
            logger.Info("Start controller to create contract...");
            var user = Session["admin"] as Account;
            bool result;
            var _con = new Contract();

            _con.area = con.area;
            _con.beginDate = con.beginDate;
            _con.commission = con.commission;
            _con.disType = con.disType;
            _con.expiredDate = con.expiredDate;
            _con.maxDebt = con.maxDebt;
            _con.minOrderTotalValue = con.minOrderTotalValue;
            _con.note = con.note;
            _con.staff = staff_Service.GetByAccount(user.UserName).idStaff;

            if (rep.idRepresentative == 0)  // Create contract for old distributor
            {
                Representative _rep = new Representative
                {
                    idRepresentative = rep_Service.GenerateRepresentativeId(),
                    name = rep.name,
                    title = rep.title,
                    phone = rep.phone,
                    email = rep.email,
                    Distributor = dis.idDistributor
                };
                _con.Representative1 = _rep;
                _con.distributor = dis.idDistributor;
                result = con_Service.CreateContract(_con, false);
            }
            else        // Create contract for potential distributor
            {
                Distributor _dis = new Distributor
                {
                    idDistributor = dis_Service.GenerateDistributorId(),
                    name = dis.name,
                    address = dis.address,
                    Email = dis.Email,
                    phone = dis.phone,
                    createdDate = DateTime.Now,
                    updatedDate = DateTime.Now,
                    debt = 0,
                    status = true
                };
                _con.Distributor1 = _dis; // _dis.idDistributor;
                _con.representative = rep.idRepresentative;
                result = con_Service.CreateContract(_con, true);
            }
            if (result == true)
            {
                TempData["success"] = "Thành công";
                model = new ContractDistributorVM();
                logger.Info("End: Successful....");
            }
            else
            {
                TempData["fail"] = "Thất bại";
                logger.Info("End: Unsuccessful....");
            }

            return RedirectToAction("CreateContract");
        }
    }
}