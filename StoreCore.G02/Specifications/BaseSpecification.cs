using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Specifications
{
    public class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = default;//null
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = default;
        public Expression<Func<TEntity, object>> OrderByDescending { get; set; } = default;
        public int Skip {  get; set; }
        public int Take {  get; set; }
        public bool IsPaginationEnabled { get; set ; }

        public BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
            //Includes = new List<Expression<Func<TEntity, object>>>();
        }
        public BaseSpecification()
        {
            //Criteria = null;
            //Includes = new List<Expression<Func<TEntity, object>>>();        }
        }
        public void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }
        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDescending = expression;
        }
        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled= true;
            Skip = skip;
            Take = take;
        }
    }
}