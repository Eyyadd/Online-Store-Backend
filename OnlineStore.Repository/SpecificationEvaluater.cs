using OnlineStore.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre
{
    internal static class SpecificationEvaluater<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> Set , ISpecifications<T> Spec)
        {
            var query = Set;
            if(Spec.Filter is not null)
               query = query.Where(Spec.Filter);


            if(Spec.Includes.Count > 0)
            {
                query = Spec.Includes
                    .Aggregate(query, (CurrentQuery, NextQuery) => CurrentQuery.Include(NextQuery));
            }

  
            return query;
        }
    }
}
