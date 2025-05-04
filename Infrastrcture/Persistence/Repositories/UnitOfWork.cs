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
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, Key> GetRepository<TEntity, Key>() where TEntity : BaseEntity<Key>
        {

            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity, Key>)_repositories[type];
            }
            else
            {
                var repository = new GenericRepository<TEntity, Key>(_dbContext);
                _repositories.Add(type, repository);
                return repository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
