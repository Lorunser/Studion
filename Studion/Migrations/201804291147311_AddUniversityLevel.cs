namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniversityLevel : DbMigration
    {
        public override void Up()
        {
            string query = @"
                            INSERT INTO Levels (LevelName)
                            VALUES 
                                ('University')
                            ";
            Sql(query);
        }
        
        public override void Down()
        {
        }
    }
}
