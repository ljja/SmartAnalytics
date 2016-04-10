using System.Data.Entity.Migrations;

namespace SmartAnalytics.Entities.Migrations
{
    public partial class AddFlowVolumeByMinuteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlowVolumeByMinute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        TotalHour = c.Int(nullable: false),
                        TotalMinute = c.Int(nullable: false),
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
            DropTable("dbo.FlowVolumeByMinute");
        }
    }
}
