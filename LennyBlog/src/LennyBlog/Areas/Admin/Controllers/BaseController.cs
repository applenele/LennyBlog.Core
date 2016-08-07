using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using LennyBlog.Models;
using Microsoft.Extensions.DependencyInjection;
using LennyBlog.Filters;
using Microsoft.AspNetCore.Http;

namespace LennyBlog.Areas.Admin.Controllers
{
    [AdminRequired]
    [Area("Admin")]
    // [Authorize]
    public class BaseController : Controller
    {

        public BlogContext DB { get { return HttpContext.RequestServices.GetService<BlogContext>(); } }

        public User CurrentUser { set; get; }

        public override void OnActionExecuting(ActionExecutingContext context)

        {
            string AdminName = context.HttpContext.Session.GetString("AdminName");
            CurrentUser = DB.Users.Where(x => x.UserName == AdminName).FirstOrDefault();
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
