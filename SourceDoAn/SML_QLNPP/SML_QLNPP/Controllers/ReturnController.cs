using DataModel;
using DataService.Interfaces;
using NLog;
using SML_QLNPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Controllers
{
    public class ReturnController : BaseController
    {
        private readonly IReturnService _returnService;
        private readonly IProductService _productService;
        private readonly IDistributorService _distributorService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ReturnController(IReturnService returnService, IProductService productService)
        {
            _returnService = returnService;
            _productService = productService;
        }
        // GET: Return
        public ActionResult Index()
        {
            isAdminLogged();
            var model = new ShowReturnCardsViewModel
            {
                ReturnCards = new List<ReturnBase>()
            };
            return View("List", model);
        }
        public ActionResult Search(string KeyWord, int TradeForm)
        {
            _logger.Info("Start Search(GET) - OrderController");
            isAdminLogged();
            var searchResult = _returnService.GetReturnCards(KeyWord, TradeForm);
            var model = new ShowReturnCardsViewModel();
            if (searchResult != null)
            {
                model = new ShowReturnCardsViewModel
                {
                    ReturnCards = searchResult
                    
                };
                _logger.Info("Success: Complete Search(GET) - ReturnController");
            }
            else
                _logger.Error("Error: Search(GET) - ReturnController | Can not find any return card with given key word and trade form!");
            return View("List", model);
        }
        public ActionResult Show(int returnId)
        {
            _logger.Info("Start Show(GET) - ReturnController");
            isAdminLogged();
            var cardDetail = _returnService.GetReturnCardDetail(returnId);
            var model = new ReturnDetailViewModel();
            if (cardDetail != null)
            {
                model.CreatedDate = cardDetail.DateCreate;
                model.DistributorId = cardDetail.Distributor.idDistributor;
                model.DistributorName = cardDetail.Distributor.name;
                model.ReturnDetails = cardDetail.ReturnDetails.ToList();
                model.ReturnForm = cardDetail.ModeOfReturn == true ? "Trả" : "Đổi";
                model.ReturnId = cardDetail.idReturn;
                model.StaffId = cardDetail.idStaff;
                model.StaffName = cardDetail.Staff.staffName;
                model.Storage = cardDetail.Storage ?? new Storage();
                model.ReturnRequestId = cardDetail.idReturnRequest;
                model.Total = cardDetail.Total;
                _logger.Info("Success: Complete Show(GET) - ReturnController");
            }
            else
                _logger.Error("Error: Show(GET) - ReturnController | Can not find any return card details with given return card id!");
            return View("Show", model);
        }

        public ActionResult Create(int ReturnRequestId)
        {
            _logger.Info("Start Create(GET) - ReturnController");
            isAdminLogged();
            var request = _returnService.GetReturnRequest(ReturnRequestId);
            var model = new CreateReturnViewModel();
            if (request != null)
            {
                model = new CreateReturnViewModel()
                {
                    Storage = request.Storage1 ?? new Storage(),
                    ReturnDetails = request.ReturnRequestDetails.Select(x => new ReturnDetail()
                    {
                        idProduct = x.IdProduct,
                        Quantity = x.Quantity,
                        Product = x.Product
                    }).ToList(),
                    Products = _productService.GetAllProducts(),
                    DistributorId = request.Distributor.HasValue ? request.Distributor.Value : 0,
                    DistributorName = request.Distributor1.name,
                    ReturnId = _returnService.GenerateReturnId()
                };
                _logger.Info("Success: Complete Create(GET) - ReturnController");
            }
            else
                _logger.Error("Error: Create(GET) - ReturnController | Can not find any return request with given id!");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateReturnViewModel model)
        {
            _logger.Info("Start Create(POST) - ReturnController");
            isAdminLogged();
            var currentUser = Session["admin"] as Account;
            var returnCard = new ReturnBase
            {
                idStorage = model.idStorage,
                Total = model.Total,
                DateCreate = DateTime.Now,
                ModeOfReturn = Convert.ToBoolean(model.ReturnForm),
                idDistributor = model.DistributorId,
                idStaff = currentUser.idUser,
                idReturnRequest = model.ReturnRequestId,
                ReturnDetails = model.ReturnDetails
            };
            var result = _returnService.CreateReturnCard(returnCard);
            if (result == "thanh cong")
            TempData["success"] = "thanh cong";   
            else
                TempData["fail"] = result;
            model.Products = _productService.GetAllProducts();
            _logger.Info("Success: Complete Create(POST) - ReturnController");
            return RedirectToAction("Create", new { ReturnRequestId = model.ReturnRequestId });

            //if (_distributorService.checkDebt(model.DistributorId) == true)
            //{
            //    return Redirect("/Debt/Create?ReturnId=" + model.ReturnId);
            //}
            //else
            //{
            //    return Redirect("/ReturnRequest/List");
            //}
        }
    }
}