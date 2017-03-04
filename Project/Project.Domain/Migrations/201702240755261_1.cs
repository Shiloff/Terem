namespace Project.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileActivities", "Icon", c => c.String());
            AddColumn("dbo.ProfileInteres", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileInteres", "Icon");
            DropColumn("dbo.ProfileActivities", "Icon");
        }
    }
}
