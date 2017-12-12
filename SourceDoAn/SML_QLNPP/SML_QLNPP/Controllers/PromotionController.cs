using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DataService.Interfaces;
using Newtonsoft.Json;
namespace SML_QLNPP.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IPromotionService _orderService;

        public PromotionController(IPromotionService orderService)
        {
            this._orderService = orderService;
        }
        // GET: Promotion
        public ActionResult List()
        {
            ViewBag.Parent = "Quản lý khuyến mãi";
            ViewBag.Child = "Tìm kiếm khuyến mãi";
            return View();
        }
        public ActionResult Detail(int id)
        {
            ViewBag.Parent = "Quản lý khuyến mãi";
            ViewBag.Child = "Chi tiết khuyến mãi";
            var model = _orderService.GetPromotion(id);
            if (model == null)
            {
                return Redirect("/Order/List");
            }
            else
            {
                return View(model);
            }
        }
        public ContentResult Search(int idpro, string startDate, string endDate)
        {
            IList<Promotion> rs = new List<Promotion>();
            if (Request.IsAjaxRequest())
            {
                rs = _orderService.Search(idpro, startDate, endDate);
                var list = JsonConvert.SerializeObject(rs.Select(x => new { x.idPromotion, x.startDate, x.endDate }));
                return Content(list, "application/json");
            }
            return null;
        }
    }
}