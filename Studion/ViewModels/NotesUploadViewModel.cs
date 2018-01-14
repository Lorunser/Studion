using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studion.Models;
using System.ComponentModel.DataAnnotations;
using Studion.Models;
using Microsoft.AspNet.Identity;

namespace Studion.ViewModels
{
    public class NotesUploadViewModel
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

        [DataType(DataType.Upload)]
        [Display(Name = "PDF File (Max 20 MB)")]
        public HttpPostedFileBase UploadedFile { get; set; }

        //methods
        public NotesUploadViewModel() { } // parameterless constructor for passing from form

        public NotesUploadViewModel(ApplicationDbContext _context)
        {
            SubjectList = _context.Subjects.ToList();
            SubjectList.Sort();

            ExamBoardList = _context.ExamBoards.ToList();
            ExamBoardList.Sort();

            LevelList = _context.Levels.ToList();
            LevelList.Sort();
        }

        public void SaveToDatabase(ApplicationDbContext _context, string userID)
        {
            Note = new Note();

            Note.Title = this.Title;
            Note.AuthorID = userID; // need to figure out
            Note.SubjectID = this.SubjectID;
            Note.ExamBoardID = this.ExamBoardID;
            Note.LevelID = this.LevelID;

            Note.UploadTime = DateTime.Now;
            Note.Downloads = 0;

            _context.Notes.Add(Note);
            _context.SaveChanges();

            UploadedFile.SaveAs("~/Documents/" + Note.NoteID + ".pdf");
        }
    }
}