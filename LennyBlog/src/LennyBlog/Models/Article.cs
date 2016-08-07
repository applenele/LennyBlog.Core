using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LennyBlog.Models.Enum;

namespace LennyBlog.Models
{
    public class Article
    {
        [Key]
        public Guid Id { set; get; }

        [MaxLength(100)]
        public string Title { set; get; }

        public string Description { set; get; }

        public string Tags { set; get; }

        [ForeignKey("Category")]
        public Guid CategoryId { set; get; }

        public virtual Category Category { set; get; }


        public DateTime CreatedDate { set; get; }

        public DateTime? ModifyDate { set; get; }

        /// <summary>
        /// 创建者
        /// </summary>
        [ForeignKey("User")]
        public Guid CreateBy { set; get; }

        public virtual User User { set; get; }

        /// <summary>
        /// 阅读数
        /// </summary>
        public int Browses { set; get; }

        public bool IsDelete { set; get; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public YesOrNo IsRecommend { set; get; }

        /// <summary>
        /// 置顶顺序
        /// </summary>
        public int Top { set; get; }
    }
}
