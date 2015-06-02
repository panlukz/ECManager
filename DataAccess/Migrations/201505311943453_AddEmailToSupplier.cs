namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToSupplier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suppliers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "Email");
        }
    }
}
