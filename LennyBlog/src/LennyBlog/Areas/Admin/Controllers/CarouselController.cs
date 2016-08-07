using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using LennyBlog.Models.ViewModels;
using LennyBlog.Models.DataModels;
using LennyBlog.Extensions;
using SmallCode.Pager;


namespace LennyBlog.Areas.Admin.Controllers
{
    public class CarouselController : BaseController
    {
        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            IQueryable<Carousel> query = DB.Carousels.AsQueryable();
                 

            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(x => x.Title.Contains(Title));
            }
            PagedList<Carousel> data = query.ToPagedList(PageIndex, PageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            CarouselViewModel model = null;
            if (id.HasValue)
            {
                Carousel carousel = DB.Carousels.FirstOrDefault(x => x.Id == id);
                model = new CarouselViewModel(carousel);
                model.IsNew = false;
            }
            else
            {
                model = new CarouselViewModel();
                model.IsNew = true;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CarouselViewModel model)
        {

            bool flag = false;
            string ReturnMsg = "";
            if (model.IsNew)
            {
                Carousel carousel = new Carousel();
                carousel.Id = model.Id;
                carousel.Title = model.Title;
                carousel.Description = model.Description;
                carousel.CreatedDate = DateTime.Now;
                carousel.IsShow = model.IsShow;
                carousel.Url = model.Url;
                DB.Carousels.Add(carousel);
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "增加成功" : "增加失败";
            }
            else
            {
                var carousel = DB.Carousels.FirstOrDefault(x => x.Id == model.Id);
                carousel.Title = model.Title;
                carousel.Description = model.Description;
                carousel.Url = model.Url;
                carousel.IsShow = model.IsShow;
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "修改成功" : "修改失败";
            }
            if (flag)
            {
                return Redirect("/Admin/Carousel/Index");
            }
            else
            {
                ModelState.AddModelError("", ReturnMsg);
                return View(model);
            }
        }


        public IActionResult Show(Guid id)
        {
            Carousel carousel = DB.Carousels.FirstOrDefault(x => x.Id == id);
            carousel.Description = CommonMark.CommonMarkConverter.Convert(carousel.Description);
            return View(carousel);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var article = DB.Articles.FirstOrDefault(x => x.Id == id);
            DB.Articles.Remove(article);
            bool flag = DB.SaveChanges() > 0;
            ReturnResult model = new ReturnResult();
            model.Status = flag ? "ok" : "error";
            model.ReturnMsg = flag ? "删除成功" : "删除失败";
            return Json(model);
        }
    }
}
