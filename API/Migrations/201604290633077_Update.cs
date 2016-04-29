namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Installations", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Installations", new[] { "Position_Id" });
            AlterColumn("dbo.Installations", "Position_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Notifications", "Msg", c => c.String(nullable: false));
            CreateIndex("dbo.Installations", "Position_Id");
            AddForeignKey("dbo.Installations", "Position_Id", "dbo.Positions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Installations", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Installations", new[] { "Position_Id" });
            AlterColumn("dbo.Notifications", "Msg", c => c.String());
            AlterColumn("dbo.Installations", "Position_Id", c => c.Long());
            CreateIndex("dbo.Installations", "Position_Id");
            AddForeignKey("dbo.Installations", "Position_Id", "dbo.Positions", "Id");
        }
    }
}
