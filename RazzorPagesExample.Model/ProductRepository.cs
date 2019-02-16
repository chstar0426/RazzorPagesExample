using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace RazzorPagesExample.Model
{
    public class ProductRepository : IProductRepository
    {

        private readonly SqlConnection _db;
        
        public ProductRepository(string connection)
        {
            _db = new SqlConnection(connection);
        }

        public Product Detail(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> Read()
        {
            return _db.Query<Product>("Select * from Product order by Id desc").ToList();

        }

        public bool Edit(Product model)
        {
            throw new NotImplementedException();
        }

        public Product Add(Product model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> Search(string query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Ordering(OrderOption orderOption)
        {
            throw new NotImplementedException();
        }

        public List<Product> Paging(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
  

   
    }
}
