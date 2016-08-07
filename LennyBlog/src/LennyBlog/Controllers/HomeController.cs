using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models.ViewModels;
using SmallCode.Pager;
using LennyBlog.Extensions;
using Microsoft.AspNetCore.Http;

namespace LennyBlog.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index(int pageIndex = 1, int pageSize = 20)
        {
        

            IQueryable<ArticleViewModel> query =
                from a in DB.Articles
                join c in DB.Categories on a.CategoryId equals c.Id
                orderby a.CreatedDate descending
                join u in DB.Users on a.CreateBy equals u.Id
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
                    TagArr = a.Tags.StringToStringArr(','),
                    CreateBy = u.Id,
                    CreateUserName = u.UserName,
                };

            return View(query.Take(10).ToList());
        }




    }
}
