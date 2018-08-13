using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// custom imports
using Studion.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Studion.ViewModels
{
    public class NotesSearchViewModel
    {
        //lists for drop down models
        public List<Subject> SubjectList { get; set; }
        public List<ExamBoard> ExamBoardList { get; set; }
        public List<Level> LevelList { get; set; }

        //form data
        //all ints made nullable as not required
        [Display(Name = "Subject")]
        public int? SubjectID { get; set; }

        [Display(Name = "Exam Board / University")]
        public int? ExamBoardID { get; set; }

        [Display(Name = "Level")]
        public int? LevelID { get; set; }

        //constructors
        public NotesSearchViewModel() { } // parameterless constructor for passing from form

        public NotesSearchViewModel(ApplicationDbContext _context)
        {
            PopulateLists(_context);
        }

        public void PopulateLists(ApplicationDbContext _context)
        {
            SubjectList = _context.Subjects.ToList();
            SubjectList.Sort();

            ExamBoardList = _context.ExamBoards.ToList();
            ExamBoardList.Sort();

            LevelList = _context.Levels.ToList();
            LevelList.Sort();
        }
    }
}