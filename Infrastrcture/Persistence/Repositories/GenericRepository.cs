using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }


        public async Task<TEntity> GetById(Tkey id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
        public async Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity, Tkey> specification)
        {
            return await PerformQuery(specification).ToListAsync();
        }

        public async Task<TEntity> GetById(ISpecification<TEntity, Tkey> specification)
        {
            return await PerformQuery(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> PerformQuery(ISpecification<TEntity, Tkey> specification)
        {
            return  SpecificationEvalutor<TEntity, Tkey>.GetQuery(_dbContext.Set<TEntity>(), specification);
        }

    }
}
