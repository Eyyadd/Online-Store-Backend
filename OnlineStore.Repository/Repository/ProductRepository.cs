using OnlineStore.Application;
using OnlineStore.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        
        private readonly DbSet<Product> Products;

        public ProductRepository(OnlineStoreDbContext onlineStoreDbContext):base(onlineStoreDbContext)
        {
            Products = onlineStoreDbContext.Products;
        }

        public IQueryable<Product> All()
        {
            return Products.Include(p => p.ProductVariants)
                           .Include(p => p.SubCategory);
        }

        public IEnumerable<Product> BestSeller(int size)
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

        public IQueryable<Product> GetByCategoryID(int CategoryID)
        {
            return Products.Where(p => p.SubCategoryId == CategoryID)
                .Include(p => p.ProductVariants)
                .Include(p => p.SubCategory);
        }

        public IQueryable<Product> NewArrival(int size)
        {
           return Products.Where(p => p.ProductVariants.Any())
                .Include(p => p.ProductVariants)
                .Include(P => P.SubCategory)
                .OrderByDescending(p => p.Id)
                .Take(size);
        }

        public Product ProductDetails(int ProductId)
        {
            var Prod = Products.Where(p => p.Id == ProductId)
                    .Include(p => p.ProductVariants)
                    .Include(p => p.SubCategory)
                    .FirstOrDefault();
            return Prod;
        }

        public IQueryable<Product> ProductHaveSale()
        {
            return this.Products.Where(p => p.Discounted)
                .Include(p => p.ProductVariants)
                .Include(p => p.SubCategory);
        }
    }
}
