using Blob.Core.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blob.Data
{
    public class EfRepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private DbSet<T> _entities;

        public EfRepositoryAsync(BlobDbContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                IQueryable<T> dbQuery = Entities;

                // include the specified navigation properties
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                list = await dbQuery.AsNoTracking().ToListAsync();

                stopwatch.Stop();
                _log.Debug(string.Format("GetAllAsync: Timespan:{0}", stopwatch.Elapsed));
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error in GetAllAsync"), e);
                throw;
            }
            return list;
        }

        public async Task<IList<T>> GetListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                IQueryable<T> dbQuery = Entities;

                // include the specified navigation properties
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                list = await dbQuery.AsNoTracking().Where(predicate).ToListAsync();

                stopwatch.Stop();
                _log.Debug(string.Format("GetListAsync: Timespan:{0}, predicate:{1}", stopwatch.Elapsed, predicate));
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error in GetListAsync(predicate={0}", predicate), e);
                throw;
            }
            return list;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item;
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                IQueryable<T> dbQuery = Entities;

                // include the specified navigation properties
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                item = await dbQuery.AsNoTracking().FirstOrDefaultAsync(predicate);

                stopwatch.Stop();
                _log.Debug(string.Format("GetListAsync: Timespan:{0}, predicate:{1}", stopwatch.Elapsed, predicate));
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error in GetListAsync(predicate={0}", predicate), e);
                throw;
            }
            return item;
        }

        public async Task InsertAsync(T entity)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                Entities.Add(entity);
                await _context.SaveChangesAsync();

                stopwatch.Stop();
                _log.Debug(string.Format("InsertAsync: Timespan:{0}, entity:{1}", stopwatch.Elapsed, entity));
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error in InsertAsync(entity={0}", entity), e);
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                var entry = _context.Entry(entity);
                Entities.Attach(entity);
                entry.State = EntityState.Modified;
                await _context.SaveChangesAsync();

                stopwatch.Stop();
                _log.Debug(string.Format("UpdateAsync: Timespan:{0}, entity:{1}", stopwatch.Elapsed, entity));
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error in UpdateAsync(entity={0}", entity), e);
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                T item = await Entities.FindAsync(entity);
                Entities.Remove(item);
                await _context.SaveChangesAsync();

                stopwatch.Stop();
                _log.Debug(string.Format("DeleteAsync: Timespan:{0}, entity:{1}", stopwatch.Elapsed, entity));

            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error in DeleteAsync(entity={0}", entity), e);
                throw;
            }
        }

        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}
