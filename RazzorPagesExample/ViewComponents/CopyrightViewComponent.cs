using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazzorPagesExample.ViewComponents
{
    
    public class CopyrightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var viewname = "Default";

            if (DateTime.Now.Second % 2 == 0)
            {
                viewname = "Details";

            }
            return View(viewname);
        }
    }
}
