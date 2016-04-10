using System.Data.Entity.Migrations;

namespace SmartAnalytics.Entities.Migrations
{
    public partial class AddCityAreaCodeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityAreaCode",
                c => new
                    {
                        AreaCode = c.String(nullable: false, maxLength: 6),
                        AreaName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AreaCode);
            
            CreateTable(
                "dbo.IndustryCode",
                c => new
                    {
                        CategoryCode = c.String(nullable: false, maxLength: 12),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryCode);
            
            CreateTable(
                "dbo.SiteCategory",
                c => new
                    {
                        Domain = c.String(nullable: false, maxLength: 50),
                        SiteName = c.String(nullable: false, maxLength: 50),
                        Summary = c.String(nullable: false, maxLength: 500),
                        Pr = c.Int(nullable: false),
                        Click = c.Int(nullable: false),
                        IndustryCode = c.String(nullable: false, maxLength: 12),
                        CityAreaCode = c.String(nullable: false, maxLength: 6),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Domain);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SiteCategory");
            DropTable("dbo.IndustryCode");
            DropTable("dbo.CityAreaCode");
        }
    }
}
