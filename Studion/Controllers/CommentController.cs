using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.Models;
using Studion.ViewModels;
using Microsoft.AspNet.Identity;

namespace Studion.Controllers
{
    public class CommentController : Controller
    {
        #region Database
        private ApplicationDbContext _context;

        public CommentController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // REMEMBER TO REWORK TO DELETE
        [HttpPost]
        [Authorize]
        public ActionResult Save(NotesDisplayViewModel ndvm)
        {
            int noteID = ndvm.Note.NoteID;

            //validation
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Display", "Notes", new { NoteID = noteID });
            }

            ndvm.SaveToDatabase(_context, User.Identity.GetUserId());
            return RedirectToAction("Display", "Notes", new { NoteID = noteID });
        }
    }
}