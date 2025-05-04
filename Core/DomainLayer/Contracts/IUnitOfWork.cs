﻿using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, Key> GetRepository<TEntity, Key>() where TEntity : BaseEntity<Key>;
        Task<int> SaveChangesAsync();
    }
}
