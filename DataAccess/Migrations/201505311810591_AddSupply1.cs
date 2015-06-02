namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupply1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Supply_Id", "dbo.Supplies");
            DropIndex("dbo.Products", new[] { "Supply_Id" });
            CreateTable(
                "dbo.SupplyProducts",
                c => new
                    {
                        Supply_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Supply_Id, t.Product_Id })
                .ForeignKey("dbo.Supplies", t => t.Supply_Id, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: false)
                .Index(t => t.Supply_Id)
                .Index(t => t.Product_Id);
            
            DropColumn("dbo.Products", "Supply_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Supply_Id", c => c.Int());
            DropForeignKey("dbo.SupplyProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.SupplyProducts", "Supply_Id", "dbo.Supplies");
            DropIndex("dbo.SupplyProducts", new[] { "Product_Id" });
            DropIndex("dbo.SupplyProducts", new[] { "Supply_Id" });
            DropTable("dbo.SupplyProducts");
            CreateIndex("dbo.Products", "Supply_Id");
            AddForeignKey("dbo.Products", "Supply_Id", "dbo.Supplies", "Id");
        }
    }
}
