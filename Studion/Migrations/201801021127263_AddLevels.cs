namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevels : DbMigration
    {
        public override void Up()
        {
            string query = @"
                            INSERT INTO Levels (LevelName)
                            VALUES 
                                ('A-level'),
                                ('Pre-U'),
                                ('IB'),
                                ('GCSE'),
                                ('IGCSE'),
                                ('Other')
                            ";
            Sql(query);
        }
        
        public override void Down()
        {
        }
    }
}
