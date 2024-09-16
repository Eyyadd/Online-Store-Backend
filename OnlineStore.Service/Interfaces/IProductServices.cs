using OnlineStore.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IProductServices
    {
        public IEnumerable<ProductElementDTO> AllProducts();

        public IEnumerable<ProductVariants> AllProductsVariants();
        public ProductDetailsDTO ProductDetails(int id);

        public IEnumerable<Product> ProductsByCategoryId(int CategoryID);

        public IEnumerable<Product> BestSellerProducts();

        public IEnumerable<Product> TopRatedProducts();
        public IEnumerable<Product> NewArraivelProducts();

        public IEnumerable<Product> SaleProducts();
    }
}
