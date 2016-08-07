using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models.Enum
{
    public enum YesOrNo
    {
        [Display(Name ="否")]
        No,

        [Display(Name = "是")]
        Yes
    }
}
