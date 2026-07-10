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
    public class EmailtemplateController : Controller
    {
        // GET: Emailtemplate
        [Obsolete]
        public ActionResult Index()
        {
            var data = webservices.EmailTemplateList(0);
            return View(data);
        }
        [Obsolete]
        public ActionResult Add()
        {       
            return View( );
        }

        [HttpPost]
        [ValidateInput(false)]
        [Obsolete]
        public async Task<ActionResult> AddtemplateAsync(EmailTempletModel  emailTempletModel )
        {
            HttpClient httpClient = new HttpClient();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }
            emailTempletModel.Createdby = Session["EmpNo"].ToString(); 
            var json = JsonConvert.SerializeObject(emailTempletModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string published = "EmailTemplate/addTemplate";
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
        public ActionResult update(int?id)
        {
            var data = webservices.EmailTemplateList(id).FirstOrDefault();
            return View(data);
        }
    }
}