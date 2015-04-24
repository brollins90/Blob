using System.Data.Entity.ModelConfiguration;
// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace Blob.Data.Mapping
{
    public abstract class BlobEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected BlobEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }
    }
}
