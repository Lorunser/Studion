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
        //database initialisation
        #region
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

        // GET: Comment/Save
        [HttpPost]
        public ActionResult Save(NotesDisplayViewModel ndvm)
        {
            // code to prevent unlogged user saving a comment
            var currentUrl = this.Url.Action("Display", "Notes", new { NoteID = ndvm.Note.NoteID }, this.Request.Url.Scheme);
            var userID = User.Identity.GetUserId();
            if (userID == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = currentUrl });
            }

            //validation
            if (ModelState.IsValid == false)
            {
                return View("Display", "Notes", ndvm);
            }

            ndvm.SaveToDatabase(_context, userID);
            return RedirectToAction("Display", "Notes", new { NoteID = ndvm.Note.NoteID });
        }
    }
}