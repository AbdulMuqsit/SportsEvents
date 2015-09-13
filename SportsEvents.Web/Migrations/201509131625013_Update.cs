namespace SportsEvents.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sports", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sports", "Name", c => c.Int(nullable: false));
        }
    }
}
