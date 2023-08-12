using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Data
{
    /// <summary>
    /// Provides an abstraction for a Sql database
    /// </summary>
    public interface ISqlDataStore : IDataStore
    {
        Task<TEntity> FindAsync<TEntity>(object[] keys, CancellationToken cancellationToken) where TEntity :class;

        Task<DbSqlQuery<TEntity>> QueryAsync<TEntity>(string query, object[] parameters, CancellationToken cancellationToken) where TEntity : class;

    }
}
