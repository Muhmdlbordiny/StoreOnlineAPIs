using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;
using StoreRepositry.G02.Data.Contexts;
using StoreRepositry.G02.Repositries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRepositry.G02
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositries;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repositries = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        =>
            await _context.SaveChangesAsync();
        

        public IGenericRepositry<TEntity, Tkey> Repositry<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var type = typeof(TEntity);
            _repositries.ContainsKey(type);
            if (!_repositries.ContainsKey(type))
            {
                var repositry = new GenericRepositry<TEntity, Tkey>(_context); 
                _repositries.Add(type, repositry);
            }
            return _repositries[type] as IGenericRepositry<TEntity, Tkey>;
        }
    }
}
