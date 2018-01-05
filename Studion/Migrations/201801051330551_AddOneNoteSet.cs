namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneNoteSet : DbMigration
    {
        public override void Up()
        {
            string query = @"
                            INSERT INTO Notes (AuthorID, SubjectID, LevelID, ExamBoardID, Title, Downloads, UploadTime)
                            VALUES('078da50d-3bf4-4cd0-a556-a6eeb308d2bb', 11, 1, 1, 'A-Level AQA Computing Notes', 0, '5/1/2018');
                            ";
            Sql(query);
        }
        
        public override void Down()
        {
        }
    }
}
