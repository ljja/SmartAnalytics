using System.Data.Entity.Migrations;

namespace SmartAnalytics.Entities.Migrations
{
    public partial class AddOriginCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OriginCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        TotalHour = c.Int(nullable: false),
                        TotalNumber = c.Int(nullable: false),
                        IndustryCode = c.String(nullable: false, maxLength: 12),
                        OriginDomain = c.String(nullable: false, maxLength: 50),
                        PageView = c.Int(nullable: false),
                        UniqueUser = c.Int(nullable: false),
                        NewUniqueUser = c.Int(nullable: false),
                        NewUniqueUserRate = c.Single(nullable: false),
                        UniqueIp = c.Int(nullable: false),
                        AccessNumber = c.Int(nullable: false),
                        UserViewPageAverage = c.Single(nullable: false),
                        ViewPageDeptAverage = c.Single(nullable: false),
                        ViewPageTimeSpanAverage = c.Int(nullable: false),
                        BounceRate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OriginCategory");
        }
    }
}
