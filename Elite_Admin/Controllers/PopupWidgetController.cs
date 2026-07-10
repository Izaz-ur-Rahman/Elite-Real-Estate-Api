using Newtonsoft.Json;
using Elite_Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
    public class PopupWidgetController : Controller
    {
        // GET: PopupWidget
        [Obsolete]
        public ActionResult Index()
        {
            
            
            if (Session["EmpRole"].ToString() == "Admin")
            {
                var data = webservices.PopupwidgetList();

                return View(data);
            }
            else
            {
                var data = webservices.PopupwidgetList().Where(x=>x.Createdby == Session["EmpNo"].ToString()).ToList();

                return View(data);
            }
         
        }

        [Obsolete]
        public ActionResult post_popWidjet()
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
        [HttpGet]
        [Obsolete]
        public ActionResult update_popWidjet(int id)
        {
            var data = webservices.PopupwidgetList().Where(x => x.Id == id).FirstOrDefault();
            var emailtemplate = (from s in webservices.EmailTemplateList(0)
                                 select new SelectListItem
                                 {
                                     Value = s.Id.ToString(),
                                     Text = s.Name
                                 }).ToList();
            ViewBag.EmailTemplateId = emailtemplate;
            return View(data);
        }
        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> PostpopWidjet(PopupDataModal formData,HttpPostedFileBase ImagePopup)
        {
            formData.Createdby = Session["EmpNo"].ToString();
            if (formData.showlinkbutton == null)
            {
                formData.showlinkbutton = false;
            }
            var ImagePopupName = "";
            if (ImagePopup != null)
            {
                var randomFileName = Path.GetRandomFileName().Replace(".", ""); // Remove dot
                var fileNames = randomFileName + Path.GetFileName(ImagePopup.FileName);
                var path = Path.Combine(Server.MapPath("~/raidetimefiles/Popupfiles"), fileNames);
                ImagePopup.SaveAs(path);
                ImagePopupName = fileNames;
            }
            formData.Image = ImagePopupName;

            HttpClient httpClient = new HttpClient();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var json = JsonConvert.SerializeObject(formData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string published = "Popupwidget/addPopupwidget";
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
        public ActionResult PopupDelete_Active(string id, string type)
        {
            
            var data = webservices.Popupwidget_Delete_Active(id,type);
            TempData["msg"] = data;

            return RedirectToAction("Index");

        }

        [Obsolete]
        public ActionResult GetPopupUser()
        {
            var data = webservices.GetPopupUser();
            foreach( var i in data)
            {
                i.PopName = webservices.PopupwidgetList().Where(x => x.Id == i.PopwidgetId).FirstOrDefault().Headlines??"";
            }
            return View(data);
        }
    }
}