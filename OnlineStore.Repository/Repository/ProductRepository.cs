using OnlineStore.Application;
using OnlineStore.Application.Repository;
using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class ProductRepository : Repository<ProductVariant>, IProductRepository
    {
        
        private readonly DbSet<ProductVariant> Products;

        public ProductRepository(OnlineStoreDbContext onlineStoreDbContext):base(onlineStoreDbContext)
        {
            Products = onlineStoreDbContext.Products;
        }

        public IQueryable<ProductVariant> All()
        {
            return Products.Include(p => p.ProductVariants)
                           .Include(p => p.SubCategory);
        }

        public IEnumerable<ProductVariant> BestSeller(int size)
        {
            var ProductVariants = Products.SelectMany(p => p.ProductVariants)
                                 .Include(p => p.Product)
                                 .ThenInclude(p => p.SubCategory)
                                 .GroupBy(p => p.ProductId);
                                                        
            var BestSellerProduct = new List<ProductVariants>();
            foreach (var Group in ProductVariants)
            {
                Group.OrderByDescending(p => p.PrecentageOfSales);
                BestSellerProduct.Add(Group.FirstOrDefault());
            };
            return BestSellerProduct.OrderByDescending(p => p.PrecentageOfSales)
                .Select(p => p.Product)
                .Take(size);
        }

        public IQueryable<ProductVariant> GetByCategoryID(int CategoryID)
        {
            return Products.Where(p => p.SubCategoryId == CategoryID)
                .Include(p => p.ProductVariants)
                .Include(p => p.SubCategory);
        }

        public IQueryable<ProductVariant> NewArrival(int size)
        {
           return Products.Where(p => p.ProductVariants.Any())
                .Include(p => p.ProductVariants)
                .Include(P => P.SubCategory)
                .OrderByDescending(p => p.Id)
                .Take(size);
        }

        //public Product ProductDetails(int ProductId)
        //{
        //    var Prod = Products.Where(p => p.Id == ProductId)
        //            .Include(p => p.ProductVariants)
        //            .Include(p => p.SubCategory)
        //            .FirstOrDefault();
        //    return Prod;
        //}

        public ProductVariant ProductDetails(int ProductId)
        {
            var Prod = Products.Where(p => p.Id == ProductId)
                    .Include(p => p.ProductVariants)
                    .Include(p => p.SubCategory)
                    .FirstOrDefault();
            return Prod;
        }



        public IQueryable<ProductVariant> ProductHaveSale()
        {
            return this.Products.Where(p => p.Discounted)
                .Include(p => p.ProductVariants)
                .Include(p => p.SubCategory);
        }

        public ProductVariants GetProductVariantByIdWithInclude(int ProductId)
        {
            var ProductV=_context.ProductVariants.Include(p=>p.Product).FirstOrDefault(p=>p.Id== ProductId);
            return ProductV;
        }
        public IEnumerable<ProductVariant> ProductByCategoryType(string inputCategoryType)
        {
            var categoryType = (CategoryType)Enum.Parse(typeof(CategoryType), inputCategoryType, true);

            return this.Products.Include(p => p.SubCategory)
                        .Where(p => p.SubCategory.CategoryType == categoryType)
                        .Include(p => p.ProductVariants);
        }


    }
}
