using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//additional usings
using Studion.Models;
using Studion.ViewModels;
using Microsoft.AspNet.Identity;

namespace Studion.Controllers
{
    public class RatingController : Controller
    {
        #region Database
        private ApplicationDbContext _context;

        public RatingController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        //save rating
        [HttpPost]
        [Authorize]
        public ActionResult Save(NotesDisplayViewModel ndvm)
        {
            var noteID = ndvm.Note.NoteID;

            int stars = ndvm.Rating;
            var raterID = User.Identity.GetUserId();

            var rating = _context.Ratings.Single(r => r.NoteID == noteID && r.RaterID == raterID);

            if(rating == null) //rating has not been made
            {
                rating = new Rating();
                rating.NoteID = noteID;
                rating.RaterID = raterID;

                rating.Stars = stars;
                _context.Ratings.Add(rating);
            }

            else // rating already made >> modify
            {
                rating.Stars = stars;
            }

            _context.SaveChanges();

            return RedirectToAction("Display", "Notes", new { NoteID = noteID});
        }
    }
}