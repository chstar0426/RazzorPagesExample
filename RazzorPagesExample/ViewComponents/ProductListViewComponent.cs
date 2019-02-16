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
    public class ProductListViewComponent : ViewComponent
    {
        private readonly RazzorPagesExampleContext _context;


        public ProductListViewComponent(RazzorPagesExampleContext context)
        {
            _context = context;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lst = await _context.Product.ToListAsync();
            return View(lst);

        }

    }
}
