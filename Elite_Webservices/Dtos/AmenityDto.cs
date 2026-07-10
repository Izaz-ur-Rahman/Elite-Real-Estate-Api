using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elite_Webservices.Dtos
{
    public class AmenityDto
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public string Icon { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}