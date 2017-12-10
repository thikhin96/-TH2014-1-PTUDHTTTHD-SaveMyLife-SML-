using DataModel;
using DataModel.Interfaces;
using DataService.Interfaces;
using Newtonsoft.Json;
using SML_QLNPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly IUnitService _unitService;

        public ProductController(IProductService productService, IProductTypeService productTypeService, IUnitService unitService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _unitService = unitService;
        }
        // GET: Product
        public ActionResult List()
        {
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
            var pro = _productService.GetProduct(id);
            if (pro != null)
            {
                var model = new ProductDetailViewModel()
                {
                    IdProduct = id,
                    ProductName = pro.ProductName,
                    Price = pro.Price,
                    ProductType = pro.ProductType.GetValueOrDefault(),
                    Unit = pro.Unit.GetValueOrDefault()
                };
                model.ProductTypes = _productTypeService.GetAllProductType();
                model.Units = _unitService.GetAllUnit();
                return View(model);
            }
            else
            {
                return Redirect("List");
            }
           
        }

        public ActionResult Create()
        {
            var model = new CreateProductViewModel()
            {
                IdProduct = _productService.GenerateProductId()
            };
            model.ProductTypes = _productTypeService.GetAllProductType();
            model.Units = _unitService.GetAllUnit();
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
                Unit = model.Unit
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
            
            var product = _productService.GetProduct(model.IdProduct);
            
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.IsDisabled = model.IsDisabled;
            product.ProductType = model.ProductType;
            product.Unit = model.Unit;

            var result = _productService.UpdateProduct(product);

            model.ProductTypes = _productTypeService.GetAllProductType();
            model.Units = _unitService.GetAllUnit();
            if (result == "ok")
            {
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