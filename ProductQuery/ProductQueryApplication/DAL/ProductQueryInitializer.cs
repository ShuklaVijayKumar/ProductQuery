using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProductQuery.Models;

namespace ProductQuery.DAL
{
    public class ProductQueryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProductQueryContext>
    {
        protected override void Seed(ProductQueryContext context)
        {
        }
    }
}