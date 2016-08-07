using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LennyBlog.Controllers
{
    public class SiteController : BaseController
    {
        [Route("Site")]
        public IActionResult Index()
        {
            string to = Request.Query["to"];
            string view = "Index";
            if (string.IsNullOrEmpty(to) || to.ToLower().Equals("applylink"))
            {
                view = "ApplyLink";
            }
            else if (to.ToLower().Equals("about"))
            {
                view = "About";
            }
            return View(view);
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("ApplyLink")]
        [HttpGet]
        public IActionResult ApplyLink()
        {
            return View();
        }

        [Route("ApplyLink")]
        [HttpPost]
        public IActionResult ApplyLink(string Site, string Url, string Email)
        {
            Link link = new Link
            {
                CreatedDate = DateTime.Now,
                Email = Email,
                IsDelete = false,
                Site = Site,
                Status = Models.Enum.LinkStatus.Applying,
                Url = Url,
            };
            DB.Links.Add(link);
            DB.SaveChanges();
            return Redirect("/Site");
        }


    }
}
