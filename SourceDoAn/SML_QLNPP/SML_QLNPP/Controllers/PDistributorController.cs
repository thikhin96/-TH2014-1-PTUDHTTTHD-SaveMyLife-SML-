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
    public class PDistributorController : BaseController
    {
        private readonly IPDistributorService _pdistributorService;
        private readonly IRepresentativeService _representativeService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IAccountService _accountService;
        public PDistributorController(IPDistributorService pdisService, IRepresentativeService repService, IAccountService accountService)
        {
            this._pdistributorService = pdisService;
            _representativeService = repService;
            _accountService = accountService;
        }

        // GET: PDistributor
        public ActionResult List()
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý đối tác";
            ViewBag.Child = "Tìm kiếm đối tác";
            return View();
        }


        public ContentResult Search(byte status)
        {
            IList<PotentialDistributor> rs = new List<PotentialDistributor>();
            if (Request.IsAjaxRequest())
            {
                rs = _pdistributorService.SearchByStatus(status);
                var list = JsonConvert.SerializeObject( rs.Select(x => new { x.idDistributor, x.name, x.address, x.phone, x.Email, x.status, x.Representatives, x.Assignments }), Formatting.Indented,
                                                                    new JsonSerializerSettings()
                                                                    {
                                                                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                                                    }) ;
                return Content(list, "application/json");
            }
            return null;
        }

        public ActionResult Detail(int id)
        {
            isAdminLogged();
            var pdis = _pdistributorService.GetPDistributor(id);
            if (pdis != null)
            {
                var model = new PDistributorDetailViewModel()
                {
                    idDistributor = id,
                    name = pdis.name,
                    address = pdis.address,
                    phone = pdis.phone,
                    Email = pdis.Email,
                    status = pdis.status,
                    note = pdis.note,
                    rep_name = pdis.Representatives.FirstOrDefault().name,
                    rep_phone = pdis.Representatives.FirstOrDefault().phone,
                    rep_email = pdis.Representatives.FirstOrDefault().email,
                    title = pdis.Representatives.FirstOrDefault().title,
                    place = pdis.Assignments.FirstOrDefault().place,
                    date = pdis.Assignments.FirstOrDefault().date,
                    result = pdis.Assignments.FirstOrDefault().result,

                };
                ViewBag.Parent = "Quản lý đối tác  >  Tìm kiếm đối tác";
                ViewBag.Child = "Chi tiết đối tác";
                return View(model);
            }
            else
            {
                return Redirect("List");
            }

        }

        public ActionResult Create()
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý đối tác";
            ViewBag.Child = "Thêm đối tác";
            CreatePDistributorViewModel model = new CreatePDistributorViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreatePDistributorViewModel model)
        {
            ViewBag.Parent = "Quản lý đối tác";
            ViewBag.Child = "Thêm đối tác";
            logger.Info("Start controller....");
            var pDis = new PotentialDistributor
            {
                idDistributor = _pdistributorService.GenerateOrderId(),
                name = model.name,
                address = model.address,
                Email = model.Email,
                phone = model.phone,
                createdDate = DateTime.Now,
                status = 0,

            };
            var rep = new Representative
            {
                idRepresentative = _representativeService.GenerateOrderId(),
                name = model.rep_name,
                email = model.rep_email,
                title = model.title,
                phone = model.rep_phone,
                PDistributor = pDis.idDistributor,
            };
            var result = _pdistributorService.Create(pDis, rep);
            if (result == true)
            {
                TempData["success"] = "Thành công";
                return RedirectToAction("Create");
            }


            else
            {
                ViewBag.fail = result;
                return View(model);
            }
        }
        public List<Staff> getStaffAssigment(int idPDis)
        {
            List<Assignment> temp = _pdistributorService.GetPDistributor(idPDis).Assignments.ToList();
            List<Staff> st = new List<Staff>();
            foreach (Assignment t in temp)
            {
                st.Add(t.Staff1);
            }
            return st;
        }
    }
}