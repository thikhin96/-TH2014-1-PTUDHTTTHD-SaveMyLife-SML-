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
            if (Session["username"] != null)
            {
                Response.Redirect("/");
            }
        }
    }
}