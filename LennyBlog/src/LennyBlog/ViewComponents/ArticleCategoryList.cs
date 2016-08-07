using LennyBlog.Models;
using LennyBlog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.ViewComponents
{
    public class ArticleCategoryList : ViewComponent
    {
        private readonly BlogContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "articleCategory";

        public ArticleCategoryList(BlogContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<SideArticleCategory>();
            if (!_memoryCache.TryGetValue(cachekey, out items))
            {
                items = GetArticleCategoryList();
                _memoryCache.Set(cachekey, items, TimeSpan.FromMinutes(10));
            }
            return View(items);
        }

        /// <summary>
        /// 获取文章排行
        /// </summary>
        /// <param name="top"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private List<SideArticleCategory> GetArticleCategoryList()
        {
            return db.Articles.Include(x => x.Category).
                   GroupBy(x => x.Category).
                   Select(x => new SideArticleCategory { Id = x.Key.Id, Description = x.Key.Name, Count = x.Count() })
                   .ToList();
        }
    }
}
