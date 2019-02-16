using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazzorPagesExample.Model;
using RazzorPagesExample.Models;

namespace RazzorPagesExample.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly RazzorPagesExample.Models.RazzorPagesExampleContext _context;

        public EditModel(RazzorPagesExample.Models.RazzorPagesExampleContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
       

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null)
            {
                return NotFound();

            }

            Product = await _context.Product.FindAsync(id);
            //pId = RouteData.Values["Id"];
            //pId = Request.Query["id"];

            if (Product == null)
            {
                return NotFound();

            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            var fse = Request.Form["SecurityText"].ToString();
            var se = HttpContext.Session.GetString("SecurityText");
            if (fse != se)
            {
                return Page();
            }


            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productUpdate = await _context.Product.FindAsync(id);

            if (await TryUpdateModelAsync<Product>(productUpdate, "product", 
                p=>p.Name, p=>p.ModelName, p=>p.Price))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            return Page();

        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
