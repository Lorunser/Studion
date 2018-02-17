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
            NotesDisplayViewModel ndvm = new NotesDisplayViewModel(NoteID, _context);
            return View(ndvm);
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
            // code to prevent unlogged user uploading a note
            var currentUrl = this.Url.Action("Upload", "Notes", new { }, this.Request.Url.Scheme);
            var userID = User.Identity.GetUserId();
            if (userID == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = currentUrl});
            }

            var viewModel = new NoteFormViewModel(_context);
            return View("NoteForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(NoteFormViewModel viewModel, HttpPostedFileBase upload)
        {
            var userID = User.Identity.GetUserId();
            string pathToSubDir = ControllerContext.HttpContext.Server.MapPath("~/Documents/");

            viewModel.SaveToDatabase(_context, userID, upload, pathToSubDir);

            return RedirectToAction("Display", "Notes", new { NoteID = viewModel.Note.NoteID });
        }

        // GET: Notes/Edit
        public ActionResult Edit(int NoteID)
        {
            var note = _context.Notes.SingleOrDefault(n => n.NoteID == NoteID);

            if (note == null)
            {
                return HttpNotFound();
            }

            var viewModel = new NoteFormViewModel(_context);
            viewModel.Editing = true;
            viewModel.Note = note;

            return View("NoteForm", viewModel); 
        }
    }
}