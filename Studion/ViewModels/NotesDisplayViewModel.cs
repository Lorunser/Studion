using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Studion.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity; // get include working

namespace Studion.ViewModels
{
    public class NotesDisplayViewModel
    {
        public Note Note { get; set; }

        [Required]
        [MaxLength(140)]
        [Display(Name = "Write comment")]
        public string CommentMessage { get; set; }

        //constructor
        public NotesDisplayViewModel() { } // parameterless constructor for passing from form

        public NotesDisplayViewModel(int NoteID, ApplicationDbContext _context)
        {
            Note = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings)
                .Include(n => n.comments)
                .Single(n => n.NoteID == NoteID);
        }

        //methods

        public void SaveToDatabase(ApplicationDbContext _context, string userID)
        {
            Comment comment = new Comment();

            //FKs
            comment.NoteID = this.Note.NoteID;
            comment.CommenterID = userID;

            //Data
            comment.PostTime = DateTime.Now;
            comment.CommentMessage = this.CommentMessage;

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}