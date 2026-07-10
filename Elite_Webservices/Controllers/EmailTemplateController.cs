//using Elite_Webservices.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace Elite_Webservices.Controllers
//{
//    public class EmailTemplateController : ApiController
//    {
//        EliteCMSEntities db = new EliteCMSEntities();

//        [System.Web.Http.HttpPost]
//        [System.Web.Http.Route("api/EmailTemplate/addTemplate")]
//        [Obsolete]
//        public HttpResponseMessage addTemplate(Cms_EmailTemplete cms_temp)
//        {
//            try
//            {
//                if (cms_temp.Id != null && cms_temp.Id > 0)
//                {
//                    var checkcms_temp = db.Cms_EmailTemplete.Where(x => x.Id == cms_temp.Id).FirstOrDefault();
//                    if (checkcms_temp != null)
//                    {


//                        checkcms_temp.UpdatedDate = DateTime.Now;
//                        checkcms_temp.IsActive = true;
//                        checkcms_temp.Details = cms_temp.Details;
//                        checkcms_temp.Name = cms_temp.Name;
//                        checkcms_temp.Subject = cms_temp.Subject;

//                        db.SaveChanges();
//                        return Request.CreateResponse(HttpStatusCode.OK, "Updated");
//                    }
//                }
//                else
//                {
//                    var cms_tempAdd = new Cms_EmailTemplete();

//                    cms_tempAdd.EntryDate = DateTime.Now;

//                    cms_tempAdd.UpdatedDate = DateTime.Now;
//                    cms_tempAdd.Details =cms_temp.Details;
//                    cms_tempAdd.Name = cms_temp.Name;
//                    cms_tempAdd.Subject = cms_temp.Subject;
//                    cms_tempAdd.Createdby = cms_temp.Createdby;
//                    cms_tempAdd.IsActive = true;
//                    db.Cms_EmailTemplete.Add(cms_tempAdd);
//                    db.SaveChanges();
//                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
//                }



//            }
//            catch (Exception ex)
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

//            }
//            return Request.CreateResponse(HttpStatusCode.OK, "");

//        }

//        [System.Web.Http.HttpGet]
//        [System.Web.Http.Route("api/EmailTemplate/EmailTemplateList")]
//        [Obsolete]
//        public HttpResponseMessage EmailTemplateList(int? id)
//        {
//            try
//            {
//                if (id != null && id > 0)
//                {
//                    var data = db.Cms_EmailTemplete.Where(x => x.Id == id).ToList();
//                    return Request.CreateResponse(HttpStatusCode.OK, data);

//                }
//                else
//                {
//                    var data = db.Cms_EmailTemplete.Where(x => x.IsActive == true).ToList();
//                    return Request.CreateResponse(HttpStatusCode.OK, data);


//                }


//            }
//            catch (Exception ex)
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

//            }

//        }
//    }
//}
// existing code with some modifications and improvements for better readability and error handling.
//using Elite_Webservices.Models;
//using System;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace Elite_Webservices.Controllers
//{
//    [RoutePrefix("api/EmailTemplate")]
//    public class EmailTemplateController : ApiController
//    {
//        EliteCMSEntities db = new EliteCMSEntities();

//        // ================= ADD / UPDATE TEMPLATE =================
//        [HttpPost]
//        [Route("addTemplate")]
//        [Obsolete]
//        public HttpResponseMessage addTemplate(Cms_EmailTemplete cms_temp)
//        {
//            try
//            {
//                if (cms_temp.Id != null && cms_temp.Id > 0)
//                {
//                    var checkcms_temp = db.Cms_EmailTemplete
//                        .Where(x => x.Id == cms_temp.Id)
//                        .FirstOrDefault();

//                    if (checkcms_temp != null)
//                    {
//                        checkcms_temp.UpdatedDate = DateTime.Now;
//                        checkcms_temp.IsActive = true;
//                        checkcms_temp.Details = cms_temp.Details;
//                        checkcms_temp.Name = cms_temp.Name;
//                        checkcms_temp.Subject = cms_temp.Subject;

//                        db.SaveChanges();
//                        return Request.CreateResponse(HttpStatusCode.OK, "Updated");
//                    }
//                }
//                else
//                {
//                    var cms_tempAdd = new Cms_EmailTemplete();

//                    cms_tempAdd.EntryDate = DateTime.Now;
//                    cms_tempAdd.UpdatedDate = DateTime.Now;
//                    cms_tempAdd.Details = cms_temp.Details;
//                    cms_tempAdd.Name = cms_temp.Name;
//                    cms_tempAdd.Subject = cms_temp.Subject;
//                    cms_tempAdd.Createdby = cms_temp.Createdby;
//                    cms_tempAdd.IsActive = true;

//                    db.Cms_EmailTemplete.Add(cms_tempAdd);
//                    db.SaveChanges();

//                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
//                }
//            }
//            catch (Exception ex)
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
//            }

//            return Request.CreateResponse(HttpStatusCode.OK, "");
//        }

//        // ================= GET EMAIL TEMPLATE LIST =================
//        [HttpGet]
//        [Route("EmailTemplateList")]
//        [Obsolete]
//        public HttpResponseMessage EmailTemplateList([FromUri] int? id = null)
//        {
//            try
//            {
//                if (id != null && id > 0)
//                {
//                    var data = db.Cms_EmailTemplete
//                        .Where(x => x.Id == id)
//                        .ToList();

//                    return Request.CreateResponse(HttpStatusCode.OK, data);
//                }
//                else
//                {
//                    var data = db.Cms_EmailTemplete
//                        .Where(x => x.IsActive == true)
//                        .ToList();

//                    return Request.CreateResponse(HttpStatusCode.OK, data);
//                }
//            }
//            catch (Exception ex)
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
//            }
//        }

//        // ================= DELETE EMAIL TEMPLATE (SOFT DELETE) =================
//        [HttpPost]
//        [Route("DeleteTemplate")]
//        [Obsolete]
//        public HttpResponseMessage DeleteTemplate(int id)
//        {
//            try
//            {
//                var template = db.Cms_EmailTemplete
//                    .Where(x => x.Id == id)
//                    .FirstOrDefault();

//                if (template == null)
//                {
//                    return Request.CreateResponse(HttpStatusCode.NotFound, "Template not found");
//                }

//                template.IsActive = false;
//                template.UpdatedDate = DateTime.Now;

//                db.SaveChanges();

//                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
//            }
//            catch (Exception ex)
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
//            }
//        }

//    }
//}


using Elite_Webservices.Models;
using Elite_Webservices.Helper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Elite_Webservices.Controllers
{
    [RoutePrefix("api/EmailTemplate")]
    public class EmailTemplateController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();

        // ================= ADD / UPDATE TEMPLATE =================
        [HttpPost]
        [Route("addTemplate")]
        [Obsolete]
        public HttpResponseMessage addTemplate(Cms_EmailTemplete cms_temp)
        {
            try
            {
                if (cms_temp.Id != null && cms_temp.Id > 0)
                {
                    var checkcms_temp = db.Cms_EmailTemplete
                        .Where(x => x.Id == cms_temp.Id)
                        .FirstOrDefault();

                    if (checkcms_temp != null)
                    {
                        checkcms_temp.UpdatedDate = DateTime.Now;
                        checkcms_temp.IsActive = true;
                        checkcms_temp.Details = cms_temp.Details;
                        checkcms_temp.Name = cms_temp.Name;
                        checkcms_temp.Subject = cms_temp.Subject;

                        db.SaveChanges();

                        // 🔔 Notification (Update)
                        NotificationHelper.Create(
                            title: "Email Template Updated",
                            message: $"Email template '{checkcms_temp.Name}' was updated",
                            type: "info",
                            action: "updated",
                            entityId: checkcms_temp.Id,
                            createdBy: User?.Identity?.Name ?? "system"
                        );

                        return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                    }
                }
                else
                {
                    var cms_tempAdd = new Cms_EmailTemplete();

                    cms_tempAdd.EntryDate = DateTime.Now;
                    cms_tempAdd.UpdatedDate = DateTime.Now;
                    cms_tempAdd.Details = cms_temp.Details;
                    cms_tempAdd.Name = cms_temp.Name;
                    cms_tempAdd.Subject = cms_temp.Subject;
                    cms_tempAdd.Createdby = cms_temp.Createdby;
                    cms_tempAdd.IsActive = true;

                    db.Cms_EmailTemplete.Add(cms_tempAdd);
                    db.SaveChanges();

                    // 🔔 Notification (Add)
                    NotificationHelper.Create(
                        title: "New Email Template Added",
                        message: $"Email template '{cms_tempAdd.Name}' was created",
                        type: "success",
                        action: "created",
                        entityId: cms_tempAdd.Id,
                        createdBy: User?.Identity?.Name ?? "system"
                    );

                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        // ================= GET EMAIL TEMPLATE LIST =================
        [HttpGet]
        [Route("EmailTemplateList")]
        [Obsolete]
        public HttpResponseMessage EmailTemplateList([FromUri] int? id = null)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var data = db.Cms_EmailTemplete
                        .Where(x => x.Id == id)
                        .ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var data = db.Cms_EmailTemplete
                        .Where(x => x.IsActive == true)
                        .ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // ================= DELETE EMAIL TEMPLATE (SOFT DELETE) =================
        [HttpPost]
        [Route("DeleteTemplate")]
        [Obsolete]
        public HttpResponseMessage DeleteTemplate(int id)
        {
            try
            {
                var template = db.Cms_EmailTemplete
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (template == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Template not found");
                }

                template.IsActive = false;
                template.UpdatedDate = DateTime.Now;
                db.SaveChanges();

                // 🔔 Notification (Delete)
                NotificationHelper.Create(
                    title: "Email Template Deleted",
                    message: $"Email template '{template.Name}' was deleted",
                    type: "warning",
                    action: "deleted",
                    entityId: template.Id,
                    createdBy: User?.Identity?.Name ?? "system"
                );

                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
