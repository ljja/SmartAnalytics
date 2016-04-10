namespace SmartAnalytics.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSurveyPageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyDomain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        TotalHour = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.SurveyPage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        Url = c.String(nullable: false, maxLength: 1024),
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
            DropTable("dbo.SurveyPage");
            DropTable("dbo.SurveyDomain");
        }
    }
}
