using StoreCore.G02.Entites;
using StoreCore.G02.Specifications;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.RepositriesContract
{
    public interface IGenericRepositry<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity,TKey> spec);

        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetWithSpecAsync(ISpecification<TEntity, TKey> spec);
       Task AddAsync(TEntity entity);
        Task<int> GetCountasync(ISpecification<TEntity, TKey> spec);
        void Update(TEntity entity);
        void Delete(TEntity entity);


    }
}
