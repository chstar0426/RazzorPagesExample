using Microsoft.AspNetCore.Mvc;
using RazzorPagesExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazzorPagesExample.ViewComponents
{
    public class SearchingViewComponent : ViewComponent
    {
        private readonly RazzorPagesExampleContext _context;

        public SearchingViewComponent(RazzorPagesExampleContext context)
        {
            _context = context;

        }

        public  IViewComponentResult Invoke(bool complate)
        {
            return View(complate);
        }

    }
}
