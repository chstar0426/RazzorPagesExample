using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazzorPagesExample.Model
{
    public class ProductRazorRepository : IProductRazorRepository<Product>
    {

        private readonly dbContext _context;

        public ProductRazorRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<IList<Product>> PagingAsync(IQueryable<Product> product, int pageIndex, int pageSize)
        {
            return await product.OrderByDescending(p => p.Id)
                 .Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync();
        }

        public IQueryable<Product> ProductList()
        {
            return from p in _context.Product select p;

        }
    }
}
