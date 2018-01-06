using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Studion.Models;

namespace Studion.ViewModels
{
    public class NotesUploadViewModel
    {
        public NotesUploadViewModel Note { get; set; }

        public List<Subject> SubjectList { get; set; }
        public List<ExamBoard> ExamBoardList { get; set; }
        public List<Level> LevelList { get; set; }

        //form data
        public string Title { get; set; }
        public int SubjectID { get; set; }
        public int ExamBoardID { get; set; }
        public int LevelID { get; set; }
        public HttpPostedFile UploadedFile { get; set; }

        public NotesUploadViewModel(ApplicationDbContext _context)
        {
            SubjectList = _context.Subjects.ToList();
            ExamBoardList = _context.ExamBoards.ToList();
            LevelList = _context.Levels.ToList();
        }

        public void SaveToDatabase(ApplicationDbContext _context)
        {

        }
    }
}