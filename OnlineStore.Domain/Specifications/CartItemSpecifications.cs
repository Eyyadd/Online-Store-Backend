using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Specifications
{
    public class CartItemSpecifications:BaseSpecification<CartItems>
    {
        public CartItemSpecifications(Expression<Func<CartItems , bool>> Filter):base()
        {
            this.Includes.Add(c => c.ProductVariants);
            this.Filter = Filter;
        }
    }
}
