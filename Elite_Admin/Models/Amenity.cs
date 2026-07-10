using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Models
{
    public class Amenity
    {
        public string AmenityName { get; set; }
        public string Icon { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }

    public class PropertyAmenity
    {
        public int Id { get; set; } // optional
        public int PropertyId { get; set; }
        public int AmenityId { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual PropertyListing Property { get; set; }
        public virtual Amenity Amenity { get; set; }
    }


    public class AmenityViewModel
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public string Icon { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }


    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

   


}