namespace SmartAnalytics.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddVisitorTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisitorBrowse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        Browse = c.String(nullable: false, maxLength: 20),
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
                "dbo.VisitorBrowseKernel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        BrowseKernel = c.String(nullable: false, maxLength: 20),
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
                "dbo.VisitorLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        Language = c.String(nullable: false, maxLength: 20),
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
                "dbo.VisitorOperatingSystem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        OperatingSystem = c.String(nullable: false, maxLength: 50),
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
                "dbo.VisitorRegion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        UserIpAddress = c.String(nullable: false, maxLength: 15),
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
                "dbo.VisitorResolution",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        Resolution = c.String(nullable: false, maxLength: 20),
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
                "dbo.VisitorTerminal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        TerminalType = c.String(nullable: false, maxLength: 50),
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
            DropTable("dbo.VisitorTerminal");
            DropTable("dbo.VisitorResolution");
            DropTable("dbo.VisitorRegion");
            DropTable("dbo.VisitorOperatingSystem");
            DropTable("dbo.VisitorLanguage");
            DropTable("dbo.VisitorBrowseKernel");
            DropTable("dbo.VisitorBrowse");
        }
    }
}
