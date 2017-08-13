using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFinalProject.Controllers
{
    public class LoginController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: Login
        public ActionResult Index()
        {
            return RedirectToAction("UserLogin");
        }

        // GET: Login/UserLogin
        public ActionResult UserLogin()
        {
            if (Request.Cookies["userInfo"] == null)
            {
                ViewBag.Message = "Please log in first.";
                return View();
            }
            return RedirectToAction("LoginActive");
        }

        // POST: Login/UserLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(User user)
        {
            try
            {
                var currentUser = db.Users.FirstOrDefault(a => a.Username == user.Username.Trim() && a.Password == user.Password);
                if (currentUser != null)
                {
                    HttpCookie cookie = new HttpCookie("userInfo");
                    cookie.Values["userID"] = currentUser.ID.ToString();
                    cookie.Values["userTypeID"] = currentUser.UserTypeID.ToString();
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("LoginSuccess");
                }
                return RedirectToAction("LoginFail");
            }
            catch (Exception e)
            {
                return RedirectToAction("LoginFail");
            }
            
        }

        // GET: Login/LoginFail
        public ActionResult LoginFail()
        {
            return View();
        }

        // GET: Login/LoginSuccess
        public ActionResult LoginSuccess()
        {
            return View();
        }

        // GET: Login/LoginActive
        public ActionResult LoginActive()
        {
            return View();
        }

    }
}