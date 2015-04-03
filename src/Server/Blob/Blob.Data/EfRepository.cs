using Blob.Core;
using Blob.Core.Data;
using log4net;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Blob.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ILog _log;
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public EfRepository(IDbContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public T GetById(long id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            _log.Debug("Inserting " + entity);
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (DbEntityValidationResult validationErrors in dbEx.EntityValidationErrors)
                    foreach (DbValidationError validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                Exception fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Update(T entity)
        {
            _log.Debug("Updating " + entity);
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string msg = string.Empty;

                foreach (DbEntityValidationResult validationErrors in dbEx.EntityValidationErrors)
                    foreach (DbValidationError validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                Exception fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public void Delete(T entity)
        {
            _log.Debug("Deleting " + entity);
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string msg = string.Empty;

                foreach (DbEntityValidationResult validationErrors in dbEx.EntityValidationErrors)
                    foreach (DbValidationError validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                Exception fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}
