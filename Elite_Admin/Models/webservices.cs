using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Models
{
    public class webservices
    {
        internal static tokenDetails gettoken()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var usernametoken = ConfigurationManager.AppSettings["tokenusername"].ToString();
                var passwordtoken = ConfigurationManager.AppSettings["tokenpassword"].ToString();
                using (WebClient client = new WebClient())
                {
                    var reqparm = new System.Collections.Specialized.NameValueCollection();
                    reqparm.Add("username", usernametoken);
                    reqparm.Add("password", passwordtoken);
                    reqparm.Add("grant_type", "password");

                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    byte[] responsebytes = client.UploadValues(ConfigurationManager.AppSettings["ApitokenUrl"], "POST", reqparm);

                    string responsebody = Encoding.UTF8.GetString(responsebytes);

                    var pro = JsonConvert.DeserializeObject<object>(responsebody);
                    return JsonConvert.DeserializeObject<tokenDetails>(pro.ToString());

                }
            }
            catch (WebException ex)
            {         
                return null;
            }
        }

        [Obsolete]
        internal static BlogPostModel BlogDetailsbySlug(string Name)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Blog/BlogDetailsbySlug?name=" + Name;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<BlogPostModel>(pro.ToString());

                return data;

            }
        }

        [Obsolete]
        internal static PropertyDetailsVM PropertybySlug(string Name)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "GetPropertybySlug?name=" + Name;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<PropertyDetailsVM>(pro.ToString());

                return data;

            }
        }

        [Obsolete]
        internal static PropertyListing PropertybyId(int id)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "GetPropertybyId?id=" + id;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<PropertyListing>(pro.ToString());

                return data;

            }
        }

        [Obsolete]
        internal static string DeleteProperty(int? id)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "DeleteProperty?id=" + id;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = pro.ToString();

                return data;
            }
        }


        [Obsolete]
        internal static List<BlogPostModel> BlogList()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Blog/BlogList?id=0";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject <List<BlogPostModel>>(pro.ToString());

                return data;

            }
        }
        public static LoginNewResponse LoginNew(string userName, string password, string appName)
        {
            try
            {
                var ss = ConfigurationManager.AppSettings["ApiUrl"];


                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var reqparm = new System.Collections.Specialized.NameValueCollection();
                    reqparm.Add("userName", userName);
                    reqparm.Add("password", password);
                    reqparm.Add("appName", appName);
                    client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiTokenHR"]);

                    byte[] responsebytes = client.UploadValues(ConfigurationManager.AppSettings["ApiUrl"] + "/login/login", "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    var pro = JsonConvert.DeserializeObject<object>(responsebody);
                    return JsonConvert.DeserializeObject<LoginNewResponse>(pro.ToString());

                }
            }
            catch (WebException ex)
            {
                

                return null;
            }
        }
        [Obsolete]
        internal static EmployeProfileModel getemprofile(string empNo)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "/Employee/EmployeeProfile?EmpNo=" + empNo;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiTokenHR"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<EmployeProfileModel>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<PopupDataModal>PopupwidgetList()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Popupwidget/PopupwidgetList?id=0";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<PopupDataModal>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static string Popupwidget_Delete_Active(string id, string type)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Popupwidget/PopupwidgetDelete_Active?id="+id+ "&type="+type;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = (pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static string PublishBlog(int? id,string Publishdate,string type)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Blog/PublishBlog?id=" + id+ "&Publishdate="+Publishdate+ "&type="+ type;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data =  pro.ToString() ;

                return data;

            }
        }
        [Obsolete]
        internal static List<Cms_BlogPopUserApply> GetPopupUser( )
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Popupwidget/GetPopupUser";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<Cms_BlogPopUserApply>>(pro.ToString());


                return data;

            }
        }
        [Obsolete]
        internal static string DeleteBlog(int? id )
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Blog/DeleteBlog?id=" + id ;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = pro.ToString();

                return data;

            }
        }
        [Obsolete]
        internal static List<UserModal> UserList()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "General/GetUser";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<UserModal>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static UserProfile GetProfile(int?id)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "General/GetProfile?id="+id;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<UserProfile>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<Cms_EmailTemplete> EmailTemplateList(int?id)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "EmailTemplate/EmailTemplateList?id="+id;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<Cms_EmailTemplete>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static string BlogOpenCount(int? id)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Blog/BlogOpencount?id=" + id;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data =  (pro.ToString());

                return data;

            }
        }

        [Obsolete]
        internal static List<SelectListItem> CountryList()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "General/GetCountrylist";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<SelectListItem>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<Cms_WebsiteAction> getWebsiteAction()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "General/getWebsiteAction";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<Cms_WebsiteAction>>(pro.ToString());

                return data;

            }
        }

        [Obsolete]
        internal static List<ContactU> getWebsiteContactus()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "General/getContactus";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<ContactU>>(pro.ToString());

                return data;

            }
        }

        [Obsolete]
        internal static List<Quotation> getQuotations()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "General/getQuotations";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<Quotation>>(pro.ToString());

                return data;

            }
        }

        [Obsolete]
        internal static List<GetCms_Campaign> getCms_Campaign(int?id)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Campaign/getCampaign?id="+id;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<GetCms_Campaign>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<GetCampaignDetals> getCms_CampaignDetails(int? id)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Campaign/getCampaignDetails?id=" + id;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<GetCampaignDetals>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<Cms_Manualuser> GetManualUser()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Campaign/getManualuser";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<Cms_Manualuser>>(pro.ToString());

                return data;

            }
        }

        //Dashboard
        [Obsolete]
        internal static CountModal getCounts(int? year)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Dashboard/getCounts?year=" + year;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<CountModal>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static ApplyUserModal getRecipentcountApplyfromwebsite(int? year)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Dashboard/getRecipentcountApplyfromwebsite?year=" + year;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<ApplyUserModal>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<UserDetailsModal> getUserDetails(int? year)
        {
           
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Dashboard/getUserDetails?year=" + year;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<UserDetailsModal>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<MonthlyBlogCount> getMonthlyBlogCount(int? year)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "Dashboard/getMonthlyBlogCount?year="+year;
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<MonthlyBlogCount>>(pro.ToString());

                return data;

            }
        }
        [Obsolete]
        internal static List<Cms_NewsLetter> getNewsLetter()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var mainurl = ConfigurationSettings.AppSettings["ApiUrl"];
                var weburl = "General/getNewsLetter";
                client.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["apiToken"]);

                string response = client.DownloadString(mainurl + weburl);
                var pro = JsonConvert.DeserializeObject<object>(response);
                var data = JsonConvert.DeserializeObject<List<Cms_NewsLetter>>(pro.ToString());

                return data;

            }
        }
    }
}