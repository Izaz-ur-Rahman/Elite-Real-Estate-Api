using Elite_Admin.Controllers;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExtendSessionFilter()); // Add your custom session extension filter here
            //filters.Add(new RedirectUrlsFilter());
        }
    }
}
