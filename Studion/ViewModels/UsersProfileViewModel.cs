using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Studion.Models;

namespace Studion.ViewModels
{
    public class UsersProfileViewModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }

        public List<Note> PublishedNotes { get; set; }

        public UsersProfileViewModel(string userID, ApplicationDbContext _context)
        {
            this.UserID = userID;
            this.UserName = _context.Users.Single(u => u.Id == this.UserID).UserName;

            PublishedNotes = _context.Notes.Where(n => n.AuthorID == this.UserID).ToList();
        }
    }
}