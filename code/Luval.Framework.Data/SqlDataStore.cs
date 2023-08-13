using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Data
{
    public class SqlDataStore : ISqlDataStore
    {
        public SqlDataStore(DbContext context)
        {
            Context = context;
            _properties = Context.GetType().GetProperties();
            _sets = new Dictionary<Type, object>();
        }

        private DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            var qType = typeof(DbSet<TEntity>);
            if (_sets.ContainsKey(qType)) return (DbSet<TEntity>)_sets[qType];

            var p = _properties.FirstOrDefault(i => i.DeclaringType == qType);
            if (p != null)
            {
                _sets[qType] = p.GetValue(Context);
                return GetSet<TEntity>();
            }
            throw new ArgumentException("Entity not found");
        }

        private PropertyInfo[] _properties;
        private Dictionary<Type, object> _sets;
        public DbContext Context { get; private set; }


        #region Interface Implementation
        public virtual Task<TEntity> FindAsync<TEntity>(object[] keys, CancellationToken cancellationToken) where TEntity : class
        {
            return GetSet<TEntity>().FindAsync(keys, cancellationToken);
        }

        public virtual Task<DbSqlQuery<TEntity>> QueryAsync<TEntity>(string query, object[] parameters, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.Run(() =>
            {
                return GetSet<TEntity>().SqlQuery(query, parameters);
            }, cancellationToken);
        }

        public virtual Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.Run(() =>
            {
                return GetSet<TEntity>().Add(entity);
            }, cancellationToken);
        }

        public virtual Task<IEnumerable<TEntity>> AddAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.Run(() =>
            {
                return GetSet<TEntity>().AddRange(entity);
            }, cancellationToken);
        }

        public virtual Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.Run(() =>
            {
                var ds = GetSet<TEntity>();
                if(!ds.Local.Any(i => i == entity))
                {
                    ds.Attach(entity);
                }
                Context.Entry(entity).State = EntityState.Modified;
                return entity;
            }, cancellationToken);
        }

        public virtual Task<IEnumerable<TEntity>> UpdateAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.Run(() => { 
                var res = new List<TEntity>();
                foreach(TEntity e in entity)
                {
                    res.Add(UpdateAsync<TEntity>(e, cancellationToken).Result);
                }
                return (IEnumerable<TEntity>)res;
            }, cancellationToken);
        }

        public virtual Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.Run(() =>
            {
                return GetSet<TEntity>().Remove(entity);
            }, cancellationToken);
        }

        public virtual Task<IEnumerable<TEntity>> DeleteAsync<TEntity>(IEnumerable<TEntity> entity, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.Run(() =>
            {
                return GetSet<TEntity>().RemoveRange(entity);
            }, cancellationToken);
        }

        public virtual  Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
        #endregion




    }
}
