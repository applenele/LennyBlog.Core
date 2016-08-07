using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using LennyBlog.Helpers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LennyBlog.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public IActionResult ImageUpload(IFormFile file, [FromServices]IHostingEnvironment env)
        {
            string root = @"uploadimages/article/";
            string ext = Path.GetExtension(file.FileName.Replace("\"", ""));
            var originalName = DateHelper.GetTimeStamp() + ext;
            var fileName = Path.Combine(root, originalName);
            string phyPath = Path.Combine(env.WebRootPath, fileName);

            if (!Directory.Exists(Path.Combine(env.WebRootPath, root)))
            {
                Directory.CreateDirectory(Path.Combine(env.WebRootPath, root));
            }
            if (file != null)
            {
                using (var stream = new FileStream(phyPath, FileMode.CreateNew))
                {
                    file.CopyTo(stream);
                }
                return Content("/" + root + originalName);
            }
            else
            {
                ModelState.AddModelError("", "没有选择文件");
                return Content("error");
            }
        }
    }
}
