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
        public List<Note> NotesList { get; set; }

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
            //instantiate empty list
            NotesList = new List<Note>();
        }

        //methods
        public void PerformSearch(ApplicationDbContext _context)
        {
            //call to database
            var notes = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings);

            //filters
            if(SubjectID != null)
            {
                notes = notes.Where(n => n.SubjectID == SubjectID);
            }

            if(ExamBoardID != null)
            {
                notes = notes.Where(n => n.ExamBoardID == ExamBoardID);
            }

            if(LevelID != null)
            {
                notes = notes.Where(n => n.LevelID == LevelID);
            }

            //turn into list
            NotesList = notes.ToList();
        }

        public void PopulateLists(ApplicationDbContext _context)
        {
            SubjectList = _context.Subjects.ToList();
            SubjectList.Sort();

            ExamBoardList = _context.ExamBoards.ToList();
            ExamBoardList.Sort();

            LevelList = _context.Levels.ToList();
            LevelList.Sort();

            if(NotesList == null)
            {
                NotesList = new List<Note>();
            }
        }

        public bool OptionsSelected()
        {
            if (SubjectID == null)
            {
                if(ExamBoardID == null)
                {
                    if(LevelID == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}