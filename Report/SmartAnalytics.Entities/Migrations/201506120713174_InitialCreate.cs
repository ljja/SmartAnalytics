using System.Data.Entity.Migrations;

namespace SmartAnalytics.Entities.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Browse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrowseName = c.String(nullable: false, maxLength: 50),
                        UserAgent = c.String(nullable: false, maxLength: 1024),
                        UserAgentHash = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        DomainAlias = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlowVolumeByDay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false, maxLength: 50),
                        TotalDate = c.DateTime(nullable: false),
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
                "dbo.FlowVolumeByHour",
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
                "dbo.IpAddressArea",
                c => new
                    {
                        Ip = c.String(nullable: false, maxLength: 15),
                        Country = c.String(maxLength: 50),
                        CountryCode = c.String(maxLength: 50),
                        Area = c.String(maxLength: 50),
                        AreaCode = c.String(maxLength: 50),
                        Region = c.String(maxLength: 50),
                        RegionCode = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        CityCode = c.String(maxLength: 50),
                        County = c.String(maxLength: 50),
                        CountyCode = c.String(maxLength: 50),
                        Isp = c.String(maxLength: 50),
                        IspCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Ip);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 50),
                        UserPwd = c.String(nullable: false, maxLength: 32),
                        Nick = c.String(nullable: false, maxLength: 50),
                        IsEnable = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.IpAddressArea");
            DropTable("dbo.FlowVolumeByHour");
            DropTable("dbo.FlowVolumeByDay");
            DropTable("dbo.Domain");
            DropTable("dbo.Browse");
        }
    }
}
