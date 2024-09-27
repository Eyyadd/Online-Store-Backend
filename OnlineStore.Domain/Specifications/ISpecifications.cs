using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T , bool>> Filter { get; set; }

        public List<Expression<Func<T , object>>> Includes { get; set; }     

        public  Expression<Func<T, object>> GroupByClause { get; set; }

        public  Func<IQueryable<T>, IOrderedQueryable<T>> OrderByClause { get; set; }



    }
}
