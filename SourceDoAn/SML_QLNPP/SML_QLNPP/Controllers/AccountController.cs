using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataService.Interfaces;
using DataModel;
namespace SML_QLNPP.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        // GET: Account
        public ActionResult Login()
        {
            isLogin();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account Login)
        {
            isLogin();
            var ret = _accountService.Login(Login.UserName, Login.Password);
            if (ret)
            {
                Session["username"] = Login.UserName;
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