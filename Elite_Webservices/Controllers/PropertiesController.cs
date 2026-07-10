using Elite_Webservices.Dtos;
using Elite_Webservices.Models;
using Elite_Webservices.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Elite_Webservices.Controllers
{
    //[System.Web.Http.Authorize]
    public class PropertiesController : ApiController
    {
        EliteCMSEntities db = new EliteCMSEntities();

        // GET: Properties
        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/GetProperties")]
        //[Obsolete]
        //public HttpResponseMessage Properties(int pageNumber = 1, int pageSize = 6)
        //{
        //    try
        //    {
        //       var propertiesQuery =  db.PropertyListings.OrderByDescending(p=>p.Id).ToList();

        //        var totalCount = propertiesQuery.Count();

        //        var propertiesList = propertiesQuery
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize)
        //            .ToList();

        //        var result = new
        //        {
        //            TotalCount = totalCount,
        //            PageNumber = pageNumber,
        //            PageSize = pageSize,
        //            Data = propertiesList
        //        };

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }

        //}

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetProperties")]
        [Obsolete]
        public HttpResponseMessage Properties(
                                              int pageNumber = 1,
                                              int pageSize = 6,
                                              string listingType = null,   // "Rent" | "Sell" | "Off-Plan" …
                                              string city = null,   // "Dubai", "Abu Dhabi", …
                                              string propertyType = null,   // "Apartment", "Villa", …
                                              string price = null,   // "500000-1000000"
                                              int? bedrooms = null)   // 1 | 2 | 3 (3 means “3 or more”)
        {
            try
            {
                var q = db.PropertyListings.AsQueryable();

                // — listing type
                if (!string.IsNullOrWhiteSpace(listingType))
                    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                if (bedrooms.HasValue)
                    q = bedrooms == 3
                        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);

                // — price range
                if (!string.IsNullOrWhiteSpace(price) && price.Contains('-'))
                {
                    var parts = price.Split('-');
                    if (decimal.TryParse(parts[0], out var min))
                        q = q.Where(p => p.SalePrice >= min || p.RentalPrice >= min || p.OffPrice >= min);
                    if (decimal.TryParse(parts[1], out var max))
                        q = q.Where(p => p.SalePrice <= max || p.RentalPrice <= max || p.OffPrice <= max);
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();
                var data = q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetProperties : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPropertybySlug")]
        [Obsolete]
        public HttpResponseMessage PropertybySlug(string name)
        {
            try
            {
                var propertybyslug = db.PropertyListings
                    .Where(x => x.PropertySlug.Trim() == name.Trim())
                    .Select(property => new
                    {
                        property.Id,
                        property.Title,
                        property.PropertyType,
                        property.ListingType,
                        property.PriceCurrency,
                        property.MainImage,
                        property.SubImage,
                        property.PaymentPlanAvailable,
                        property.PropertySlug,
                        property.PropertyCategory,
                        property.DeveloperName,
                        property.HandoverDate,
                        property.PaymentPlanStructure,
                        property.PostHandoverPayment,
                        property.ConstructionStatus,
                        property.ModelUnitImages,
                        property.ServiceCharges,
                        property.ShowroomAvailable,
                        property.PropertyAge,
                        property.TenancyStatus,
                        property.MortgageStatus,
                        property.PreviousOwnership,
                        property.TitleDeedAvailable,
                        property.RentalPrice,
                        property.PaymentTerms,
                        property.ChequesAccepted,
                        property.CommissionFee,
                        property.TenancyContractLength,
                        property.VacatingNoticePeriod,
                        property.Deposit,
                        property.Bedrooms,
                        property.Bathrooms,
                        property.FloorLevel,
                        property.BalconyTerrace,
                        property.PropertyView,
                        property.BuiltUpArea,
                        property.ParkingSpaces,
                        property.KitchenType,
                        property.MaidsRoom,
                        property.SmartHomeFeatures,
                        property.StorageRoom,
                        property.PlotSize,
                        property.PrivateGarden,
                        property.PrivatePool,
                        property.Furnished,
                        property.UnitType,
                        property.PantryKitchen,
                        property.Washroom,
                        property.LicenseTypeSupport,
                        property.SwimmingPool,
                        property.CommunityName,
                        property.City,
                        property.Neighborhood,
                        property.ProximityToMetro,
                        property.NearbyLandmarks,
                        property.SchoolHospitalProximity,
                        property.ROI,
                        property.RentalYield,
                        property.AnnualServiceCharges,
                        property.MortgageCalculator,
                        property.RentalPaymentOptions,
                        property.ListingId,
                        property.RealEstateAgencyName,
                        property.RERAPermitNumber,
                        property.PaymentMethod,
                        property.DLDTransferFee,
                        property.CoolingSystem,
                        property.PetPolicy,
                        property.TenancyContractStatus,
                        property.LeaseExpiryDate,
                        property.BoostListing,
                        property.PriorityPlacement,
                        property.CreatedDate,
                        property.ModifiedDate,
                        property.CreatedBy,
                        property.OffPrice,
                        property.SalePrice,
                        property.VillaBedrooms,
                        property.VillaBathrooms,
                        property.VillaBuiltUpArea,
                        property.VillaParkingSpaces,
                        property.VillaFloorLevel,
                        property.VillaFurnished,
                        property.VillaMaidsRoom,
                        property.VillaDeveloperName,
                        property.CommercialBuiltUpArea,
                        property.CommercialDeveloperName,
                        property.CommercialParkingSpaces,
                        property.CommercialFloorLevel,
                        property.CommercialFurnished,
                        property.Description,
                        property.PaymentPlanAvailableDescription,
                        property.ChequesAcceptedRental,
                        property.Property_Title_AR,
                        property.Property_Size,
                        property.Listing_Agent,
                        property.Listing_Agent_Phone,
                        property.Listing_Agent_Email,

                        Amenities = property.PropertyAmenities.Select(pa => new
                        {
                            pa.AmenityId,
                            pa.IsAvailable,
                            Icon = pa.Amenity.Icon,
                            AmenityName = pa.Amenity.AmenityName,
                            Category = pa.Amenity.AmenityCategory.CategoryName
                        }).ToList()
                    })
                    .FirstOrDefault();

                return Request.CreateResponse(HttpStatusCode.OK, propertybyslug);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetPropertybySlug : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPropertybyId")]
        [Obsolete]
        public HttpResponseMessage PropertybyId(int id)
        {
            try
            {
                var property = db.PropertyListings.Where(x => x.Id == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, property);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetPropertybyId : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PropertiesListing/AddProperty")]
        public async Task<HttpResponseMessage> SaveProperty()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                // Get form data
                var propertyJson = result.FormData["property"];
                var model = JsonConvert.DeserializeObject<PropertyListing>(propertyJson);

                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Handle file uploads
                var uploadPath = HttpContext.Current.Server.MapPath("~/EliteFiles/Propertiesfiles/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Main Image
                var mainImageFile = result.FileData.FirstOrDefault(f => f.Headers.ContentDisposition.Name.Contains("mainImage"));
                if (mainImageFile != null)
                {
                    var mainFileName = Guid.NewGuid() + Path.GetExtension(mainImageFile.Headers.ContentDisposition.FileName.Replace("\"", ""));
                    var mainImagePath = Path.Combine(uploadPath, mainFileName);
                    File.Move(mainImageFile.LocalFileName, mainImagePath);
                    model.MainImage = "/EliteFiles/Propertiesfiles/" + mainFileName;
                }

                // Sub Images
                var subImageFiles = result.FileData.Where(f => f.Headers.ContentDisposition.Name.Contains("subImages"));
                var subImagePaths = new List<string>();

                foreach (var file in subImageFiles.Take(4))
                {
                    var subFileName = Guid.NewGuid() + Path.GetExtension(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                    var subImagePath = Path.Combine(uploadPath, subFileName);
                    File.Move(file.LocalFileName, subImagePath);
                    subImagePaths.Add("/EliteFiles/Propertiesfiles/" + subFileName);
                }

                model.SubImage = string.Join(",", subImagePaths);

                // Save property
                model.CreatedDate = DateTime.UtcNow;
                model.Status = "Active";
                model.Off_plan = model.ListingType == "Off-Plan";
                model.Property_purpose = model.ListingType == "For Rent" ? "Rent" : "Sell";

                db.PropertyListings.Add(model);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { PropertyId = model.Id });
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");

                var fullErrorMessage = string.Join("; ", errorMessages);

                Logging.WriteLog(LogType.Error, $"DbEntityValidationException: {fullErrorMessage}");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Errors = errorMessages });
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, $"SaveProperty Exception: {ex.Message}, Inner: {ex.InnerException?.Message}");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            //catch (Exception ex)
            //{
            //    Logging.WriteLog(LogType.Error, "SaveProperty : " + (ex.InnerException?.Message ?? ex.Message));
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            //}
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/CheckPropertySlug")]
        public HttpResponseMessage CheckPropertySlug(string slug, int? propertyId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(slug))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Slug is required");

                var query = db.PropertyListings.AsQueryable();

                if (propertyId.HasValue)
                    query = query.Where(x => x.Id != propertyId.Value);

                bool isDuplicate = query.Any(x => x.PropertySlug == slug);

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    isDuplicate,
                    message = isDuplicate ? "Slug already exists" : "Slug is available"
                });
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "CheckPropertySlug: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/PropertiesListing/AddProperty11")]
        //[Obsolete]
        //public HttpResponseMessage SaveProperty11(PropertyListing model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }

        //        model.CreatedDate = DateTime.UtcNow;
        //        model.Status = "Active";

        //        if (model.ListingType == "Off-Plan")
        //        {
        //            model.Off_plan = true;
        //        }
        //        else
        //        {
        //            model.Off_plan = false;
        //        }
        //        model.Property_purpose = model.ListingType == "For Rent" ? "Rent" : "Sell";

        //        db.PropertyListings.Add(model);
        //        db.SaveChanges();

        //        return Request.CreateResponse(HttpStatusCode.OK, new { PropertyId = model.Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "SaveProperty : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }

        //}

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddPropertyAmenity")]
        [Obsolete]
        public HttpResponseMessage SavePropertyAmenity(List<PropertyAmenity> model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                db.PropertyAmenities.AddRange(model);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Property Amenities Added");
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "SavePropertyAmenity : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [System.Web.Http.HttpDelete]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DeletePropertyAmenity")]
        public HttpResponseMessage DeletePropertyAmenity(int propertyId, int amenityId)
        {
            try
            {
                var amenity = db.PropertyAmenities
                                .FirstOrDefault(x => x.PropertyId == propertyId
                                                  && x.AmenityId == amenityId);

                if (amenity == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Amenity not found");

                db.PropertyAmenities.Remove(amenity);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Property Amenity Deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //[System.Web.Http.HttpDelete]
        //[System.Web.Http.Route("api/DeletePropertyAmenity")]
        //[Obsolete]
        //public HttpResponseMessage DeletePropertyAmenity(int propertyId, int amenityId)
        //{
        //    try
        //    {
        //        var amenity = db.PropertyAmenities
        //                        .FirstOrDefault(x => x.PropertyId == propertyId
        //                                          && x.AmenityId == amenityId);

        //        if (amenity == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "Amenity not found");
        //        }

        //        db.PropertyAmenities.Remove(amenity);
        //        db.SaveChanges();

        //        return Request.CreateResponse(HttpStatusCode.OK, "Property Amenity Deleted");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "DeletePropertyAmenity : " +
        //            (ex.InnerException == null ? ex.Message : ex.InnerException.Message));

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/AddPropertyAmenity")]
        //[Obsolete]
        //public HttpResponseMessage SavePropertyAmenity(List<AmenitySelectionViewModel> model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }

        //        List<PropertyAmenity> propertyAmenities = new List<PropertyAmenity>();

        //        if (model != null && model.Count > 0)
        //        {
        //            // Filter selected amenities
        //            var selectedAmenities = model.Where(x => x.IsSelected == true).ToList();

        //            foreach (var item in selectedAmenities)
        //            {
        //                PropertyAmenity pa = new PropertyAmenity
        //                {
        //                    PropertyId = item.PropertyId,
        //                    AmenityId = item.AmenityId,
        //                    IsAvailable = true
        //                };

        //                propertyAmenities.Add(pa);
        //            }
        //        }

        //        if (propertyAmenities.Count > 0)
        //        {
        //            db.PropertyAmenities.AddRange(propertyAmenities);
        //            db.SaveChanges();
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK, "Selected Property Amenities Added");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "SavePropertyAmenity : " +
        //            (ex.InnerException == null ? ex.Message : ex.InnerException.Message));

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UpdatePropertyAmenity")]
        [Obsolete]
        public HttpResponseMessage UpdatePropertyAmenity(List<PropertyAmenity> model)
        {
            try
            {
                if (model == null || !model.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No amenities provided");
                }

                int propertyId = (int)model.First().PropertyId;

                // Remove existing amenities for the property
                var existingAmenities = db.PropertyAmenities
                                          .Where(pa => pa.PropertyId == propertyId)
                                          .ToList();

                db.PropertyAmenities.RemoveRange(existingAmenities);
                db.SaveChanges();

                // Add the updated amenities
                db.PropertyAmenities.AddRange(model);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Property Amenities Updated");
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "UpdatePropertyAmenity : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetAmenitybyPropertyId")]
        [Obsolete]
        public HttpResponseMessage GetAmenitybyPropertyId(int id)
        {
            try
            {
                var property = (from p in db.PropertyAmenities
                                join a in db.Amenities
                                on p.AmenityId equals a.AmenityId
                                where p.PropertyId == id
                                select new
                                {
                                    p.AmenityId,
                                    p.IsAvailable,
                                    a.AmenityName
                                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, property);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetAmenitybyPropertyId : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/UpdateProperty11")]
        //[Obsolete]
        //public HttpResponseMessage UpdateProperty11(PropertyListing model)
        //{
        //    if (model == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, "Model is null. Check if the request body is being sent correctly.");
        //    }
        //    try
        //    {
        //        var property = db.PropertyListings.Find(model.Id);
        //        if (property == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }

        //        // Updating all fields
        //        property.Title = model.Title;
        //        property.PropertyType = model.PropertyType;
        //        property.ListingType = model.ListingType;
        //        property.PriceCurrency = model.PriceCurrency;
        //        property.MainImage = model.MainImage;
        //        property.SubImage = model.SubImage;
        //        property.PaymentPlanAvailable = model.PaymentPlanAvailable;
        //        property.PaymentPlanAvailableDescription = model.PaymentPlanAvailableDescription;
        //        property.PropertySlug = model.PropertySlug;
        //        property.ServiceCharges = model.ServiceCharges;
        //        property.PreviousOwnership = model.PreviousOwnership;
        //        property.TitleDeedAvailable = model.TitleDeedAvailable;
        //        property.UnitType = model.UnitType;
        //        property.PantryKitchen = model.PantryKitchen;
        //        property.LicenseTypeSupport = model.LicenseTypeSupport;
        //        property.Washroom = model.Washroom;
        //        property.Bedrooms = model.Bedrooms;
        //        property.Bathrooms = model.Bathrooms;
        //        property.BuiltUpArea = model.BuiltUpArea;
        //        property.PlotSize = model.PlotSize;
        //        property.Furnished = model.Furnished;
        //        property.FloorLevel = model.FloorLevel;
        //        property.PropertyView = model.PropertyView;
        //        property.KitchenType = model.KitchenType;
        //        property.ParkingSpaces = model.ParkingSpaces;
        //        property.StorageRoom = model.StorageRoom;
        //        property.SmartHomeFeatures = model.SmartHomeFeatures;
        //        property.DeveloperName = model.DeveloperName;
        //        property.HandoverDate = model.HandoverDate;
        //        property.PaymentPlanStructure = model.PaymentPlanStructure;
        //        property.PostHandoverPayment = model.PostHandoverPayment;
        //        property.PaymentTerms = model.PaymentTerms;
        //        property.CommissionFee = model.CommissionFee;
        //        property.TenancyContractLength = model.TenancyContractLength;
        //        property.VacatingNoticePeriod = model.VacatingNoticePeriod;
        //        property.Deposit = model.Deposit;
        //        property.ShowroomAvailable = model.ShowroomAvailable;
        //        property.MaidsRoom = model.MaidsRoom;
        //        property.BalconyTerrace = model.BalconyTerrace;
        //        property.CommunityName = model.CommunityName;
        //        property.City = model.City;
        //        property.Neighborhood = model.Neighborhood;
        //        property.ProximityToMetro = model.ProximityToMetro;
        //        property.NearbyLandmarks = model.NearbyLandmarks;
        //        property.SchoolHospitalProximity = model.SchoolHospitalProximity;
        //        property.SwimmingPool = model.SwimmingPool;
        //        property.ROI = model.ROI;
        //        property.RentalYield = model.RentalYield;
        //        property.AnnualServiceCharges = model.AnnualServiceCharges;
        //        property.MortgageCalculator = model.MortgageCalculator;
        //        property.RentalPaymentOptions = model.RentalPaymentOptions;
        //        property.ChequesAccepted = model.ChequesAccepted;
        //        property.ListingId = model.ListingId;
        //        property.RealEstateAgencyName = model.RealEstateAgencyName;
        //        property.RERAPermitNumber = model.RERAPermitNumber;
        //        property.PaymentMethod = model.PaymentMethod;
        //        property.DLDTransferFee = model.DLDTransferFee;
        //        property.CoolingSystem = model.CoolingSystem;
        //        property.PetPolicy = model.PetPolicy;
        //        property.TenancyContractStatus = model.TenancyContractStatus;
        //        property.LeaseExpiryDate = model.LeaseExpiryDate;
        //        property.BoostListing = model.BoostListing;
        //        property.PriorityPlacement = model.PriorityPlacement;
        //        property.RentalPrice = model.RentalPrice;
        //        property.OffPrice = model.OffPrice;
        //        property.SalePrice = model.SalePrice;
        //        property.VillaBedrooms = model.VillaBedrooms;
        //        property.VillaBathrooms = model.VillaBathrooms;
        //        property.VillaBuiltUpArea = model.VillaBuiltUpArea;
        //        property.VillaFurnished = model.VillaFurnished;
        //        property.VillaDeveloperName = model.VillaDeveloperName;
        //        property.VillaParkingSpaces = model.VillaParkingSpaces;
        //        property.VillaMaidsRoom = model.VillaMaidsRoom;
        //        property.CommercialBuiltUpArea = model.CommercialBuiltUpArea;
        //        property.CommercialDeveloperName = model.CommercialDeveloperName;
        //        property.CommercialFurnished = model.CommercialFurnished;
        //        property.CommercialParkingSpaces = model.CommercialParkingSpaces;
        //        property.CommercialFloorLevel = model.CommercialFloorLevel;
        //        property.Description = model.Description;
        //        property.ChequesAcceptedRental = model.ChequesAcceptedRental;
        //        property.ModifiedDate = DateTime.UtcNow;

        //        property.Off_plan = model.ListingType == "Off-Plan" ? true : false;
        //        property.Property_purpose = model.ListingType == "For Rent" ? "Rent" : "Sell";
        //        property.Property_Title_AR = model.Property_Title_AR;
        //        property.Property_Size = model.Property_Size;
        //        property.Property_Size_Unit = model.Property_Size_Unit;
        //        property.plotArea = model.plotArea;
        //        property.Portals = model.Portals;
        //        property.Rent_Frequency = model.Rent_Frequency;
        //        property.offplanDetails_saleType = model.offplanDetails_saleType;
        //        property.offplanDetails_dldWaiver = model.offplanDetails_dldWaiver;
        //        property.offplanDetails_originalPrice = model.offplanDetails_originalPrice;
        //        property.offplanDetails_amountPaid = model.offplanDetails_amountPaid;
        //        property.Videos = model.Videos;
        //        property.Locality = model.Locality;
        //        property.Sub_Locality = model.Sub_Locality;
        //        property.Tower_Name = model.Tower_Name;
        //        property.Listing_Agent = model.Listing_Agent;
        //        property.Listing_Agent_Phone = model.Listing_Agent_Phone;
        //        property.Listing_Agent_Email = model.Listing_Agent_Email;

        //        //if (model.PropertyAmenities.Count > 0)
        //        //{
        //        //    foreach (var i in model.PropertyAmenities)
        //        //    {
        //        //        //if(i.Amenity == "")
        //        //    }
        //        //}


        //        db.SaveChanges();

        //        return Request.CreateResponse(HttpStatusCode.OK, "Property Updated Successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "UpdateProperty : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UpdateProperty")]
        public async Task<HttpResponseMessage> UpdateProperty()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                // Get form data
                var propertyJson = result.FormData["property"];
                var existingMainImage = result.FormData["existingMainImage"];
                var existingSubImage = result.FormData["existingSubImage"];

                var model = JsonConvert.DeserializeObject<PropertyListing>(propertyJson);

                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // Handle file uploads
                var uploadPath = HttpContext.Current.Server.MapPath("~/EliteFiles/Propertiesfiles/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                //var baseUrl = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}";

                // Handle Main Image
                var mainImageFile = result.FileData.FirstOrDefault(f => f.Headers.ContentDisposition.Name.Contains("mainImage"));
                if (mainImageFile != null)
                {
                    var mainFileName = Guid.NewGuid() + Path.GetExtension(mainImageFile.Headers.ContentDisposition.FileName.Replace("\"", ""));
                    var mainImagePath = Path.Combine(uploadPath, mainFileName);
                    File.Move(mainImageFile.LocalFileName, mainImagePath);
                    model.MainImage = "/EliteFiles/Propertiesfiles/" + mainFileName;
                }
                else
                {
                    // Keep existing image if no new one provided
                    model.MainImage = existingMainImage;
                }

                // Handle Sub Images
                var subImageFiles = result.FileData.Where(f => f.Headers.ContentDisposition.Name.Contains("subImages"));
                var subImagePaths = new List<string>();

                if (subImageFiles.Any())
                {
                    foreach (var file in subImageFiles.Take(4))
                    {
                        var subFileName = Guid.NewGuid() + Path.GetExtension(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                        var subImagePath = Path.Combine(uploadPath, subFileName);
                        File.Move(file.LocalFileName, subImagePath);
                        subImagePaths.Add("/EliteFiles/Propertiesfiles/" + subFileName);
                    }
                    model.SubImage = string.Join(",", subImagePaths);
                }
                else
                {
                    // Keep existing sub images if no new ones provided
                    model.SubImage = existingSubImage;
                }




                // Update property in database
                var existingProperty = db.PropertyListings.Find(model.Id);
                if (existingProperty == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Property not found");
                }

                // Updating all fields
                existingProperty.Title = model.Title;
                existingProperty.PropertyType = model.PropertyType;
                existingProperty.ListingType = model.ListingType;
                existingProperty.PriceCurrency = model.PriceCurrency;
                existingProperty.MainImage = model.MainImage;
                existingProperty.SubImage = model.SubImage;
                existingProperty.PaymentPlanAvailable = model.PaymentPlanAvailable;
                existingProperty.PropertySlug = model.PropertySlug;
                existingProperty.PreviousOwnership = model.PreviousOwnership;
                existingProperty.TitleDeedAvailable = model.TitleDeedAvailable;
                existingProperty.UnitType = model.UnitType;
                existingProperty.PantryKitchen = model.PantryKitchen;
                existingProperty.LicenseTypeSupport = model.LicenseTypeSupport;
                existingProperty.Washroom = model.Washroom;
                existingProperty.Bedrooms = model.Bedrooms;
                existingProperty.Bathrooms = model.Bathrooms;
                existingProperty.BuiltUpArea = model.BuiltUpArea;
                existingProperty.PlotSize = model.PlotSize;
                existingProperty.Furnished = model.Furnished;
                existingProperty.FloorLevel = model.FloorLevel;
                existingProperty.PropertyView = model.PropertyView;
                existingProperty.KitchenType = model.KitchenType;
                existingProperty.ParkingSpaces = model.ParkingSpaces;
                existingProperty.StorageRoom = model.StorageRoom;
                existingProperty.SmartHomeFeatures = model.SmartHomeFeatures;
                existingProperty.DeveloperName = model.DeveloperName;
                existingProperty.HandoverDate = model.HandoverDate;
                existingProperty.PaymentPlanStructure = model.PaymentPlanStructure;
                existingProperty.PostHandoverPayment = model.PostHandoverPayment;
                existingProperty.PaymentTerms = model.PaymentTerms;
                existingProperty.CommissionFee = model.CommissionFee;
                existingProperty.TenancyContractLength = model.TenancyContractLength;
                existingProperty.VacatingNoticePeriod = model.VacatingNoticePeriod;
                existingProperty.Deposit = model.Deposit;
                existingProperty.ShowroomAvailable = model.ShowroomAvailable;
                existingProperty.MaidsRoom = model.MaidsRoom;
                existingProperty.BalconyTerrace = model.BalconyTerrace;
                existingProperty.CommunityName = model.CommunityName;
                existingProperty.City = model.City;
                existingProperty.Neighborhood = model.Neighborhood;
                existingProperty.ProximityToMetro = model.ProximityToMetro;
                existingProperty.NearbyLandmarks = model.NearbyLandmarks;
                existingProperty.SchoolHospitalProximity = model.SchoolHospitalProximity;
                existingProperty.SwimmingPool = model.SwimmingPool;
                existingProperty.ROI = model.ROI;
                existingProperty.RentalYield = model.RentalYield;
                existingProperty.AnnualServiceCharges = model.AnnualServiceCharges;
                existingProperty.MortgageCalculator = model.MortgageCalculator;
                existingProperty.RentalPaymentOptions = model.RentalPaymentOptions;
                existingProperty.ChequesAccepted = model.ChequesAccepted;
                existingProperty.ListingId = model.ListingId;
                existingProperty.RealEstateAgencyName = model.RealEstateAgencyName;
                existingProperty.RERAPermitNumber = model.RERAPermitNumber;
                existingProperty.PaymentMethod = model.PaymentMethod;
                existingProperty.DLDTransferFee = model.DLDTransferFee;
                existingProperty.CoolingSystem = model.CoolingSystem;
                existingProperty.PetPolicy = model.PetPolicy;
                existingProperty.TenancyContractStatus = model.TenancyContractStatus;
                existingProperty.LeaseExpiryDate = model.LeaseExpiryDate;
                existingProperty.BoostListing = model.BoostListing;
                existingProperty.PriorityPlacement = model.PriorityPlacement;
                existingProperty.RentalPrice = model.RentalPrice;
                existingProperty.OffPrice = model.OffPrice;
                existingProperty.SalePrice = model.SalePrice;
                existingProperty.VillaBedrooms = model.VillaBedrooms;
                existingProperty.VillaBathrooms = model.VillaBathrooms;
                existingProperty.VillaBuiltUpArea = model.VillaBuiltUpArea;
                existingProperty.VillaFurnished = model.VillaFurnished;
                existingProperty.VillaDeveloperName = model.VillaDeveloperName;
                existingProperty.VillaParkingSpaces = model.VillaParkingSpaces;
                existingProperty.VillaMaidsRoom = model.VillaMaidsRoom;
                existingProperty.CommercialBuiltUpArea = model.CommercialBuiltUpArea;
                existingProperty.CommercialDeveloperName = model.CommercialDeveloperName;
                existingProperty.CommercialFurnished = model.CommercialFurnished;
                existingProperty.CommercialParkingSpaces = model.CommercialParkingSpaces;
                existingProperty.CommercialFloorLevel = model.CommercialFloorLevel;
                existingProperty.Description = model.Description;
                existingProperty.ChequesAcceptedRental = model.ChequesAcceptedRental;
                existingProperty.ModifiedDate = DateTime.UtcNow;

                existingProperty.Off_plan = model.ListingType == "Off-Plan" ? true : false;
                existingProperty.Property_purpose = model.ListingType == "For Rent" ? "Rent" : "Sell";
                existingProperty.Property_Title_AR = model.Property_Title_AR;
                existingProperty.Property_Size = model.Property_Size;
                existingProperty.Property_Size_Unit = model.Property_Size_Unit;
                existingProperty.plotArea = model.plotArea;
                existingProperty.Portals = model.Portals;
                existingProperty.Rent_Frequency = model.Rent_Frequency;
                existingProperty.offplanDetails_saleType = model.offplanDetails_saleType;
                existingProperty.offplanDetails_dldWaiver = model.offplanDetails_dldWaiver;
                existingProperty.offplanDetails_originalPrice = model.offplanDetails_originalPrice;
                existingProperty.offplanDetails_amountPaid = model.offplanDetails_amountPaid;
                existingProperty.Videos = model.Videos;
                existingProperty.Locality = model.Locality;
                existingProperty.Sub_Locality = model.Sub_Locality;
                existingProperty.Tower_Name = model.Tower_Name;
                existingProperty.Listing_Agent = model.Listing_Agent;
                existingProperty.Listing_Agent_Phone = model.Listing_Agent_Phone;
                existingProperty.Listing_Agent_Email = model.Listing_Agent_Email;
                // Add other properties as needed

                db.Entry(existingProperty).State = EntityState.Modified;
                db.SaveChanges();

                // return Request.CreateResponse(HttpStatusCode.OK, new { PropertyId = model.Id });
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "Property updated successfully",
                    PropertyId = model.Id
                });

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.PropertyName + ": " + x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);

                Logging.WriteLog(LogType.Error, $"UpdateProperty Validation Failed: {fullErrorMessage}");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Errors = errorMessages });
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "UpdateProperty : " + (ex.InnerException?.Message ?? ex.Message));
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/CheckReraPermit")]
        public HttpResponseMessage CheckReraPermit(string permitNumber, int? propertyId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(permitNumber))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "RERA Permit Number is required");

                var query = db.PropertyListings.AsQueryable();

                // 🔹 Exclude current property when updating
                if (propertyId.HasValue)
                    query = query.Where(x => x.Id != propertyId.Value);

                bool isDuplicate = query.Any(x => x.RERAPermitNumber == permitNumber);

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    isDuplicate,
                    message = isDuplicate
                        ? "RERA Permit Number already exists"
                        : "RERA Permit Number is available"
                });
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "CheckReraPermit: " + ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/ValidateReraPermit")]   
        //public HttpResponseMessage ValidateReraPermit(ValidateReraPermitRequest model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }

        //        var query = db.PropertyListings
        //                      .Where(x => x.RERAPermitNumber == model.RERAPermitNumber);

        //        // 🔹 If updating existing property, exclude its own record
        //        if (model.PropertyId.HasValue)
        //        {
        //            query = query.Where(x => x.Id != model.PropertyId.Value);
        //        }

        //        bool exists = query.Any();

        //        if (exists)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, new
        //            {
        //                isValid = false,
        //                message = "RERA Permit Number already exists"
        //            });
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK, new
        //        {
        //            isValid = true,
        //            message = "RERA Permit Number is available"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "ValidateReraPermit : " + ex.Message);
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/DeleteProperty")]
        [Obsolete]
        public HttpResponseMessage DeleteProperty(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var property = db.PropertyListings.FirstOrDefault(x => x.Id == id);
                    if (property != null)
                    {
                        // First delete related PropertyAmenities
                        var relatedAmenities = db.PropertyAmenities.Where(pa => pa.PropertyId == id).ToList();
                        db.PropertyAmenities.RemoveRange(relatedAmenities);

                        // Then delete the property
                        db.PropertyListings.Remove(property);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Property not found");
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "DeleteProperty : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request");
        }



        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetOffPlanProperties")]
        [Obsolete]
        public HttpResponseMessage OffPlanProperties(int pageNumber = 1, int pageSize = 6, string city = null,
                                              string propertyType = null,
                                              string price = null,
                                              int? bedrooms = null)
        {
            try
            {
                var propertiesQuery = db.PropertyListings
                                        .Where(x => x.ListingType == "Off-Plan")
                                        .OrderByDescending(p => p.Id);

                var q = propertiesQuery.AsQueryable();

                //// — listing type
                //if (!string.IsNullOrWhiteSpace(listingType))
                //    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                if (bedrooms.HasValue)
                    q = bedrooms == 3
                        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);


                if (!string.IsNullOrWhiteSpace(price))
                {
                    if (price == "low")
                    {
                        q = q.OrderByDescending(p => p.SalePrice.Value);
                    }
                    else
                    {
                        q = q.OrderBy(p => p.SalePrice.Value);
                    }
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();



                //var totalCount = propertiesQuery.Count();

                var propertiesList = q// propertiesQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = propertiesList
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetOffPlanProperties : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/GetOffPlanbySlug")]
        //[Obsolete]
        //public HttpResponseMessage OffPlanbySlug(string name)
        //{
        //    try
        //    {
        //        var propertybyslug = db.PropertyListings
        //            .Where(x => x.PropertySlug.Trim() == name.Trim() && x.ListingType == "Off-Plan")
        //            .Select(property => new
        //            {
        //                property.Id,
        //                property.Title,
        //                property.PropertyType,
        //                property.ListingType,
        //                property.PriceCurrency,
        //                property.MainImage,
        //                property.SubImage,
        //                property.PaymentPlanAvailable,
        //                property.PropertySlug,
        //                property.PropertyCategory,
        //                property.DeveloperName,
        //                property.HandoverDate,
        //                property.PaymentPlanStructure,
        //                property.PostHandoverPayment,
        //                property.ConstructionStatus,
        //                property.ModelUnitImages,
        //                property.ServiceCharges,
        //                property.ShowroomAvailable,
        //                property.PropertyAge,
        //                property.TenancyStatus,
        //                property.MortgageStatus,
        //                property.PreviousOwnership,
        //                property.TitleDeedAvailable,
        //                property.PaymentTerms,
        //                property.ChequesAccepted,
        //                property.CommissionFee,
        //                property.TenancyContractLength,
        //                property.VacatingNoticePeriod,
        //                property.Deposit,
        //                property.Bedrooms,
        //                property.Bathrooms,
        //                property.FloorLevel,
        //                property.BalconyTerrace,
        //                property.PropertyView,
        //                property.BuiltUpArea,
        //                property.ParkingSpaces,
        //                property.KitchenType,
        //                property.MaidsRoom,
        //                property.SmartHomeFeatures,
        //                property.StorageRoom,
        //                property.PlotSize,
        //                property.PrivateGarden,
        //                property.PrivatePool,
        //                property.Furnished,
        //                property.UnitType,
        //                property.PantryKitchen,
        //                property.Washroom,
        //                property.LicenseTypeSupport,
        //                property.SwimmingPool,
        //                property.CommunityName,
        //                property.City,
        //                property.Neighborhood,
        //                property.ProximityToMetro,
        //                property.NearbyLandmarks,
        //                property.SchoolHospitalProximity,
        //                property.ROI,
        //                property.RentalYield,
        //                property.AnnualServiceCharges,
        //                property.MortgageCalculator,
        //                property.RentalPaymentOptions,
        //                property.ListingId,
        //                property.RealEstateAgencyName,
        //                property.RERAPermitNumber,
        //                property.PaymentMethod,
        //                property.DLDTransferFee,
        //                property.CoolingSystem,
        //                property.PetPolicy,
        //                property.TenancyContractStatus,
        //                property.LeaseExpiryDate,
        //                property.BoostListing,
        //                property.PriorityPlacement,
        //                property.CreatedDate,
        //                property.ModifiedDate,
        //                property.CreatedBy,
        //                property.OffPrice,
        //                property.VillaBedrooms,
        //                property.VillaBathrooms,
        //                property.VillaBuiltUpArea,
        //                property.VillaParkingSpaces,
        //                property.VillaFloorLevel,
        //                property.VillaFurnished,
        //                property.VillaMaidsRoom,
        //                property.VillaDeveloperName,
        //                property.CommercialBuiltUpArea,
        //                property.CommercialDeveloperName,
        //                property.CommercialParkingSpaces,
        //                property.CommercialFloorLevel,
        //                property.CommercialFurnished,
        //                property.Description,
        //                property.PaymentPlanAvailableDescription,
        //                property.ChequesAcceptedRental,

        //                Amenities = property.PropertyAmenities.Select(pa => new
        //                {
        //                    pa.AmenityId,
        //                    pa.IsAvailable,

        //                    AmenityName = pa.Amenity.AmenityName
        //                }).ToList()
        //            })
        //            .FirstOrDefault();

        //        return Request.CreateResponse(HttpStatusCode.OK, propertybyslug);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "GetOffPlanbySlug : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetRentalProperties")]
        [Obsolete]
        public HttpResponseMessage RentalProperties(int pageNumber = 1, int pageSize = 6,
                                              string city = null,
                                              string propertyType = null,
                                              string price = null,
                                              int? bedrooms = null)
        {
            try
            {
                var propertiesQuery = db.PropertyListings.Where(x => x.ListingType == "For Rent").OrderByDescending(p => p.Id).ToList();


                var q = propertiesQuery.AsQueryable();

                //// — listing type
                //if (!string.IsNullOrWhiteSpace(listingType))
                //    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                if (bedrooms.HasValue)
                    q = bedrooms == 3
                        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);


                if (!string.IsNullOrWhiteSpace(price))
                {
                    if (price == "low")
                    {
                        q = q.OrderByDescending(p => p.SalePrice.Value);
                    }
                    else
                    {
                        q = q.OrderBy(p => p.SalePrice.Value);
                    }
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();
                //  var data = q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();



                // var totalCount = propertiesQuery.Count();

                var propertiesList = q // propertiesQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = propertiesList
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetRentalProperties : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }

        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/GetRentalbySlug")]
        //[Obsolete]
        //public HttpResponseMessage RentalbySlug(string name)
        //{
        //    try
        //    {
        //        var propertybyslug = db.PropertyListings
        //            .Where(x => x.PropertySlug.Trim() == name.Trim() && x.ListingType == "For Rent")
        //            .Select(property => new
        //            {
        //                property.Id,
        //                property.Title,
        //                property.PropertyType,
        //                property.ListingType,
        //                property.PriceCurrency,
        //                property.MainImage,
        //                property.SubImage,
        //                property.PaymentPlanAvailable,
        //                property.PropertySlug,
        //                property.PropertyCategory,
        //                property.DeveloperName,
        //                property.HandoverDate,
        //                property.PaymentPlanStructure,
        //                property.PostHandoverPayment,
        //                property.ConstructionStatus,
        //                property.ModelUnitImages,
        //                property.ServiceCharges,
        //                property.ShowroomAvailable,
        //                property.PropertyAge,
        //                property.TenancyStatus,
        //                property.MortgageStatus,
        //                property.PreviousOwnership,
        //                property.TitleDeedAvailable,
        //                property.RentalPrice,
        //                property.PaymentTerms,
        //                property.ChequesAccepted,
        //                property.CommissionFee,
        //                property.TenancyContractLength,
        //                property.VacatingNoticePeriod,
        //                property.Deposit,
        //                property.Bedrooms,
        //                property.Bathrooms,
        //                property.FloorLevel,
        //                property.BalconyTerrace,
        //                property.PropertyView,
        //                property.BuiltUpArea,
        //                property.ParkingSpaces,
        //                property.KitchenType,
        //                property.MaidsRoom,
        //                property.SmartHomeFeatures,
        //                property.StorageRoom,
        //                property.PlotSize,
        //                property.PrivateGarden,
        //                property.PrivatePool,
        //                property.Furnished,
        //                property.UnitType,
        //                property.PantryKitchen,
        //                property.Washroom,
        //                property.LicenseTypeSupport,
        //                property.SwimmingPool,
        //                property.CommunityName,
        //                property.City,
        //                property.Neighborhood,
        //                property.ProximityToMetro,
        //                property.NearbyLandmarks,
        //                property.SchoolHospitalProximity,
        //                property.ROI,
        //                property.RentalYield,
        //                property.AnnualServiceCharges,
        //                property.MortgageCalculator,
        //                property.RentalPaymentOptions,
        //                property.ListingId,
        //                property.RealEstateAgencyName,
        //                property.RERAPermitNumber,
        //                property.PaymentMethod,
        //                property.DLDTransferFee,
        //                property.CoolingSystem,
        //                property.PetPolicy,
        //                property.TenancyContractStatus,
        //                property.LeaseExpiryDate,
        //                property.BoostListing,
        //                property.PriorityPlacement,
        //                property.CreatedDate,
        //                property.ModifiedDate,
        //                property.CreatedBy,
        //                property.VillaBedrooms,
        //                property.VillaBathrooms,
        //                property.VillaBuiltUpArea,
        //                property.VillaParkingSpaces,
        //                property.VillaFloorLevel,
        //                property.VillaFurnished,
        //                property.VillaMaidsRoom,
        //                property.VillaDeveloperName,
        //                property.CommercialBuiltUpArea,
        //                property.CommercialDeveloperName,
        //                property.CommercialParkingSpaces,
        //                property.CommercialFloorLevel,
        //                property.CommercialFurnished,
        //                property.Description,
        //                property.PaymentPlanAvailableDescription,
        //                property.ChequesAcceptedRental,

        //                Amenities = property.PropertyAmenities.Select(pa => new
        //                {
        //                    pa.AmenityId,
        //                    pa.IsAvailable,

        //                    AmenityName = pa.Amenity.AmenityName
        //                }).ToList()
        //            })
        //            .FirstOrDefault();

        //        return Request.CreateResponse(HttpStatusCode.OK, propertybyslug);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "GetRentalbySlug : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSecondryProperties")]
        [Obsolete]
        public HttpResponseMessage SecondryProperties(int pageNumber = 1, int pageSize = 6, string city = null,
                                              string propertyType = null,
                                              string price = null,
                                              int? bedrooms = null)
        {
            try
            {
                var propertiesQuery = db.PropertyListings.Where(x => x.ListingType == "For Sale").OrderByDescending(p => p.Id).ToList();

                var q = propertiesQuery.AsQueryable();

                //// — listing type
                //if (!string.IsNullOrWhiteSpace(listingType))
                //    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                if (bedrooms.HasValue)
                    q = bedrooms == 3
                        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);


                if (!string.IsNullOrWhiteSpace(price))
                {
                    if (price == "low")
                    {
                        q = q.OrderByDescending(p => p.SalePrice.Value);
                    }
                    else
                    {
                        q = q.OrderBy(p => p.SalePrice.Value);
                    }
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();


                // var totalCount = propertiesQuery.Count();

                var propertiesList = q// propertiesQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = propertiesList
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetSecondryProperties : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }

        //[System.Web.Http.AllowAnonymous]
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/GetSecondrybySlug")]
        //[Obsolete]
        //public HttpResponseMessage SecondrybySlug(string name)
        //{
        //    try
        //    {
        //        var propertybyslug = db.PropertyListings
        //            .Where(x => x.PropertySlug.Trim() == name.Trim() && x.ListingType == "For Sale")
        //            .Select(property => new
        //            {
        //                property.Id,
        //                property.Title,
        //                property.PropertyType,
        //                property.ListingType,
        //                property.PriceCurrency,
        //                property.MainImage,
        //                property.SubImage,
        //                property.PaymentPlanAvailable,
        //                property.PropertySlug,
        //                property.PropertyCategory,
        //                property.DeveloperName,
        //                property.HandoverDate,
        //                property.PaymentPlanStructure,
        //                property.PostHandoverPayment,
        //                property.ConstructionStatus,
        //                property.ModelUnitImages,
        //                property.ServiceCharges,
        //                property.ShowroomAvailable,
        //                property.PropertyAge,
        //                property.TenancyStatus,
        //                property.MortgageStatus,
        //                property.PreviousOwnership,
        //                property.TitleDeedAvailable,
        //                property.SalePrice,
        //                property.PaymentTerms,
        //                property.ChequesAccepted,
        //                property.CommissionFee,
        //                property.TenancyContractLength,
        //                property.VacatingNoticePeriod,
        //                property.Deposit,
        //                property.Bedrooms,
        //                property.Bathrooms,
        //                property.FloorLevel,
        //                property.BalconyTerrace,
        //                property.PropertyView,
        //                property.BuiltUpArea,
        //                property.ParkingSpaces,
        //                property.KitchenType,
        //                property.MaidsRoom,
        //                property.SmartHomeFeatures,
        //                property.StorageRoom,
        //                property.PlotSize,
        //                property.PrivateGarden,
        //                property.PrivatePool,
        //                property.Furnished,
        //                property.UnitType,
        //                property.PantryKitchen,
        //                property.Washroom,
        //                property.LicenseTypeSupport,
        //                property.SwimmingPool,
        //                property.CommunityName,
        //                property.City,
        //                property.Neighborhood,
        //                property.ProximityToMetro,
        //                property.NearbyLandmarks,
        //                property.SchoolHospitalProximity,
        //                property.ROI,
        //                property.RentalYield,
        //                property.AnnualServiceCharges,
        //                property.MortgageCalculator,
        //                property.RentalPaymentOptions,
        //                property.ListingId,
        //                property.RealEstateAgencyName,
        //                property.RERAPermitNumber,
        //                property.PaymentMethod,
        //                property.DLDTransferFee,
        //                property.CoolingSystem,
        //                property.PetPolicy,
        //                property.TenancyContractStatus,
        //                property.LeaseExpiryDate,
        //                property.BoostListing,
        //                property.PriorityPlacement,
        //                property.CreatedDate,
        //                property.ModifiedDate,
        //                property.CreatedBy,
        //                property.VillaBedrooms,
        //                property.VillaBathrooms,
        //                property.VillaBuiltUpArea,
        //                property.VillaParkingSpaces,
        //                property.VillaFloorLevel,
        //                property.VillaFurnished,
        //                property.VillaMaidsRoom,
        //                property.VillaDeveloperName,
        //                property.CommercialBuiltUpArea,
        //                property.CommercialDeveloperName,
        //                property.CommercialParkingSpaces,
        //                property.CommercialFloorLevel,
        //                property.CommercialFurnished,
        //                property.Description,
        //                property.PaymentPlanAvailableDescription,
        //                property.ChequesAcceptedRental,

        //                Amenities = property.PropertyAmenities.Select(pa => new
        //                {
        //                    pa.AmenityId,
        //                    pa.IsAvailable,

        //                    AmenityName = pa.Amenity.AmenityName
        //                }).ToList()
        //            })
        //            .FirstOrDefault();

        //        return Request.CreateResponse(HttpStatusCode.OK, propertybyslug);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "GetSecondrybySlug : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPropertiesbayut")]
        [Obsolete]
        public HttpResponseMessage GetPropertiesbayut()
        {
            try
            {
                var data =
                    (from p in db.PropertyListings
                     where p.Status == "Active"
                     select new
                     {
                         // All Property Fields
                         p.Id,
                         p.Title,
                         p.PropertyType,
                         p.ListingType,
                         p.PriceCurrency,
                         p.MainImage,
                         p.SubImage,
                         p.PaymentPlanAvailable,
                         p.PaymentPlanAvailableDescription,
                         p.PropertySlug,
                         p.PropertyCategory,
                         p.DeveloperName,
                         p.HandoverDate,
                         p.PaymentPlanStructure,
                         p.PostHandoverPayment,
                         p.ConstructionStatus,
                         p.ModelUnitImages,
                         p.ServiceCharges,
                         p.ShowroomAvailable,
                         p.PropertyAge,
                         p.TenancyStatus,
                         p.MortgageStatus,
                         p.PreviousOwnership,
                         p.TitleDeedAvailable,
                         p.RentalPrice,
                         p.PaymentTerms,
                         p.ChequesAccepted,
                         p.CommissionFee,
                         p.TenancyContractLength,
                         p.VacatingNoticePeriod,
                         p.Deposit,
                         p.Bedrooms,
                         p.Bathrooms,
                         p.FloorLevel,
                         p.BalconyTerrace,
                         p.PropertyView,
                         p.BuiltUpArea,
                         p.ParkingSpaces,
                         p.KitchenType,
                         p.MaidsRoom,
                         p.SmartHomeFeatures,
                         p.StorageRoom,
                         p.PlotSize,
                         p.PrivateGarden,
                         p.PrivatePool,
                         p.Furnished,
                         p.UnitType,
                         p.PantryKitchen,
                         p.Washroom,
                         p.LicenseTypeSupport,
                         p.SwimmingPool,
                         p.CommunityName,
                         p.City,
                         p.Neighborhood,
                         p.ProximityToMetro,
                         p.NearbyLandmarks,
                         p.SchoolHospitalProximity,
                         p.ROI,
                         p.RentalYield,
                         p.AnnualServiceCharges,
                         p.MortgageCalculator,
                         p.RentalPaymentOptions,
                         p.ListingId,
                         p.RealEstateAgencyName,
                         p.RERAPermitNumber,
                         p.PaymentMethod,
                         p.DLDTransferFee,
                         p.CoolingSystem,
                         p.PetPolicy,
                         p.TenancyContractStatus,
                         p.LeaseExpiryDate,
                         p.BoostListing,
                         p.PriorityPlacement,
                         p.CreatedDate,
                         p.ModifiedDate,
                         p.CreatedBy,
                         p.OffPrice,
                         p.SalePrice,
                         p.VillaBedrooms,
                         p.VillaBathrooms,
                         p.VillaParkingSpaces,
                         p.VillaFloorLevel,
                         p.VillaBuiltUpArea,
                         p.VillaMaidsRoom,
                         p.VillaDeveloperName,
                         p.VillaFurnished,
                         p.CommercialBuiltUpArea,
                         p.CommercialParkingSpaces,
                         p.CommercialFloorLevel,
                         p.CommercialDeveloperName,
                         p.CommercialFurnished,
                         p.Description,
                         p.ChequesAcceptedRental,
                         p.Status,
                         p.Property_Title_AR,
                         p.Property_purpose,
                         p.Property_Size,
                         p.Property_Size_Unit,
                         p.plotArea,
                         p.Features,
                         p.Off_plan,
                         p.Portals,
                         p.Property_Description_AR,
                         p.Rent_Frequency,
                         p.offplanDetails_saleType,
                         p.offplanDetails_dldWaiver,
                         p.offplanDetails_originalPrice,
                         p.offplanDetails_amountPaid,
                         p.Videos,
                         p.Locality,
                         p.Sub_Locality,
                         p.Tower_Name,
                         p.Listing_Agent,
                         p.Listing_Agent_Phone,
                         p.Listing_Agent_Email,

                         // Join Amenities
                         Amenities =
                            (from pa in db.PropertyAmenities
                             join a in db.Amenities on pa.AmenityId equals a.AmenityId
                             where pa.PropertyId == p.Id
                             select new
                             {
                                 a.AmenityId,
                                 a.AmenityName,
                                 a.Icon
                             }).ToList()
                     }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    Data = data
                });
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetPropertiesbayut : " +
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message));

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        //public HttpResponseMessage GetPropertiesbayut111()
        //{
        //    try
        //    {
        //        var data = db.PropertyListings.Where(x => x.Status == "Active").ToList();
        //        return Request.CreateResponse(HttpStatusCode.OK, new
        //        {
        //            Data = data
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(LogType.Error, "GetPropertiesbayut : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPropertyList")]
        [Obsolete]
        public HttpResponseMessage GetPropertyList(
                                              int pageNumber = 1,
                                              int pageSize = 6,
                                              string listingType = null,   // "Rent" | "Sell" | "Off-Plan" …
                                              string city = null,   // "Dubai", "Abu Dhabi", …
                                              string propertyType = null,   // "Apartment", "Villa", …
                                              string price = null,   // "500000-1000000"
                                              int? bedrooms = null)   // 1 | 2 | 3 (3 means “3 or more”)
        {
            try
            {
                var q = db.PropertyListings.AsQueryable();

                // — listing type
                if (!string.IsNullOrWhiteSpace(listingType))
                    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                if (bedrooms.HasValue)
                    q = bedrooms == 3
                        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);

                // — price range
                if (!string.IsNullOrWhiteSpace(price) && price.Contains('-'))
                {
                    var parts = price.Split('-');
                    if (decimal.TryParse(parts[0], out var min))
                        q = q.Where(p => p.SalePrice >= min || p.RentalPrice >= min || p.OffPrice >= min);
                    if (decimal.TryParse(parts[1], out var max))
                        q = q.Where(p => p.SalePrice <= max || p.RentalPrice <= max || p.OffPrice <= max);
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();
                // var data = q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var data = q
    .OrderByDescending(p => p.Id)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .Select(p => new PropertyListDTO
    {
        Id = p.Id,
        Title = p.Title,
        PropertyType = p.PropertyType,
        ListingType = p.ListingType,
        PriceCurrency = p.PriceCurrency,
        Price = p.ListingType == "Off-Plan" ? p.OffPrice : p.ListingType == "For Rent" ? p.RentalPrice : p.SalePrice,
        Area = p.PropertyType == "Apartment" ? p.BuiltUpArea : p.PropertyType == "Villa" ? p.VillaBuiltUpArea : p.PropertyType == "Commercial" ? p.CommercialBuiltUpArea : p.BuiltUpArea,
        Bedrooms = p.PropertyType == "Apartment" ? p.Bedrooms : p.VillaBedrooms,
        Bathrooms = p.PropertyType == "Apartment" ? p.Bathrooms : p.VillaBathrooms,
        ParkingSpaces = p.PropertyType == "Commercial" ? p.CommercialParkingSpaces : p.PropertyType == "Villa" ? p.VillaParkingSpaces : p.ParkingSpaces,
        City = p.City,
        Neighborhood = p.Neighborhood,
        PropertySlug = p.PropertySlug,
        CreatedDate = p.CreatedDate,
        MainImage = p.MainImage

    })
    .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetPropertyList : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPropertydetailbyId")]
        [Obsolete]
        public HttpResponseMessage GetPropertydetailbyId(int id)
        {
            try
            {
                var propertybyslug = db.PropertyListings
                    .Where(x => x.Id == id)
                    .Select(property => new
                    {
                        property.Id,
                        property.Title,
                        property.PropertyType,
                        property.ListingType,
                        property.PriceCurrency,
                        PaymentPlanAvailable = property.PaymentPlanAvailable == null ? false : property.PaymentPlanAvailable,
                        property.PropertySlug,
                        property.PropertyCategory,
                        DeveloperName = property.PropertyType == "Villa" ? property.VillaDeveloperName : property.PropertyType == "Commercial" ? property.CommercialDeveloperName : property.DeveloperName,
                        HandoverDate = property.HandoverDate == null ? "" : property.HandoverDate,
                        PaymentPlanStructure = property.PaymentPlanStructure == null ? "" : property.PaymentPlanStructure,
                        PostHandoverPayment = property.PostHandoverPayment == null ? "" : property.PostHandoverPayment,
                        property.ConstructionStatus,
                        property.ModelUnitImages,
                        property.ServiceCharges,
                        property.ShowroomAvailable,
                        property.PropertyAge,
                        property.TenancyStatus,
                        property.MortgageStatus,
                        property.PreviousOwnership,
                        property.TitleDeedAvailable,
                        //property.RentalPrice,
                        property.PaymentTerms,
                        property.ChequesAccepted,
                        property.CommissionFee,
                        property.TenancyContractLength,
                        property.VacatingNoticePeriod,
                        property.Deposit,
                        Bedrooms = property.PropertyType == "Villa" ? property.VillaBedrooms : property.Bedrooms,
                        Bathrooms = property.PropertyType == "Villa" ? property.VillaBathrooms : property.Bathrooms,
                        FloorLevel = property.PropertyType == "Villa" ? property.VillaFloorLevel : property.PropertyType == "" ? property.CommercialFloorLevel : property.FloorLevel,
                        property.BalconyTerrace,
                        property.PropertyView,
                        BuiltUpArea = property.PropertyType == "Villa" ? property.VillaBuiltUpArea : property.PropertyType == "Commercial" ? property.CommercialBuiltUpArea : property.BuiltUpArea,
                        ParkingSpaces = property.PropertyType == "Villa" ? property.VillaParkingSpaces : property.PropertyType == "Commercial" ? property.CommercialParkingSpaces : property.ParkingSpaces,
                        property.KitchenType,
                        MaidsRoom = property.PropertyType == "Villa" ? property.VillaMaidsRoom : property.MaidsRoom,
                        property.SmartHomeFeatures,
                        property.StorageRoom,
                        property.PlotSize,
                        property.PrivateGarden,
                        property.PrivatePool,
                        Furnished = property.PropertyType == "Villa" ? property.VillaFurnished : property.PropertyType == "Commercial" ? property.CommercialFurnished : property.Furnished,
                        property.UnitType,
                        property.PantryKitchen,
                        property.Washroom,
                        property.LicenseTypeSupport,
                        property.SwimmingPool,
                        property.CommunityName,
                        property.City,
                        property.Neighborhood,
                        property.ProximityToMetro,
                        property.NearbyLandmarks,
                        property.SchoolHospitalProximity,
                        property.ROI,
                        property.RentalYield,
                        property.AnnualServiceCharges,
                        property.MortgageCalculator,
                        property.RentalPaymentOptions,
                        property.ListingId,
                        property.RealEstateAgencyName,
                        property.RERAPermitNumber,
                        property.PaymentMethod,
                        property.DLDTransferFee,
                        property.CoolingSystem,
                        property.PetPolicy,
                        property.TenancyContractStatus,
                        property.LeaseExpiryDate,
                        property.BoostListing,
                        property.PriorityPlacement,
                        Price = property.ListingType == "Off-Plan" ? property.OffPrice : property.ListingType == "For Rent" ? property.RentalPrice : property.SalePrice,
                       
                        property.Description,
                        property.PaymentPlanAvailableDescription,
                        property.ChequesAcceptedRental,
                        property.MainImage,
                        property.SubImage,
                        property.CreatedDate,
                        property.ModifiedDate,
                        property.CreatedBy,
                        property.Property_Title_AR,
                        property.Property_Size,
                        property.Listing_Agent,
                        property.Listing_Agent_Phone,
                        property.Listing_Agent_Email,

                        Amenities = property.PropertyAmenities.Select(pa => new
                        {
                            pa.AmenityId,
                            pa.IsAvailable,
                            Icon = pa.Amenity.Icon,
                            AmenityName = pa.Amenity.AmenityName,
                            Category = pa.Amenity.AmenityCategory.CategoryName
                        }).ToList()
                    })
                    .FirstOrDefault();

                return Request.CreateResponse(HttpStatusCode.OK, propertybyslug);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetPropertydetailbyId : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPropertydetailbySlug")]
        [Obsolete]
        public HttpResponseMessage GetPropertydetailbySlug(string name)
        {
            try
            {
                var propertybyslug = db.PropertyListings
                    .Where(x => x.PropertySlug.Trim() == name.Trim())
                    .Select(property => new
                    {
                        property.Id,
                        property.Title,
                        property.PropertyType,
                        property.ListingType,
                        property.PriceCurrency,
                        PaymentPlanAvailable = property.PaymentPlanAvailable == null ? false :property.PaymentPlanAvailable,
                        property.PropertySlug,
                        property.PropertyCategory,
                        DeveloperName = property.PropertyType == "Villa" ? property.VillaDeveloperName :property.PropertyType == "Commercial" ? property.CommercialDeveloperName : property.DeveloperName,
                        HandoverDate = property.HandoverDate == null ? "" : property.HandoverDate,
                        PaymentPlanStructure = property.PaymentPlanStructure == null ? "" : property.PaymentPlanStructure,
                        PostHandoverPayment= property.PostHandoverPayment == null ?"": property.PostHandoverPayment,
                        property.ConstructionStatus,
                        property.ModelUnitImages,
                        property.ServiceCharges,
                        property.ShowroomAvailable,
                        property.PropertyAge,
                        property.TenancyStatus,
                        property.MortgageStatus,
                        property.PreviousOwnership,
                        property.TitleDeedAvailable,
                        //property.RentalPrice,
                        property.PaymentTerms,
                        property.ChequesAccepted,
                        property.CommissionFee,
                        property.TenancyContractLength,
                        property.VacatingNoticePeriod,
                        property.Deposit,
                        Bedrooms = property.PropertyType == "Villa" ? property.VillaBedrooms : property.Bedrooms,
                        Bathrooms = property.PropertyType == "Villa" ? property.VillaBathrooms : property.Bathrooms,
                        FloorLevel = property.PropertyType == "Villa" ? property.VillaFloorLevel : property.PropertyType == "" ? property.CommercialFloorLevel : property.FloorLevel,
                        property.BalconyTerrace,
                        property.PropertyView,
                        BuiltUpArea = property.PropertyType == "Villa" ? property.VillaBuiltUpArea: property.PropertyType == "Commercial" ? property.CommercialBuiltUpArea : property.BuiltUpArea,
                        ParkingSpaces = property.PropertyType == "Villa" ? property.VillaParkingSpaces : property.PropertyType == "Commercial" ? property.CommercialParkingSpaces : property.ParkingSpaces,
                        property.KitchenType,
                        MaidsRoom = property.PropertyType == "Villa" ? property.VillaMaidsRoom : property.MaidsRoom,
                        property.SmartHomeFeatures,
                        property.StorageRoom,
                        property.PlotSize,
                        property.PrivateGarden,
                        property.PrivatePool,
                        Furnished = property.PropertyType == "Villa" ? property.VillaFurnished :property.PropertyType == "Commercial" ? property.CommercialFurnished : property.Furnished,
                        property.UnitType,
                        property.PantryKitchen,
                        property.Washroom,
                        property.LicenseTypeSupport,
                        property.SwimmingPool,
                        property.CommunityName,
                        property.City,
                        property.Neighborhood,
                        property.ProximityToMetro,
                        property.NearbyLandmarks,
                        property.SchoolHospitalProximity,
                        property.ROI,
                        property.RentalYield,
                        property.AnnualServiceCharges,
                        property.MortgageCalculator,
                        property.RentalPaymentOptions,
                        property.ListingId,
                        property.RealEstateAgencyName,
                        property.RERAPermitNumber,
                        property.PaymentMethod,
                        property.DLDTransferFee,
                        property.CoolingSystem,
                        property.PetPolicy,
                        property.TenancyContractStatus,
                        property.LeaseExpiryDate,
                        property.BoostListing,
                        property.PriorityPlacement,
                        Price = property.ListingType == "Off-Plan" ? property.OffPrice : property.ListingType == "For Rent" ? property.RentalPrice : property.SalePrice,
                        //property.OffPrice,
                        //property.SalePrice,
                        //property.VillaBedrooms,
                        //property.VillaBathrooms,
                        //property.VillaBuiltUpArea,
                        // property.VillaParkingSpaces,
                        //  property.VillaFloorLevel ,
                        // property.VillaFurnished,
                        // property.VillaMaidsRoom,
                        // property.VillaDeveloperName,
                        // property.CommercialBuiltUpArea,
                        // property.CommercialDeveloperName,
                        // property.CommercialParkingSpaces,
                        // property.CommercialFloorLevel,
                        // property.CommercialFurnished,
                        property.Description,
                        property.PaymentPlanAvailableDescription,
                        property.ChequesAcceptedRental,
                        property.MainImage,
                        property.SubImage,
                        property.CreatedDate,
                        property.ModifiedDate,
                        property.CreatedBy,
                        property.Property_Title_AR,
                        property.Property_Size,
                        property.Listing_Agent,
                        property.Listing_Agent_Phone,
                        property.Listing_Agent_Email,

                        Amenities = property.PropertyAmenities.Select(pa => new
                        {
                            pa.AmenityId,
                            pa.IsAvailable,
                            Icon = pa.Amenity.Icon,
                            AmenityName = pa.Amenity.AmenityName,
                            Category = pa.Amenity.AmenityCategory.CategoryName
                        }).ToList()
                    })
                    .FirstOrDefault();

                return Request.CreateResponse(HttpStatusCode.OK, propertybyslug);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetPropertybySlug : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetOffPlanPropertyList")]
        [Obsolete]
        public HttpResponseMessage GetOffPlanPropertyList(int pageNumber = 1, int pageSize = 6, string city = null,
                                              string propertyType = null,
                                              string price = null,
                                              int? bedrooms = null)
        {
            try
            {
                var propertiesQuery = db.PropertyListings
                                        .Where(x => x.ListingType == "Off-Plan")
                                        .OrderByDescending(p => p.Id);

                var q = propertiesQuery.AsQueryable();

                //// — listing type
                //if (!string.IsNullOrWhiteSpace(listingType))
                //    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                //if (bedrooms.HasValue)
                //    q = bedrooms == 3
                //        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                //        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);
                if (bedrooms.HasValue)
                {
                    int b = bedrooms.Value;

                    q = b == 3
                        ? q.Where(p =>
                            (p.PropertyType == "Villa" &&
                             p.VillaBedrooms.HasValue &&
                             p.VillaBedrooms >= 3)
                            ||
                            (p.PropertyType != "Villa" &&
                             p.Bedrooms.HasValue &&
                             p.Bedrooms >= 3))
                        : q.Where(p =>
                            (p.PropertyType == "Villa" &&
                             p.VillaBedrooms.HasValue &&
                             p.VillaBedrooms == b)
                            ||
                            (p.PropertyType != "Villa" &&
                             p.Bedrooms.HasValue &&
                             p.Bedrooms == b));
                }

                if (!string.IsNullOrWhiteSpace(price))
                {
                    if (price == "low")
                    {
                        q = q.OrderByDescending(p => p.OffPrice.Value);
                    }
                    else
                    {
                        q = q.OrderBy(p => p.OffPrice.Value);
                    }
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();



                //var totalCount = propertiesQuery.Count();

                //var propertiesList = q// propertiesQuery
                //    .Skip((pageNumber - 1) * pageSize)
                //    .Take(pageSize)
                //    .ToList();

                var propertiesList = q
    .OrderByDescending(p => p.Id)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .Select(p => new PropertyListDTO
    {
        Id = p.Id,
        Title = p.Title,
        PropertyType = p.PropertyType,
        ListingType = p.ListingType,
        PriceCurrency = p.PriceCurrency,
        Price = p.ListingType == "Off-Plan" ? p.OffPrice : p.ListingType == "For Rent" ? p.RentalPrice : p.SalePrice,
        Area = p.PropertyType == "Apartment" ? p.BuiltUpArea : p.PropertyType == "Villa" ? p.VillaBuiltUpArea : p.PropertyType == "Commercial" ? p.CommercialBuiltUpArea : p.BuiltUpArea,
        Bedrooms = p.PropertyType == "Apartment" ? p.Bedrooms : p.VillaBedrooms,
        Bathrooms = p.PropertyType == "Apartment" ? p.Bathrooms : p.VillaBathrooms,
        ParkingSpaces = p.PropertyType == "Commercial" ? p.CommercialParkingSpaces : p.PropertyType == "Villa" ? p.VillaParkingSpaces : p.ParkingSpaces,
        City = p.City,
        Neighborhood = p.Neighborhood,
        PropertySlug = p.PropertySlug,
        CreatedDate = p.CreatedDate,
        MainImage = p.MainImage

    })
    .ToList();


                var result = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = propertiesList
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetOffPlanPropertyList : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetRentalPropertyList")]
        [Obsolete]
        public HttpResponseMessage GetRentalPropertyList(int pageNumber = 1, int pageSize = 6,
                                              string city = null,
                                              string propertyType = null,
                                              string price = null,
                                              int? bedrooms = null)
        {
            try
            {
                var propertiesQuery = db.PropertyListings.Where(x => x.ListingType == "For Rent").OrderByDescending(p => p.Id).ToList();


                var q = propertiesQuery.AsQueryable();

                //// — listing type
                //if (!string.IsNullOrWhiteSpace(listingType))
                //    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                //if (bedrooms.HasValue)
                //    q = bedrooms == 3
                //        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                //        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);

                if (bedrooms.HasValue)
                {
                    int b = bedrooms.Value;

                    q = b == 3
                        ? q.Where(p =>
                            (p.PropertyType == "Villa" &&
                             p.VillaBedrooms.HasValue &&
                             p.VillaBedrooms >= 3)
                            ||
                            (p.PropertyType != "Villa" &&
                             p.Bedrooms.HasValue &&
                             p.Bedrooms >= 3))
                        : q.Where(p =>
                            (p.PropertyType == "Villa" &&
                             p.VillaBedrooms.HasValue &&
                             p.VillaBedrooms == b)
                            ||
                            (p.PropertyType != "Villa" &&
                             p.Bedrooms.HasValue &&
                             p.Bedrooms == b));
                }



                if (!string.IsNullOrWhiteSpace(price))
                {
                    if (price == "low")
                    {
                        q = q.OrderByDescending(p => p.RentalPrice.Value);
                    }
                    else
                    {
                        q = q.OrderBy(p => p.RentalPrice.Value);
                    }
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();
                //  var data = q.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();



                // var totalCount = propertiesQuery.Count();

                //var propertiesList = q // propertiesQuery
                //    .Skip((pageNumber - 1) * pageSize)
                //    .Take(pageSize)
                //    .ToList();

                var propertiesList = q
   .OrderByDescending(p => p.Id)
   .Skip((pageNumber - 1) * pageSize)
   .Take(pageSize)
   .Select(p => new PropertyListDTO
   {
       Id = p.Id,
       Title = p.Title,
       PropertyType = p.PropertyType,
       ListingType = p.ListingType,
       PriceCurrency = p.PriceCurrency,
       Price = p.ListingType == "Off-Plan" ? p.OffPrice : p.ListingType == "For Rent" ? p.RentalPrice : p.SalePrice,
       Area = p.PropertyType == "Apartment" ? p.BuiltUpArea : p.PropertyType == "Villa" ? p.VillaBuiltUpArea : p.PropertyType == "Commercial" ? p.CommercialBuiltUpArea : p.BuiltUpArea,
       Bedrooms = p.PropertyType == "Apartment" ? p.Bedrooms : p.VillaBedrooms,
       Bathrooms = p.PropertyType == "Apartment" ? p.Bathrooms : p.VillaBathrooms,
       ParkingSpaces = p.PropertyType == "Commercial" ? p.CommercialParkingSpaces : p.PropertyType == "Villa" ? p.VillaParkingSpaces : p.ParkingSpaces,
       City = p.City,
       Neighborhood = p.Neighborhood,
       PropertySlug = p.PropertySlug,
       CreatedDate = p.CreatedDate,
       MainImage = p.MainImage

   }).ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = propertiesList
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetRentalPropertyList : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }


        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSecondryPropertyList")]
        [Obsolete]
        public HttpResponseMessage GetSecondryPropertyList(int pageNumber = 1, int pageSize = 6, string city = null,
                                             string propertyType = null,
                                             string price = null,
                                             int? bedrooms = null)
        {
            try
            {
                var propertiesQuery = db.PropertyListings.Where(x => x.ListingType == "For Sale").OrderByDescending(p => p.Id).ToList();

                var q = propertiesQuery.AsQueryable();

                //// — listing type
                //if (!string.IsNullOrWhiteSpace(listingType))
                //    q = q.Where(p => p.ListingType == listingType);

                // — city / location
                if (!string.IsNullOrWhiteSpace(city))
                    q = q.Where(p => p.City == city);

                // — property type
                if (!string.IsNullOrWhiteSpace(propertyType))
                    q = q.Where(p => p.PropertyType == propertyType);

                // — bedrooms
                //if (bedrooms.HasValue)
                //    q = bedrooms == 3
                //        ? q.Where(p => p.Bedrooms >= 3 || p.VillaBedrooms >= 3)
                //        : q.Where(p => p.Bedrooms == bedrooms || p.VillaBedrooms == bedrooms);
                if (bedrooms.HasValue)
                {
                    int b = bedrooms.Value;

                    q = b == 3
                        ? q.Where(p =>
                            (p.PropertyType == "Villa" &&
                             p.VillaBedrooms.HasValue &&
                             p.VillaBedrooms >= 3)
                            ||
                            (p.PropertyType != "Villa" &&
                             p.Bedrooms.HasValue &&
                             p.Bedrooms >= 3))
                        : q.Where(p =>
                            (p.PropertyType == "Villa" &&
                             p.VillaBedrooms.HasValue &&
                             p.VillaBedrooms == b)
                            ||
                            (p.PropertyType != "Villa" &&
                             p.Bedrooms.HasValue &&
                             p.Bedrooms == b));
                }

                if (!string.IsNullOrWhiteSpace(price))
                {
                    if (price == "low")
                    {
                        q = q.OrderByDescending(p => p.SalePrice.Value);
                    }
                    else
                    {
                        q = q.OrderBy(p => p.SalePrice.Value);
                    }
                }

                // — paging
                q = q.OrderByDescending(p => p.Id);
                var totalCount = q.Count();


                // var totalCount = propertiesQuery.Count();

                //var propertiesList = q// propertiesQuery
                //    .Skip((pageNumber - 1) * pageSize)
                //    .Take(pageSize)
                //    .ToList();

                var propertiesList = q
   .OrderByDescending(p => p.Id)
   .Skip((pageNumber - 1) * pageSize)
   .Take(pageSize)
   .Select(p => new PropertyListDTO
   {
       Id = p.Id,
       Title = p.Title,
       PropertyType = p.PropertyType,
       ListingType = p.ListingType,
       PriceCurrency = p.PriceCurrency,
       Price = p.ListingType == "Off-Plan" ? p.OffPrice : p.ListingType == "For Rent" ? p.RentalPrice : p.SalePrice,
       Area = p.PropertyType == "Apartment" ? p.BuiltUpArea : p.PropertyType == "Villa" ? p.VillaBuiltUpArea : p.PropertyType == "Commercial" ? p.CommercialBuiltUpArea : p.BuiltUpArea,
       Bedrooms = p.PropertyType == "Apartment" ? p.Bedrooms : p.VillaBedrooms,
       Bathrooms = p.PropertyType == "Apartment" ? p.Bathrooms : p.VillaBathrooms,
       ParkingSpaces = p.PropertyType == "Commercial" ? p.CommercialParkingSpaces : p.PropertyType == "Villa" ? p.VillaParkingSpaces : p.ParkingSpaces,
       City = p.City,
       Neighborhood = p.Neighborhood,
       PropertySlug = p.PropertySlug,
       CreatedDate = p.CreatedDate,
       MainImage = p.MainImage

   }).ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = propertiesList
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "GetSecondryPropertyList : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
    }
}