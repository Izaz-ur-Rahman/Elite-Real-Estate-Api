using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin
{

    public class RedirectUrlsFilter: ActionFilterAttribute
    {
        private readonly List<string> redirectUrls = new List<string>
    {
        "/Website/indexxx",
        "/oldpage/about",
        "/outdated/page" // Add other URLs here
    };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Get the current URL
            string currentUrl = HttpContext.Current.Request.Url.AbsolutePath.ToLower();

            // Check if the current URL is in the list of URLs to be redirected
            if (redirectUrls.Any(url => currentUrl.EndsWith(url)))
            {
                // Redirect to home (or any other desired action)
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                    { "controller", "Account" },
                    { "action", "Index" }
                    });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}