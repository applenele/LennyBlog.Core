using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models;
using SmallCode.Pager;
using LennyBlog.Models.ViewModels;
using LennyBlog.Models.DataModels;

namespace LennyBlog.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<Category> data = DB.Categories.AsQueryable().ToPagedList(PageIndex, PageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            CategoryViewModel model = null;
            if (Id.HasValue)
            {
                Category category = DB.Categories.FirstOrDefault(x => x.Id == Id);
                model = new CategoryViewModel(category);
                model.IsNew = false;
            }
            else
            {
                model = new CategoryViewModel();
                model.IsNew = true;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            bool flag = false;
            string ReturnMsg = "";

            if (model.IsNew)
            {
                Category category = new Category();
                category.Name = model.Name;
                category.CreatedDate = DateTime.Now;
                DB.Categories.Add(category);
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "增加成功" : "增加失败";
            }
            else
            {
                var category = DB.Categories.FirstOrDefault(x => x.Id == model.Id);
                category.Name = model.Name;
                category.ModifyDate = DateTime.Now;
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "修改成功" : "修改失败";
            }

            if (flag)
            {
                return Redirect("/Admin/Category/Index");
            }
            else
            {
                ModelState.AddModelError("", ReturnMsg);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var category = DB.Categories.FirstOrDefault(x => x.Id == id);
            DB.Categories.Remove(category);
            bool flag = DB.SaveChanges() > 0;
            ReturnResult model = new ReturnResult();
            model.Status = flag ? "ok" : "error";
            model.ReturnMsg = flag ? "删除成功" : "删除失败";
            return Json(model);
        }

    }
}
