using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazzorPagesExample.Model;
using RazzorPagesExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazzorPagesExample.ViewComponents
{
    public class ProductModifyFormViewComponent : ViewComponent
    {
        private readonly RazzorPagesExampleContext _context;

        public ProductModifyFormViewComponent(RazzorPagesExampleContext context)
        {
            _context = context;

        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            
            var product = new Product();

            if (id != null)
            {
                product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            }
           
            return View(product);

        }
    }
}
