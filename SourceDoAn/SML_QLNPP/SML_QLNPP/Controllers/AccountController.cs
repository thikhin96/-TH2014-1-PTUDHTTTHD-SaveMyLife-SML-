using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataService.Interfaces;
using DataModel;
namespace SML_QLNPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account Login)
        {
            var ret = _accountService.Login(Login.UserName, Login.Password);
            if (ret)
            {
                Session["user_id"] = Login.UserName;
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu chưa chính xác");
                return View();
            }
        }
        public ActionResult Logout()
        {
            if (Session["username"] != null)
            {
                Session.Remove("username");
                return Redirect("/");
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}