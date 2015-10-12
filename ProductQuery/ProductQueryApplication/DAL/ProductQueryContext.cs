using ProductQuery.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProductQuery.DAL
{
    public class ProductQueryContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Product>().MapToStoredProcedures();
        }
    }
}