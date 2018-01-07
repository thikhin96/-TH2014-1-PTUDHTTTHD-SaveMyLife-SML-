using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
using SML_QLNPP.Models;

namespace SML_QLNPP.Controllers
{
    public class StorageController : Controller
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            this._storageService = storageService;
        }
        // GET: Storage
        public ContentResult Search(int id)
        {
            IList<Storage> rs = new List<Storage>();
            if (Request.IsAjaxRequest())
            {
                rs = _storageService.SearchStorage(id);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.HouseNumber_Street, x.Ward_Commune, x.District, x.City }));
                return Content(list, "application/json");
            }
            return null;
        }
    }
}