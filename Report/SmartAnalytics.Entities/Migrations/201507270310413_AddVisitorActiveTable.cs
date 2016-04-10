namespace SmartAnalytics.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddVisitorActiveTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisitorActive",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        Depth = c.Int(nullable: false),
                        DepthCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitorLoyalty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        FrequencyCount = c.Int(nullable: false),
                        UniqueUser = c.Int(nullable: false),
                        PageView = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitorNewOld",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
                        IsNewVisitor = c.Boolean(nullable: false),
                        PageView = c.Int(nullable: false),
                        UniqueUser = c.Int(nullable: false),
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
            DropTable("dbo.VisitorNewOld");
            DropTable("dbo.VisitorLoyalty");
            DropTable("dbo.VisitorActive");
        }
    }
}
