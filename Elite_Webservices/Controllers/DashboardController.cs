using Elite_Webservices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace Elite_Webservices.Controllers
{
    public class DashboardController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Dashboard/getCounts")]
        [Obsolete]
        public HttpResponseMessage getCounts(int year)
        {

            CountModal data = new CountModal();

            data.PropertiesCount = db.PropertyListings.Count();
            data.OffPlanCount = db.PropertyListings.Where(x =>x.ListingType == "Off-Plan" ).Count();
            data.RentalCount = db.PropertyListings.Where(x => x.ListingType == "For Rent").Count();
            data.SecondryCount = db.PropertyListings.Where(x => x.ListingType == "For Sale").Count();
            data.ContactCount = db.ContactUs.Count();
            data.QueriesCount = db.Quotations.Count();
            data.UserCount = db.Users.Where(x =>  ((year != null && year > 0) ? x.CreatedDate.Value.Year == year : true)).Count();
            data.BlogsCount = db.Cms_blogDetail.Where(x => x.IsActive == true && ((year != null && year > 0) ? x.EntryDate.Value.Year == year : true)).Count();
            data.PublishBlogCount = db.Cms_blogDetail.Where(x => x.Status == "published" && ((year != null && year > 0) ? x.EntryDate.Value.Year == year : true)).Count();
          
            return Request.CreateResponse(HttpStatusCode.OK, data);


        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Dashboard/getRecipentcountApplyfromwebsite")]
        [Obsolete]
        public HttpResponseMessage getUsercountApplyfromwebsite(int year)
        {
            ApplyUserModal data = new ApplyUserModal();
            data.ContactusCount = db.Cms_WebsiteContactus.Where(x => x.IsActive == true && ((year != null && year > 0) ? x.EntryDate.Value.Year == year : true)).Count();
            data.PopUserCount = db.Cms_BlogPopUserApply.Where(x => x.IsActive == true && ((year != null && year > 0) ? x.EntryDate.Value.Year == year : true)).Count();
            data.Demo = db.Cms_WebsiteAction.Where(x => x.ApplyFor == "Demo" && ((year != null && year > 0) ? x.EntryDate.Value.Year == year : true)).Count();
            data.Trial = db.Cms_WebsiteAction.Where(x => x.ApplyFor != "Demo" && ((year != null && year > 0) ? x.EntryDate.Value.Year == year : true)).Count();
            data.Manual = db.Cms_Manualuser.Where(x => x.IsActive == true && ((year != null && year > 0) ? x.EntryDate.Value.Year == year : true)).Count();
            return Request.CreateResponse(HttpStatusCode.OK, data);

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Dashboard/getUserDetails")]
        [Obsolete]
        public HttpResponseMessage getUserDetails(int year)
        {
            List<UserDetailsModal> data = new List<UserDetailsModal>();
            var user = db.TblEmployees.ToList();
            foreach (var i in user)
            {
                UserDetailsModal dd = new UserDetailsModal();
                dd.Name = i.FULL_NAME;
                dd.Picname = i.ProfilePicName;
                dd.Designation = i.Position;
                dd.BlogCount = db.Cms_blogDetail.Where(x => x.IsActive == true && x.Createdby == i.EMPLOYEE_NUMBER).Count();
                data.Add(dd);
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Dashboard/getMonthlyBlogCount")]
        public HttpResponseMessage GetMonthlyBlogCount(int year)
        {
            // Get the monthly blog data
            var monthlyBlogData = db.Cms_blogDetail
                .Where(x => x.IsActive == true && x.EntryDate.Value.Year == year)
                .GroupBy(x => x.EntryDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    BlogCount = g.Count()
                })
                .ToList();
            // Create a list of all months with abbreviations (Jan, Feb, etc.)
            var allMonths = Enumerable.Range(1, 12)
                .Select(month => new MonthlyBlogCount
                {
                    Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat
                               .GetMonthName(month)
                               .Substring(0, 3),  // Get the first 3 characters for the abbreviation
            BlogCount = 0 // Default blog count
        })
                .ToList();

            // Update blog counts for existing months
            foreach (var blogData in monthlyBlogData)
            {
                allMonths[blogData.Month - 1].BlogCount = blogData.BlogCount; // Month - 1 for zero-based index
            }

            return Request.CreateResponse(HttpStatusCode.OK, allMonths);
        }

    
         
        public class MonthlyBlogCount
        {
            public string Month { get; set; }
            public int BlogCount { get; set; }
        }
        public class UserDetailsModal
        {
            public string Name { get; set; }
            public string Picname { get; set; }
            public string Designation { get; set; }
            public int? BlogCount { get; set; }
           // public string Role { get; set; }
        }
            public class ApplyUserModal
        {
            public int? ContactusCount { get; set; }
            public int? PopUserCount { get; set; }
            public int? Demo { get; set; }
            public int? Trial { get; set; }
            public int? Manual { get; set; }


        }

        public class CountModal
        {
            public int? PropertiesCount { get; set; }
            public int? OffPlanCount { get; set; }
            public int? RentalCount { get; set; }
            public int? SecondryCount { get; set; }
            public int? ContactCount { get; set; }
            public int? QueriesCount { get; set; }

            public int? UserCount { get; set; }
            public int? BlogsCount { get; set; }
            public int? PublishBlogCount { get; set; }
           
      
        }
    }
}
