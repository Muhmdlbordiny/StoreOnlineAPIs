using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.RepositriesContract
{
    public interface IUnitOfWork
    {
       Task<int> CompleteAsync();
        //create Repositry<T> and return
        IGenericRepositry<TEntity, Tkey> Repositry<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
