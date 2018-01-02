namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubjects : DbMigration
    {
        public override void Up()
        {
            string query = @"
                            INSERT INTO Subjects (SubjectName)
                            VALUES 
                                ('Accounting'),
                                ('Anthropology'),
                                ('Ancient History'),
                                ('Archaeology'),
                                ('Art'),
                                ('Bengali'),
                                ('Biology'),
                                ('Chemistry'),
                                ('Chinese'),
                                ('Classics'),
                                ('Computing'),
                                ('Dance'),
                                ('Design and Technology'),
                                ('Drama'),
                                ('Economics'),
                                ('Electronics'),
                                ('Engineering'),
                                ('English Language'),
                                ('English Literature'),
                                ('English'),
                                ('EPQ'),
                                ('Food'),
                                ('French'),
                                ('General Studies'),
                                ('Geography'),
                                ('German'),
                                ('Health and Social Care'),
                                ('Hebrew'),
                                ('History'),
                                ('History of Art'),
                                ('Home Economics'),
                                ('ICT'),
                                ('Italian'),
                                ('Languages'),
                                ('Law'),
                                ('Leisure and Tourism'),
                                ('Mathematics'),
                                ('Media Studies'),
                                ('Music'),
                                ('Panjabi'),
                                ('Philosophy'),
                                ('Physical Education'),
                                ('Physics'),
                                ('Polish'),
                                ('Politics'),
                                ('Psychology'),
                                ('Religious Studies'),
                                ('Science'),
                                ('Sociology'),
                                ('Spanish'),
                                ('Statistics'),
                                ('STEM'),
                                ('Urdu'),
                                ('Business'),
                                ('Dutch'),
                                ('Geology'),
                                ('Health and Safety'),
                                ('Gujarati'),
                                ('Other'),
                                ('Sociology'),
                                ('Food technology'),
                                ('Turkish'),
                                ('Retail'),
                                ('Theology'),
                                ('Law'),
                                ('Greek'),
                                ('Latin')
                            ";
            Sql(query);
        }
        
        public override void Down()
        {
        }
    }
}
