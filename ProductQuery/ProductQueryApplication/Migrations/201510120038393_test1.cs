namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Query",
                c => new
                    {
                        QueryID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        AddressLine1 = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 20),
                        Postcode = c.String(nullable: false, maxLength: 8),
                        Country = c.String(nullable: false, maxLength: 10),
                        Phone = c.String(nullable: false, maxLength: 14),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QueryID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                    },
                body:
                    @"INSERT [dbo].[Product]([Name])
                      VALUES (@Name)
                      
                      DECLARE @ProductID int
                      SELECT @ProductID = [ProductID]
                      FROM [dbo].[Product]
                      WHERE @@ROWCOUNT > 0 AND [ProductID] = scope_identity()
                      
                      SELECT t0.[ProductID]
                      FROM [dbo].[Product] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ProductID] = @ProductID"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Update",
                p => new
                    {
                        ProductID = p.Int(),
                        Name = p.String(maxLength: 50),
                    },
                body:
                    @"UPDATE [dbo].[Product]
                      SET [Name] = @Name
                      WHERE ([ProductID] = @ProductID)"
            );
            
            CreateStoredProcedure(
                "dbo.Product_Delete",
                p => new
                    {
                        ProductID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Product]
                      WHERE ([ProductID] = @ProductID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Product_Delete");
            DropStoredProcedure("dbo.Product_Update");
            DropStoredProcedure("dbo.Product_Insert");
            DropForeignKey("dbo.Query", "ProductID", "dbo.Product");
            DropIndex("dbo.Query", new[] { "ProductID" });
            DropTable("dbo.Query");
            DropTable("dbo.Product");
        }
    }
}
