using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Controllers
{
    public class AuthorizeWithSessionAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            //var dta = db.Contacts.FirstOrDefault().FirstName_En;
            //httpContext.Session["Nizam"] = dta;
            if (httpContext.Session == null || httpContext.Session["userid"] == null)

                return false;

            return base.AuthorizeCore(httpContext);
        }
        
    }
}