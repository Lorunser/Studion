using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Studion.Models;

namespace Studion.ViewModels
{
    public class UsersProfileViewModel
    {
        public CustomUser User { get; set; }

        public List<NotesDisplayViewModel> PublishedNotes { get; set; }

        public void Default()
        {
            User = new CustomUser();
            User.Default();

            PublishedNotes = new List<NotesDisplayViewModel>();
            NotesDisplayViewModel defaultNDVM = new NotesDisplayViewModel();
            defaultNDVM.Default();
            PublishedNotes.Add(defaultNDVM);
        }
    }
}