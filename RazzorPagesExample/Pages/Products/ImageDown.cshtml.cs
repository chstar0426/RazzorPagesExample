using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazzorPagesExample.Pages.Products
{
    public class ImageDownModel : PageModel
    {
        private readonly IHostingEnvironment _host;

        public ImageDownModel(IHostingEnvironment host)
        {
            _host = host;

        }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Request.Query["FileName"]))
                return Content("filename not present");

            string fileName = Request.Query["FileName"].ToString();
            string ext = Path.GetExtension(fileName);
            string contentType = string.Empty;

            if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
            {
                switch (ext)
                {
                    case ".gif":
                        contentType = "image/gif"; break;
                    case ".jpg":
                        contentType = "image/jpeg"; break;
                    case ".jpeg":
                        contentType = "image/jpeg"; break;
                    case ".png":
                        contentType = "image/png"; break;
                }

            }
            else
            {
                return Content("이미지 파일이 아닙니다.");

            }

            string dir = _host.WebRootPath + "\\uploads\\";
            string file = Path.Combine(dir, fileName);

            return File(System.IO.File.OpenRead(file),contentType );

        }

    }
}