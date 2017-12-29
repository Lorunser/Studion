using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Studion.Models;

namespace Studion.ViewModels
{
    public class NotesDisplayViewModel
    {
        public Note Note { get; set; }

        public string AuthorUsername { get; set; }
        public string SubjectName { get; set; }
        public string LevelName { get; set; }
        public string ExamBoardName { get; set; }

        public double Rating { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public void Default()
        {
            Note = new Note();
            Note.Default();

            AuthorUsername = "Lorunser";
            SubjectName = "Computing";
            LevelName = "A-Level";
            ExamBoardName = "AQA";

            Rating = 4.5;

            Comments = new List<CommentViewModel>();
            CommentViewModel defaultComment = new CommentViewModel();
            defaultComment.Default();
            Comments.Add(defaultComment);
        }
    }
}