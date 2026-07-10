using Newtonsoft.Json;
using Elite_Admin.Models;
 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Controllers
{
    [Authorize]

    [AuthorizeWithSession]

    public class BlogController : Controller
    {
        // GET: Blog
        [HttpGet]
        [Obsolete]
        public ActionResult Index()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            if (Session["EmpRole"].ToString() == "Admin")
            {
                var data = webservices.BlogList().ToList();
                return View(data);
            }
            else
            {
                var data = webservices.BlogList().Where(x => x.CreatedBy == Session["EmpNo"].ToString()).ToList();
                return View(data);
            }

        }
        [HttpGet]

        public ActionResult post_blog()
        {
            return View();
        }
        [HttpGet]
        [Obsolete]
        public ActionResult Update_blog(int id)
        {
            var data = webservices.BlogList().Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Obsolete]
        public async Task<ActionResult> addblogdata(BlogPostModel blogPost, HttpPostedFileBase Coverimage)
        {
            var sessiontimeees = Session.Timeout;
            blogPost.CreatedBy = Session["EmpNo"].ToString();
            var readingtime = CalculateReadingTime(blogPost.BlogDetails);
            var CoverimageName = "";
            if (Coverimage != null)
            {
                var randomFileName = Path.GetRandomFileName().Replace(".", ""); // Remove dot
                var fileNames = randomFileName + Path.GetFileName(Coverimage.FileName);
                var path = Path.Combine(Server.MapPath("~/EliteFiles/blogfiles"), fileNames);
                Coverimage.SaveAs(path);
                CoverimageName = fileNames;
            }
            blogPost.CoverImage = CoverimageName;

            HttpClient httpClient = new HttpClient();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            blogPost.Blog_readTime = readingtime;
            var json = JsonConvert.SerializeObject(blogPost);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string published = "Blog/addBlogDetails";
            string urlg = ConfigurationSettings.AppSettings["ApiUrl"] + published;
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

            response = await httpClient.PostAsync(urlg, content);

            if (response.IsSuccessStatusCode)
            {
                var cnt = await response.Content.ReadAsStringAsync();
                var pro = JsonConvert.DeserializeObject<object>(cnt);
                if (pro.ToString() == "Updated")
                {
                    TempData["msg"] = "Updated";

                }
                else
                {

                    TempData["msg"] = "Added";
                }


                return RedirectToAction("Index");


            }
            return RedirectToAction("Index");
        }
        public static int CalculateReadingTime(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
            {
                return 0;
            }

            // Strip HTML tags
            string textContent = StripHtmlTags(htmlContent);

            // Average reading speed: 200 words per minute
            const int wordsPerMinute = 200;

            // Count the words in the text content
            int wordCount = textContent.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

            // Calculate reading time in minutes
            int readingTime = (int)Math.Ceiling((double)wordCount / wordsPerMinute);

            return readingTime;
        }
        private static string StripHtmlTags(string input)
        {
            // Remove script and style tags
            input = Regex.Replace(input, "<(script|style)[^>]*?>.*?</\\1>", "", RegexOptions.IgnoreCase);

            // Remove binary data (e.g., images in base64 format)
            input = Regex.Replace(input, "data:image[^;]+;base64[^'\"]+", "", RegexOptions.IgnoreCase);

            // Remove all other HTML tags
            input = Regex.Replace(input, "<[^>]+>", "", RegexOptions.IgnoreCase);

            // Decode HTML entities (e.g., &nbsp;)
            input = System.Net.WebUtility.HtmlDecode(input);

            return input;
        }

        [AllowAnonymous]
        [HttpGet]
        [Obsolete]
        public ActionResult Blogdetails(string blogname)
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }



            var data = webservices.BlogDetailsbySlug(blogname);
            // Retrieve the cookie
            var cookie = Request.Cookies["VisitedBlogs"];

            // Create or retrieve a list of visited blogs from the cookie
            List<string> visitedBlogs = new List<string>();
            if (cookie != null)
            {
                visitedBlogs = cookie.Value.Split(',').ToList();
            }

            // Check if the current blog has already been counted
            if (!visitedBlogs.Contains(blogname))
            {
                // Increment the blog view count for new blog visits
                webservices.BlogOpenCount(data.Id);

                // Add the blogname to the list of visited blogs
                visitedBlogs.Add(blogname);

                // Update the cookie with the new list of visited blogs
                var updatedCookie = new HttpCookie("VisitedBlogs", string.Join(",", visitedBlogs))
                {
                    Expires = DateTime.Now.AddYears(1) // Set cookie to expire in 1 year
                };
                Response.Cookies.Set(updatedCookie);
            }


            var empNo = data.CreatedBy;
            var userdata = webservices.UserList().Where(x => x.EmpNo == empNo).FirstOrDefault();
            ViewBag.userdata = userdata;
            var userid = userdata.id;
            var Profiledata = webservices.GetProfile(userid);
            ViewBag.profiledata = Profiledata;

            var papolar = webservices.BlogList().Where(x => x.Status == "Published" && x.Blog_openCount != null).ToList().OrderByDescending(x => x.Blog_openCount).Take(6).ToList();
            foreach (var i in papolar)
            {
                var empNo1 = i.CreatedBy;
                var userdata1 = webservices.UserList().Where(x => x.EmpNo == empNo1).FirstOrDefault();
                i.CreatedBy = userdata1.Name;
                i.Profileimage = userdata1.Profileimage;
            }
            ViewBag.Papular = papolar;

            return View(data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Blog/Detail/{blogname}")]
        [Obsolete]
        public JsonResult BlogDetail(string blogname)
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var data = webservices.BlogDetailsbySlug(blogname);

            // Retrieve the cookie
            var cookie = Request.Cookies["VisitedBlogs"];

            // Create or retrieve a list of visited blogs from the cookie
            List<string> visitedBlogs = new List<string>();
            if (cookie != null)
            {
                visitedBlogs = cookie.Value.Split(',').ToList();
            }

            // Check if the current blog has already been counted
            if (!visitedBlogs.Contains(blogname))
            {
                // Increment the blog view count for new blog visits
                webservices.BlogOpenCount(data.Id);

                // Add the blogname to the list of visited blogs
                var updatedCookie = new HttpCookie("VisitedBlogs", string.Join(",", visitedBlogs))
                {
                    Expires = DateTime.Now.AddYears(1) // Set cookie to expire in 1 year
                };
                Response.Cookies.Set(updatedCookie);
            }

            var empNo = data.CreatedBy;
            var userdata = webservices.UserList().FirstOrDefault(x => x.EmpNo == empNo);
            var userid = userdata?.id;
            var profileData = webservices.GetProfile(userid);

            var popularBlogs = webservices.BlogList()
                .Where(x => x.Status == "Published" && x.Blog_openCount != null)
                .OrderByDescending(x => x.Blog_openCount)
                .Take(6)
                .ToList();

            foreach (var blog in popularBlogs)
            {
                var empNo1 = blog.CreatedBy;
                var userData1 = webservices.UserList().FirstOrDefault(x => x.EmpNo == empNo1);
                if (userData1 != null)
                {
                    blog.CreatedBy = userData1.Name;
                    blog.Profileimage = userData1.Profileimage;
                }
            }

            return Json(new
            {
                BlogData = data,
                ProfileData = profileData,
                PopularBlogs = popularBlogs
            }, JsonRequestBehavior.AllowGet);
        }


    }
}