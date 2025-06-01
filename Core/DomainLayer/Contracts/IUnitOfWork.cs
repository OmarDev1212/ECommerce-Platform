using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, Key> GetRepository<TEntity, Key>() where TEntity : BaseEntity<Key>;
        Task<int> SaveChangesAsync();
    }
}
