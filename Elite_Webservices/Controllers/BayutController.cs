using Elite_Webservices.Dtos;
using Elite_Webservices.Models;
using Elite_Webservices.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Elite_Webservices.Controllers
{

    [System.Web.Http.RoutePrefix("api/bayut")]
    public class BayutController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();



        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("bayutpropertyfeed")]
        public HttpResponseMessage bayutpropertyfeed()
        {
            try
            {
                var properties = GetPropertiesForBayutFeed();
                var xmlDocument = GenerateBayutXml(properties);

                // Return XML response
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(xmlDocument.ToString(), Encoding.UTF8, "application/xml");
                return response;
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetPropertiesbayut : " +
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message));

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private List<PropertyListingBayut> GetPropertiesForBayutFeed()
        {
            var properties = new List<PropertyListingBayut>();

            var data = (from p in db.PropertyListings
                        where p.Status == "Active"
                        select new
                        {
                            // All Property Fields
                            p.Id,
                            p.Title,
                            p.PropertyType,
                            p.ListingType,
                            p.MainImage,
                            p.SubImage,
                            p.RentalPrice,
                            p.SalePrice,
                            p.OffPrice,
                            p.Bedrooms,
                            p.Bathrooms,
                            p.VillaBedrooms,
                            p.VillaBathrooms,
                            p.Furnished,
                            p.City,
                            p.Locality,
                            p.Sub_Locality,
                            p.Tower_Name,
                            p.Description,
                            p.Property_Title_AR,
                            p.Property_purpose,
                            p.Property_Size,
                            p.Property_Size_Unit,
                            p.plotArea,
                            p.Off_plan,
                            p.Rent_Frequency,
                            p.offplanDetails_saleType,
                            p.offplanDetails_dldWaiver,
                            p.offplanDetails_originalPrice,
                            p.offplanDetails_amountPaid,
                            p.Videos,
                            p.Listing_Agent,
                            p.Listing_Agent_Phone,
                            p.Listing_Agent_Email,
                            p.ModifiedDate,
                            p.RERAPermitNumber,

                            Amenities = (from pa in db.PropertyAmenities
                                         join a in db.Amenities on pa.AmenityId equals a.AmenityId
                                         where pa.PropertyId == p.Id
                                         select new
                                         {
                                             a.AmenityName
                                         }).ToList()
                        }).ToList();

            // Convert to Bayut model
            foreach (var item in data)
            {
                var property = new PropertyListingBayut
                {
                    Id = item.Id.ToString(),
                    Title = item.Title ?? "",
                    PropertyType = item.PropertyType ?? "",
                    ListingType = item.ListingType ?? "",
                    MainImage = item.MainImage ?? "",
                    SubImage = item.SubImage ?? "",
                    RentalPrice = item.RentalPrice?.ToString() ?? "",
                    SalePrice = item.SalePrice?.ToString() ?? "",
                    OffPrice = item.OffPrice?.ToString() ?? "",
                    Bedrooms = item.Bedrooms?.ToString() ?? "",
                    Bathrooms = item.Bathrooms?.ToString() ?? "",
                    VillaBedrooms = item.VillaBedrooms?.ToString() ?? "",
                    VillaBathrooms = item.VillaBathrooms?.ToString() ?? "",
                    Furnished = GetFurnishedStatus(item.Furnished),
                    City = item.City ?? "",
                    Locality = item.Locality ?? "",
                    Sub_Locality = item.Sub_Locality ?? "",
                    Tower_Name = item.Tower_Name ?? "",
                    Description = item.Description ?? "",
                    Property_Title_AR = item.Property_Title_AR ?? item.Title ?? "",
                    Property_Size = item.Property_Size?.ToString() ?? "",
                    Property_Size_Unit = item.Property_Size_Unit ?? "sqft",
                    plotArea = item.plotArea?.ToString() ?? "",
                    Rent_Frequency = item.Rent_Frequency ?? "",
                    offplanDetails_saleType = item.offplanDetails_saleType ?? "",
                    offplanDetails_dldWaiver = item.offplanDetails_dldWaiver ?? "",
                    offplanDetails_originalPrice = item.offplanDetails_originalPrice?.ToString() ?? "",
                    offplanDetails_amountPaid = item.offplanDetails_amountPaid?.ToString() ?? "",
                    Videos = item.Videos ?? "",
                    Listing_Agent = item.Listing_Agent ?? "",
                    Listing_Agent_Phone = item.Listing_Agent_Phone ?? "",
                    Listing_Agent_Email = item.Listing_Agent_Email ?? "",
                    ModifiedDate = item.ModifiedDate,
                    RERAPermitNumber = item.RERAPermitNumber ?? "",
                    ParkingSpaces = "0", // You can map this from your database if available
                    Amenities = item.Amenities.Select(a => new AmenityBayut
                    {
                        AmenityName = a.AmenityName ?? ""
                    }).ToList()
                };

                properties.Add(property);
            }

            return properties;
        }


        private XDocument GenerateBayutXml(List<PropertyListingBayut> properties)
        {
            var externalAppUrl = ConfigurationManager.AppSettings["ExternalAppUrl"] ??
                                 System.Web.Configuration.WebConfigurationManager.AppSettings["ExternalAppUrl"];

            var propertiesElement = new XElement("Properties");

            foreach (var property in properties)
            {
                var propertyElement = new XElement("Property",
                    // Required Basic Fields
                    new XElement("Property_Ref_No", property.Id),
                    new XElement("Permit_Number", property.RERAPermitNumber ?? ""),
                    new XElement("Property_Status", "live"),
                    new XElement("Property_purpose", GetPropertyPurpose(property)),
                    new XElement("Property_Type", property.PropertyType),
                    new XElement("Bedrooms", property.PropertyType == "Villa" ? property.VillaBedrooms : property.Bedrooms),
                    new XElement("Bathrooms", property.PropertyType == "Villa" ? property.VillaBathrooms : property.Bathrooms),
                    new XElement("Price", property.ListingType == "Off-Plan" ? property.OffPrice :
                        (property.ListingType == "For Rent" ? property.RentalPrice : property.SalePrice)),

                    // Location
                    new XElement("City", property.City),
                    new XElement("Locality", property.Locality),
                    new XElement("Sub_Locality", property.Sub_Locality ?? ""),
                    new XElement("Tower_Name", property.Tower_Name ?? ""),

                    // Content
                    new XElement("Property_Title", property.Title),
                    new XElement("Property_Description", property.Description),
                    new XElement("Property_Title_AR", property.Property_Title_AR ?? property.Title),
                    new XElement("Property_Description_AR", property.Property_Description_AR ?? property.Description),

                    // Size
                    new XElement("Property_Size", property.Property_Size),
                    new XElement("Property_Size_Unit", property.Property_Size_Unit),
                    new XElement("plotArea", property.plotArea),

                    // Rent Frequency
                    new XElement("Rent_Frequency", property.Rent_Frequency ?? ""),

                    // Furnishing
                    new XElement("Furnished", property.Furnished),

                    // Off-plan
                    new XElement("Off_Plan", property.ListingType == "Off-Plan" ? "Yes" : "No"),

                    new XElement("offplanDetails_saleType", property.offplanDetails_saleType),
                    new XElement("offplanDetails_dldWaiver", property.offplanDetails_dldWaiver),
                    new XElement("offplanDetails_originalPrice", property.offplanDetails_originalPrice),
                    new XElement("offplanDetails_amountPaid", property.offplanDetails_amountPaid),

                    // Features
                    GenerateFeaturesElement(property),

                    // Images
                    GenerateImagesElement(property, externalAppUrl),

                    // Videos
                    GenerateVideoElement(property),

                    // Portals (CRITICAL - Bayut requires this)
                    new XElement("Portals", new XElement("Portal", "bayut")),

                    new XElement("Listing_Agent", property.Listing_Agent),
                    new XElement("Listing_Agent_Phone", property.Listing_Agent_Phone),
                    new XElement("Listing_Agent_Email", property.Listing_Agent_Email),

                    // Last Updated
                    new XElement("Last_Updated", property.ModifiedDate.HasValue ?
                        property.ModifiedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "")
                );

                propertiesElement.Add(propertyElement);
            }

            return new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                propertiesElement
            );
        }

    
        private string GetPropertyPurpose(PropertyListingBayut property)
        {
            return !string.IsNullOrEmpty(property.ListingType) && property.ListingType == "For Rent"
                ? "Rent"
                : "Buy";
        }

       
        private string GetFurnishedStatus(string furnished)
        {
            if (string.IsNullOrEmpty(furnished))
                return "Unfurnished";

            furnished = furnished.ToLower();

            switch (furnished)
            {
                case "yes":
                case "true":
                case "furnished":
                    return "Furnished";
                default:
                    return "Unfurnished";
            }
        }

        private XElement GenerateFeaturesElement(PropertyListingBayut property)
        {
            var features = new XElement("Features");

            if (property.Amenities?.Count > 0)
            {
                foreach (var amenity in property.Amenities)
                {
                    if (!string.IsNullOrWhiteSpace(amenity.AmenityName))
                    {
                        features.Add(new XElement("Feature", amenity.AmenityName.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(property.ParkingSpaces) && property.ParkingSpaces != "0")
                features.Add(new XElement("Feature", "Parking"));

            return features.Elements().Any() ? features : null;
        }

        private XElement GenerateImagesElement(PropertyListingBayut property, string externalURL)
        {
            var images = new XElement("Images");

            // Add main image
            if (!string.IsNullOrEmpty(property.MainImage))
            {
                var mainImageUrl = property.MainImage.StartsWith("http")
                    ? property.MainImage
                    : $"{externalURL.TrimEnd('/')}/{property.MainImage.TrimStart('/')}";
                images.Add(new XElement("Image", mainImageUrl));
            }

            // Add sub images
            if (!string.IsNullOrEmpty(property.SubImage))
            {
                var subImages = property.SubImage.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var subImage in subImages)
                {
                    var trimmedImage = subImage.Trim();
                    if (!string.IsNullOrEmpty(trimmedImage))
                    {
                        var subImageUrl = trimmedImage.StartsWith("http")
                            ? trimmedImage
                            : $"{externalURL.TrimEnd('/')}/{trimmedImage.TrimStart('/')}";
                        images.Add(new XElement("Image", subImageUrl));
                    }
                }
            }

            return images.Elements().Any() ? images : null;
        }

        private XElement GenerateVideoElement(PropertyListingBayut property)
        {
            var videos = new XElement("Videos");

            if (!string.IsNullOrEmpty(property.Videos))
            {
                // Handle multiple videos if separated by commas
                var videoUrls = property.Videos.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var videoUrl in videoUrls)
                {
                    var trimmedUrl = videoUrl.Trim();
                    if (!string.IsNullOrEmpty(trimmedUrl))
                    {
                        videos.Add(new XElement("Video", trimmedUrl));
                    }
                }
            }

            return videos.Elements().Any() ? videos : null;
        }

        // Helper models
        public class PropertyListingBayut
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string PropertyType { get; set; }
            public string ListingType { get; set; }
            public string MainImage { get; set; }
            public string SubImage { get; set; }
            public string RentalPrice { get; set; }
            public string SalePrice { get; set; }
            public string OffPrice { get; set; }
            public string Bedrooms { get; set; }
            public string Bathrooms { get; set; }
            public string VillaBedrooms { get; set; }
            public string VillaBathrooms { get; set; }
            public string Furnished { get; set; }
            public string City { get; set; }
            public string Locality { get; set; }
            public string Sub_Locality { get; set; }
            public string Tower_Name { get; set; }
            public string Description { get; set; }
            public string Property_Title_AR { get; set; }
            public string Property_Description_AR { get; set; }
            public string Property_Size { get; set; }
            public string Property_Size_Unit { get; set; }
            public string plotArea { get; set; }
            public string Rent_Frequency { get; set; }
            public string offplanDetails_saleType { get; set; }
            public string offplanDetails_dldWaiver { get; set; }
            public string offplanDetails_originalPrice { get; set; }
            public string offplanDetails_amountPaid { get; set; }
            public string Videos { get; set; }
            public string Listing_Agent { get; set; }
            public string Listing_Agent_Phone { get; set; }
            public string Listing_Agent_Email { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public string RERAPermitNumber { get; set; }
            public string ParkingSpaces { get; set; }
            public List<AmenityBayut> Amenities { get; set; } = new List<AmenityBayut>();
        }

        public class AmenityBayut
        {
            public string AmenityName { get; set; }
        }
    }
}