using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Filters
{
    public class AdminRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsAdmin(context))
                context.Result = new RedirectResult("/Account/Login?returnUrl=" + context.HttpContext.Request.Path);
            base.OnActionExecuting(context);
        }

        private static bool IsAdmin(ActionExecutingContext context)
        {
            return context.HttpContext.Session.GetString("Admin")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}
