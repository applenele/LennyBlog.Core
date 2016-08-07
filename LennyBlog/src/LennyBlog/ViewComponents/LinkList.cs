using LennyBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.ViewComponents
{
    public class LinkList : ViewComponent
    {
        private readonly BlogContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "LinkList";

        public LinkList(BlogContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<Link>();
            if (!_memoryCache.TryGetValue(cachekey, out items))
            {
                items = GetLinkList(20);
                _memoryCache.Set(cachekey, items, TimeSpan.FromMinutes(10));
            }
            return View(items);
        }

        private List<Link> GetLinkList(int v)
        {
            return db.Links.Where(x => x.Status == Models.Enum.LinkStatus.Pass).OrderByDescending(x => x.Top).ToList();
        }
    }
}
