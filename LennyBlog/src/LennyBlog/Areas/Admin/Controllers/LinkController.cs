using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models;
using SmallCode.Pager;
using LennyBlog.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LennyBlog.Areas.Admin.Controllers
{
    public class LinkController : BaseController
    {
        public IActionResult Index(string Title, int PageIndex = 1, int PageSize = 20)
        {
            IQueryable<Link> query = DB.Links.AsQueryable();


            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(x => x.Site.Contains(Title));
            }
            PagedList<Link> data = query.ToPagedList(PageIndex, PageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            LinkViewModel model = null;
            if (id.HasValue)
            {
                Link link = DB.Links.FirstOrDefault(x => x.Id == id);
                model = new LinkViewModel(link);
                model.IsNew = false;
            }
            else
            {
                model = new LinkViewModel();
                model.IsNew = true;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(LinkViewModel model)
        {

            bool flag = false;
            string ReturnMsg = "";
            if (model.IsNew)
            {
                Link link = new Link();
                link.Id = model.Id;
                link.Site = model.Site;
                link.Url = model.Url;
                link.Status = model.Status;
                link.CreatedDate = DateTime.Now;
                DB.Links.Add(link);
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "增加成功" : "增加失败";
            }
            else
            {
                var link = DB.Links.FirstOrDefault(x => x.Id == model.Id);
                link.Site = model.Site;
                link.Url = model.Url;
                link.Status = model.Status;
                flag = DB.SaveChanges() > 0;
                ReturnMsg = flag ? "修改成功" : "修改失败";
            }
            if (flag)
            {
                return Redirect("/Admin/Index/Index");
            }
            else
            {
                ModelState.AddModelError("", ReturnMsg);
                return View(model);
            }
        }
    }
}
