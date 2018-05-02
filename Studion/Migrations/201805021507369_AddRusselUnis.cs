namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRusselUnis : DbMigration
    {
        public override void Up()
        {
            string query = @"
                            INSERT INTO ExamBoards (ExamBoardName)
                            VALUES 
                                ('University of Birmingham'),
                                ('University of Bristol'),
                                ('University of Cambridge'),
                                ('Cardiff University'),
                                ('Durham University'),
                                ('University of Edinburgh'),
                                ('University of Exeter'),
                                ('University of Glasgow'),
                                ('Imperial College London'),
                                ('Kings College London'),
                                ('University of Leeds'),
                                ('University of Liverpool'),
                                ('LSE'),
                                ('University of Manchester'),
                                ('Newcastle University'),
                                ('University of Nottingham'),
                                ('University of Oxford'),
                                ('Queen Mary University of London'),
                                ('Queens University Belfast'),
                                ('University of Sheffield'),
                                ('University of Southampton'),
                                ('UCL'),
                                ('University of Warwick'),
                                ('University of York')
                            ";
            Sql(query);
        }
        
        public override void Down()
        {
        }
    }
}
