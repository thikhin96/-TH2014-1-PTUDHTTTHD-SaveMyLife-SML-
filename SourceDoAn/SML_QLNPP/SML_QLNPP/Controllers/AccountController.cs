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
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="LogLoginService"></param>
        /// <returns></returns>
        public AccountController(IAccountService accountService, ILogLoginService LogLoginService)
        {
            this._accountService = accountService;
            this._LogLoginService = LogLoginService;
        }
        /// <summary>
        /// GET - ActionResult render ra giao diện đăng nhập cho user
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            isLogin();
            return View();
        }

        /// <summary>
        /// POST - Hàm gửi dữ liệu từ form login lên serve
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Account Login)
        {
            isLogin();
            var ret = _accountService.Login(Login.UserName, Login.Password);
            if (ret!=null && ret.decentralization == 3)
            {
                if(ret.locked == true)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn đã bị khoá");
                    return View();
                }
                Session["user"] = ret as Account;
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
                //Response.Redirect("cpanel.aspx", false); 
                return RedirectToAction("Index", "Home");
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
        /// <summary>
        /// GET - ActionResult render ra giao diện đăng nhập cho admin
        /// </summary>
        /// <returns></returns>
        public ActionResult ALogin()
        {
            isAdminLogin();
            return View();
        }
        /// <summary>
        /// POST - Hàm gửi dữ liệu từ form login lên server, check login admin
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ALogin(Account Login)
        {
            isAdminLogin();
            var ret = _accountService.Login(Login.UserName, Login.Password);
            if (ret != null)
            {
                if (ret.decentralization == 3)
                {
                    ModelState.AddModelError("", "Bạn không đủ quyền để đăng nhập vào hệ thống");
                    return View();
                }
                else if (ret.locked == true)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn đã bị khoá");
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
                    Session["admin"] = ret as Account;
                    return RedirectToAction("Index", "Admin");
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
        /// <summary>
        /// GET logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (Session["user"] != null)
            {
                Session.Remove("user");
                return RedirectToAction("Index", "Home");
            }
            else if (Session["admin"] != null)
            {
                Session.Remove("admin");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}