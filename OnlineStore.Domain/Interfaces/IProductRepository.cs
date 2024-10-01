using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<ProductVariant> All();
        IQueryable<ProductVariant> NewArrival(int size);
        IQueryable<ProductVariant> ProductHaveSale();
        IQueryable<ProductVariant> GetByCategoryID(int CategoryID);
        IEnumerable<ProductVariant> BestSeller(int size);
        ProductVariant ProductDetails(int ProductId);
        public ProductVariants GetProductVariantByIdWithInclude(int ProductId);
        IEnumerable<ProductVariant> ProductByCategoryType(string categoryType);

    }
}
