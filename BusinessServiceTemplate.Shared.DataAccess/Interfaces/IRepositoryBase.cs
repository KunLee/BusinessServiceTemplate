using System.Linq.Expressions;

namespace BusinessServiceTemplate.Shared.DataAccess.Interfaces
{
    /// <summary>
    /// The base interface for all repository interfaces
    /// </summary>
    /// <typeparam name="T">The entity type for which the repository is created</typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Find one object via Id
        /// </summary>
        /// <param name="id">The specified Id</param>
        /// <returns></returns>
        Task<T> Find(object id);

        /// <summary>
        /// Find one object by its entity
        /// </summary>
        /// <param name="entity">The specified entity</param>
        /// <returns></returns>
        Task<T> Find(T entity);
        /// <summary>
        /// Return all of the entities
        /// </summary>
        /// <param name="hasChangesTracked">Specify if tracks the changes for returned objects</param>
        /// <returns></returns>
        Task<IQueryable<T>> FindAll(bool hasChangesTracked = true);

        /// <summary>
        /// Check for duplicate records
        /// </summary>
        /// <param name="expression">The input expression with conditions</param>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Find records by an expression input
        /// </summary>
        /// <param name="expression">The input expression with conditions</param>
        /// <param name="hasChangesTracked">Specify if tracks the changes for returned objects</param>
        /// <returns></returns>
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool hasChangesTracked = true);
        /// <summary>
        /// Create an entity to database
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns></returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Create a bunch of entities
        /// </summary>
        /// <param name="entity">The entity array</param>
        /// <returns></returns>
        Task Create(T[] entity);

        /// <summary>
        /// Full update on the specified entity object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Update(T entity);

        /// <summary>
        /// Mark the specified entity as modified, save the only changed properties
        /// </summary>
        /// <param name="entity"></param>
        void UpdateChanges(T entity);

        /// <summary>
        /// Update a group of entities
        /// </summary>
        /// <param name="entities">The array of the entities</param>
        /// <returns></returns>
        Task Update(T[] entities);

        /// <summary>
        /// Delete a specified entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns></returns>
        Task<T> Delete(T entity);

        /// <summary>
        /// Delete a group of entities
        /// </summary>
        /// <param name="entities">The array of entities</param>
        /// <returns></returns>
        Task Delete(T[] entities);
    }
}
