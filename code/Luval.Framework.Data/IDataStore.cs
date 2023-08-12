using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Data
{
    /// <summary>
    /// Provides an abstration for a Data Store
    /// </summary>
    public interface IDataStore
    {


        /// <summary>
        /// Adds the given entity to the context underlying the set in the Added state such that it will be inserted into the data store when SaveChanges is called.
        /// </summary>
        /// <typeparam name="TEntity">The data entity</typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> with the number of affected recordds</returns>
        Task<int> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the given entities to the context underlying the set in the Added state such that it will be inserted into the data store when SaveChanges is called.
        /// </summary>
        /// <typeparam name="TEntity">The data entity</typeparam>
        /// <param name="entity">The entities</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> with the number of affected recordds</returns>
        Task<int> AddAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the given entity to the context underlying the set in the Updated state such that it will be inserted into the data store when SaveChanges is called.
        /// </summary>
        /// <typeparam name="TEntity">The data entity</typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> with the number of affected recordds</returns>
        Task<int> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the given entity to the context underlying the set in the Updated state such that it will be inserted into the data store when SaveChanges is called.
        /// </summary>
        /// <typeparam name="TEntity">The data entity</typeparam>
        /// <param name="entity">The entities</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> with the number of affected recordds</returns>
        Task<int> UpdateAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken);


        /// <summary>
        /// Marks the given entity as Deleted such that it will be deleted from the database when SaveChanges is called. Note that the entity must exist in the context in some other state before this method is called.
        /// </summary>
        /// <typeparam name="TEntity">The data entity</typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> with the number of affected recordds</returns>
        Task<int> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Marks the given entities as Deleted such that it will be deleted from the database when SaveChanges is called. Note that the entity must exist in the context in some other state before this method is called.
        /// </summary>
        /// <typeparam name="TEntity">The data entity</typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> with the number of affected recordds</returns>
        Task<int> DeleteAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken);

        /// <summary>
        /// Saves all changes made in this context to the target data store
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the underlying data store. This can include state entries for entities and/or relationships. Relationship state entries are created for many-to-many relationships and relationships where there is no foreign key property included in the entity class (often referred to as independent associations).</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
