using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Controllers
{
    public class DataSourceController : Controller
    {
        private readonly IDistributorService _distributorService;


        public DataSourceController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        public JsonResult GetDistributors(string search)
        {
            var distributors = _distributorService.SearchByName(search);
            if (distributors.Count == 0)
                return Json(new { }, JsonRequestBehavior.AllowGet);
            var results = distributors.Select(x => new
            {
                id = x.idDistributor,
                text = x.name
            });
              
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStorages(string search, int distributorId)
        {
            var storages = _distributorService.GetStorages(search, distributorId);
            if (storages.Count == 0)
                return Json(new { }, JsonRequestBehavior.AllowGet);
            var results = storages.Select(x => new
            {
                id = x.IdStorage,
                text = x.HouseNumber_Street + ", Phường " + x.Ward_Commune + ", Quận " + x.District + ", Thành phố " + x.City
            });

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}