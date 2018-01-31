using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.Models;
using Studion.ViewModels;
using System.Data.Entity; // get include working
using Microsoft.AspNet.Identity;

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

        // GET: Notes/Display/{NoteID}
        [Route("Notes/Display/{NoteID}")]
        public ActionResult Display(int NoteID)
        {
            Note note = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings)
                .Include(n => n.comments)
                .Single(n => n.NoteID == NoteID);

            return View(note);
        }

        // GET: Notes/Search
        public ActionResult Search()
        {
            var notes = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings)
                .ToList();

            return View(notes);
        }

        // GET: Notes/Upload
        public ActionResult Upload()
        {
            var viewModel = new NotesUploadViewModel(_context);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(NotesUploadViewModel viewModel, HttpPostedFileBase upload)
        {
            var userID = User.Identity.GetUserId();
            string pathToSubDir = ControllerContext.HttpContext.Server.MapPath("~/Documents/");

            viewModel.SaveToDatabase(_context, userID, upload, pathToSubDir);

            return RedirectToAction("Display", "Notes", new { NoteID = viewModel.Note.NoteID });
        }
    }
}