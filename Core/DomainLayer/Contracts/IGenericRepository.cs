using DomainLayer.Models;
using DomainLayer.Models.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    //refactor repositries to enable specification how ?
    //by making new methods that takes Ispecifaction as i develop against interface and i don't which specification will be passed
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(ISpecification<TEntity,Tkey> specification);
        Task<TEntity> GetById(Tkey id);
        Task<TEntity> GetById(ISpecification<TEntity,Tkey> specification);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
