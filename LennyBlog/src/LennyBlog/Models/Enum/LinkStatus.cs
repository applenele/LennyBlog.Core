using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models.Enum
{
    public enum LinkStatus
    {
        [Display(Name = "申请中")]
        Applying,

        [Display(Name = "接受")]
        Pass,

        [Display(Name = "撤销")]
        Revoke
    }
}
