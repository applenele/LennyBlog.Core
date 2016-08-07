using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {

        public CategoryViewModel()
        { }

        public CategoryViewModel(Category model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.ModifyDate = model.ModifyDate;
            this.CreatedDate = model.CreatedDate;
        }

        public Guid Id { set; get; }

        public string Name { set; get; }

        public DateTime CreatedDate { set; get; }

        public DateTime? ModifyDate { set; get; }

    }
}
