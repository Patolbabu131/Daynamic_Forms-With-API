using Azure;
using Daynamic_forms_mvc.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;

namespace Daynamic_forms_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        Uri baseaddress = new Uri("https://localhost:7231/api/Forms/");
        HttpClient client;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            List<Forms> forms = new List<Forms>();  
            HttpResponseMessage responce = client.GetAsync("GetForms").Result;
            if (responce.IsSuccessStatusCode)
            {
                var data = responce.Content.ReadAsStringAsync().Result;
                forms = JsonConvert.DeserializeObject<List<Forms>>(data);
                return Json(new
                {
                    aaData = forms
                });
            }
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetEdit(int id)
        {
            List<Forms> forms = new List<Forms>();
            HttpResponseMessage responce = client.GetAsync("GetEdit/?id=" + id.ToString()).Result;
            if (responce.IsSuccessStatusCode)
            {
                var data = responce.Content.ReadAsStringAsync().Result;
                forms = JsonConvert.DeserializeObject<List<Forms>>(data);
                return Json(forms);
            }
            return Json("not ok");
        }
        public IActionResult Create()
        {
            return View();
        }
  

        public IActionResult Edit()
        {
            return PartialView("Edit");
        }
        
        public IActionResult PostEdit(Forms forms)
        {
            HttpResponseMessage responce = client.PutAsJsonAsync<Forms>("PutForms", forms).Result;
            if (responce.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var body = responce.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return Json(body);
            }
                        if (responce.IsSuccessStatusCode)
            {
                return Json("Form is Created...");
            }
            return Json("Form not created...");
        }

        //public IActionResult SaveForms(Forms forms)
        //{
        //    string data = JsonConvert.SerializeObject(forms);
        //    var responce = client.PostAsJsonAsync<Forms>("PostForms", forms);
        //    StringContent content= new StringContent(data,Encoding.UTF8,"application/json");
        //    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "PostForms", content).Result;
        //    if (response.StatusCode==System.Net.HttpStatusCode.BadRequest) 
        //    {
        //        var body= response.Content.ReadAsStringAsync();
        //        Console.WriteLine (body);
        //        return View("index");
        //    }
        //    return View("index");
        //}

        public IActionResult SaveForms(Forms forms)
        {
            HttpResponseMessage responce = client.PostAsJsonAsync<Forms>("PostForms",forms).Result;

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var body = responce.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return View("index");
            }
            if (responce.IsSuccessStatusCode)
            {
                return Json("Form is Created...");
            }
            return Json("Form not created...");
        }

        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync("Delete/?id=" + id.ToString()).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var body = response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return View("index");
            }
            if (response.IsSuccessStatusCode)
            {
                return Json("Form is Deleted..");
            }
            return Json("From is not Deleted. Sum error is occure");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}