using LennyBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.ViewComponents
{
    public class ArticleRecommendList : ViewComponent
    {
        private readonly BlogContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "articleRecommend";

        public ArticleRecommendList(BlogContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<Article>();
            if (!_memoryCache.TryGetValue(cachekey, out items))
            {
                items = GetRecommendArticles(10);
                _memoryCache.Set(cachekey, items, TimeSpan.FromMinutes(10));
            }
            return View(items);
        }

        /// <summary>
        ///  获取推荐的文章
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        private List<Article> GetRecommendArticles(int top)
        {
            return db.Articles.Where(x => x.IsRecommend == Models.Enum.YesOrNo.Yes).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
    }
}
