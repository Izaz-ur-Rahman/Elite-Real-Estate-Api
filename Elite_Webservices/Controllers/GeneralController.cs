using Elite_Webservices.Helper;
using Elite_Webservices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Elite_Webservices.Controllers
{
    public class GeneralController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/GetUser")]
        [Obsolete]
        public HttpResponseMessage GetUser()
        {
            var data = (from u in db.Users
                        join e in db.TblEmployees on u.EmpId equals e.ID
                        select new UserModal
                        {
                            Email = e.EMAIL_ADDRESS,
                            id = u.ID,
                            Role = u.RoleNames,
                            Name = e.FULL_NAME,
                            Profileimage = e.ProfilePicName,
                            EmpId = u.EmpId,
                            EmpNo = e.EMPLOYEE_NUMBER,

                        }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, data);

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/GetUserbyId")]
        [Obsolete]
        public HttpResponseMessage GetUserbyId(int Id)
        {
            var data = (from u in db.Users
                        join e in db.TblEmployees on u.EmpId equals e.ID
                        select new UserModal
                        {
                            Email = e.EMAIL_ADDRESS,
                            id = u.ID,
                            Role = u.RoleNames,
                            Name = e.FULL_NAME,
                            Profileimage = e.ProfilePicName,
                            EmpId = u.EmpId,
                            EmpNo = e.EMPLOYEE_NUMBER,

                        }).ToList();
            var user = data.Where(x => x.id == Id).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, user);

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/addUser11")]
        [Obsolete]
        public HttpResponseMessage addUser11(UserModal userModal)
        {
            try
            {


                if (userModal.id != null && userModal.id > 0)
                {
                    var userdata = db.Users.Where(x => x.ID == userModal.id).FirstOrDefault();

                    userdata.Password = Utility.EncryptionHelper.EncrptPassword(userModal.Password);
                    userdata.AppWiseRoles = userModal.Role + "-";
                    userdata.RoleNames = userModal.Role + "-";
                    userdata.AppIDs = "raideTime-";
                    userdata.IsActive = true;
                    userdata.UpdatedDate = DateTime.Now;

                    db.SaveChanges();

                    var empid = userdata.EmpId;
                    var empdata = db.TblEmployees.Where(x => x.ID == empid).FirstOrDefault();
                    empdata.FULL_NAME = userModal.Name;
                    empdata.FIRST_NAME = userModal.Name;

                    if (!String.IsNullOrEmpty(userModal.Profileimage))
                    {
                        empdata.ProfilePicName = userModal.Profileimage;
                    }
                    db.SaveChanges();



                    return Request.CreateResponse(HttpStatusCode.OK, "Updated");



                }
                else
                {
                    var checkusername = db.Users.Where(x => x.UserName == userModal.Email).FirstOrDefault();
                    if (checkusername != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "UsernameExist");

                    }
                    var empdata = new TblEmployee();
                    empdata.FULL_NAME = userModal.Name;
                    empdata.FIRST_NAME = userModal.Name;
                    empdata.EMAIL_ADDRESS = userModal.Email;
                    empdata.ProfilePicName = userModal.Profileimage;
                    db.TblEmployees.Add(empdata);
                    db.SaveChanges();
                    var empid = empdata.ID;
                    var empdataup = db.TblEmployees.Where(x => x.ID == empid).FirstOrDefault();
                    empdataup.EMPLOYEE_NUMBER = empid.ToString();
                    db.SaveChanges();



                    var userdata = new User();
                    userdata.UserName = userModal.Email;
                    userdata.Password = Utility.EncryptionHelper.EncrptPassword(userModal.Password);
                    userdata.AppWiseRoles = userModal.Role + "-";
                    userdata.RoleNames = userModal.Role + "-";
                    userdata.AppIDs = "raideTime-";
                    userdata.EmpId = empid;
                    userdata.IsActive = true;
                    userdata.CreatedBy = 0;
                    userdata.CreatedDate = DateTime.Now;
                    userdata.UpdatedDate = DateTime.Now;
                    db.Users.Add(userdata);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Added");

                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/addUser")]
        public async Task<HttpResponseMessage> addUser()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid multipart request");

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                UserModal userModal = null;
                string profileImageName = "";

                foreach (var content in provider.Contents)
                {
                    var name = content.Headers.ContentDisposition.Name?.Trim('"');

                    // -------- JSON MODEL --------
                    if (name == "userModal")
                    {
                        var json = await content.ReadAsStringAsync();
                        userModal = JsonConvert.DeserializeObject<UserModal>(json);
                    }

                    // -------- FILE --------
                    if (content.Headers.ContentDisposition.FileName != null)
                    {
                        var fileBytes = await content.ReadAsByteArrayAsync();

                        var fileName =
                            Path.GetRandomFileName().Replace(".", "") +
                            Path.GetExtension(content.Headers.ContentDisposition.FileName.Trim('"'));

                        var folderPath = HttpContext.Current.Server.MapPath("~/EliteFiles/ProfileImages/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        File.WriteAllBytes(Path.Combine(folderPath, fileName), fileBytes);
                        profileImageName = fileName;
                    }
                }

                // -------- SAFETY CHECK --------
                if (userModal == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "User data missing");

                userModal.Profileimage = profileImageName;

                // ---------------- BUSINESS LOGIC ----------------

                if (userModal.id.HasValue && userModal.id > 0)
                {
                    var userdata = db.Users.FirstOrDefault(x => x.ID == userModal.id);
                    if (userdata == null)
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "User not found");

                    userdata.Password = Utility.EncryptionHelper.EncrptPassword(userModal.Password);
                    userdata.AppWiseRoles = userModal.Role + "-";
                    userdata.RoleNames = userModal.Role + "-";
                    userdata.AppIDs = "raideTime-";
                    userdata.IsActive = true;
                    userdata.UpdatedDate = DateTime.Now;
                    db.SaveChanges();

                    var empdata = db.TblEmployees.FirstOrDefault(x => x.ID == userdata.EmpId);
                    if (empdata != null)
                    {
                        empdata.FULL_NAME = userModal.Name;
                        empdata.FIRST_NAME = userModal.Name;

                        if (!string.IsNullOrEmpty(profileImageName))
                            empdata.ProfilePicName = profileImageName;

                        db.SaveChanges();
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                }
                else
                {
                    if (db.Users.Any(x => x.UserName == userModal.Email))
                        return Request.CreateResponse(HttpStatusCode.OK, "UsernameExist");

                    var emp = new TblEmployee
                    {
                        FULL_NAME = userModal.Name,
                        FIRST_NAME = userModal.Name,
                        EMAIL_ADDRESS = userModal.Email,
                        ProfilePicName = profileImageName
                    };

                    db.TblEmployees.Add(emp);
                    db.SaveChanges();

                    emp.EMPLOYEE_NUMBER = emp.ID.ToString();
                    db.SaveChanges();

                    var user = new User
                    {
                        UserName = userModal.Email,
                        Password = Utility.EncryptionHelper.EncrptPassword(userModal.Password),
                        AppWiseRoles = userModal.Role + "-",
                        RoleNames = userModal.Role + "-",
                        AppIDs = "raideTime-",
                        EmpId = emp.ID,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/updateProfile")]
        [Obsolete]
        public HttpResponseMessage updateProfile(UserProfile userModal)
        {
            try
            {
                var empdata = db.TblEmployees.Where(x => x.ID == userModal.Empid).FirstOrDefault();
                if (empdata != null)
                {
                    empdata.Address = userModal.Address;
                    empdata.Bio = userModal.Bio;
                    empdata.Facebook = userModal.Facebook;
                    empdata.LinkedIn = userModal.LinkedIn;
                    empdata.MOBILE_M = userModal.MobileNo;
                    empdata.WhatsappNo = userModal.WhatsappNo;
                    empdata.EMAIL_ADDRESS = userModal.Email;
                    empdata.Twitter = userModal.Twitter;
                    empdata.DATE_OF_BIRTH = userModal.Birthday;
                    empdata.Position = userModal.Position;

                    db.SaveChanges();

                }
                return Request.CreateResponse(HttpStatusCode.OK, "Updated");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/General/GetProfile")]
        //[Obsolete]
        //public HttpResponseMessage GetProfile(int id)
        //{
        //    try
        //    {
        //        var empid = db.Users.Where(x => x.ID == id).FirstOrDefault().EmpId;

        //        var data = (from e in db.TblEmployees
        //                    where e.ID == empid
        //                    select new UserProfile
        //                    {
        //                        Address = e.Address,
        //                        Bio = e.Bio,
        //                        Birthday=e.DATE_OF_BIRTH,
        //                        Empid=e.ID,
        //                        Facebook=e.Facebook,
        //                        LinkedIn=e.LinkedIn,
        //                        MobileNo=e.MOBILE_M,
        //                        WhatsappNo = e.WhatsappNo,
        //                        Position =e.Position,
        //                        Twitter=e.Twitter,
        //                        Email=e.EMAIL_ADDRESS,
        //                        EmpNo=e.EMPLOYEE_NUMBER,

        //                    }).FirstOrDefault();
        //        return Request.CreateResponse(HttpStatusCode.OK, data);


        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

        //    }
        //}

        [System.Web.Http.HttpGet]
        //[Route("api/General/GetProfile")]
        [System.Web.Http.Route("api/General/GetProfile")]
        public HttpResponseMessage GetProfile(int empId)
        {
            try
            {
                var data = (from e in db.TblEmployees
                            where e.ID == empId
                            select new UserProfile
                            {
                                Empid = e.ID,
                                EmpNo = e.EMPLOYEE_NUMBER,
                                Address = e.Address,
                                Bio = e.Bio,
                                Birthday = e.DATE_OF_BIRTH,
                                Facebook = e.Facebook,
                                LinkedIn = e.LinkedIn,
                                MobileNo = e.MOBILE_M,
                                WhatsappNo = e.WhatsappNo,
                                Position = e.Position,
                                Twitter = e.Twitter,
                                Email = e.EMAIL_ADDRESS
                            }).FirstOrDefault();

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/GetCountrylist")]
        [Obsolete]
        public HttpResponseMessage GetCountrylist()
        {
            try
            {
                var data = (from c in db.Countries
                            select new
                            {
                                Value = c.id.ToString(),
                                Text = c.EnName
                            }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/deleteUser")]
        [Obsolete]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User not found.");
                }

                // Optionally delete employee data if you want to clean up completely
                var employee = db.TblEmployees.FirstOrDefault(e => e.ID == user.EmpId);
                if (employee != null)
                {
                    db.TblEmployees.Remove(employee);
                }

                db.Users.Remove(user);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/AddWebsiteAction")]
        [Obsolete]
        public HttpResponseMessage AddWebsiteAction(Cms_WebsiteAction userModal)
        {
            try
            {

                userModal.EntryDate = DateTime.Now;
                userModal.IsActive = true;
                userModal.Notes = "-";
                db.Cms_WebsiteAction.Add(userModal);
                db.SaveChanges();
                try
                {
                    string emailtype = "Welcome";
                    string subject = "Welcome to RaideTime!";

                    string Message = "Welcome to the RaideTime community! We're thrilled to have you on board. Get ready for exclusive content, updates, and much more!";
                    // var emaildata = emailsend.SendEmails((pop.Email, pop.Name, Message, emailtype, subject));
                    string recemial = "nadeemullah@raideit.com";
                    if (userModal.ApplyFor == "Demo")
                    {
                        var popemailtemplete = db.Cms_EmailTemplete.Where(x => x.Name.ToLower() == "live demo").FirstOrDefault();
                        if (popemailtemplete != null)
                        {
                            var tempid = popemailtemplete.Id;
                            var tempalte = db.Cms_EmailTemplete.Where(x => x.Id == tempid).FirstOrDefault();
                            if (tempalte != null)
                            {
                                Message = tempalte.Details;

                            }
                            recemial = "";
                        }
                        var Messages = Message.Replace("[Name]", userModal.Name);
                        var emaildata1 = emailsend.SendEmailsWithTemplete((userModal.Email, userModal.Name, Messages, emailtype, subject, recemial));
                    }
                    else
                    {
                        var popemailtemplete = db.Cms_EmailTemplete.Where(x => x.Name.ToLower() == "free trial").FirstOrDefault();
                        if (popemailtemplete != null)
                        {
                            var tempid = popemailtemplete.Id;
                            var tempalte = db.Cms_EmailTemplete.Where(x => x.Id == tempid).FirstOrDefault();
                            if (tempalte != null)
                            {
                                Message = tempalte.Details;

                            }
                            recemial = "";
                        }
                        var Messages = Message.Replace("[Name]", userModal.Name);
                        var emaildata1 = emailsend.SendEmailsWithTemplete((userModal.Email, userModal.Name, Messages, emailtype, subject, recemial));
                    }
                }
                catch (Exception ex)
                {

                }
                return Request.CreateResponse(HttpStatusCode.OK, "Added");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/getWebsiteAction")]
        [Obsolete]
        public HttpResponseMessage getWebsiteAction()
        {
            try
            {

                var list = db.Cms_WebsiteAction.Where(x => x.IsActive == true).ToList();
                foreach (var i in list)
                {
                    if (i.CountryId != null)
                    {
                        i.Country = db.Countries.Where(C => C.id == i.CountryId).FirstOrDefault().EnName;
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/AddWebsiteContactus")]
        [Obsolete]
        public HttpResponseMessage AddWebsiteContactus(Cms_WebsiteContactus userModal)
        {
            try
            {

                userModal.EntryDate = DateTime.Now;
                userModal.IsActive = true;
                userModal.Notes = userModal.Notes;
                db.Cms_WebsiteContactus.Add(userModal);
                db.SaveChanges();
                try
                {
                    string emailtype = "Welcome";
                    string subject = "Welcome to RaideTime!";
                    string recemial = "";
                    string Message = "";
                    var popemailtemplete = db.Cms_EmailTemplete.Where(x => x.Name.ToLower() == "contact us").FirstOrDefault();
                    if (popemailtemplete != null)
                    {
                        var tempid = popemailtemplete.Id;
                        var tempalte = db.Cms_EmailTemplete.Where(x => x.Id == tempid).FirstOrDefault();
                        if (tempalte != null)
                        {
                            Message = tempalte.Details;

                        }
                        recemial = "";
                    }
                    var Messages = Message.Replace("[Name]", userModal.Name);
                    var emaildata1 = emailsend.SendEmailsWithTemplete((userModal.Email, userModal.Name, Messages, emailtype, subject, recemial));
                }
                catch (Exception ex)
                {

                }
                return Request.CreateResponse(HttpStatusCode.OK, "Added");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/Contactus")]
        [Obsolete]
        public HttpResponseMessage AddContactus(ContactU contact)
        {
            try
            {
                // Save to the database
                db.ContactUs.Add(contact);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Contact information submitted successfully." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = $"Internal Server Error: {ex.Message}" });
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/getContactus")]
        [Obsolete]
        public HttpResponseMessage getContactus()
        {
            try
            {

                var list = db.ContactUs.OrderByDescending(c => c.Id).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/getAgentContactEmails")]
        [Obsolete]
        public HttpResponseMessage getAgentContactEmails()
        {
            try
            {

                var list = db.ContactUs.Where(x => x.PageSource == "Agent Contact").OrderByDescending(c => c.Id).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/AddQuotations")]
        [Obsolete]
        public HttpResponseMessage AddShareNeeds(Quotation quotation)
        {
            try
            {
                // Save to the database
                db.Quotations.Add(quotation);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Your needs information are submitted successfully." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = $"Internal Server Error: {ex.Message}" });
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/getQuotations")]
        [Obsolete]
        public HttpResponseMessage getShareNeeds()
        {
            try
            {

                var list = db.Quotations.OrderByDescending(x => x.Id).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/getWebsiteContactus")]
        [Obsolete]
        public HttpResponseMessage getWebsiteContactus()
        {
            try
            {

                var list = db.Cms_WebsiteContactus.Where(x => x.IsActive == true).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/getNewsLetter")]
        [Obsolete]
        public HttpResponseMessage getNewsLetter()
        {
            try
            {

                var list = db.Cms_NewsLetter.Where(x => x.isActive == true).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/AddNewsLetter")]
        [Obsolete]
        public HttpResponseMessage AddNewsLetter(Cms_NewsLetter cms_News)
        {
            try
            {

                cms_News.isActive = true;
                cms_News.EntryDate = DateTime.Now;
                cms_News.Notes = "-";
                db.Cms_NewsLetter.Add(cms_News);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Added");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message.ToString());

            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/Contactemail")]
        public async Task<HttpResponseMessage> ContactUsSubmission(ContactUsemail contact)
        {
            try
            {
                // Validate model
                if (contact == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Contact information is required.");
                }

                // Basic validation
                if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.FirstName))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Name and email are required fields.");
                }

                ContactusEmail objemail = new ContactusEmail();

                // Send email to admin
                var adminResult = await objemail.SendContactUsToAdminAsync(contact);

                // Send thank you email to customer
                var customerResult = await objemail.SendThankYouEmailAsync(contact.FirstName, contact.Email);

                if (adminResult > 0 && customerResult > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        success = true,
                        message = "Thank you for contacting us! We'll get back to you soon."
                    });
                }
                else if (adminResult > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        success = true,
                        message = "Message received! We'll get back to you soon. (Confirmation email failed)"
                    });
                }
                else if (customerResult > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        success = true,
                        message = "Confirmation email sent! (But there was an issue notifying our team)"
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                    {
                        success = false,
                        message = "Failed to send emails. Please try again later."
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the actual exception for debugging
                System.Diagnostics.Debug.WriteLine($"ContactUsSubmission error: {ex.Message}");

                return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while processing your request."
                });
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/AgentContactemail")]
        public async Task<HttpResponseMessage> AgentContactemail(AgentContactemail contact)
        {
            try
            {
                // Validate model
                if (contact == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Contact information is required.");
                }

                // Basic validation
                if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.FullName))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Name and email are required fields.");
                }

                ContactusEmail objemail = new ContactusEmail();

                // Send email to admin
                //  var adminResult = await objemail.SendContactUsToAdminAsync(contact);

                // Send thank you email to customer
                var customerResult = await objemail.SendAgentEmailAsync(contact);

                if (customerResult > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        success = true,
                        message = "Thank you for contacting us! We'll get back to you soon."
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                    {
                        success = false,
                        message = "Failed to send emails. Please try again later."
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the actual exception for debugging
                System.Diagnostics.Debug.WriteLine($"AgentContactemail error: {ex.Message}");

                return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while processing your request."
                });
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/SendQuotationEmail")]
        public async Task<HttpResponseMessage> SendQuotationEmail(QuotationModel contact)
        {
            try
            {
                // Validate model
                if (contact == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Contact information is required.");
                }

                // Basic validation
                if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.FullName))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Name and email are required fields.");
                }

                ContactusEmail objemail = new ContactusEmail();

                // Send email to admin
                //  var adminResult = await objemail.SendContactUsToAdminAsync(contact);

                // Send thank you email to customer
                var customerResult = await objemail.QuotationEmailAsync(contact);

                if (customerResult > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        success = true,
                        message = "Thank you for contacting us! We'll get back to you soon."
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                    {
                        success = false,
                        message = "Failed to send emails. Please try again later."
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the actual exception for debugging
                System.Diagnostics.Debug.WriteLine($"SendQuotationEmail error: {ex.Message}");

                return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    success = false,
                    message = "An error occurred while processing your request."
                });
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/General/ManageFile")]
        public HttpResponseMessage ManageFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                // Get Id and FileType from form
                int id = Convert.ToInt32(httpRequest.Form["Id"]);
                string fileType = httpRequest.Form["FileType"];

                if (httpRequest.Files.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "No file uploaded." });

                var postedFile = httpRequest.Files[0];
                string fileName = Path.GetFileName(postedFile.FileName);
                string fileExtension = Path.GetExtension(fileName);

                // Create unique file name
                string uniqueFileName = $"{fileType}{id}{DateTime.Now.Ticks}{fileExtension}";
                string filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + uniqueFileName);

                // Save file to server
                postedFile.SaveAs(filePath);
                // Absolute URL
                var requestUri = HttpContext.Current.Request.Url;
                var baseUrl = $"{requestUri.Scheme}://{requestUri.Host}{(requestUri.IsDefaultPort ? "" : ":" + requestUri.Port)}";
                string fileUrl = $"{baseUrl}/Uploads/{uniqueFileName}";

                // If profile image, update TblEmployees
                if (fileType.ToLower() == "profile")
                {
                    var emp = db.TblEmployees.FirstOrDefault(x => x.ID == id);
                    if (emp != null)
                    {
                        emp.ProfilePicName = fileUrl;
                        db.SaveChanges();
                    }
                }
                else
                {

                }

                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "File uploaded successfully.", FileUrl = fileUrl });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success = false, message = ex.Message });
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/WebsiteInquiries")]
        public async Task<IHttpActionResult> WebsiteInquiries()
        {
            var contactUsLeads = db.ContactUs
                .Where(x => x.PageSource != "Agent Contact")
                .Select(x => new LeadDto
                {
                    Id = x.Id,
                    FullName = x.FirstName + " " + x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Category = "",
                    Message = x.Message,
                    BudgetMin = "",
                    BudgetMax = "",
                    Currency = "",
                    PageSource = x.PageSource,
                    Country = "",
                    Source = x.Source,
                    AgentName = x.AgentName,
                    RecordType = "ContactUs"
                });

            var quotationLeads = db.Quotations
                .Select(x => new LeadDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Category = x.Category,
                    Message = x.Message,
                    BudgetMin = x.BudgetMin,
                    BudgetMax = x.BudgetMax,
                    Currency = x.Currency,
                    PageSource = x.PageSource,
                    Country = x.Country,
                    Source = x.Source,
                    AgentName = "",
                    RecordType = "Quotation"
                });

            var result = await contactUsLeads
                .Union(quotationLeads)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/General/GetAgents")]
        [Obsolete]
        public HttpResponseMessage GetAgents()
        {
            var data = (from u in db.Users
                        join e in db.TblEmployees on u.EmpId equals e.ID
                        where u.RoleNames.Contains("Agent")
                        select new UserModal
                        {
                            Email = e.EMAIL_ADDRESS,
                            id = u.ID,
                            Role = u.RoleNames,
                            Name = e.FULL_NAME,
                            Profileimage = e.ProfilePicName,
                            EmpId = u.EmpId,
                            EmpNo = e.EMPLOYEE_NUMBER,
                        }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

    
        public class UserModal
        {
            public int? id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public int? EmpId { get; set; }
            public string EmpNo { get; set; }

            public string Profileimage { get; set; }
        }
        public class UserProfile
        {
            public int? Empid { get; set; }
            public string EmpNo { get; set; }
            public string Bio { get; set; }
            public string Email { get; set; }

            public string Position { get; set; }

            public DateTime? Birthday { get; set; }

            public string MobileNo { get; set; }
            public string WhatsappNo { get; set; }

            public string LinkedIn { get; set; }

            public string Facebook { get; set; }

            public string Twitter { get; set; }

            public string Address { get; set; }
        }

        public class LeadDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Category { get; set; }
            public string Message { get; set; }
            public string BudgetMin { get; set; }
            public string BudgetMax { get; set; }
            public string Currency { get; set; }
            public string PageSource { get; set; }
            public string Country { get; set; }
            public string Source { get; set; }
            public string AgentName { get; set; }
            public string RecordType { get; set; }
        }

    }
}
