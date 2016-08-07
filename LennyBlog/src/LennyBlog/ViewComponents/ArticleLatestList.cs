using LennyBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.ViewComponents
{
    public class ArticleLatestList: ViewComponent
    {
        private readonly BlogContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "articleLatest";

        public ArticleLatestList(BlogContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<Article>();
            if (!_memoryCache.TryGetValue(cachekey, out items))
            {
                items = GetLatestArticles(10);
                _memoryCache.Set(cachekey, items, TimeSpan.FromMinutes(10));
            }
            return View(items);
        }

       /// <summary>
       ///  获取最新的文章
       /// </summary>
       /// <param name="top"></param>
       /// <returns></returns>
        private List<Article> GetLatestArticles(int top)
        {
            return db.Articles.OrderByDescending(x=>x.CreatedDate).Take(top).ToList();
        }
    }
}
