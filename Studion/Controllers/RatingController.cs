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
        //database initialisation
        #region
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
        public ActionResult Save(NotesDisplayViewModel ndvm)
        {
            var noteID = ndvm.Note.NoteID;

            if (Request.IsAuthenticated)
            {
                int stars = ndvm.Rating;
                var raterID = User.Identity.GetUserId();

                var ratingList = _context.Ratings.Where(r => r.NoteID == noteID && r.RaterID == raterID).ToList();

                if(ratingList.Count == 0) //rating has not been made
                {
                    var newRating = new Rating();
                    newRating.NoteID = noteID;
                    newRating.RaterID = raterID;

                    newRating.Stars = stars;
                    _context.Ratings.Add(newRating);
                }

                else // rating already made >> modify
                {
                    var oldRating = ratingList.First();

                    oldRating.Stars = stars;
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Display", "Notes", new { NoteID = noteID});
        }
    }
}