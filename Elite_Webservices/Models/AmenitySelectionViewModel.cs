using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Elite_Webservices.Models
{
    public class AmenitySelectionViewModel
    {
        public int PropertyId { get; set; }
        public int AmenityId { get; set; }
   
        public bool? IsSelected { get; set; }
    }
}