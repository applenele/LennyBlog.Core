using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models;
using LennyBlog.Models.ViewModels;
using CommonMark;
using LennyBlog.Extensions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LennyBlog.Controllers
{
    public class CarouselController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCarousels(int count = 4)
        {
            List<CarouselViewModel> _carousels = new List<CarouselViewModel>();
            var carousels = DB.Carousels.Where(x => x.IsShow == true).Take(count).ToList();
            foreach (var item in carousels)
            {
                string[] imageUrls = CommonMarkConverter.Convert(item.Description).GetHtmlImageUrlList();
                var carousel = new CarouselViewModel(item);
                carousel.Description = CommonMarkConverter.Convert(item.Description).ReplaceHtmlTag().FilterSpecial();
                carousel.ImageUrl = (imageUrls.Length > 0) ? imageUrls[0] : "";
                _carousels.Add(carousel);
            }
            return Json(_carousels);
        }
    }
}
