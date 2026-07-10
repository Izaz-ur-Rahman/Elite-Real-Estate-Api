using Elite_Admin.Models;
using Elite_Admin.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Elite_Admin.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly HttpClient _httpClient;

        public PropertiesController()
        {
            var configurationManager = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configurationManager)
            };
        }

        // GET: Properties
        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 1000)
        {
            try
            {
                List<PropertyListing> properties = new List<PropertyListing>();

                using (HttpClient client = new HttpClient())
                {
                    var baseUrl = ConfigurationManager.AppSettings["ApiUrl"];
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"GetPropertyList?pageNumber={pageNumber}&pageSize={pageSize}");

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<PropertiesApiResponse>(result);
                        properties = apiResponse.Data;
                    }
                }

                return View(properties);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "Index : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
                return View();
            }
           
        }


        public async Task<ActionResult> PropertyDetails(string propertyName)
        {
            try
            {
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }

                var data = webservices.PropertybySlug(propertyName);

                return View(data);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "PropertyDetails : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
                return View(ex.Message);
            }

        }

        public async Task<ActionResult> AddProperty()
        {
            var response = await _httpClient.GetAsync("GetAmenities");
            var viewModel = new PropertyListingVM();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var amenities = JsonConvert.DeserializeObject<List<AmenityViewModel>>(json);

                viewModel.Amenities = amenities.Select(a => new AmenitySelectionViewModel
                {
                    AmenityId = a.AmenityId,
                    AmenityName = a.AmenityName,
                    Icon = a.Icon,
                    CategoryName = a.CategoryName,
                    IsSelected = null // default to not set
                }).ToList();
            }

            viewModel.Property = new PropertyListing();
            return View(viewModel);
           
        }

        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> AddProperty111(PropertyListingVM model, HttpPostedFileBase MainImage, IEnumerable<HttpPostedFileBase> SubImage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var sessiontimeees = Session.Timeout;
                model.Property.CreatedBy = Session["EmpNo"].ToString();

                var uploadPath = Server.MapPath("~/EliteFiles/Propertiesfiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Save MainImage
                if (MainImage != null && MainImage.ContentLength > 0)
                {
                    var mainImagePath = Path.Combine(uploadPath, Path.GetFileName(MainImage.FileName));
                    MainImage.SaveAs(mainImagePath);
                    model.Property.MainImage = "/EliteFiles/Propertiesfiles/" + MainImage.FileName;
                }

                // Save up to 4 SubImages
                if (SubImage != null)
                {
                    var subImagePaths = new List<string>();
                    int count = 0;
                    foreach (var file in SubImage)
                    {
                        if (file != null && file.ContentLength > 0 && count < 4)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var subImagePath = Path.Combine(uploadPath, fileName);
                            file.SaveAs(subImagePath);
                            subImagePaths.Add("/EliteFiles/Propertiesfiles/" + fileName);
                            count++;
                        }
                    }

                    // Assuming SubImage is a comma-separated string in your model
                    model.Property.SubImage = string.Join(",", subImagePaths);
                }

                // Send data to API
                HttpClient httpClient = new HttpClient();
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }

                var json = JsonConvert.SerializeObject(model.Property);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                var url = ConfigurationSettings.AppSettings["ApiUrl"] + "PropertiesListing/AddProperty";
                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Data saved successfully!";
                }

                // Get the created Property ID from response (optional: depends on API return)
                var propertyResultJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(propertyResultJson);
                int propertyId = (int)result.PropertyId;

                // Prepare list of selected amenities
                var selectedAmenities = model.Amenities
                     .Where(a => a.IsSelected.HasValue && a.IsSelected.Value)
                     .Select(a => new
                     {
                         PropertyId = propertyId, // from response
                         AmenityId = a.AmenityId,
                         IsAvailable = true
                     })
                     .ToList();


                // Send Amenities if any
                if (selectedAmenities.Any())
                {
                    var amenityJson = JsonConvert.SerializeObject(selectedAmenities);
                    var amenityContent = new StringContent(amenityJson, Encoding.UTF8, "application/json");

                    var amenityUrl = ConfigurationSettings.AppSettings["ApiUrl"] + "AddPropertyAmenity";
                    var amenityResponse = await httpClient.PostAsync(amenityUrl, amenityContent);

                    if (!amenityResponse.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Property saved, but amenities failed.";
                        return RedirectToAction("Index");
                    }
                }

                TempData["Message"] = "Property and amenities saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "AddProperty : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
                return View(model); // Or create a dedicated error view
            }
        }


        [HttpGet]
        public async Task<ActionResult> UpdateProperty(int id)
        {
            try
            {
                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }
                ViewBag.baseapiurl = ConfigurationSettings.AppSettings["ApiBaseUrl"];
                // Fetch property data
                var property = webservices.PropertybyId(id); // PropertyListing
                var viewModel = new PropertyListingVM
                {
                    Property = property,
                    Amenities = new List<AmenitySelectionViewModel>()
                };

                // Fetch all amenities
                var amenityResponse = await _httpClient.GetAsync("GetAmenities");
                if (amenityResponse.IsSuccessStatusCode)
                {
                    var amenityJson = await amenityResponse.Content.ReadAsStringAsync();
                    var allAmenities = JsonConvert.DeserializeObject<List<AmenityViewModel>>(amenityJson);

                    // Get selected Amenity IDs from the property
                    var selectedAmenityIds = property.PropertyAmenities?.Select(pa => pa.AmenityId).ToList() ?? new List<int>();

                    viewModel.Amenities = allAmenities.Select(a => new AmenitySelectionViewModel
                    {
                        AmenityId = a.AmenityId,
                        AmenityName = a.AmenityName,
                        IsSelected = selectedAmenityIds.Contains(a.AmenityId)
                    }).ToList();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "UpdateProperty : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
                return View("Error", new HandleErrorInfo(ex, "Property", "UpdateProperty"));
            }
        }

        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> UpdateProperty11( PropertyListingVM model, HttpPostedFileBase MainImage, IEnumerable<HttpPostedFileBase> SubImageFiles, List<AmenitySelectionViewModel> Amenities ) // <-- Add this to receive selected amenities
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var uploadPath = Server.MapPath("~/EliteFiles/Propertiesfiles");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Handle main image
                if (MainImage != null && MainImage.ContentLength > 0)
                {
                    var mainImagePath = Path.Combine(uploadPath, Path.GetFileName(MainImage.FileName));
                    MainImage.SaveAs(mainImagePath);
                    model.Property.MainImage = "/EliteFiles/Propertiesfiles/" + MainImage.FileName;
                }
                else
                {
                    model.Property.MainImage = Request["ExistingMainImage"]; // Fallback to existing image
                }

                // Handle sub images
                var subImagePaths = new List<string>();
                if (SubImageFiles != null)
                {
                    int count = 0;
                    foreach (var file in SubImageFiles)
                    {
                        if (file != null && file.ContentLength > 0 && count < 4)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var fullPath = Path.Combine(uploadPath, fileName);
                            file.SaveAs(fullPath);
                            subImagePaths.Add("/EliteFiles/Propertiesfiles/" + fileName);
                            count++;
                        }
                    }

                    if (subImagePaths.Any())
                    {
                        model.Property.SubImage = string.Join(",", subImagePaths);
                    }
                    else
                    {
                        model.Property.SubImage = Request["ExistingSubImage"];
                    }
                }

                HttpClient httpClient = new HttpClient();

                if (Session["apiToken"] == null)
                {
                    var gettoken = webservices.gettoken();
                    Session["apiToken"] = gettoken.access_token;
                }

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                // 1. Update the property
                var json = JsonConvert.SerializeObject(model.Property);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = ConfigurationSettings.AppSettings["ApiUrl"] + "UpdateProperty";
                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // 2. Prepare amenities and call UpdatePropertyAmenity API
                    if (Amenities != null && Amenities.Any())
                    {
                        var selectedAmenities = Amenities
                            .Where(a => a.IsSelected == true)
                            .Select(a => new PropertyAmenity
                            {
                                PropertyId = model.Property.Id,
                                AmenityId = a.AmenityId,
                                IsAvailable = a.IsSelected
                                
                            }).ToList();

                        var amenityJson = JsonConvert.SerializeObject(selectedAmenities);
                        var amenityContent = new StringContent(amenityJson, Encoding.UTF8, "application/json");

                        string amenityUrl = ConfigurationSettings.AppSettings["ApiUrl"] + "UpdatePropertyAmenity";
                        var amenityResponse = await httpClient.PostAsync(amenityUrl, amenityContent);

                        if (!amenityResponse.IsSuccessStatusCode)
                        {
                            TempData["Message"] = "Property updated, but amenities update failed.";
                        }
                        else
                        {
                            TempData["Message"] = "Property and amenities updated successfully!";
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "Failed to update property.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "UpdatePropertymodel : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
                TempData["Error"] = "An error occurred while updating property: " + ex.Message;
                return View("Index", model);
            }
        }


        [Obsolete]
        public ActionResult DeleteProperty(int id)
        {
            try
            {
                var data = webservices.DeleteProperty(id);
                TempData["msg"] = data;

                return RedirectToAction("Index", "Properties");
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "DeleteProperty : " + (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message).ToString());
                return RedirectToAction("Index", "Properties");
            }
            
        }


        #region new approach

        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> AddProperty(PropertyListingVM model, HttpPostedFileBase MainImage, IEnumerable<HttpPostedFileBase> SubImage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.Property.CreatedBy = Session["EmpNo"].ToString();

                using (var httpClient = new HttpClient())
                {
                    if (Session["apiToken"] == null)
                    {
                        var gettoken = webservices.gettoken();
                        Session["apiToken"] = gettoken.access_token;
                    }

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                    // Create multipart form data
                    var formData = new MultipartFormDataContent();

                    // Add property data
                    var propertyJson = JsonConvert.SerializeObject(model.Property);
                    formData.Add(new StringContent(propertyJson, Encoding.UTF8, "application/json"), "property");

                    // Add main image
                    if (MainImage != null && MainImage.ContentLength > 0)
                    {
                        var mainImageContent = new StreamContent(MainImage.InputStream);
                        mainImageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MainImage.ContentType);
                        formData.Add(mainImageContent, "mainImage", MainImage.FileName);
                    }

                    // Add sub images
                    if (SubImage != null)
                    {
                        int count = 0;
                        foreach (var file in SubImage)
                        {
                            if (file != null && file.ContentLength > 0 && count < 4)
                            {
                                var subImageContent = new StreamContent(file.InputStream);
                                subImageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                                formData.Add(subImageContent, "subImages", file.FileName);
                                count++;
                            }
                        }
                    }

                    var url = ConfigurationSettings.AppSettings["ApiUrl"] + "PropertiesListing/AddProperty";
                    var response = await httpClient.PostAsync(url, formData);

                    if (response.IsSuccessStatusCode)
                    {
                        var resultJson = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(resultJson);
                        int propertyId = (int)result.PropertyId;

                        // Save amenities (your existing code)
                        await SaveAmenities(propertyId, model.Amenities, httpClient);

                        TempData["Message"] = "Property and amenities saved successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "Error saving property.";
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "AddProperty : " + (ex.InnerException?.Message ?? ex.Message));
                return View(model);
            }
        }

        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> UpdateProperty(PropertyListingVM model, HttpPostedFileBase MainImage, IEnumerable<HttpPostedFileBase> SubImageFiles, List<AmenitySelectionViewModel> Amenities)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (var httpClient = new HttpClient())
                {
                    if (Session["apiToken"] == null)
                    {
                        var gettoken = webservices.gettoken();
                        Session["apiToken"] = gettoken.access_token;
                    }

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["apiToken"]);

                    // Create multipart form data
                    var formData = new MultipartFormDataContent();

                    // Add property data
                    var propertyJson = JsonConvert.SerializeObject(model.Property);
                    formData.Add(new StringContent(propertyJson, Encoding.UTF8, "application/json"), "property");

                    // Add existing images data (important for update)
                    formData.Add(new StringContent(Request["ExistingMainImage"] ?? ""), "existingMainImage");
                    formData.Add(new StringContent(Request["ExistingSubImage"] ?? ""), "existingSubImage");

                    // Add main image if provided
                    if (MainImage != null && MainImage.ContentLength > 0)
                    {
                        var mainImageContent = new StreamContent(MainImage.InputStream);
                        mainImageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MainImage.ContentType);
                        formData.Add(mainImageContent, "mainImage", MainImage.FileName);
                    }

                    // Add sub images if provided
                    if (SubImageFiles != null)
                    {
                        int count = 0;
                        foreach (var file in SubImageFiles)
                        {
                            if (file != null && file.ContentLength > 0 && count < 4)
                            {
                                var subImageContent = new StreamContent(file.InputStream);
                                subImageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                                formData.Add(subImageContent, "subImages", file.FileName);
                                count++;
                            }
                        }
                    }

                    // 1. Update the property with images
                    string url = ConfigurationSettings.AppSettings["ApiUrl"] + "UpdateProperty";
                    var response = await httpClient.PostAsync(url, formData);

                    if (response.IsSuccessStatusCode)
                    {
                        // 2. Prepare amenities and call UpdatePropertyAmenity API
                        if (Amenities != null && Amenities.Any())
                        {
                            var selectedAmenities = Amenities
                                .Where(a => a.IsSelected == true)
                                .Select(a => new PropertyAmenity
                                {
                                    PropertyId = model.Property.Id,
                                    AmenityId = a.AmenityId,
                                    IsAvailable = a.IsSelected
                                }).ToList();

                            var amenityJson = JsonConvert.SerializeObject(selectedAmenities);
                            var amenityContent = new StringContent(amenityJson, Encoding.UTF8, "application/json");

                            string amenityUrl = ConfigurationSettings.AppSettings["ApiUrl"] + "UpdatePropertyAmenity";
                            var amenityResponse = await httpClient.PostAsync(amenityUrl, amenityContent);

                            if (!amenityResponse.IsSuccessStatusCode)
                            {
                                TempData["Message"] = "Property updated, but amenities update failed.";
                            }
                            else
                            {
                                TempData["Message"] = "Property and amenities updated successfully!";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "Property updated successfully!";
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["Message"] = "Failed to update property. " + errorContent;
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog(LogType.Error, "UpdateProperty : " + (ex.InnerException?.Message ?? ex.Message));
                TempData["Error"] = "An error occurred while updating property: " + ex.Message;
                return View("Index", model);
            }
        }
        private async Task SaveAmenities(int propertyId, List<AmenitySelectionViewModel> amenities, HttpClient httpClient)
        {
            var selectedAmenities = amenities
                .Where(a => a.IsSelected.HasValue && a.IsSelected.Value)
                .Select(a => new
                {
                    PropertyId = propertyId,
                    AmenityId = a.AmenityId,
                    IsSelected= a.IsSelected,
                    IsAvailable = true
                })
                .ToList();

            if (selectedAmenities.Any())
            {
                var amenityJson = JsonConvert.SerializeObject(selectedAmenities);
                var amenityContent = new StringContent(amenityJson, Encoding.UTF8, "application/json");
                var amenityUrl = ConfigurationSettings.AppSettings["ApiUrl"] + "AddPropertyAmenity";
                await httpClient.PostAsync(amenityUrl, amenityContent);
            }
        }

        #endregion


    }
}