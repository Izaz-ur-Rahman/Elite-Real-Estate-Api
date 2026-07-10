using Elite_Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Controllers
{
    [AuthorizeWithSession]
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        [Obsolete]
        public ActionResult Index()
        {
            ViewBag.baseapiurl = ConfigurationSettings.AppSettings["ApiBaseUrl"];

            var userlist = webservices.UserList();
            return View(userlist);
        }

        [Obsolete]
        public ActionResult UpdateUser(int?id)
        {
            var user= webservices.UserList().Where(x=>x.id==id).FirstOrDefault();

            return View(user);
        }
        public ActionResult AddUser()
        {
            return View();
        }

        [Obsolete]
        public async Task<ActionResult> AddUserDetailsAsync11(UserModal userModal,HttpPostedFileBase Profileimage)
        {
            var ProfileimageName = "";
            if (Profileimage != null)
            {
                var randomFileName = Path.GetRandomFileName().Replace(".", ""); // Remove dot
                var fileNames = randomFileName + Path.GetFileName(Profileimage.FileName);
                var path = Path.Combine(Server.MapPath("~/EliteFiles/ProfileImages"), fileNames);
                Profileimage.SaveAs(path);
                ProfileimageName = fileNames;
            }
            userModal.Profileimage = ProfileimageName;

            HttpClient httpClient = new HttpClient();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var json = JsonConvert.SerializeObject(userModal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string published = "General/addUser";
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
                else if(pro.ToString()== "UsernameExist")
                {
                    TempData["msg"] = "UserNameExist";

                }else
                {

                    TempData["msg"] = "Added";

                }
                return RedirectToAction("Index");


            }
            return RedirectToAction("Index");

        }


        public async Task<ActionResult> AddUserDetailsAsync(UserModal userModal,HttpPostedFileBase Profileimage)
        {
            HttpClient httpClient = new HttpClient();

            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["apiToken"].ToString());

            string url = ConfigurationSettings.AppSettings["ApiUrl"] + "General/addUser";

            using (var form = new MultipartFormDataContent())
            {
                // Add JSON model
                var json = JsonConvert.SerializeObject(userModal);
                form.Add(new StringContent(json, Encoding.UTF8, "application/json"), "userModal");

                // Add file
                if (Profileimage != null && Profileimage.ContentLength > 0)
                {
                    var streamContent = new StreamContent(Profileimage.InputStream);
                    streamContent.Headers.ContentType =
                        new MediaTypeHeaderValue(Profileimage.ContentType);

                    form.Add(streamContent, "Profileimage", Profileimage.FileName);
                }

                var response = await httpClient.PostAsync(url, form);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    if (result.Contains("Updated"))
                        TempData["msg"] = "Updated";
                    else if (result.Contains("UsernameExist"))
                        TempData["msg"] = "UserNameExist";
                    else
                        TempData["msg"] = "Added";
                }
            }

            return RedirectToAction("Index");
        }


        //[Obsolete]
        //public async Task<ActionResult> DeleteUserAsync(int id)
        //{
        //    HttpClient httpClient = new HttpClient();

        //    if (Session["apiToken"] == null)
        //    {
        //        var gettoken = webservices.gettoken();
        //        Session["apiToken"] = gettoken.access_token;
        //    }

        //    string apiUrl = ConfigurationSettings.AppSettings["ApiUrl"] + "General/deleteUser?id=" + id;

        //    httpClient.DefaultRequestHeaders.Clear();
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

        //     HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);
           

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();
        //        TempData["msg"] = "Deleted";
        //    }
        //    else
        //    {
        //        TempData["msg"] = "Error deleting user.";
        //    }

        //    return RedirectToAction("Index");
        //}

        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            HttpClient httpClient = new HttpClient();

            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            string apiUrl = ConfigurationSettings.AppSettings["ApiUrl"] + "General/deleteUser?id=" + id;

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

            // Use PostAsync with empty content
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                TempData["msg"] = "Deleted";
            }
            else
            {
                TempData["msg"] = "Error deleting user.";
            }

            return RedirectToAction("Index");
        }


        [Obsolete]
        public ActionResult Profile(int? id)
        {
            ViewBag.baseapiurl = ConfigurationSettings.AppSettings["ApiBaseUrl"];

            var userdata = webservices.UserList().Where(x => x.id == id).FirstOrDefault();
            ViewBag.userdata= userdata;
            var data = webservices.GetProfile(id);
            return View(data);
        }

        [Obsolete]
        public async Task<ActionResult> updateUserDetailsProfile(UserModal userModal )
        {
            
            HttpClient httpClient = new HttpClient();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var json = JsonConvert.SerializeObject(userModal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string published = "General/addUser";
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
                else if (pro.ToString() == "UsernameExist")
                {
                    TempData["msg"] = "UserNameExist";

                }
                else
                {

                    TempData["msg"] = "Added";

                }
                return RedirectToAction("Index","Home");


            }
            return RedirectToAction("Index", "Home");


        }
        [Obsolete]
        public async Task<ActionResult> updateEmployeeDetailsProfile(UserProfile userModal)
        {



            HttpClient httpClient = new HttpClient();
            if (Session["apiToken"] == null)
            {
                var gettoken = webservices.gettoken();
                Session["apiToken"] = gettoken.access_token;
            }

            var json = JsonConvert.SerializeObject(userModal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string published = "General/updateProfile";
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
                else if (pro.ToString() == "UsernameExist")
                {
                    TempData["msg"] = "UserNameExist";

                }
                else
                {

                    TempData["msg"] = "Added";

                }
                return RedirectToAction("Index", "Home");


            }
            return RedirectToAction("Index", "Home");


        }
    }
}