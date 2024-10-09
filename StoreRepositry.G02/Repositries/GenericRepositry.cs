using Microsoft.EntityFrameworkCore;
using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;
using StoreCore.G02.Specifications;
using StoreRepositry.G02.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRepositry.G02.Repositries
{
    public class GenericRepositry<TEntity, TKey> : IGenericRepositry<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepositry(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>) await _context.Products.OrderBy(p=>p.Name).Include(p => p.Brand).Include(p => p.Type).ToListAsync();
            }
           return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                //return await _context.Products.Include(p => p.Brand).Include(p => p.Type)
                //    .FirstOrDefaultAsync(p=>p.Id==id as int?)as TEntity;
                return await _context.Products.Where(p=>p.Id==id as int?).Include(p => p.Brand).Include(p => p.Type)
                   .FirstOrDefaultAsync() as TEntity;
            }
            return await  _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> spec)
        {
          return await ApplySpecfication(spec).ToListAsync();
        }

        public async Task<TEntity> GetWithSpecAsync(ISpecification<TEntity, TKey> spec)
        {
          return await ApplySpecfication(spec).FirstOrDefaultAsync();

        }
        private IQueryable<TEntity> ApplySpecfication(ISpecification<TEntity, TKey> spec)
        {
            return  SpecificationsEvalutor<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec);
        }

        public async Task<int> GetCountasync(ISpecification<TEntity, TKey> spec)
        {
            return await ApplySpecfication(spec).CountAsync();
        }
    }
}
