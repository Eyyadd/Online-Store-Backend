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

        public ProductElementDTO CreateProduct(CreateProductDTO createProductDTO , string ImagePath);

        public IEnumerable<ProductVariantDTO> AllProductsVariants();
        public ProductDetailsDTO ProductDetails(int id);

        public IEnumerable<ProductElementDTO> ProductsByCategoryId(int CategoryID);

        //public IQueryable<BestSeller> BestSellerProducts(int size);
        public IEnumerable<ProductElementDTO> BestSellerProducts(int size);

        public IEnumerable<ProductElementDTO> NewArraivelProducts(int size);

        public IEnumerable<ProductElementDTO> SaleProducts();

        public IEnumerable<string> ProuctsSaller();
       
    }
}
