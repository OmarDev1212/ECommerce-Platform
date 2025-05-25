using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    //2.implement interface Ispecification 
    //set properties in the same class so properties will be private set
    //to implement get by id mechanism create ctor that takes filteration property
    //so derived class can set conditions to make filteration
    public class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification()
        {

        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExpression) => IncludeExpressions.Add(IncludeExpression);
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescinding { get; private set; }

        public int Skip { get; private set; }
        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        protected void AddOrderByDescinding(Expression<Func<TEntity, object>> orderByDesc)
        {
            OrderByDescinding = orderByDesc;
        }
        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

    }
}
