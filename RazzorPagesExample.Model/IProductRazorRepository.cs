using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazzorPagesExample.Model
{
    public interface IProductRazorRepository <T> where T : class
   {

        IQueryable<Product> ProductList();

        Task<IList<Product>> PagingAsync(IQueryable<Product> product, int pageIndex, int pageSize);

        Task<Product> SingleProduct(int id);
    }

}
