using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Blob.Data
{
    public class BlobDbConfiguration : DbConfiguration
    {
        public BlobDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
