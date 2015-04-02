
namespace Blob.Core.Data
{
    public interface IRepository<T>
    {
        T GetById(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
