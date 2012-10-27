using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        protected override void ExecuteCore()
        {
            string CultrueName = null;

            HttpCookie CultrueCookie = Request.Cookies["_culture"];
            if (CultrueCookie != null)
            {
                CultrueName = CultrueCookie.Value;
            }
            else
            {
                CultrueName = Request.UserLanguages[0];
            }

            CultrueName = BLL.CultureHelper.GetValidCulture(CultrueName); // This is safe

            // Modify current thread's culture          
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CultrueName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(CultrueName);

            base.ExecuteCore();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            List<string> RightToLeftList = new List<string>();
            RightToLeftList.Add("he-il");
            RightToLeftList.Add("ar-sa");

            string CultrueName = Thread.CurrentThread.CurrentCulture.Name.ToLower();
            if (RightToLeftList.Contains(CultrueName))
            {
                filterContext.Controller.ViewBag.RightToLeft = true;
            }
            else
            {
                filterContext.Controller.ViewBag.RightToLeft = false;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
