using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using LennyBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LennyBlog.Controllers
{
    public class BaseController : Controller
    {
        public BlogContext DB { get { return HttpContext.RequestServices.GetService<BlogContext>(); } }

        private async Task<User> GetCurrentUserAsync()
        {
            return await DB.Users.FirstOrDefaultAsync(u => u.UserName.Equals(HttpContext.Session.GetString("AdminName")));
        }
    }
}
