namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateToSupply : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplies", "DeliveryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplies", "DeliveryDate");
        }
    }
}
