using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFinalProject.Controllers
{
    public class LogoutController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();


        // GET: Logout
        public ActionResult Index()
        {
            return RedirectToAction("UserLogout");
        }

        // GET: Logout/UserLogout
        public ActionResult UserLogout()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                return View();
            }
            return RedirectToAction("LoggedOut");
        }

        // GET: Logout/LoggedOut
        public ActionResult LoggedOut()
        {
            return View();
        }
    }
}