using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studion.Models;
using System.ComponentModel.DataAnnotations;
using Studion.Models;
using Microsoft.AspNet.Identity;
using Microsoft.SqlServer;

namespace Studion.ViewModels
{
    public class NoteFormViewModel
    {
        public Note Note { get; set; }

        //lists for drop down models
        public List<Subject> SubjectList { get; set; }
        public List<ExamBoard> ExamBoardList { get; set; }
        public List<Level> LevelList { get; set; }

        //form data
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Subject")]
        public int SubjectID { get; set; }

        [Display(Name = "ExamBoard")]
        public int ExamBoardID { get; set; }

        [Display(Name = "Level")]
        public int LevelID { get; set; }

        // view data
        public bool Editing { get; set; }

        //methods
        public NoteFormViewModel(bool editing = false) { } // parameterless constructor for passing from form

        public NoteFormViewModel(ApplicationDbContext _context)
        {
            SubjectList = _context.Subjects.ToList();
            SubjectList.Sort();

            ExamBoardList = _context.ExamBoards.ToList();
            ExamBoardList.Sort();

            LevelList = _context.Levels.ToList();
            LevelList.Sort();
        }

        public void SaveToDatabase(ApplicationDbContext _context, string userID, HttpPostedFileBase upload, string pathToSubDir)
        {
            if (Note == null)
            {
                // adding new note
                Note = new Note();

                //assign properties
                Note.Title = this.Title;
                Note.AuthorID = userID; // need to figure out
                Note.SubjectID = this.SubjectID;
                Note.ExamBoardID = this.ExamBoardID;
                Note.LevelID = this.LevelID;

                Note.UploadTime = DateTime.Now;
                Note.Downloads = 0;


                //add to database and save
                _context.Notes.Add(Note);
                _context.SaveChanges();

                //generate filename, must come after saving to db as then allocated id
                string path = pathToSubDir + Note.NoteID + ".pdf";

                //save file
                upload.SaveAs(path);
            }
            else
            {
                // updating existing note
                var noteInDB = Note;

                // assign relevant properties
                // cannot edit: AuthorID, UploadTime, Downloads, pdf file
                noteInDB.Title = this.Title;
                noteInDB.SubjectID = this.SubjectID;
                noteInDB.ExamBoardID = this.ExamBoardID;
                noteInDB.LevelID = this.LevelID;

                // save changes
                _context.SaveChanges();
            }            
        }
    }
}