using OnlineStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Helper
{
    public static class Pagination
    {
        public async static Task<PaginationDTO<T>> Paginate<T>(this IQueryable<T> query , int size , int index)
        {
            int recordes = await query.CountAsync();
            if(recordes == 0)
            {
                return new PaginationDTO<T>
                {
                    Index = 0,
                    Size = 0,
                    Items = new List<T>(),
                    NoOfPages = 0,
                    Recordes = 0
                };
            }
            int PageSize = size > 0 ? size : 5;
            int PageIndex = index > 0 ? index : 1;
            
            int TotalNoOfPages =(int) Math.Ceiling((double)recordes / PageSize);
            PageIndex = PageIndex > TotalNoOfPages ? TotalNoOfPages : PageIndex;

            var SkipedItems = (PageIndex - 1) * size;

            var Items = query.Skip(SkipedItems).Take(PageSize);

            return new PaginationDTO<T>
            {
                Index = PageIndex,
                Size = PageSize,
                Items = Items,
                NoOfPages = TotalNoOfPages,
                Recordes = recordes
            };
        }
    }
}
