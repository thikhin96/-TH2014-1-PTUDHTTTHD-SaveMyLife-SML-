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
    public class ReturnRequestController : BaseController
    {
        private readonly IReturnRequestService _returnRequestService;
        private readonly IProductService _productService;
        private readonly IStorageService _storageService;
        private readonly IDistributorService _distributorService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ReturnRequestController(IReturnRequestService returnRequestService, IProductService productService, IStorageService storageService, IDistributorService distributorService)
        {
            this._returnRequestService = returnRequestService;
            _productService = productService;
            _storageService = storageService;
            _distributorService = distributorService;
        }
        // GET: ReturnRequest
        public ActionResult List()
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý đơn yêu cầu đổi trả";
            ViewBag.Child = "Tìm kiếm đơn yêu cầu đổi trả";
            return View();
        }
        public ContentResult Search(string keyword, int status)
        {
            IList<ReturnRequest> rs = new List<ReturnRequest>();
            if (Request.IsAjaxRequest())
            {
                rs = _returnRequestService.SearchReturnRequest(keyword, status);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.IdReturnRequest, x.Distributor1.idDistributor, x.Distributor1.name, x.DateCreate, x.Status }));
                return Content(list, "application/json");
            }
            return null;
        }
        public ActionResult Detail(int id)
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý đơn yêu cầu đổi trả";
            ViewBag.Child = "Chi tiết đơn yêu cầu đổi trả";
            var rq = _returnRequestService.GetReturnRequest(id);
            if (rq == null)
            {
                return RedirectToAction("List", "ReturnRequest");
            }
            else
            {
                string temp = "";
                if (rq.Status == 0)
                    temp = "Chưa xử lý";
                else if (rq.Status == 1)
                    temp = "Đã xử lý";
                else
                    temp = "Đã từ chối";
                var model = new ReturnRequestDetailViewModel()
                {
                    idReturnRequest = id,
                    idDistributor = rq.Distributor1.idDistributor,
                    status = temp,
                    note = rq.Note,
                    dName = rq.Distributor1.name,
                    modeOfReturn = rq.ModeOfReturn == true ? "Trả" : "Đổi",
                    Storage = rq.Storage1,
                    
                };
                if (rq.Staff1 != null)
                    model.Staff = rq.Staff1.staffName;
                else
                    model.Staff = null;
                model.ReturnRequestDetails = rq.ReturnRequestDetails.ToList();
                return View(model);
            }
        }
        
        public ActionResult Create()
        {
            isLogged();
            var user = Session["user"] as Account;
    
            var dis = _distributorService.getDistributorByUser(user.UserName);
            if (dis != null)
            {
                var model = new CreateReturnRequestViewModel()
                {
                    idReturnRequest = _returnRequestService.GenerateReturnRequestId(),
                    idDistributor = dis.idDistributor,
                    Storages = dis.Storages.ToList(),
                    ReturnRequestDetails = new List<ReturnRequestDetail>()
                };
                model.Products = _productService.GetAllProducts();
                return View(model);
            }
            else
            {
                return Redirect("/");
            }

        }

        [HttpPost]
        public ActionResult Create(CreateReturnRequestViewModel model, [Bind(Prefix = "ReturnRequestDetails")]List<ReturnRequestDetail> ReturnRequestDetails, [Bind(Prefix = "Storages")]List<Storage> Storages)
        {
                model.Products = _productService.GetAllProducts();
                var returnRequest = new ReturnRequest
                {
                    Distributor = model.idDistributor,
                    DateCreate = DateTime.Now,
                    Status = 0,
                    Note = null,
                    ModeOfReturn = Convert.ToBoolean(model.modeOfReturn),
                    Storage = model.idStorage,
                    Staff = null,
                    ReturnRequestDetails = ReturnRequestDetails
                };
            //};
            var result = _returnRequestService.CreateReturnRequest(returnRequest);
            if (result == "thanh cong")
            {
                TempData["success"] = "thanh cong";
                model = new CreateReturnRequestViewModel();
            }
            else
                TempData["fail"] = result;
            return RedirectToAction("Create");
        }

        [HttpPost]
        public int approveReturnRequest(int id, string note)
        {
            isAdminLogged();
            var currentUser = Session["admin"] as Account;
            var returnRequest = _returnRequestService.GetReturnRequest(id);
            returnRequest.Note = note;
            returnRequest.Status = 1;
            returnRequest.Staff = currentUser.idUser;
            var result = _returnRequestService.UpdateReturnRequest(returnRequest);
            if (result == "ok")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        public int denyReturnRequest(int id, string note)
        {
            isAdminLogged();
            var currentUser = Session["admin"] as Account;
            var returnRequest = _returnRequestService.GetReturnRequest(id);
            returnRequest.Note = note;
            returnRequest.Status = 2;
            returnRequest.Staff = currentUser.idUser;
            var result = _returnRequestService.UpdateReturnRequest(returnRequest);
            if (result == "ok")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ContentResult getStorageInfo(int id)
        {
            Storage rs = new Storage();
            if (Request.IsAjaxRequest())
            {
                rs = _storageService.GetStorage(id);
                var rss = JsonConvert.SerializeObject(new { rs.HouseNumber_Street, rs.Ward_Commune, rs.District, rs.City });
                return Content(rss, "application/json");
            }
            return null;
        }
    }
}