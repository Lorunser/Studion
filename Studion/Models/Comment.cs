using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        public int NoteID { get; set; }
        public int CommenterID { get; set; }
        public string CommentMessage { get; set; }

        public DateTime PostTime { get; set; }

        public void Default()
        {
            CommentID = 0;
            NoteID = 0;
            CommenterID = 0;
            CommentMessage = "One word, AMAZING";
        }
    }
}