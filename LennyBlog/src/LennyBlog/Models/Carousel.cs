using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models
{
    /// <summary>
    /// 轮播
    /// </summary>
    public class Carousel
    {
        public Guid Id { set; get; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { set; get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        public static implicit operator Guid(Carousel v)
        {
            throw new NotImplementedException();
        }
    }
}
