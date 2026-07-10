using Elite_Admin.Models;
using Elite_Admin.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Elite_Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private string returnUrl;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> Login(string googlecapchatoken, string login_username, string login_password/*,string applicationcheck,string applicationcheckrole*/)
        {
            //var isCaptchaValid = await IsCaptchaValid(googlecapchatoken);
            var isCaptchaValid = true;
            if (isCaptchaValid)
            {
                try
                {
                    var Appname = ConfigurationManager.AppSettings["AppName"];

                    if (string.IsNullOrEmpty(Appname))
                    {
                        return Json("AppName configuration is missing in web.config");
                    }

                    // Check Session
                    if (Session == null)
                    {
                        return Json("Session is null - check SessionState configuration");
                    }

                    var gettoken = webservices.gettoken();
                    if (gettoken == null)
                    {
                        return Json("Failed to get token from web service");
                    }

                    Session["apiTokenHR"] = gettoken.access_token;
                    RequestADLogin reqAD = new RequestADLogin();
                    reqAD.UserName = login_username;
                    reqAD.Password = login_password;
                    reqAD.AppName = ConfigurationManager.AppSettings["AppName"].ToString();

                    var output = webservices.LoginNew(login_username, login_password, Appname);
                    if (output != null && !string.IsNullOrEmpty(output.EmpNo.ToString()))
                    {
                        Session["DefaultLanguage"] = 0;
                        string lang = "";

                        var profile = webservices.getemprofile(output.EmpNo.ToString());
                        Session["EmployeeID"] = output.Empid;
                        Session["EmpNo"] = output.EmpNo;
                        Session["userid"] = output.EmpNo;
                        Session["Name"] = profile.EmployeeName;
                        Session["EmpNameAr"] = profile.EmployeeName;
                        Session["Password"] = login_password.Trim();
                        Session["EmpRole"] = output.AppRoleName.Trim();
                        Session["jobtitle"] = profile.Designation;
                        if (profile.profilepicname != null)
                        {
                            string img = profile.profilepicname;
                            // Session["Pic"] = string.Format("data:image;base64,{0}", img);
                            Session["Pic"] = img;
                        }
                        else
                        {
                           Session["Pic"] = "download.png";
                        }
                        FormsAuthentication.SetAuthCookie(login_username, false);
                        if (returnUrl != null && returnUrl != "")
                        {
                            return Redirect(returnUrl);
                        }
                        return Redirect("/Home/index");
                    }
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(LogType.Error, "Login : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                    return Json(ex.Message.ToString());
                   return Content("<script>alert('some thing went wrong please try again.');window.location.href = '/Login/LogOut';</script>");
                }
                TempData["error"] = "1";
                return Redirect("index");
                //end of company table
            }
            else
            {
                return Content("<script>alert('some thing went wrong please try again.');window.location.href = '/Login/LogOut';</script>");
            }
        }

        public ActionResult LogOut()
        {             
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();           
            FormsAuthentication.SignOut();
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();
            return RedirectToAction("index", "Account");
        }
        public class CaptchaResponseViewModel
        {
            public bool Success { get; set; }

           
            public IEnumerable<string> ErrorCodes { get; set; }

           
            public DateTime ChallengeTime { get; set; }

            public string HostName { get; set; }
            public double Score { get; set; }
            public string Action { get; set; }
        }

    }
}