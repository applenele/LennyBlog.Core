using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LennyBlog.Models;
using LennyBlog.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using LennyBlog.Filters;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LennyBlog.Controllers
{
    public class AccountController : BaseController
    {


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (HttpContext.Session.GetString("Admin")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/Admin/Home/Index");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserName, string Password, string returnUrl)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("warning", "用户名或者密码不能为空");
            }
            else
            {
                User user = DB.Users.FirstOrDefault(x => x.UserName == UserName && x.Password == Password.ToMD5Hash());
                if (user == null)
                {
                    ModelState.AddModelError("warning", "用户名或者密码错误");
                }
                else
                {
                    HttpContext.Session.SetString("Admin", "true");
                    HttpContext.Session.SetString("AdminName", UserName);
                    if (string.IsNullOrEmpty(returnUrl))
                        return Redirect("/Home/Index");
                    else
                        return Redirect(returnUrl);
                }
            }
            return View();
        }

        [AdminRequired]
        [HttpGet]
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
