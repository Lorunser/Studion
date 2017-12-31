using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("note")]
        public int NoteID { get; set; }
        public Note note { get; set; }

        [ForeignKey("commenter")]
        public string CommenterID { get; set; }
        public ApplicationUser commenter { get; set; }

        [Required]
        [MaxLength(140, ErrorMessage = "Comment must be 140 characters or less")]
        public string CommentMessage { get; set; }

        public DateTime PostTime { get; set; }

        public void Default()
        {
            CommentID = 0;
            NoteID = 0;
            CommenterID = "0";
            CommentMessage = "One word, AMAZING";
        }
    }
}