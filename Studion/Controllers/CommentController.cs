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
            // code to prevent unlogged user uploading a note
            var currentUrl = this.Url.Action("Upload", "Notes", new { }, this.Request.Url.Scheme);
            var userID = User.Identity.GetUserId();
            if (userID == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = currentUrl });
            }


            ndvm.SaveToDatabase(_context, userID);
            return View();
        }
    }
}