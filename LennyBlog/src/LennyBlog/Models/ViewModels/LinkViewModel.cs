using LennyBlog.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models.ViewModels
{
    public class LinkViewModel : BaseViewModel
    {
        public Guid Id { set; get; }

        public string Email { set; get; }

        public string Site { set; get; }

        public string Url { set; get; }

        public LinkStatus Status { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { set; get; }

        /// <summary>
        ///  删除
        /// </summary>
        public bool IsDelete { set; get; }

        public int Top { set; get; }

        public LinkViewModel() { }

        public LinkViewModel(Link model)
        {
            this.Id = model.Id;
            this.Email = model.Email;
            this.CreatedDate = model.CreatedDate;
            this.IsDelete = model.IsDelete;
            this.Site = model.Site;
            this.Status = model.Status;
            this.Url = model.Url;
            this.Top = model.Top;
        }
    }
}
