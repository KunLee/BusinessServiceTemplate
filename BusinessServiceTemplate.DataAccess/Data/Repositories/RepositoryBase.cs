﻿using BusinessServiceTemplate.Shared.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessServiceTemplate.Shared.DataAccess
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext _repositoryContext;
        protected DbSet<T> _dbSet;
        public RepositoryBase(DbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _dbSet = repositoryContext.Set<T>();
        }

        public async Task<T> Find(T entity)
        {
            return await Task.Run(() => _dbSet.Find(entity));
        }
        public async Task<T> Find(object id)
        {
            return await Task.Run(() => _dbSet.Find(id));
        }
        public async Task<IQueryable<T>> FindAll(bool hasChangesTracked) =>
                hasChangesTracked ? await Task.Run(() => _dbSet) : await Task.Run(() => _dbSet.AsNoTracking());
        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool hasChangesTracked) =>
                hasChangesTracked ? await Task.Run(() => _dbSet.Where(expression)) : await Task.Run(() => _dbSet.Where(expression).AsNoTracking());
        public async Task<T> Create(T entity)
        {
            var entityAdded = await _dbSet.AddAsync(entity);
            return entityAdded.Entity;
        }
        public async Task Create(T[] entities) => await _dbSet.AddRangeAsync(entities);
        public async Task<T> Update(T entity)
        {
            var entityUpdated = await Task.Run(() => _dbSet.Update(entity));
            return entityUpdated.Entity;
        }
        public void UpdateChanges(T entity)
        {
            _repositoryContext.Entry(entity).State = EntityState.Modified;
        }
        public async Task Update(T[] entities) => await Task.Run(() => _dbSet.UpdateRange(entities));
        public async Task<T> Delete(T entity) { 
            var entityDeleted = await Task.Run(() => _dbSet.Remove(entity)); 
            return entityDeleted.Entity;
        }
        public async Task Delete(T[] entities) => await Task.Run(() => _dbSet.RemoveRange(entities));
    }
}
