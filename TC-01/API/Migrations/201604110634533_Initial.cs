namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Latitude_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.Latitude_Id)
                .Index(t => t.Latitude_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Logitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Installations", "Latitude_Id", "dbo.Positions");
            DropIndex("dbo.Installations", new[] { "Latitude_Id" });
            DropTable("dbo.Positions");
            DropTable("dbo.Installations");
            DropTable("dbo.Cases");
        }
    }
}
