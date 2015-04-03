using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class CustomerMap : BlobEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customers");
            HasKey(x => x.Id);
        }
    }
}
