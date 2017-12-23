using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using DataService.Services;
using Newtonsoft.Json;
using SML_QLNPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly IUnitService _unitService;
        private readonly IAccountService _accountService;
        private readonly ILogProductService _logProductService;

        public ProductController(IProductService productService, IProductTypeService productTypeService, IUnitService unitService, IAccountService accountService,ILogProductService logProductService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _unitService = unitService;
            _accountService = accountService;
            _logProductService = logProductService;
        }
        // GET: Product
        public ActionResult List()
        {
            isAdminLogged();
            ViewBag.Parent = "Quản lý sản phẩm";
            ViewBag.Child = "Tìm kiếm sản phẩm";
            return View();
        }

        public ContentResult Search(string keyword)
        {
            IList<Product> rs = new List<Product>();
            if (Request.IsAjaxRequest())
            {
                rs = _productService.Search(keyword);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.IdProduct, x.ProductName, x.Price, x.IsDisabled }));
                return Content(list, "application/json");
            }
            return null;
        }

        //public ActionResult Detail(int id)
        //{
        //    var model = _productService.GetProduct(id);
        //    if (model == null)
        //    {
        //        return Redirect("List");
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        public ActionResult Detail(int id)
        {
            isAdminLogged();
            var pro = _productService.GetProduct(id);
            if (pro != null)
            {
                var model = new ProductDetailViewModel()
                {
                    IdProduct = id,
                    ProductName = pro.ProductName,
                    Price = pro.Price,
                    IsDisabled = pro.IsDisabled.GetValueOrDefault(),
                    ProductType = pro.ProductType.GetValueOrDefault(),
                    Unit = pro.Unit.GetValueOrDefault(),
                    Quantity = pro.Quantity.GetValueOrDefault(),
                    description_log = null
                };
                model.ProductTypes = _productTypeService.GetAllProductType();
                model.Units = _unitService.GetAllUnit();
                ViewBag.Parent = "Quản lý sản phẩm  >  Tìm kiếm sản phẩm";
                ViewBag.Child = "Chi tiết sản phẩm";
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
            var model = new CreateProductViewModel()
            {
                IdProduct = _productService.GenerateProductId()
            };
            model.ProductTypes = _productTypeService.GetAllProductType();
            model.Units = _unitService.GetAllUnit();
            ViewBag.Parent = "Quản lý sản phẩm";
            ViewBag.Child = "Tạo sản phẩm mới";
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateProductViewModel model)
        {
            model.ProductTypes = _productTypeService.GetAllProductType();
            model.Units = _unitService.GetAllUnit();
            var product = new Product
            {
                IdProduct = model.IdProduct,
                ProductName = model.ProductName,
                Price = model.Price,
                IsDisabled = false,
                ProductType = model.ProductType,
                Unit = model.Unit,
                Quantity = model.Quantity
            };
            var result = _productService.CreateProduct(product);
            if (result == "ok")
            {
                TempData["msg"] = "Tạo sản phẩm thành công";
                return RedirectToRoute("Default", new { controller = "Product", action = "Detail", id = product.IdProduct });
            }
            else
            {
                ViewBag.fail = result;
                model.IdProduct = _productService.GenerateProductId();
                return View(model);
            }
               
        }

        [HttpPost]
        public ActionResult Detail(ProductDetailViewModel model)
        {
            var username = Session["a_username"].ToString();
            var user_id = _accountService.Get(username).idUser;

            var product = _productService.GetProduct(model.IdProduct);

            var old_price = product.Price;

            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.IsDisabled = model.IsDisabled;
            product.ProductType = model.ProductType;
            product.Unit = model.Unit;
            product.Quantity = model.Quantity;

            var result = _productService.UpdateProduct(product);

            model.ProductTypes = _productTypeService.GetAllProductType();
            model.Units = _unitService.GetAllUnit();
            if (result == "ok")
            {
                if (model.description_log != null)
                {
                    Log_Product logP = new Log_Product();
                    logP.createdDate = DateTime.Now;
                    logP.idStaff = user_id;
                    logP.oldPrice = Decimal.ToInt32(old_price.GetValueOrDefault());
                    logP.newPrice = Decimal.ToInt32(product.Price.GetValueOrDefault());
                    logP.description = model.description_log;
                    _logProductService.Add(logP);
                }
                TempData["msg"] = "Cập nhật sản phẩm thành công";
                return RedirectToRoute("Default", new { controller = "Product", action = "Detail", id = model.IdProduct });
            }
            else
            {
                model.IdProduct = model.IdProduct;
                ViewBag.fail = result;
                return View(model);
            }

        }


    }
}