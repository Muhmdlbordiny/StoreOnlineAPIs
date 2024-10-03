using StoreCore.G02.Entites;
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
       Task<TEntity> GetAsync(TKey id);
       Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
