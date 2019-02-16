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
    public class IndexProductModel : PageModel
    {

        private readonly IProductRepository _repository;


        public IndexProductModel(IProductRepository repository)
        {
            _repository = repository;

        }
        
        public List<Product> Product { get;set; }

        public void OnGet()
        {
            Product = _repository.Read();
            

        }

      
    }
}
