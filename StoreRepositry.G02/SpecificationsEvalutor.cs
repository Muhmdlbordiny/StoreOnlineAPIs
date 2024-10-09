using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Entites;
using StoreCore.G02.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRepositry.G02
{
     public static class SpecificationsEvalutor<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        //create And Return Query
        public   static IQueryable<TEntity> GetQuery(IQueryable<TEntity>inputquery,ISpecification<TEntity,Tkey>spec)
        {
            var query = inputquery;
            if(spec.Criteria is not null)
            {
              query=  query.Where(spec.Criteria);
            }
            if (spec.OrderBy is not null) 
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending is not null) 
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPaginationEnabled)
            {
               query= query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, 
                (currentQuery, includeexpression) =>
                currentQuery.Include(includeexpression));
            return query;
        }
    }
}
