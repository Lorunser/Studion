namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExamBoards : DbMigration
    {
        public override void Up()
        {
            string query = @"
                            INSERT INTO ExamBoards (ExamBoardName)
                            VALUES 
                                ('AQA'),
                                ('CIE'),
                                ('CCEA'),
                                ('Edexcel'),
                                ('Eduqas'),
                                ('ICAAE'),
                                ('OCR'),
                                ('Other'),
                                ('WJEC')
                            ";
            Sql(query);
        }
        
        public override void Down()
        {
        }
    }
}
