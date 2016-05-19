namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Worker = c.String(),
                        Time = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Observer = c.Int(nullable: false),
                        ErrorDescription = c.String(),
                        MadeRepair = c.String(),
                        UserComment = c.String(),
                        InstallationId_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Installations", t => t.InstallationId_Id, cascadeDelete: true)
                .Index(t => t.InstallationId_Id);
            
            CreateTable(
                "dbo.Installations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        Position_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.Position_Id)
                .Index(t => t.Position_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Msg = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cases", "InstallationId_Id", "dbo.Installations");
            DropForeignKey("dbo.Installations", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Installations", new[] { "Position_Id" });
            DropIndex("dbo.Cases", new[] { "InstallationId_Id" });
            DropTable("dbo.Notifications");
            DropTable("dbo.Positions");
            DropTable("dbo.Installations");
            DropTable("dbo.Cases");
        }
    }
}
