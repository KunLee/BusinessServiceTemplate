using BusinessServiceTemplate.Shared.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessServiceTemplate.Shared.DataAccess
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext RepositoryContext { get; set; }
        public RepositoryBase(DbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public async Task<IQueryable<T>> FindAll(bool hasChangesTracked) =>
                hasChangesTracked ? await Task.Run(() => RepositoryContext.Set<T>()) : await Task.Run(() => RepositoryContext.Set<T>().AsNoTracking());
        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool hasChangesTracked) =>
                hasChangesTracked ? await Task.Run(() => RepositoryContext.Set<T>().Where(expression)) : await Task.Run(() => RepositoryContext.Set<T>().Where(expression).AsNoTracking());
        public async Task Create(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);
        public async Task Create(T[] entities) => await RepositoryContext.Set<T>().AddRangeAsync(entities);
        public async Task Update(T entity) => await Task.Run(() => RepositoryContext.Set<T>().Update(entity));
        public async Task Update(T[] entities) => await Task.Run(() => RepositoryContext.Set<T>().UpdateRange(entities));
        public async Task Delete(T entity) => await Task.Run(() => RepositoryContext.Set<T>().Remove(entity));
        public async Task Delete(T[] entities) => await Task.Run(() => RepositoryContext.Set<T>().RemoveRange(entities));
    }
}
