namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "MadeRepair", c => c.String());
            DropColumn("dbo.Cases", "MadePepair");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cases", "MadePepair", c => c.String());
            DropColumn("dbo.Cases", "MadeRepair");
        }
    }
}
