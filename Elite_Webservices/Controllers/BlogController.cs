using Elite_Webservices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Elite_Webservices.Controllers
{
    [System.Web.Http.Authorize]

    public class BlogController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Blog/addBlogDetails")]
        [Obsolete]
        public HttpResponseMessage addBlogDetails(Cms_blogDetail cms_Blog)
        {
            try
            {
                if (cms_Blog.Id != null && cms_Blog.Id > 0)
                {
                    var checkblog = db.Cms_blogDetail.Where(x => x.Id == cms_Blog.Id).FirstOrDefault();
                    if (checkblog != null)
                    {
                       
                        
                        checkblog.UpdateDate = DateTime.Now;
                        checkblog.IsActive = true;
                        checkblog.BlogDetails = cms_Blog.BlogDetails;
                        checkblog.Category = cms_Blog.Category;
                        if (!String.IsNullOrEmpty(cms_Blog.CoverImage))
                        {
                            checkblog.CoverImage = cms_Blog.CoverImage;
                        }
                        checkblog.MetaDescription = cms_Blog.MetaDescription;
                        checkblog.Slug = cms_Blog.Slug;
                        checkblog.Status = "Updated";
                        checkblog.Tags = cms_Blog.Tags;
                        checkblog.Title = cms_Blog.Title;
                        checkblog.Blog_readTime = cms_Blog.Blog_readTime;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Updated");
                    }
                }
                else
                {
                    cms_Blog.EntryDate = DateTime.Now;
                    cms_Blog.Status = "Not Publish";
                    cms_Blog.UpdateDate = DateTime.Now;
                    cms_Blog.IsActive = true;
                    db.Cms_blogDetail.Add(cms_Blog);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
                }


             
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }
            return Request.CreateResponse(HttpStatusCode.OK, "");

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Blog/BlogOpencount")]
        [Obsolete]
        public HttpResponseMessage BlogOpencount(int? id)
        {
            var checkblog = db.Cms_blogDetail.Where(x => x.Id == id).FirstOrDefault();
            if (checkblog != null)
            {
                if (checkblog.Blog_openCount == null)
                {
                    checkblog.Blog_openCount = 1;
                }
                else
                {
                    checkblog.Blog_openCount = checkblog.Blog_openCount + 1;
                }
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Count updated");

        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Blog/BlogList")]
        [Obsolete]
        public HttpResponseMessage BlogList(int?id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var data = db.Cms_blogDetail.Where(x => x.Id == id).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var data = db.Cms_blogDetail.Where(x => x.IsActive==true).OrderByDescending(x=>x.Id).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Blog/PublishedBlogs")]
        [Obsolete]
        public HttpResponseMessage PublishedBlogs(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var data = db.Cms_blogDetail.Where(x => x.Id == id && x.Status == "Published" ).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var data = db.Cms_blogDetail.Where(x => x.IsActive == true && x.Status == "Published").OrderByDescending(x => x.Id).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Blog/BlogListbyId")]
        [Obsolete]
        public HttpResponseMessage BlogListbyId(int id)
        {
            try
            {
                var data = db.Cms_blogDetail.FirstOrDefault(x => x.Id == id); // Return a single object
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Blog not found.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Blog/BlogDetailsbySlug")]
        [Obsolete]
        public HttpResponseMessage BlogDetailsbySlug(string name)
        {
            try
            {
                var data1 = db.Cms_blogDetail.ToList();
                var data = db.Cms_blogDetail.Where(x => x.Slug.Trim() == name.Trim()).FirstOrDefault();
                if (data != null  )
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    Cms_blogDetail dd = new Cms_blogDetail();
                    return Request.CreateResponse(HttpStatusCode.OK, dd);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Blog/PublishBlog")]
        [Obsolete]
        public HttpResponseMessage PublishBlog(int? id,string Publishdate,string type)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var data = db.Cms_blogDetail.Where(x => x.Id == id).FirstOrDefault();
                    if (data != null)
                    {
                        var publishdt =Convert.ToDateTime(Publishdate);
                        data.PublishingDate = publishdt;
                        data.Status = type;
                        db.SaveChanges();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, "published");
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Blog/DeleteBlog")]
        [Obsolete]
        public HttpResponseMessage DeleteBlog(int? id )
        {
            try
            {
                if (id != null && id > 0)
                {
                    var data = db.Cms_blogDetail.Where(x => x.Id == id).FirstOrDefault();
                    if (data != null)
                    {
                        db.Cms_blogDetail.Remove(data);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
        

    }
}
