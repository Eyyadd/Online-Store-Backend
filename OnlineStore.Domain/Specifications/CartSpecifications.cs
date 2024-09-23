using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Specifications
{
    public class CartSpecifications : BaseSpecification<Cart>
    {
        public CartSpecifications(Expression<Func<Cart, bool>> filter)
        {
            this.Filter = filter;
        }
    }
}
