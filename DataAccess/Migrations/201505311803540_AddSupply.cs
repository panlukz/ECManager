namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupply : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            AddColumn("dbo.Products", "Supply_Id", c => c.Int());
            CreateIndex("dbo.Products", "Supply_Id");
            AddForeignKey("dbo.Products", "Supply_Id", "dbo.Supplies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplies", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Products", "Supply_Id", "dbo.Supplies");
            DropIndex("dbo.Supplies", new[] { "SupplierId" });
            DropIndex("dbo.Products", new[] { "Supply_Id" });
            DropColumn("dbo.Products", "Supply_Id");
            DropTable("dbo.Supplies");
        }
    }
}
