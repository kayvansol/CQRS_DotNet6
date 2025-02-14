using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList.Extensions;

namespace Store.Domain.Extensions
{
    public static class PaginationExtension
    {
        public class Pagination<T>
        {
            public int PageCount { get; set; }
            public int TotalCount { get; set; }
            public List<T> Data { get; set; }
        }

        public static Pagination<T> ToPagination<T>(this IQueryable<T> query, int startPage, int pageSize)
        {
            startPage = startPage == 0 ? 1 : startPage;

            if(pageSize == 0) pageSize = 10;

            var pageList = query.ToPagedList(startPage, pageSize);
            return new Pagination<T>
            {
                PageCount = pageList.PageCount,
                Data = pageList.ToList(),
                TotalCount = pageList.TotalItemCount
            };
        }
    }
}
