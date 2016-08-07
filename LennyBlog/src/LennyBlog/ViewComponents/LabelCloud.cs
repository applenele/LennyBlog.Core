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
    public class LabelCloud : ViewComponent
    {
        private readonly BlogContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "LabelCloud";

        public LabelCloud(BlogContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<string>();
            if (!_memoryCache.TryGetValue(cachekey, out items))
            {
                items = GetLabelCloud(50);
                _memoryCache.Set(cachekey, items, TimeSpan.FromMinutes(10));
            }
            return View(items);
        }

        /// <summary>
        ///  获取推荐的文章
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        private List<string> GetLabelCloud(int top)
        {
            List<string> labels = new List<string>();
            var tags = db.Articles.Select(x => x.Tags).ToList();
            foreach (var item in tags)
            {
                if (!string.IsNullOrEmpty(item))
                    labels.AddRange(item.Split(',').ToList());
            }
            return labels.Distinct().Take(top).ToList();
        }
    }
}
