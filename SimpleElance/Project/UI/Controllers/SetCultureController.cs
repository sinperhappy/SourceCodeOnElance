using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class SetCultureController : Controller
    {
        public ActionResult Set(string CultureList)
        {
            CultureList = BLL.CultureHelper.GetValidCulture(CultureList);

            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
            {
                cookie.Value = CultureList;
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.HttpOnly = false; // Not accessible by JS.
                cookie.Value = CultureList;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            HttpCookie UrlCookie = Request.Cookies["_url"];
            if (UrlCookie != null)
            {
                string UrlStr = UrlCookie.Value;
                string[] Url = UrlStr.Split(',');

                return RedirectToAction(Url[1], Url[0]);
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

    }
}
