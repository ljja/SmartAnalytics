namespace SmartAnalytics.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTotalCountField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OriginPage", "TotalCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OriginPage", "TotalCount");
        }
    }
}
