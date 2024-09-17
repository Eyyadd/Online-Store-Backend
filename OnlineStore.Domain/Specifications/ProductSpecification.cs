using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Specifications
{
    public class ProductSpecification:BaseSpecification<Product>
    {
        public ProductSpecification():base()
        {
            Includes.Add(p => p.ProductVariants);
            Includes.Add(p => p.SubCategory);
        }
        public ProductSpecification(Expression<Func<Product , bool>> expression)
        {
            this.Filter = expression;
        }

        public ProductSpecification(Expression<Func<Product, bool>> expression , List<Expression<Func<Product , Object>>> Includes)
        {
            this.Filter = expression;
            this.Includes = Includes;
        }

        public ProductSpecification(List<Expression<Func<Product, Object>>> Includes)
        {
            this.Includes = Includes;
        }




    }
}
