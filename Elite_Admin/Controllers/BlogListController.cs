using Elite_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Controllers
{
    public class BlogListController : Controller
    {
        // GET: BlogList
        [Obsolete]
        public ActionResult Index()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var data = webservices.BlogList().Where(x => x.Status == "Published").ToList();
            return View(data);
        }
        [AuthorizeWithSession]
        [Obsolete]
        public ActionResult Publishblog(int? id,string type)
        {
            var data = webservices.PublishBlog(id, DateTime.Now.ToString("yyyy-MM-dd"),type);
            TempData["msg"] = "published";
            return RedirectToAction("Index", "Blog");
        }

        [Obsolete]
        public ActionResult DeleteBlog(int id)
        {
            var data = webservices.DeleteBlog(id);
            TempData["msg"] = data;

            return RedirectToAction("Index","blog");
        }
       
        [Route("blogs")]

        [Obsolete]
        public ActionResult blogdetail()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var data = webservices.BlogList().Where(x => x.Status == "Published").ToList().OrderByDescending(x=>x.UpdateDate).Take(6).ToList();
            foreach(var i in data)
            {
                var empNo = i.CreatedBy;
                var userdata = webservices.UserList().Where(x => x.EmpNo == empNo).FirstOrDefault();
                i.CreatedBy = userdata.Name;
                i.Profileimage = userdata.Profileimage;
            }
            var papolar = webservices.BlogList().Where(x => x.Status == "Published" && x.Blog_openCount!=null).ToList().OrderByDescending(x => x.Blog_openCount).Take(6).ToList();
            foreach (var i in papolar)
            {
                var empNo = i.CreatedBy;
                var userdata = webservices.UserList().Where(x => x.EmpNo == empNo).FirstOrDefault();
                i.CreatedBy = userdata.Name;
                i.Profileimage = userdata.Profileimage;
            }
            ViewBag.Papular = papolar;
            return View(data);

        }
        public ActionResult blogauthor()
        {
            return View();

        }
    }
}