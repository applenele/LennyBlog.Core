using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models
{
    /// <summary>
    /// 回复
    /// </summary>
    public class Reply
    {
        [Key]
        public Guid Id { set; get; }

        public Guid ArticleId { set; get; }

        public string Email { set; get; }

        public string Description { set; get; }

        public DateTime CreatedDate { set; get; }

        public Guid? Father { set; get; }
    }
}
