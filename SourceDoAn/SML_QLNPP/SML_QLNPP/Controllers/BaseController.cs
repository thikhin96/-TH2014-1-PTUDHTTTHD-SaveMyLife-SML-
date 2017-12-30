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
                Response.Redirect("/");
            }
        }
        protected void isAdminLogin()
        {
            if (Session["admin"] != null)
            {
                Response.Redirect("/Order/List");
            }
        }
        protected void isAdminLogged()
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("/Account/ALogin");
            }
        }
    }
}