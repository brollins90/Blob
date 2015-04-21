using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using log4net;

namespace Blob.Data
{
    internal class GenericEntityStore<TEntity> where TEntity : class
    {
        private readonly ILog _log;

        public GenericEntityStore(DbContext context)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing GenericEntityStore for " + typeof(TEntity));
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        public DbContext Context { get; private set; }

        public IQueryable<TEntity> EntitySet
        {
            get { return DbEntitySet; }
        }

        public DbSet<TEntity> DbEntitySet { get; private set; }


        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return DbEntitySet.FindAsync(id);
        }

        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
