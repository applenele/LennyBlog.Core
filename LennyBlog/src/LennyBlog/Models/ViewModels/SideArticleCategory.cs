using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models.ViewModels
{
    public class SideArticleCategory
    {
        public Guid Id { set; get; }

        public string Description { set; get; }

        public int Count { set; get; }
    }
}
