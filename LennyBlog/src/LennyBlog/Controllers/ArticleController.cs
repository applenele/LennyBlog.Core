using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using LennyBlog.Models;
using SmallCode.Pager;
using LennyBlog.Extensions;
using System.Linq.Expressions;

namespace LennyBlog.Controllers
{
    public class ArticleController : BaseController
    {


        public ActionResult Index(int pageIndex = 1, int pageSize = 20, string tag = "", string orderby = "")
        {
            Expression<Func<ArticleViewModel, bool>> exp = x => x.IsDelete == false;
            if (!string.IsNullOrEmpty(tag))
            {
                exp = exp.And(x => x.TagArr.Contains(tag));
            }
            IQueryable<ArticleViewModel> query =
              (from a in DB.Articles
               join c in DB.Categories on a.CategoryId equals c.Id
               join u in DB.Users on a.CreateBy equals u.Id
               orderby a.CreatedDate descending
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
               }).Where(exp).OrderBy(orderby);

            PagedList<ArticleViewModel> data = query.ToPagedList(pageIndex, pageSize);
            return View(data);
        }

        public IActionResult Show(Guid id)
        {
            var article = DB.Articles.Include(x => x.Category).Include(x => x.User).FirstOrDefault(x => x.Id == id);
            article.Browses++; ///访问增加1
            DB.SaveChanges();
            var _article = new ArticleViewModel(article);
            var lasArticle = DB.Articles.Where(x => x.CreatedDate < article.CreatedDate).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            var netxArticle = DB.Articles.Where(x => x.CreatedDate > article.CreatedDate).OrderBy(x => x.CreatedDate).FirstOrDefault();
            if (lasArticle != null)
            {
                _article.LastTitle = lasArticle.Title;
                _article.LastId = lasArticle.Id;
            }

            if (netxArticle != null)
            {
                _article.NextTitle = netxArticle.Title;
                _article.NextId = netxArticle.Id;
            }

            ViewBag.Replies = DB.Replies.Where(x => x.ArticleId == id).ToList();
            return View(_article);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Reply(Guid Id, string Email, string Description)
        {
            Reply reply = new Reply();
            reply.Email = Email;
            reply.CreatedDate = DateTime.Now;
            reply.Description = Description;
            reply.ArticleId = Id;
            DB.Replies.Add(reply);
            DB.SaveChanges();
            return Redirect("/Article/Show/" + Id);
        }
    }
}
