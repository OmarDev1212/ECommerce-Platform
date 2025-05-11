using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    //1. Property Signature for every part of query => will be readonly , set will be in derived class [Base Specification]
    //like filteration [where] => that takes Func<T,bool>
    //List of Include Expressions to enable eager loading =>[Include] that takes func<T,Object> why object as i don't what type to return it is generic interface

    public interface ISpecification<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        Expression<Func<TEntity,bool>> Criteria { get; }
        List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }
    }
}
