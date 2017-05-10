using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace spalpuGitHub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string strpage = "main")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(strpage);
            }
            return View();
        }

    }
}