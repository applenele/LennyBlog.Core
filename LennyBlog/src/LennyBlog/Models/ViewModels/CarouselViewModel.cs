using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models.ViewModels
{
    public class CarouselViewModel:BaseViewModel
    {
     
        public CarouselViewModel() { }

        public CarouselViewModel(Carousel model)
        {
            this.Id = model.Id;
            this.Title = model.Title;
            this.Description = model.Description;
            this.Url = model.Url;
            this.CreatedDate = model.CreatedDate;
            this.IsShow = model.IsShow;
        }

        public Guid Id { set; get; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string ImageUrl { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { set; get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}
