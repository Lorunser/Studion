using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Studion.Models;
using System.Data.Entity;

namespace Studion.ViewModels
{
    public class UsersProfileViewModel
    {
        //properties
        public string UserID { get; private set; }
        public string UserName { get; private set; }

        public List<Note> PublishedNotes { get; private set; }

        public int NumNotesPublished { get; private set; }
        public int NumNotesRated { get; private set; }
        public int NumCommentsMade { get; private set; }
        public int TotalDownloads { get; private set; }

        public double AverageNoteRating { get; private set; }

        //constructor
        public UsersProfileViewModel(string userID, ApplicationDbContext _context)
        {
            this.UserID = userID;
            this.UserName = _context.Users.Single(u => u.Id == this.UserID).UserName;

            this.PublishedNotes = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings)
                .Where(n => n.AuthorID == this.UserID)
                .ToList();

            this.NumNotesPublished = PublishedNotes
                .Count();
            this.NumNotesRated = _context.Ratings
                .Count(r => r.RaterID == this.UserID);
            this.NumCommentsMade = _context.Comments
                .Count(c => c.CommenterID == this.UserID);

            this.TotalDownloads = 0;
            double ratingSum = 0;

            foreach (var note in PublishedNotes)
            {
                this.TotalDownloads += note.Downloads;
                ratingSum += note.GetAvRating();
            }
            
            if(this.NumNotesPublished > 0)
            {
                this.AverageNoteRating = ratingSum / this.NumNotesPublished;
            }
            else
            {
                this.AverageNoteRating = 0;
            }
            //all props now set
        }

        //methods
        public int GetUserScore()
        {
            const int NOTE_VALUE = 50;
            const int RATING_VALUE = 5;
            const int COMMENT_VALUE = 10;
            const double DOWNLOAD_VALUE = 0.1;

            double score = 0;

            score += this.NumNotesPublished * NOTE_VALUE;
            score += this.NumNotesRated * RATING_VALUE;
            score += this.NumCommentsMade * COMMENT_VALUE;
            score += this.TotalDownloads * DOWNLOAD_VALUE;

            double multiplier = 1.0;
            if (this.AverageNoteRating > multiplier)
            {
                multiplier = this.AverageNoteRating;
            }

            score = score * multiplier;

            return Convert.ToInt32(score);
        }
    }
}