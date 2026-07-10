using Elite_Webservices.Helper;
using Elite_Webservices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
 

namespace Elite_Webservices.Controllers
{
    public class PopupwidgetController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Popupwidget/addPopupwidget")]
        [Obsolete]
        public HttpResponseMessage addPopupwidget(Cms_blogPopupWidget cms_BlogPopup)
        {
            try
            {

                if (cms_BlogPopup.Id != null && cms_BlogPopup.Id > 0)
                {
                    var checkblog = db.Cms_blogPopupWidget.Where(x => x.Id == cms_BlogPopup.Id).FirstOrDefault();
                    if (checkblog != null)
                    {

                        checkblog.Status = "Updated";
                        checkblog.UpdateDate = DateTime.Now;
                        checkblog.IsActive = true;
                        checkblog.DisplayFrequency = cms_BlogPopup.DisplayFrequency;
                        checkblog.Headlines = cms_BlogPopup.Headlines;
                        if (!String.IsNullOrEmpty(cms_BlogPopup.Image))
                        {
                            checkblog.Image = cms_BlogPopup.Image;
                        }
                        checkblog.Name = cms_BlogPopup.Name;
                        checkblog.PagelinkToShow = cms_BlogPopup.PagelinkToShow;
                        checkblog.Sentence = cms_BlogPopup.Sentence;
                        checkblog.TriggerType = cms_BlogPopup.TriggerType;
                        checkblog.Receivableemail = cms_BlogPopup.Receivableemail;
                        checkblog.showlinkbutton = cms_BlogPopup.showlinkbutton;
                        checkblog.EmailTemplateId = cms_BlogPopup.EmailTemplateId;
                        checkblog.ButtonLink = cms_BlogPopup.ButtonLink;
                        checkblog.ButtonText = cms_BlogPopup.ButtonText;
                        

                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                    }
                }
                else
                {
                    cms_BlogPopup.EntryDate = DateTime.Now;
                    cms_BlogPopup.Status = "Submitted";
                    cms_BlogPopup.UpdateDate = DateTime.Now;
                    cms_BlogPopup.IsActive = true;
                    db.Cms_blogPopupWidget.Add(cms_BlogPopup);
                    db.SaveChanges();


                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }
            return Request.CreateResponse(HttpStatusCode.OK, "");

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Popupwidget/PopupwidgetList")]
        [Obsolete]
        public HttpResponseMessage PopupwidgetList(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var data = db.Cms_blogPopupWidget.Where(x => x.Id == id).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, data);

                }
                else
                {
                    var data = db.Cms_blogPopupWidget/*.Where(x => x.IsActive == true)*/.ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, data);


                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Popupwidget/PopupwidgetDelete_Active")]
        [Obsolete]
        public HttpResponseMessage PopupwidgetDelete_Active(int? id,string type)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var data = db.Cms_blogPopupWidget.Where(x => x.Id == id).FirstOrDefault();
                    if (data != null)
                    {
                        if (type == "Delete")
                        {
                            db.Cms_blogPopupWidget.Remove(data);
                            db.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");

                        }
                        else
                        {
                            if (data.IsActive == true)
                            {
                                data.IsActive = false;
                                db.SaveChanges();
                            }
                            else
                            {
                                data.IsActive = true;
                                db.SaveChanges();
                            }
                            return Request.CreateResponse(HttpStatusCode.OK, "Status Changed");

                        }
                    }
                }
                        return Request.CreateResponse(HttpStatusCode.OK, "try again check id & other Details");

                
               


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }

        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Popupwidget/AddPopupUser")]
        [Obsolete]
        public HttpResponseMessage AddPopupUser(PopModalCls pop)
        {
            try
            {

                var dd = new Cms_BlogPopUserApply();
                dd.Email = pop.Email;
                dd.Name = pop.Name;
                dd.IsActive = true;
                dd.Notes = "-";
                dd.PopwidgetId = pop.popId;
                dd.Status = "Submitted";
                dd.EntryDate = DateTime.Now;
                db.Cms_BlogPopUserApply.Add(dd);
                db.SaveChanges();
                try
                {
                    string emailtype = "Welcome";
                    string subject = "Welcome to RaideTime!";
                 
                    string Message="Welcome to the RaideTime community! We're thrilled to have you on board. Get ready for exclusive content, updates, and much more!";
                    // var emaildata = emailsend.SendEmails((pop.Email, pop.Name, Message, emailtype, subject));
                    string recemial = "nadeemullah@raideit.com";
                    var popemailtemplete = db.Cms_blogPopupWidget.Where(x => x.Id == pop.popId).FirstOrDefault();
                    if (popemailtemplete != null)
                    {
                        var tempid = popemailtemplete.EmailTemplateId ?? 0;
                        var tempalte = db.Cms_EmailTemplete.Where(x => x.Id == tempid).FirstOrDefault();
                        if (tempalte != null)
                        {
                            Message = tempalte.Details;
                            
                        }
                        recemial = popemailtemplete.Receivableemail?? "nadeemullah@raideit.com";
                    }
                   
                    var emaildata1 = emailsend.SendEmailsWithTemplete((pop.Email,pop.Name,Message,emailtype,subject, recemial));
                }
                catch (Exception ex)
                {

                }
                return Request.CreateResponse(HttpStatusCode.OK, "Added");





            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Popupwidget/GetPopupUser")]
        [Obsolete]
        public HttpResponseMessage GetPopupUser( )
        {
            try
            {

                var data = db.Cms_BlogPopUserApply.Where(x => x.IsActive == true).ToList();
              

                return Request.CreateResponse(HttpStatusCode.OK, data);
                 
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }

        }
    
}
    public class PopModalCls
    {

        public int? popId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }
}
