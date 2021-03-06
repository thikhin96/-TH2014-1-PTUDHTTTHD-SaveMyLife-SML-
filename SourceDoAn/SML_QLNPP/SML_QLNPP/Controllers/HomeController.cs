﻿using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPromotionService _promotionService;

        public HomeController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Nhà phân phối sữa Vitamilk | Nhà phân phối sữa hàng đầu Việt Nam";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}