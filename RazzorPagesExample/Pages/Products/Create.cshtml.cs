using Library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazzorPagesExample.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RazzorPagesExample.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly RazzorPagesExample.Models.RazzorPagesExampleContext _context;
        private readonly IHostingEnvironment _host;

        public CreateModel(RazzorPagesExample.Models.RazzorPagesExampleContext context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync(ICollection<IFormFile> files)
        {
            
            var fse = Request.Form["SecurityText"].ToString();
            var se = HttpContext.Session.GetString("SecurityText");
            var fa = files;

            if (fse != se)
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyProduct = new Product();

            if (await TryUpdateModelAsync<Product>(emptyProduct, "product",
                s => s.Name, s => s.ModelName, s => s.Price))
            {
                if (files.Count() > 0 )
                {
                    string strName = string.Empty;
                    string[] pathSplit;

                    foreach (var file in files)
                    {

                        if (file.Length > 0)
                        {

                            //IE에서는 파일경로까지 포함되어 이를 제거
                            pathSplit = file.FileName.Split('\\');
                            strName = pathSplit[pathSplit.Length - 1];



                            //string condir=_host.ContentRootPath + "\\uloads\\";  컨텐츠 경로

                            string dir = _host.WebRootPath + "\\uploads\\";


                            //string strName = Path.GetFileNameWithoutExtension(file.FileName);
                            // 확장자 : .txt
                            //string strExt = Path.GetExtension(file.FileName);

                            
                            strName = FileUtility.GetFileNameWithNumbering(dir, strName);

                            using (var fileStream = new FileStream(Path.Combine(dir, strName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);

                            }

                        }
                        
                    }

                    emptyProduct.AttachFile = strName;
                }


                _context.Product.Add(emptyProduct);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");


            }

           

            return Page();
        }
    }
}