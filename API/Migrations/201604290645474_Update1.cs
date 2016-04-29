namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "UserComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases", "UserComment");
        }
    }
}
