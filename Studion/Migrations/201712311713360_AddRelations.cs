namespace Studion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelations : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Ratings");
            AlterColumn("dbo.Comments", "CommenterID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "CommentMessage", c => c.String(nullable: false, maxLength: 140));
            AlterColumn("dbo.ExamBoards", "ExamBoardName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Levels", "LevelName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Notes", "AuthorID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Notes", "Title", c => c.String(maxLength: 40));
            AlterColumn("dbo.Ratings", "RaterID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Subjects", "SubjectName", c => c.String(nullable: false, maxLength: 40));
            AddPrimaryKey("dbo.Ratings", new[] { "NoteID", "RaterID" });
            CreateIndex("dbo.Comments", "NoteID");
            CreateIndex("dbo.Comments", "CommenterID");
            CreateIndex("dbo.Notes", "AuthorID");
            CreateIndex("dbo.Notes", "SubjectID");
            CreateIndex("dbo.Notes", "LevelID");
            CreateIndex("dbo.Notes", "ExamBoardID");
            CreateIndex("dbo.Ratings", "NoteID");
            CreateIndex("dbo.Ratings", "RaterID");
            AddForeignKey("dbo.Comments", "CommenterID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Notes", "AuthorID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Notes", "ExamBoardID", "dbo.ExamBoards", "ExamBoardID", cascadeDelete: true);
            AddForeignKey("dbo.Notes", "LevelID", "dbo.Levels", "LevelID", cascadeDelete: true);
            AddForeignKey("dbo.Notes", "SubjectID", "dbo.Subjects", "SubjectID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "NoteID", "dbo.Notes", "NoteID", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "NoteID", "dbo.Notes", "NoteID", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "RaterID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "RaterID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "NoteID", "dbo.Notes");
            DropForeignKey("dbo.Comments", "NoteID", "dbo.Notes");
            DropForeignKey("dbo.Notes", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Notes", "LevelID", "dbo.Levels");
            DropForeignKey("dbo.Notes", "ExamBoardID", "dbo.ExamBoards");
            DropForeignKey("dbo.Notes", "AuthorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "CommenterID", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "RaterID" });
            DropIndex("dbo.Ratings", new[] { "NoteID" });
            DropIndex("dbo.Notes", new[] { "ExamBoardID" });
            DropIndex("dbo.Notes", new[] { "LevelID" });
            DropIndex("dbo.Notes", new[] { "SubjectID" });
            DropIndex("dbo.Notes", new[] { "AuthorID" });
            DropIndex("dbo.Comments", new[] { "CommenterID" });
            DropIndex("dbo.Comments", new[] { "NoteID" });
            DropPrimaryKey("dbo.Ratings");
            AlterColumn("dbo.Subjects", "SubjectName", c => c.String());
            AlterColumn("dbo.Ratings", "RaterID", c => c.Int(nullable: false));
            AlterColumn("dbo.Notes", "Title", c => c.String());
            AlterColumn("dbo.Notes", "AuthorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Levels", "LevelName", c => c.String());
            AlterColumn("dbo.ExamBoards", "ExamBoardName", c => c.String());
            AlterColumn("dbo.Comments", "CommentMessage", c => c.String());
            AlterColumn("dbo.Comments", "CommenterID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Ratings", new[] { "NoteID", "RaterID" });
        }
    }
}
