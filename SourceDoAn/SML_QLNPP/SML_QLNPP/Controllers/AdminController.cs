using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            isAdminLogged();
            ViewBag.Parent = "Trang chủ Admin Dashboard";
            ViewBag.Child = "Trang quản trị hệ thống Vitamilk";
            return View();
        }
    }
}