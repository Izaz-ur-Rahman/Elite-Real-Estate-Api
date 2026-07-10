using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Elite_Webservices.Dtos
{
    public class PropertyListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PropertyType { get; set; }
        public string ListingType { get; set; }

        public string PriceCurrency { get; set; }
        public decimal? Price { get; set; }
        //public decimal? SalePrice { get; set; }
        //public decimal? RentalPrice { get; set; }
        //public decimal? OffPrice { get; set; }

        public int? Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        //public int? VillaBedrooms { get; set; }

        public string City { get; set; }
        public string Neighborhood { get; set; }
        public DateTime? CreatedDate { get; set; }
        // small image (thumbnail)
       
        // slug for detail page
        public string PropertySlug { get; set; }
        public string ParkingSpaces { get; set; }
        public string Area { get; set; }
        public string MainImage { get; set; }

    }
}