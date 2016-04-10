namespace SmartAnalytics.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrlMapTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrlMap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        UrlTitle = c.String(nullable: false, maxLength: 50),
                        UrlAddress = c.String(nullable: false, maxLength: 1024),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UrlMap");
        }
    }
}
