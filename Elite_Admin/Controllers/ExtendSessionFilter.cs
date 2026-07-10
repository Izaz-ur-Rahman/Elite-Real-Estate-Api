using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Controllers
{
    public class ExtendSessionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Update the session's "LastAccessed" time to extend the session
            if (filterContext.HttpContext.Session != null)
            {
                filterContext.HttpContext.Session["LastAccessed"] = DateTime.Now;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}