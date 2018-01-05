namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNavProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "Downloads", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "Downloads");
        }
    }
}
