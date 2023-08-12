using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Data
{
    public class SqlDataStore : IDataStore
    {
        public SqlDataStore()
        {
        }

        public Task<int> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
