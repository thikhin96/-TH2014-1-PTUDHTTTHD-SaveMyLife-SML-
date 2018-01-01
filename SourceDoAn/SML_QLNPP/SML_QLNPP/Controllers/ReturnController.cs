using DataModel;
using DataService.Interfaces;
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
            isAdminLogged();
            var searchResult = _returnService.GetReturnCards(KeyWord, TradeForm);
            var model = new ShowReturnCardsViewModel
            {
                ReturnCards = searchResult

            };
            return View("List", model);
        }
        public ActionResult Show(int returnId)
        {
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
            }
            return View("Show", model);
        }

        public ActionResult Create(int ReturnRequestId)
        {
            isAdminLogged();
            var request = _returnService.GetReturnRequest(ReturnRequestId);
            var model = new CreateReturnViewModel()
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
                DistributorName = request.Distributor1.name
            };
            return View(model);
        }
    }
}