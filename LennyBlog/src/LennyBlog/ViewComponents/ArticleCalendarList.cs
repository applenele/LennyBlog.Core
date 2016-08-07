using LennyBlog.Models;
using LennyBlog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.ViewComponents
{
    public class ArticleCalendarList : ViewComponent
    {
        private readonly BlogContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "articleCalendar";

        public ArticleCalendarList(BlogContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<SideArticleCalendar>();
            if (!_memoryCache.TryGetValue(cachekey, out items))
            {
                items = GetArticleCalendarList();
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
        private List<SideArticleCalendar> GetArticleCalendarList()
        {
            return db.Articles.Select(a => a.CreatedDate)
                  .GroupBy(a => a.GetDateTimeFormats('y')[0])
                  .Select(g => (new SideArticleCalendar() { DateDisplay = g.Key, Count = g.Count() }))
                  .ToList();
        }
    }
}
