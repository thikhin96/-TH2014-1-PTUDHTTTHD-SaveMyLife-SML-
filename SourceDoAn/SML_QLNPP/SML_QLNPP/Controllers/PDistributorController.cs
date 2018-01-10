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
        private readonly IAccountService _accountService;
        private readonly IStaffService _staffService;
        private readonly IAssignmentService _assigmentService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public PDistributorController(IPDistributorService pdisService, IRepresentativeService repService, IAccountService accountService,
            IStaffService staffService, IAssignmentService assigmentService)
        {
            this._pdistributorService = pdisService;
            _representativeService = repService;
            _accountService = accountService;
            _staffService = staffService;
            _assigmentService = assigmentService;
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

        public ActionResult Create()
        {
            ViewBag.Parent = "Quản lý đối tác";
            ViewBag.Child = "Thêm đối tác";
            CreatePDistributorViewModel model = new CreatePDistributorViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreatePDistributorViewModel model)
        {
            logger.Info("Start Create(POST) - PDistributorController");
            var pDis = new PotentialDistributor
            {
                idDistributor = _pdistributorService.GeneratePDistributorId(),
                name = model.name,
                address = model.address,
                Email = model.Email,
                phone = model.phone,
                createdDate = DateTime.Now,
                status = 0,

            };
            var rep = new Representative
            {
                idRepresentative = _representativeService.GenerateRepresentativeId(),
                name = model.rep_name,
                email = model.rep_email,
                title = model.title,
                phone = model.rep_phone,
                PDistributor = pDis.idDistributor,
            };
            var result = _pdistributorService.Create(pDis, rep);
            if (result == true)
            {
                logger.Info("Success: Complete Create(POST) - PDistributorController");
                TempData["success"] = "Thành công";
                return RedirectToAction("Create");
            }
            else
            {
                logger.Info("Fail: Create(POST) - PDistributorController");
                TempData["fail"] = result;
                return View(model);
            }
        }
        public ContentResult getStaffAssigment()
        {
            IList<Staff> rs = new List<Staff>();
            if (Request.IsAjaxRequest())
            {
                rs = _staffService.getAll();
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idStaff, x.staffName }));
                return Content(list, "application/json");
            }
            return null;
        }

        public ContentResult getStaff(int id)
        {
           var rs = new Staff();
            if (Request.IsAjaxRequest())
            {
                rs = _staffService.GetStaff(id);
                var r = JsonConvert.SerializeObject(new { rs.idStaff, rs.staffName });
                return Content(r, "application/json");
            }
            return null;
        }

       public ActionResult Detail(int id)
        {
            isAdminLogged();
            var pdis = _pdistributorService.GetPDistributor(id);
            if (pdis != null)
            {
                var model = new PDistributorDetailViewModel();
                model.idDistributor = pdis.idDistributor;
                model.name = pdis.name;
                model.Email = pdis.Email;
                model.address = pdis.address;
                model.phone = pdis.phone;
                model.status = pdis.status;
                model.note = pdis.note;
                model.rep_name = pdis.Representatives.FirstOrDefault().name;
                model.title = pdis.Representatives.FirstOrDefault().title;
                model.rep_email = pdis.Representatives.FirstOrDefault().email;
                model.rep_phone = pdis.Representatives.FirstOrDefault().phone;
                if(pdis.Assignments.FirstOrDefault() != null)
                {
                    model.place = pdis.Assignments.FirstOrDefault().place;
                    model.date = pdis.Assignments.FirstOrDefault().date;
                    model.result = pdis.Assignments.FirstOrDefault().result;
                    model.staffAssigment = pdis.Assignments.FirstOrDefault().Staff1.idStaff;
                }
               
                model.allStaff = _staffService.getAll();
                ViewBag.Parent = "Quản lý đối tác  >  Tìm kiếm đối tác";
                ViewBag.Child = "Chi tiết đối tác";
                return View(model);
            }
            else
            {
                return Redirect("List");
            }
        }

        [HttpPost]
        public ActionResult Detail (PDistributorDetailViewModel model)
        {
            //var user = Session["admin"] as Account;
            var pdis = _pdistributorService.GetPDistributor(model.idDistributor);
            pdis.idDistributor = model.idDistributor;
            pdis.name = model.name;
            pdis.Email = model.Email;
            pdis.address = model.address;
            pdis.phone = model.phone;
            pdis.status = model.status;
            pdis.note = model.note;

            // var assig = _assigmentService.CreateAssignment(pdis.Assignments.FirstOrDefault().staff, pdis.Assignments.FirstOrDefault().PDistributor);

            var res = _representativeService.GetRepresentative(pdis.Representatives.FirstOrDefault().idRepresentative);
            res.name = model.rep_name;
            res.title = model.title;
            res.email = model.rep_email;
            res.phone = model.rep_phone;


            //var assigTemp = _assigmentService.GetAssignment(pdis.Assignments.FirstOrDefault().staff, pdis.Assignments.FirstOrDefault().PDistributor);
            var assig = new Assignment();
            assig.staff = model.staffAssigment;
            assig.PDistributor = pdis.idDistributor;
            assig.date = model.date;
            assig.place = model.place;

            if (pdis.status == 3 || pdis.status == 4)
                assig.isComplete = true;
            assig.isComplete = false;
            assig.result = model.result;
            assig.staff = model.staffAssigment;

            var rs1= _representativeService.UpdateRepresentative(res);
            //var rs2 = _assigmentService.DeleteAssignment(assigTemp);
            var rs3 = _assigmentService.CreateAssignment(assig);
            pdis.updatedDate = DateTime.Now;
            var result = _pdistributorService.UpdatePDistributor(pdis);

            if (result == true)
            {
                TempData["success"] = "Thành công";
                return RedirectToRoute("Default", new { controller = "PDistributor", action = "Detail", id = model.idDistributor });
            }
            else
            {
                model.idDistributor = model.idDistributor;
                TempData["fail"] = result;
                return View(model);
            }
        }

    }
}