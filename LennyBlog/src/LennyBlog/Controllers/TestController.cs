using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Localization;


namespace LennyBlog.Controllers
{
    public class TestController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            string url = @"http://api.duoshuo.com/threads/counts.json?short_name=lennyblog&threads=0ad2df7a-585e-4400-aa3e-9e948a49d6a8";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://api.duoshuo.com");
            HttpResponseMessage response = await client.GetAsync(url);
            string str = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(str);
            string comment = obj["response"]["0ad2df7a-585e-4400-aa3e-9e948a49d6a8"]["comments"];
            return View();
        }
    }
}
