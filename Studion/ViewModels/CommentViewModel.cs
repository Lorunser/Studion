using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Studion.Models;

namespace Studion.ViewModels
{
    public class CommentViewModel
    {
        public Comment Comment { get; set; }

        public string CommenterUsername { get; set; }

        public void Default()
        {
            Comment = new Comment();
            Comment.Default();
            CommenterUsername = "Victor";
        }
    }

}