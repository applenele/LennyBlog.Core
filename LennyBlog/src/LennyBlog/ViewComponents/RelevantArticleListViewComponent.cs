using LennyBlog.Extensions;
using LennyBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.ViewComponents
{
    /// <summary>
    /// 根据文章的id查找相关文章
    /// </summary>
    public class RelevantArticleListViewComponent : ViewComponent
    {
        private readonly BlogContext db;

        public RelevantArticleListViewComponent(BlogContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke(Guid id)
        {
            var items = new List<Article>();
            items = GetRelevantArticles(id);
            return View(items);
        }

        /// <summary>
        /// 获取相关文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<Article> GetRelevantArticles(Guid id)
        {
            List<Article> articles = new List<Article>();
            Article article = db.Articles.FirstOrDefault(x => x.Id == id);
            articles.AddRange(db.Articles.Where(x => x.CategoryId == article.CategoryId).ToList());
            if (article.Tags != null)
            {
                article.Tags.Split(',').ToList().ForEach(t =>
                {
                    articles.AddRange(db.Articles.Where(x => x.Tags.Contains(t)).ToList());
                });
            }
            return articles.DistinctBy(x => x.Id).Where(x=>x.Id!=id).ToList();
        }
    }
}
