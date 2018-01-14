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

        public HttpPostedFile UploadedFile { get; set; }

        //additional props for Note class
        public DateTime UploadTime { get; set; }
        public string AuthorID { get; set; }

        //methods
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

            Note.AuthorID = userID; // need to figure out
            Note.SubjectID = this.SubjectID;
            Note.ExamBoardID = this.ExamBoardID;
            Note.LevelID = this.LevelID;
            Note.UploadTime = DateTime.Now;

            _context.Notes.Add(Note);
            _context.SaveChanges();
        }
    }
}