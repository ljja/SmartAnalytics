using System.Data.Entity.Migrations;

namespace SmartAnalytics.Entities.Migrations
{
    public partial class AddParentCodeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndustryCode", "ParentCode", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndustryCode", "ParentCode");
        }
    }
}
