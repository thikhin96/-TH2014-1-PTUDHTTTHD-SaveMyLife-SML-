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
        private readonly ILogLoginService _LogLoginService;

        public AccountController(IAccountService accountService, ILogLoginService LogLoginService)
        {
            this._accountService = accountService;
            this._LogLoginService = LogLoginService;
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
            if (ret!=null)
            {
                Session["username"] = Login.UserName;
                Log_Login log = new Log_Login();
                log.idAccount = ret.idUser;
                log.status = true;
                log.at_time = DateTime.Now;
                try
                {
                    _LogLoginService.Add(log);
                }
                catch
                {

                }
                return Redirect("/");
            }
            else
            {
                var logUser = _accountService.Get(Login.UserName);
                if(logUser != null)
                {
                    Log_Login log = new Log_Login();
                    log.idAccount = logUser.idUser;
                    log.status = false;
                    log.at_time = DateTime.Now;
                    try
                    {
                        _LogLoginService.Add(log);
                    }
                    catch
                    {

                    }
                }
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu chưa chính xác");
                return View();
            }
        }
        public ActionResult ALogin()
        {
            isLogin();
            return View();
        }
        [HttpPost]
        public ActionResult ALogin(Account Login)
        {
            isLogin();
            var ret = _accountService.Login(Login.UserName, Login.Password);
            if (ret != null)
            {
                if (ret.decentralization == 3)
                {
                    ModelState.AddModelError("", "Bạn không đủ quyền để đăng nhập vào hệ thống");
                    return View();
                }
                else
                {
                    Log_Login log = new Log_Login();
                    log.idAccount = ret.idUser;
                    log.status = true;
                    log.at_time = DateTime.Now;
                    try
                    {
                        _LogLoginService.Add(log);
                    }
                    catch
                    {

                    }
                    Session["a_username"] = Login.UserName;
                    return Redirect("/");
                }
            }
            else
            {
                var logUser = _accountService.Get(Login.UserName);
                if (logUser != null)
                {
                    Log_Login log = new Log_Login();
                    log.idAccount = logUser.idUser;
                    log.status = false;
                    log.at_time = DateTime.Now;
                    try
                    {
                        _LogLoginService.Add(log);
                    }
                    catch
                    {

                    }
                }
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