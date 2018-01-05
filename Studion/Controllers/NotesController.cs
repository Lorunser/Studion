using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.Models;
using System.Data.Entity; // get include working

namespace Studion.Controllers
{
    public class NotesController : Controller
    {
        //database initialisation
        #region
        private ApplicationDbContext _context;

        public NotesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: Notes/{NoteID}
        [Route("Notes/NoteID")]
        public ActionResult Display(int NoteID)
        {
            Note note = _context.Notes.Single(n => n.NoteID == NoteID);
            return View(note);
        }

        // GET: Notes/Search
        public ActionResult Search()
        {
            var notes = _context.Notes;

            notes.Include(n => n.author);
            notes.Include(n => n.subject);
            notes.Include(n => n.level);
            notes.Include(n => n.examBoard);
            notes.Include(n => n.ratings);
            var noteList = notes.ToList();

            return View(noteList);
        }

        // GET: Notes/Upload
        public ActionResult Upload()
        {
            return View();
        }
    }
}