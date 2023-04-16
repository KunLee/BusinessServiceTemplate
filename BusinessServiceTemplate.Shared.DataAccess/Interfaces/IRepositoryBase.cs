using System.Linq.Expressions;

namespace BusinessServiceTemplate.Shared.DataAccess.Interfaces
{
    /// <summary>
    /// The base interface for all repository interfaces
    /// </summary>
    /// <typeparam name="T">The entity type for which the repository is created</typeparam>
    public interface IRepositoryBase<T>
    {
        Task<IQueryable<T>> FindAll(bool hasChangesTracked = true);
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool hasChangesTracked = true);
        Task Create(T entity);
        Task Create(T[] entity);
        Task Update(T entity);
        Task Update(T[] entities);
        Task Delete(T entity);
        Task Delete(T[] entities);
    }
}
