using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FuegoSoft.Pegasus.Lib.Core.Utilities.Interface;
using Microsoft.EntityFrameworkCore;

namespace FuegoSoft.Pegasus.Lib.Core.Utilities
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context) => Context = context;

        public void Add(TEntity entity) => Context.Set<TEntity>().Add(entity);

        public void AddRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().AddRange(entities);

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => Context.Set<TEntity>().Where(predicate);

        public TEntity Get(int Id) => Context.Set<TEntity>().Find(Id);

        public IEnumerable<TEntity> GetAll() => Context.Set<TEntity>().ToList();

        public void Remove(TEntity entity) => Context.Set<TEntity>().Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().RemoveRange(entities);

        public void Save() => Context.SaveChanges();

        public void Update(TEntity entity) => Context.Set<TEntity>().Attach(entity);
        public void UpdateRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().AttachRange(entities);

        public TEntity SaveAndRetrieve(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
            return entity;
        }
    }
}
