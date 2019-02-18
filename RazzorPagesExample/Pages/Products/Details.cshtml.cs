using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazzorPagesExample.Model;
using RazzorPagesExample.Models;

namespace RazzorPagesExample.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRazorRepository<Product> _repo;

        public DetailsModel(IProductRazorRepository<Product> repo)
        {
            _repo = repo;
            
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            
            
            Product = await _repo.SingleProduct((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
