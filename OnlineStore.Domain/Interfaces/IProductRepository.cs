using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> All();
        IQueryable<Product> NewArrival(int size);
        IQueryable<Product> ProductHaveSale();
        IQueryable<Product> GetByCategoryID(int CategoryID);
        IEnumerable<Product> BestSeller(int size);
        Product ProductDetails(int ProductId);
    }
}
