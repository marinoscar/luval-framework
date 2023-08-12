using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Luval.Framework.Data
{
    /// <summary>
    /// Provides extension methods for the <see cref="IDataStore"/>
    /// </summary>
    public static class DataStoreExtensions
    {

        /// <summary>
        /// Saves all changes made in this context to the target data store
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the underlying data store. This can include state entries for entities and/or relationships. Relationship state entries are created for many-to-many relationships and relationships where there is no foreign key property included in the entity class (often referred to as independent associations).</returns>
        public static Task<int> SaveChangesAsync(this IDataStore ds)
        {
            return ds.SaveChangesAsync(CancellationToken.None);
        }

        /// <summary>
        /// Saves all changes made in this context to the target data store
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the underlying data store. This can include state entries for entities and/or relationships. Relationship state entries are created for many-to-many relationships and relationships where there is no foreign key property included in the entity class (often referred to as independent associations).</returns>
        public static int SaveChanges(this IDataStore ds)
        {
            return ds.SaveChangesAsync().Result;
        }


        public static Task<TEntity> FindAsync<TEntity>(this ISqlDataStore ds, object[] keys)
        {
            return ds.FindAsync<TEntity>(keys, CancellationToken.None);
        }

        public static TEntity Find<TEntity>(this ISqlDataStore ds, object[] keys)
        {
            return ds.FindAsync<TEntity>(keys, CancellationToken.None).Result;
        }


        public static Task<int> AddAsync<TEntity>(this IDataStore ds, TEntity entity)
        {
            return ds.AddAsync(entity, CancellationToken.None);
        }

        public static Task<int> AddAsync<TEntity>(this IDataStore ds, IEnumerable<TEntity> entity)
        {
            return ds.AddAsync(entity, CancellationToken.None);
        }

        public static int Add<TEntity>(this IDataStore ds, TEntity entity)
        {
            return ds.AddAsync(entity, CancellationToken.None).Result;
        }

        public static int Add<TEntity>(this IDataStore ds, IEnumerable<TEntity> entity)
        {
            return ds.AddAsync(entity, CancellationToken.None).Result;
        }

        public static Task<int> UpdateAsync<TEntity>(this IDataStore ds, TEntity entity)
        {
            return ds.UpdateAsync(entity, CancellationToken.None);
        }

        public static Task<int> UpdateAsync<TEntity>(this IDataStore ds, IEnumerable<TEntity> entity)
        {
            return ds.UpdateAsync(entity, CancellationToken.None);
        }

        public static int Update<TEntity>(this IDataStore ds, TEntity entity)
        {
            return ds.UpdateAsync(entity, CancellationToken.None).Result;
        }

        public static int Update<TEntity>(this IDataStore ds, IEnumerable<TEntity> entity)
        {
            return ds.UpdateAsync(entity, CancellationToken.None).Result;
        }

        public static Task<int> DeleteAsync<TEntity>(this IDataStore ds, TEntity entity)
        {
            return ds.DeleteAsync(entity, CancellationToken.None);
        }

        public static Task<int> DeleteAsync<TEntity>(this IDataStore ds, IEnumerable<TEntity> entity)
        {
            return ds.DeleteAsync(entity, CancellationToken.None);
        }

        public static int Delete<TEntity>(this IDataStore ds, TEntity entity)
        {
            return ds.DeleteAsync(entity, CancellationToken.None).Result;
        }

        public static int Delete<TEntity>(this IDataStore ds, IEnumerable<TEntity> entity)
        {
            return ds.DeleteAsync(entity, CancellationToken.None).Result;
        }


        public static int AddOrUpdate<TEntity>(this ISqlDataStore ds, IEnumerable<TEntity> entities) where TEntity : class
        {
            return ds.AddOrUpdateAsync(entities, CancellationToken.None).Result;
        }

        public static Task<int> AddOrUpdateAsync<TEntity>(this ISqlDataStore ds, IEnumerable<TEntity> entities) where TEntity : class
        {
            return ds.AddOrUpdateAsync(entities, CancellationToken.None);
        }

        public static int AddOrUpdate<TEntity>(this ISqlDataStore ds, TEntity entity) where TEntity : class
        {
            return AddOrUpdateAsync(ds, entity, CancellationToken.None).Result;
        }

        public static Task<int> AddOrUpdateAsync<TEntity>(this ISqlDataStore ds, TEntity entity) where TEntity : class
        {
            return AddOrUpdateAsync(ds, entity, CancellationToken.None);
        }

        public static Task<int> AddOrUpdateAsync<TEntity>(this ISqlDataStore ds, TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            return AddOrUpdateAsync(ds, new[] { entity }, cancellationToken);
        }

        public static async  Task<int> AddOrUpdateAsync<TEntity>(this ISqlDataStore ds, IEnumerable<TEntity> entities, CancellationToken cancellationToken) where TEntity : class
        {
            var res = 0;
            foreach (var item in entities)
            {
                var keys = GetKeysFromEntity<TEntity>(item);
                if(keys != null && keys.Length > 0)
                {
                    var e = await ds.FindAsync<TEntity>(keys, cancellationToken);
                    if (e != null)
                        res += await ds.UpdateAsync<TEntity>(e, cancellationToken);
                    else
                        res += await ds.AddAsync<TEntity>(item, cancellationToken);
                }
                else
                    res += await ds.AddAsync<TEntity>(item, cancellationToken);
            }
            return res;
        }

        private static object[] GetKeysFromEntity<TEntity>(TEntity entity) where TEntity : class
        {
            var res = new List<object>();
            //First we check for attributes
            var type = entity.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var attrs = prop.DeclaringType?.GetCustomAttributes(true).Where(i => i.GetType() == typeof(KeyAttribute)).ToList();
                if (attrs.Count > 0)
                {
                    res.Add(prop.GetValue(entity));
                }
            }
            if(res.Count <= 0)
            {
                //Finds an id column
                var p = props.Where(i => i.Name.ToLowerInvariant() == "id").FirstOrDefault();
                if(p != null)
                {
                    res.Add(p.GetValue(entity));
                    return res.ToArray();
                }
                var k = props.Where(i => i.Name.ToLowerInvariant().EndsWith("id")).FirstOrDefault();
                if(k != null)
                    res.Add(k.GetValue(entity));
            }
            return res.ToArray();
        }

    }
}