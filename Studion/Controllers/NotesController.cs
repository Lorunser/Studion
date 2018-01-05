using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.Models;

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
            var notes = _context.Notes.ToList();
            return View(notes);
        }
    }
}