using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SML_QLNPP.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected void isLogin()
        {
            if (Session["user"] != null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
            }
        }
        protected void isAdminLogin()
        {
            if (Session["admin"] != null)
            {
                Response.Redirect(Url.Action("List", "Order"));
            }
        }
        protected void isAdminLogged()
        {
            if (Session["admin"] == null)
            {
                Response.Redirect(Url.Action("ALogin", "Account"));
            }
        }
        protected object GetCurrentUser()
        {
            return Session["user"] ?? Session["admin"];
        }
        protected void CheckForAuthorization()
        {
            if (Session["user"] == null && Session["admin"] == null)
                Response.Redirect(Url.Action("Index", "Home"));
        }
    }
}