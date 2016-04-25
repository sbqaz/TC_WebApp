namespace WebLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InstallationId = c.Long(nullable: false),
                        Worker = c.String(),
                        Time = c.DateTime(nullable: false),
                        Observer = c.Int(nullable: false),
                        ErrorDescription = c.String(),
                        MadePepair = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Installations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
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
                        Logitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Msg = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        EmailNotification = c.Boolean(nullable: false),
                        SMSNotification = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Installations", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Installations", new[] { "Position_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Notifications");
            DropTable("dbo.Positions");
            DropTable("dbo.Installations");
            DropTable("dbo.Cases");
        }
    }
}
