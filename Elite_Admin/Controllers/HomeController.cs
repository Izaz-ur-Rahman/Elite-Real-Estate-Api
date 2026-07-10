using Newtonsoft.Json;
using Elite_Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elite_Admin.Utility;

namespace Elite_Admin.Controllers
{
    [Authorize]
    [AuthorizeWithSession]
 
    public class HomeController : Controller
    {
        [Obsolete]
        public ActionResult Index()
        {
            try
            {
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }
                var employeedata = webservices.getUserDetails(DateTime.Now.Year);
                var Countdata = webservices.getCounts(DateTime.Now.Year);
                ViewBag.Counts = Countdata;
                var blogs = webservices.BlogList().Where(x => x.Status != "Published").ToList();
                ViewBag.Blogs = blogs;
                return View(employeedata);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "Index : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return View();
            }
            
        }
        [AllowAnonymous]
        [Obsolete]
        [HttpPost]
        public ActionResult PopNotification(string links)
        {
            links = links.ToLower();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var data = webservices.PopupwidgetList().Where(x => x.IsActive == true && x.DisplayRule == "Entire Site").OrderByDescending(x => x.Id).FirstOrDefault();
            if (data == null)
            {

                var datapage1 = webservices.PopupwidgetList().Where(x => x.IsActive == true && x.PagelinkToShow != null && x.PagelinkToShow.Split(',').Contains(links)).ToList();
                var datapage = datapage1.OrderByDescending(x => x.Id).ToList().FirstOrDefault();
                if (datapage != null)
                {
                    data = datapage;
                }
            }
            else
            {


            }

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Extend()
        {
            // This action is called to extend the session
            Session["LastAccessed"] = DateTime.Now;
            //Session["LastAccessed"] = DateTime.Now; // Any operation to keep the session alive
            return Json(new { success = true });
        }

        [Obsolete]
        public ActionResult GetApplyUserModal()
        {
            var data = webservices.getRecipentcountApplyfromwebsite(DateTime.Now.Year);
            return Json(data, JsonRequestBehavior.AllowGet);


        }
        [Obsolete]
        public ActionResult getMonthlyBlogCount()
        {
            var data = webservices.getMonthlyBlogCount(DateTime.Now.Year);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
            [AllowAnonymous]
        [HttpPost]

        [Obsolete]
        public ActionResult poppersession(int id)
        {
            var data = webservices.PopupwidgetList().Where(x => x.Id == id).FirstOrDefault();
            if (data.DisplayFrequency == "Once Per Session")
            {
                Session["displaysession"] = id.ToString();
            }
            else
            {
                Session["displaysession"] = "always";
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        [AllowAnonymous]
        [HttpPost]
        [Obsolete]
        //, string googlecapchatoken
        public async Task<ActionResult> AddPopUpUserDataAsync(PopModalCls modalCls,string captchakey)
        {
            var checkcaptcha = await IsCaptchaValid(captchakey);
            if (checkcaptcha)
            {
                Session["displaysession"] = modalCls.popId.ToString();
                HttpClient httpClient = new HttpClient();
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }

                var json = JsonConvert.SerializeObject(modalCls);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                string published = "Popupwidget/AddPopupUser";
                string urlg = ConfigurationSettings.AppSettings["ApiUrl"] + published;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                response = await httpClient.PostAsync(urlg, content);

                if (response.IsSuccessStatusCode)
                {
                    var cnt = await response.Content.ReadAsStringAsync();
                    var pro = JsonConvert.DeserializeObject<object>(cnt);


                    //TempData["msg"] = "Added";



                    return Json("Added", JsonRequestBehavior.AllowGet);



                }
            }
            return Json("error", JsonRequestBehavior.AllowGet);

        }

        [AllowAnonymous]
        [HttpPost]
        [Obsolete]

        public async Task<ActionResult> AddWebsiteAction(Cms_WebsiteAction modalCls, string pagelink ,string captchakey)
        {
            var checkcaptcha = await IsCaptchaValid(captchakey);
                if(checkcaptcha)
                {
                HttpClient httpClient = new HttpClient();
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }

                var json = JsonConvert.SerializeObject(modalCls);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                string published = "General/AddWebsiteAction";
                string urlg = ConfigurationSettings.AppSettings["ApiUrl"] + published;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                response = await httpClient.PostAsync(urlg, content);

                if (response.IsSuccessStatusCode)
                {
                    var cnt = await response.Content.ReadAsStringAsync();
                    var pro = JsonConvert.DeserializeObject<object>(cnt);

                    if (modalCls.ApplyFor == "Trail")
                    {
                        TempData["msgWebAction"] = "Trial_Added";


                    }
                    else
                    {
                        TempData["msgWebAction"] = "Added";


                    }



                    return Redirect(pagelink);



                }
            }
            TempData["msgWebAction"] = "error";

            return Redirect(pagelink);


        }
       
        [AllowAnonymous]
        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> AddNewsLetterAsync(Cms_NewsLetter modalCls, string captchakey)
        {
            var checkcaptcha = await IsCaptchaValid(captchakey);
            if (checkcaptcha)
            {
                HttpClient httpClient = new HttpClient();
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }

                var json = JsonConvert.SerializeObject(modalCls);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                string published = "General/AddNewsLetter";
                string urlg = ConfigurationSettings.AppSettings["ApiUrl"] + published;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                response = await httpClient.PostAsync(urlg, content);

                if (response.IsSuccessStatusCode)
                {
                    var cnt = await response.Content.ReadAsStringAsync();
                    var pro = JsonConvert.DeserializeObject<object>(cnt);

               return Json("Added", JsonRequestBehavior.AllowGet);

                }
            }
            return Json("error", JsonRequestBehavior.AllowGet);

        }


        [AllowAnonymous]
        [Obsolete]
        public ActionResult CountryList()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            var data = webservices.CountryList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult RefreshSession()
        {
            // Extend session timeout (e.g., by accessing session to keep it alive)
            Session["LastRefresh"] = DateTime.Now;
            var sessiontime = Session.Timeout;
            // Return a JSON response
            return Json(new { success = true });
        }
        [Authorize]
        [Obsolete]

        public async Task<ActionResult> GetWebsiteAction()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            var data = webservices.getWebsiteAction();
            return View(data);

        }

        [AllowAnonymous]
        [Obsolete]
        public async Task<ActionResult> Addcontactus(Cms_WebsiteContactus Cms_WebsiteContactus,string captchakey)
        {
            var checkcaptcha = await IsCaptchaValid(captchakey);
            if (checkcaptcha)
            {
                HttpClient httpClient = new HttpClient();
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }

                var json = JsonConvert.SerializeObject(Cms_WebsiteContactus);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                string published = "General/AddWebsiteContactus";
                string urlg = ConfigurationSettings.AppSettings["ApiUrl"] + published;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                response = await httpClient.PostAsync(urlg, content);

                if (response.IsSuccessStatusCode)
                {
                    var cnt = await response.Content.ReadAsStringAsync();
                    var pro = JsonConvert.DeserializeObject<object>(cnt);


                    TempData["msgWebAction"] = "Added";



                    return Redirect("/contact-us");



                }
            }
            TempData["msgWebAction"] = "error";

            return Redirect("/contact-us");


        }
   
        [Authorize]
        [Obsolete]
        public ActionResult Getcontactus()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            var data = webservices.getWebsiteContactus();
            return View(data);
        }

        [Authorize]
        [Obsolete]
        public ActionResult GetQuotations()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            var data = webservices.getQuotations();
            return View(data);
        }

        [AuthorizeWithSession]
        [Obsolete]

        public ActionResult GetNewsLetter()
        {
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            var data = webservices.getNewsLetter();
            return View(data);

        }
        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                var secret = "6Lf1BlQaAAAAAO8rayTlV0J8XNHNosr7cJSn2HLI";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        {"remoteip", Request.UserHostAddress}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseViewModel>(captchaResponseJson);
                    var status = captchaResult.Success;
                    return status;
                    //&& captchaResult.Action == "insertData"
                    //&& captchaResult.Score > 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public class CaptchaResponseViewModel
        {
            public bool Success { get; set; }

            [JsonProperty(PropertyName = "error-codes")]
            public IEnumerable<string> ErrorCodes { get; set; }

            [JsonProperty(PropertyName = "challenge_ts")]
            public DateTime ChallengeTime { get; set; }

            public string HostName { get; set; }
            public double Score { get; set; }
            public string Action { get; set; }
        }
    }

    }