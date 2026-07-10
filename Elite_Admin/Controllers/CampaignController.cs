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

namespace Elite_Admin.Controllers
{
    [Authorize]
    [AuthorizeWithSession]
    public class CampaignController : Controller
    {
        // GET: Campaign
        [Obsolete]
        public ActionResult Index()
        {

            var data = webservices.getCms_Campaign(0);
            return View(data);
        }
        [Obsolete]
        public ActionResult PostCampaign()
        {
            var emailtemplate = (from s in webservices.EmailTemplateList(0)
                                 select new SelectListItem
                                 {
                                     Value = s.Id.ToString(),
                                     Text = s.Name
                                 }).ToList();
            ViewBag.EmailTemplateId = emailtemplate;
            return View();
        }
        [Obsolete]
        public ActionResult updateCampaign(int?id)
        {
            var data = webservices.getCms_Campaign(id).FirstOrDefault();
            var details = webservices.getCms_CampaignDetails(data.Id);
            ViewBag.Details = details.Select(x => x.tblid).ToArray();

            var emailtemplate = (from s in webservices.EmailTemplateList(0)
                                 select new SelectListItem
                                 {
                                     Value = s.Id.ToString(),
                                     Text = s.Name
                                 }).ToList();
            emailtemplate.Where(x => x.Value == data.EmailTemplateId.ToString()).First().Selected = true;
            ViewBag.EmailTemplateId = emailtemplate;
            return View(data);
          
        }
        [Obsolete]
        public async Task<ActionResult> AddCampaignAsync(Cms_Campaign cms_Campaign, int[] recipientCheckbox)
        {
            HttpClient httpClient = new HttpClient();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            cms_Campaign.Createdby = Session["EmpNo"].ToString();

            var json = JsonConvert.SerializeObject(cms_Campaign);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string published = "Campaign/addCampaignDetails";
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

        [Obsolete]
        public ActionResult Recipientlist(int id)
        {
            var data = webservices.getCms_CampaignDetails(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [Obsolete]
        public ActionResult LoadList(string id,int? popid)
        {
            List<CampaignModal> datalist = new List<CampaignModal>();
 
            if (id == "Pop_UP")
            {
                if (popid != null && popid > 0)
                {
                    var data = webservices.GetPopupUser().Where(x=>x.PopwidgetId==popid).ToList();
                    foreach (var i in data)
                    {
                        CampaignModal dd = new CampaignModal();
                        dd.Name = i.Name;
                        dd.Email = i.Email;
                        dd.releatedTo = "Pop_UP";
                        dd.id = i.Id;
                        dd.Popid = i.PopwidgetId;
                        datalist.Add(dd);
                    }
                }
                else
                {
                    var data = webservices.GetPopupUser();
                    foreach (var i in data)
                    {
                        CampaignModal dd = new CampaignModal();
                        dd.Name = i.Name;
                        dd.Email = i.Email;
                        dd.releatedTo = "Pop_UP";
                        dd.id = i.Id;
                        dd.Popid = i.PopwidgetId;
                        datalist.Add(dd);
                    }
                }
            }
            else if (id == "Contactus")
            {
                var data = webservices.getWebsiteContactus();
                foreach (var i in data)
                {
                    CampaignModal dd = new CampaignModal();
                    dd.Name = i.FirstName;
                    dd.Email = i.Email;
                    dd.releatedTo = "Contactus";
                    dd.id = i.Id;
                    dd.Popid = 0;
                    datalist.Add(dd);
                }
            }
            else if (id == "Demo_Trial")
            {
                var data = webservices.getWebsiteAction();
                foreach (var i in data)
                {
                    CampaignModal dd = new CampaignModal();
                    dd.Name = i.Name;
                    dd.Email = i.Email;
                    dd.releatedTo = "Contactus";
                    dd.id = i.Id;
                    dd.Popid = 0;
                    datalist.Add(dd);
                }
            }
            else if(id== "ManualUser")
            {

                var data = webservices.GetManualUser();
                foreach (var i in data)
                {
                    CampaignModal dd = new CampaignModal();
                    dd.Name = i.Name;
                    dd.Email = i.Email;
                    dd.releatedTo = "ManualUser";
                    dd.id = i.Id;
                    dd.Popid = 0;
                    datalist.Add(dd);
                }
            }
            else
            {

            }

            return Json(datalist, JsonRequestBehavior.AllowGet);
        }

        [Obsolete]
        public ActionResult PopUplist()
        {
            var datalist = webservices.PopupwidgetList().ToList();
            return Json(datalist, JsonRequestBehavior.AllowGet);

        }
        [Obsolete]
        public ActionResult GetManualUser()
        {
            var datalist = webservices.GetManualUser().ToList();
            return View(datalist);

        }
        [Obsolete]
        public ActionResult PostManualUser()
        {
           
            return View();

        }
        [Obsolete]
        public ActionResult UpdateManualUser(int? id)
        {
            var datalist = webservices.GetManualUser().Where(x=>x.Id==id).FirstOrDefault();
            return View(datalist);

        }
        [HttpPost]
        [Obsolete]

        public async Task<ActionResult> AddManualUser(Cms_Manualuser modalCls )
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

            string published = "Campaign/AddManualuser";
            string urlg = ConfigurationSettings.AppSettings["ApiUrl"] + published;
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

            response = await httpClient.PostAsync(urlg, content);

            if (response.IsSuccessStatusCode)
            {
                var cnt = await response.Content.ReadAsStringAsync();
                var pro = JsonConvert.DeserializeObject<object>(cnt);

                if (pro.ToString() == "updated")
                {
                    TempData["msgWebAction"] = "Updated";

                }
                else
                {
                    TempData["msgWebAction"] = "Added";
                }


                return Redirect("GetManualUser");



            }
            return Redirect("GetManualUser");
            

        }
        public class CampaignModal
        {
            public string Name { get; set; }
            public string releatedTo { get; set; }
            public string Email { get; set; }
            public int? id { get; set; }
            public int? Popid { get; set; }
        }
    }
}