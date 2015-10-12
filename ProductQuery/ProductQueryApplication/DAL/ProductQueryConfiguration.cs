using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ProductQuery.DAL
{
    public class ProductQueryConfiguration : DbConfiguration
    {
        public ProductQueryConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}