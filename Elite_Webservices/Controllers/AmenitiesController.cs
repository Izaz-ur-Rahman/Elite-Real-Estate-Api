using Elite_Webservices.Dtos;
using Elite_Webservices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Elite_Webservices.Controllers
{
    //[System.Web.Http.Authorize]
    public class AmenitiesController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Categories")]
        public IHttpActionResult GetCategories()
        {
            var categories = db.AmenityCategories
                               .Select(c => new {
                                   c.CategoryId,
                                   c.CategoryName
                               }).ToList();
            return Ok(categories);
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CreateCategory")]
        public IHttpActionResult CreateCategory(AmenityCategory category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                db.AmenityCategories.Add(category);
                db.SaveChanges();

                return Ok("Success");
            }
            catch (Exception)
            {
                return Ok("Failed");
            }
           
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CreateAmenity")]
        public IHttpActionResult CreateAmenity(Amenity model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                db.Amenities.Add(model);
                db.SaveChanges();

                return Ok("Success");
            }
            catch (Exception)
            {
                return Ok("Failed");
            }
            
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetAmenities")]
        public IHttpActionResult GetAmenities()
        {
            var amenities = db.Amenities.Select(a => new AmenityDto
            {
                AmenityId = a.AmenityId,
                AmenityName = a.AmenityName,
                Icon = a.Icon,
                CategoryId = a.CategoryId,
                CategoryName = a.AmenityCategory.CategoryName
            }).ToList();

            return Ok(amenities);
        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DeleteAmenity")]
        public IHttpActionResult DeleteAmenity(int amenityId)
        {
            try
            {
                var amenity = db.Amenities.FirstOrDefault(a => a.AmenityId == amenityId);

                if (amenity == null)
                    return Ok("Amenity not found");

                // 🔥 Remove related property amenities first
                var propertyAmenities = db.PropertyAmenities
                                          .Where(pa => pa.AmenityId == amenityId)
                                          .ToList();

                if (propertyAmenities.Any())
                    db.PropertyAmenities.RemoveRange(propertyAmenities);

                db.Amenities.Remove(amenity);
                db.SaveChanges();

                return Ok("Amenity deleted successfully");
            }
            catch (Exception ex)
            {
                return Ok("Failed: " + ex.Message);
            }
        }

        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/AddAmenities")]
        //public IHttpActionResult AddAmenities(List<Amenity> model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    foreach (var amenity in model)
        //    {
        //        db.Amenities.Add(amenity);
        //    }
        //    db.SaveChanges();

        //    return Ok();
        //}

    }
}