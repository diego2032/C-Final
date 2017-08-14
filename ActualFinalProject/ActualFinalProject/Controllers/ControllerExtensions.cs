using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFinalProject.Controllers
{
    public static class ControllerExtensions
    {
        public static int AuthenticateUser(this Controller controller, HttpRequestBase request)
        {
            if (request.Cookies["userInfo"] == null || controller == null)
            {
                return 0;
            }
            return Int32.Parse(request.Cookies["userInfo"]["userTypeID"]);
        }
    }
}