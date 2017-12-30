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
    public class PDistributorController : Controller
    {
        private readonly IPDistributorService _pdistributorService;
        private readonly IRepresentativeService _representativeService;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public PDistributorController(IPDistributorService pdisService, IRepresentativeService repService)
        {
            this._pdistributorService = pdisService;
            _representativeService = repService;
        }

        // GET: PDistributor
        public ActionResult List()
        {
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
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idDistributor, x.name, x.address, x.phone, x.Email, x.status, x.Representatives, x.Assignments }));
                return Content(list, "application/json");
            }

            return null;
        }

        public ActionResult SearchPDistributor(PDistributorViewModel model)
        {
            try
            {
                if (model.status != 5)
                {
                    model.listPDistributor = _pdistributorService.SearchByStatus(model.status);
                    View(model);
                }
                else
                {

                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return View(model);
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
                model = new CreatePDistributorViewModel();
            }


            else
                TempData["fail"] = result;
            return RedirectToAction("Create");
        }

    }
}