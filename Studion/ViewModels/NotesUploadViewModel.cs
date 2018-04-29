using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studion.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Studion.Models;
using Microsoft.AspNet.Identity;
using Microsoft.SqlServer;
using System.IO;

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
        [Required] //int are implicitly required
        [MaxLength(80)]
        public string Title { get; set; }

        [Display(Name = "Subject")]
        public int SubjectID { get; set; }

        [Display(Name = "ExamBoard")]
        public int ExamBoardID { get; set; }

        [Display(Name = "Level")]
        public int LevelID { get; set; }

        //constructor
        public NoteFormViewModel() { } // parameterless constructor for passing from form

        public NoteFormViewModel(ApplicationDbContext _context)
        {
            GenLists(_context);
        }

        //methods
        public void GenLists(ApplicationDbContext _context)
        {
            SubjectList = _context.Subjects.ToList();
            SubjectList.Sort();

            ExamBoardList = _context.ExamBoards.ToList();
            ExamBoardList.Sort();

            LevelList = _context.Levels.ToList();
            LevelList.Sort();
        }       

        public void SaveToDatabase(ApplicationDbContext _context, IIdentity identity, HttpPostedFileBase upload, string pathToSubDir)
        {
            var userID = identity.GetUserId();

            if (Note == null) //not yet set
            {
                // adding new note
                Note = new Note();

                Note.UploadTime = DateTime.Now;
                Note.Downloads = 0;

                //add to database
                _context.Notes.Add(Note);
            }
            else
            {
                var note = _context.Notes.Single(n => n.NoteID == Note.NoteID);

                if(userID != note.AuthorID) // add check for admin
                {
                    throw new UnauthorizedAccessException();
                }

                Note = note;
            }

            //assign properties
            Note.Title = this.Title;
            Note.AuthorID = userID;
            Note.SubjectID = this.SubjectID;
            Note.ExamBoardID = this.ExamBoardID;
            Note.LevelID = this.LevelID;

            //save to database
            _context.SaveChanges(); // must be called to assign ID

            //generate filename, must come after saving to db as then allocated id
            string filename = Note.NoteID + ".pdf";
            string filepath = Path.Combine(pathToSubDir, filename);

            //save file
            upload.SaveAs(filepath);
        }

        public void SetProps(Note note)
        {
            //assign note
            this.Note = note;

            //assign form props
            this.Title = this.Note.Title;
            this.SubjectID = this.Note.SubjectID;
            this.ExamBoardID = this.Note.ExamBoardID;
            this.LevelID = this.Note.LevelID;
        }
    }
}