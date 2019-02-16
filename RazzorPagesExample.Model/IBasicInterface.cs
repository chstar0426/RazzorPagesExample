using System;
using System.Collections.Generic;
using System.Text;

namespace RazzorPagesExample.Model
{
    public interface IBasicInterface <T> where T : class 
    {
       
        T Detail(int id);

       
        List<T> Read();

        bool Edit(T model);

        
        T Add(T model);

        
        bool Delete(int id);

       
        List<T> Search(string query);

        IEnumerable<T> Ordering(OrderOption orderOption);

        List<T> Paging(int pageNumber, int pageSize);
    }
}
