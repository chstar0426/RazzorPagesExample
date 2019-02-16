using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazzorPagesExample.Models;

namespace RazzorPagesExample.Pages.Products
{
    public class DownloadModel : PageModel
    {
        private readonly RazzorPagesExampleContext _context;
        private readonly IHostingEnvironment _host;

        public DownloadModel(RazzorPagesExampleContext context, IHostingEnvironment host)
        {
            _context = context;

            _host = host;
        }

        public async Task<IActionResult> OnGetAsync(int? Id)
        {
            if (Id==null)
            {
                return NotFound();

            }

            var product = await _context.Product.FindAsync(Id);

            if (product == null)
            {
                return NotFound();
            }

            string filename = product.AttachFile;

            if (string.IsNullOrEmpty(filename))
                return Content("filename not present");

            string dir = _host.WebRootPath + "\\uploads\\";

            var path = Path.Combine(dir, filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            string type = "application/octet-stream"; // GetContentType(path);
            return File(memory, type, Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}