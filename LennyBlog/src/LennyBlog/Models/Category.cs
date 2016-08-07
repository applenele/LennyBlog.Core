using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Category
    {
        [Key]
        public Guid Id { set; get; }

        [MaxLength(100)]
        public string Name { set; get; }

        public DateTime CreatedDate { set; get; }

        public DateTime? ModifyDate { set; get; }

        public static implicit operator Guid(Category v)
        {
            throw new NotImplementedException();
        }
    }
}
