using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elite_Admin.Models
{
    public class PropertyListingVM
    {
        public PropertyListing Property { get; set; }
        public List<AmenitySelectionViewModel> Amenities { get; set; }
    }


    public class AmenitySelectionViewModel
    {
        
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public string Icon { get; set; } // NEW
        public string CategoryName { get; set; } // NEW
        public bool? IsSelected { get; set; } // Nullable bool for Yes/No/Unset
    }

    public class PropertiesApiResponse
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<PropertyListing> Data { get; set; }
    }


}