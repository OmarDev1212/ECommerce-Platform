using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    //why this class?
    //this class will be used to create query
    public static class SpecificationEvalutor<TEntity, Key> where TEntity : BaseEntity<Key>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, Key> specification)
        {
            //query=_dbcontext.Products
            var query = inputQuery;
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);//query=_dbcontext.Products.where(specification.Criteria)
            }
            if (specification.IncludeExpressions is not null)
            {
                //foreach (var include in specification.IncludeExpressions)
                //{
                //    query = query.Include(include);
                //}
                query = specification.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));
                //query=_dbcontext.Products.where(specification.Criteria).Include(p=>ProductBrand)
                //query=_dbcontext.Products.where(specification.Criteria).Include(p=>ProductBrand).Include(p=>p.ProductType)

            }
            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);   //query=_dbcontext.Products.where(specification.Criteria).Include(p=>ProductBrand).Include(p=>p.ProductType).Orderby(p=>p.Name)
            else if (specification.OrderByDescinding is not null)
                query = query.OrderByDescending(specification.OrderByDescinding);//query=_dbcontext.Products.where(specification.Criteria).Include(p=>ProductBrand).Include(p=>p.ProductType).OrderByDescening(p=>p.Price)

            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);//query=_dbcontext.Products.where(specification.Criteria).Include(p=>ProductBrand).Include(p=>p.ProductType).OrderByDescening(p=>p.Price).Skip(5).Take(10)


            return query;//query=_dbcontext.Products.where(specification.Criteria).Include(p=>ProductBrand).Include(p=>p.ProductType).OrderByDescening(p=>p.Price).Skip(5).Take(10)
        }
    }

}

