using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazzorPagesExample.Model;
using RazzorPagesExample.Models;

namespace RazzorPagesExample.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRazorRepository<Product> _repo;
        
        private readonly IHostingEnvironment _host;

        public IndexModel(IProductRazorRepository<Product> repo, IHostingEnvironment host)
        {
            _repo = repo;
            _host = host;
        }
        
        [BindProperty]
        public IList<Product> IProducts { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }

        public string strImg { get; set; }
        public string cookieValue { get; set; }


        #region 검색관련 변수
        public bool SearchMode { get; set; } = false;
        #endregion
        

        public async Task OnGetAsync()
        {
            int pageIndex = 0;
            int pageSize = 2;
            string SearchField = "Name";
            string SearchQuery = string.Empty;

            #region 검색모드확인
            if (
                    !String.IsNullOrEmpty(Request.Query["SearchQuery"])
                    &&
                    !String.IsNullOrEmpty(Request.Query["SearchField"]))
            {
                SearchMode = true;
                SearchField = SqlUtility.EncodeSqlString(Request.Query["SearchField"]); // 검색할 필드
                SearchQuery = SqlUtility.EncodeSqlString(Request.Query["SearchQuery"]); // 검색할 내용
            }
            #endregion

            IQueryable<Product> products = _repo.ProductList();

            if (SearchMode==true)
            {
                switch (SearchField)
                {
                    case "ModelName":
                        products = products.Where(p => p.ModelName.Contains(SearchQuery));
                        break;

                    default:
                        products = products.Where(p => p.Name.Contains(SearchQuery));
                        break;
                }
                
            }
           

            //[1] 쿼리스트링에 따른 페이지 보여주기
            if (!string.IsNullOrEmpty(Request.Query["Page"].ToString()))
            {
                // Page는 보여지는 쪽은 1, 2, 3, ... 코드단에서는 0, 1, 2, ...
                pageIndex = Convert.ToInt32(Request.Query["Page"]) - 1;
                Response.Cookies.Append("Page", pageIndex.ToString());
            }
            else
            {
                if (SearchMode == true)
                {
                    pageIndex = 0;
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request.Cookies["Page"]))
                    {
                        pageIndex = Convert.ToInt32(Request.Cookies["Page"]);
                    }

                }
               
                

            }

            //IProducts = await products.OrderByDescending(p => p.Id)
            //    .Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync();

            IProducts = await _repo.PagingAsync(products, pageIndex, pageSize);

            TotalCount = products.Count();

            PageIndex = pageIndex + 1;
           

            //Cookie를 테스트 하기 위해 작성한 것임...

            if (Request.Cookies["cookieValue"] != null)
            {
                cookieValue = Request.Cookies["cookieValue"].ToString();

            }
            else
            {
                cookieValue = $"쿠키값 없음";

            }
            //var random = new Random().Next().ToString();
            var random = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");


            Response.Cookies.Append("cookieValue", random);

        }


        

    }
}