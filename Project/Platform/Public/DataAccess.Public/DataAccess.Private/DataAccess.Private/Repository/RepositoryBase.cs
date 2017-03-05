using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataAccess.Public.Repository;

namespace DataAccess.Private.Repository
{
    public class RepositoryBase<TEntity, TKey>: IRepository<TEntity, TKey> where TEntity: class
    {
        protected DbContext Context;

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }
        public TEntity Get(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }


        public void Update(TKey id, Expression<Func<TEntity>> entityExpression)
        {
            var exp = (MemberInitExpression) entityExpression.Body;
            var obj = entityExpression.Compile()();

            Context.Set<TEntity>().Attach(obj);
            var entry = Context.Entry(obj);
            foreach (var binding in exp.Bindings)
            {
                var assignment = (MemberAssignment) binding;
                var property = (PropertyInfo) assignment.Member;
                entry.Property(property.Name).IsModified = true;
            }
        }
    }
}
