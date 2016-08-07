using LennyBlog.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models
{
    /// <summary>
    /// 链接
    /// </summary>
    public class Link
    {
        [Key]
        public Guid Id { set; get; }

        public string Email { set; get; }

        public string Site { set; get; }

        public string Url { set; get; }

        public LinkStatus Status { set; get; }

        public int Top { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { set; get; }

        /// <summary>
        ///  删除
        /// </summary>
        public bool IsDelete { set; get; }
    }
}
