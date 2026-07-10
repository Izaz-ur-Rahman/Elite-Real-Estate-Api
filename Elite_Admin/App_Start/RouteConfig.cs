using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Elite_Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
    
            routes.MapRoute(
                name: "PostBlog",
                url: "Blog/post_blog",
                defaults: new { controller = "Blog", action = "post_blog" }
            );

            // Route for Blog Index
            routes.MapRoute(
                name: "Index",
                url: "Blog/Index",
                defaults: new { controller = "Blog", action = "Index" }
            );
            routes.MapRoute(
              name: "addblogdata",
              url: "Blog/addblogdata",
              defaults: new { controller = "Blog", action = "addblogdata" }
          );
            routes.MapRoute(
         name: "Update_blog",
         url: "Blog/Update_blog/{id}",
         defaults: new { controller = "Blog", action = "Update_blog" }
     );


            routes.MapRoute(
                  name: "BlogDetails",
                  url: "Blog/{blogname}",
                  defaults: new { controller = "Blog", action = "Blogdetails" }
              );

            routes.MapRoute(
                name: "BlogDetail",
                url: "api/Blog/Detail/{blogname}",
                defaults: new { controller = "Blog", action = "BlogDetail", blogname = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "PropertyIndex",
                 url: "Properties/Index",
                 defaults: new { controller = "Properties", action = "Index" }
             );

            routes.MapRoute(
                 name: "AddProperty",
                 url: "Properties/AddProperty",
                 defaults: new { controller = "Properties", action = "AddProperty" }
             );

            routes.MapRoute(
                 name: "PropertyUpdate",
                 url: "Properties/UpdateProperty",
                 defaults: new { controller = "Properties", action = "UpdateProperty" }
             );

            routes.MapRoute(
                name: "PropertyDelete",
                url: "Properties/DeleteProperty/{id}",
                defaults: new { controller = "Properties", action = "DeleteProperty" }
            );

            routes.MapRoute(
                  name: "PropertyDetails",
                  url: "Properties/{propertyName}",
                  defaults: new { controller = "Properties", action = "Propertydetails" }
              );

           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
