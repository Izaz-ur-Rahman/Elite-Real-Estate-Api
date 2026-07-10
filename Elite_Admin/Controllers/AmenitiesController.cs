using Elite_Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Elite_Admin.Controllers
{
    public class AmenitiesController : Controller
    {
        private readonly HttpClient _httpClient;

        public AmenitiesController()
        {
            var configurationManager = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configurationManager)
            };
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("GetAmenities");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var amenities = JsonConvert.DeserializeObject<List<AmenityViewModel>>(json);
                
                return View(amenities);
            }
            return View();
        }


        public async Task<ActionResult> CreateAmenity()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Categories");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(json);

                var model = new Amenity
                {
                    Categories = categories.Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                };

                return View(model);
            }

            ModelState.AddModelError("", "Failed to load categories.");
            return View(new Amenity());
        }

        [HttpPost]
        public async Task<ActionResult> CreateAmenity(Amenity model, HttpPostedFileBase iconFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string iconUrl = null;

            if (iconFile != null && iconFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(iconFile.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                var relativePath = "~/EliteFiles/AmenityIcons/" + uniqueFileName;
                var absolutePath = Server.MapPath(relativePath);

                // Ensure directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));

                // Save the file
                iconFile.SaveAs(absolutePath);

                // Set URL to store in DB
                iconUrl = Url.Content(relativePath); // returns a path like "/EliteFiles/AmenityIcons/abc.png"
            }

            var amenity = new
            {
                AmenityName = model.AmenityName,
                Icon = iconUrl, // store the URL to the uploaded icon
                CategoryId = model.CategoryId
            };

            string json = JsonConvert.SerializeObject(amenity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("CreateAmenity", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error saving amenity.");
            return View(model);
        }


        //[HttpPost]
        //public async Task<ActionResult> CreateAmenity(Amenity model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var amenity = new
        //    {
        //        AmenityName = model.AmenityName,
        //        Icon = model.Icon,
        //        CategoryId = model.CategoryId
        //    };

        //    string json = JsonConvert.SerializeObject(amenity);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync("CreateAmenity", content);

        //    if (response.IsSuccessStatusCode)
        //        return RedirectToAction("Index");

        //    ModelState.AddModelError("", "Error saving amenity.");
        //    return View(model);
        //}


        public async Task<ActionResult> CategoriesList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Categories");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(json);

                return View(categories);
            }
            return View();
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("CreateCategory", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("CategoriesList", "Amenities"); // Or any other desired action

            ModelState.AddModelError("", "Failed to add category.");
            return View(model);
        }



    }
}