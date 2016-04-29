namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "Longitude", c => c.Double(nullable: false));
            DropColumn("dbo.Positions", "Logitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "Logitude", c => c.Double(nullable: false));
            DropColumn("dbo.Positions", "Longitude");
        }
    }
}
