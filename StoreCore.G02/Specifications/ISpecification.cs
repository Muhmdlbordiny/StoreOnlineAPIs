using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Specifications
{
    public interface ISpecification<TEntity,Tkey> where TEntity :BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } //where
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }//Include
        public Expression<Func<TEntity, object>> OrderBy { get; set; } //orderby
        public Expression<Func<TEntity, object>> OrderByDescending { get; set; }//orderbydesc
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

    }
}
