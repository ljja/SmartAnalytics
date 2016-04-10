using System.Data.Entity.Migrations;

namespace SmartAnalytics.Entities.Migrations
{
    public partial class AddOriginWordTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OriginPage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        TotalHour = c.Int(nullable: false),
                        OriginUrl = c.String(nullable: false, maxLength: 1024),
                        OriginDomain = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OriginWord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        WordText = c.String(nullable: false, maxLength: 50),
                        BaiDuTotalCount = c.Int(nullable: false),
                        HaoSouTotalCount = c.Int(nullable: false),
                        SouGouTotalCount = c.Int(nullable: false),
                        GoogleTotalCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OriginWord");
            DropTable("dbo.OriginPage");
        }
    }
}
