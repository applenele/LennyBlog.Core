using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models.ViewModels;
using SmallCode.Pager;
using Microsoft.EntityFrameworkCore;
using LennyBlog.Extensions;
using LennyBlog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using LennyBlog.Models.DataModels;

namespace LennyBlog.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            IQueryable<ArticleViewModel> query =
                  from a in DB.Articles
                  join c in DB.Categories on a.CategoryId equals c.Id
                  select new ArticleViewModel
                  {
                      Browses = a.Browses,
                      CategoryName = c.Name,
                      Title = a.Title,
                      Id = a.Id,
                      CategoryId = a.CategoryId,
                      CreatedDate = a.CreatedDate,
                      Description = a.Description,
                      IsDelete = a.IsDelete,
                      Summary = a.Description.SubString(200, "......"),
                      Tags = a.Tags,
                      TagArr = a.Tags.StringToStringArr('c')
                  };

            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(x => x.Title.Contains(Title));
            }
            PagedList<ArticleViewModel> data = query.ToPagedList(PageIndex, PageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            ArticleViewModel model = null;
            if (Id.HasValue)
            {
                Article article = DB.Articles.FirstOrDefault(x => x.Id == Id);
                model = new ArticleViewModel(article);
                model.IsNew = false;
            }
            else
            {
                model = new ArticleViewModel();
                model.IsNew = true;
            }
            List<SelectListItem> categories = DB.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            categories.Prepend(new SelectListItem { Value = "", Text = "选择分类" });
            ViewData["CategoryList"] = categories;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ArticleViewModel model)
        {

            List<SelectListItem> categories = DB.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            categories.Prepend(new SelectListItem { Value = "", Text = "选择分类" });
            ViewData["CategoryList"] = categories;

            bool flag = false;
            string ReturnMsg = "";

            if (model.IsNew)
            {
                Article article = new Article();
                article.Id = model.Id;
                article.Title = model.Title;
                article.Description = model.Description;
                article.CategoryId = model.CategoryId;
                article.CreatedDate = DateTime.Now;
                article.Tags = model.Tags;
                article.Top = model.Top;
                article.IsRecommend = model.IsRecommend;
                article.CreateBy = CurrentUser.Id;
                DB.Articles.Add(article);
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "增加成功" : "增加失败";
            }
            else
            {
                var article = DB.Articles.FirstOrDefault(x => x.Id == model.Id);
                article.Title = model.Title;
                article.Description = model.Description;
                article.CategoryId = model.CategoryId;
                article.ModifyDate = DateTime.Now;
                article.Top = model.Top;
                article.IsRecommend = model.IsRecommend;
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "修改成功" : "修改失败";
            }
            if (flag)
            {
                return Redirect("/Admin/Article/Index");
            }
            else
            {
                ModelState.AddModelError("", ReturnMsg);
                return View(model);
            }
        }


        public IActionResult Show(Guid id)
        {
            Article article = DB.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            article.Description = CommonMark.CommonMarkConverter.Convert(article.Description);
            return View(new ArticleViewModel(article));
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
