using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studion.Dtos
{
    public class CommentDto
    {
        //system assigned
        public int CommentID { get; set; }

        public int NoteID { get; set; }
        public string CommenterID { get; set; }
        public DateTime PostTime { get; set; }


        //user assigned
        [Required]
        [MaxLength(140, ErrorMessage = "Comment must be 140 characters or less")]
        public string CommentMessage { get; set; }

        //helper
        public string CommenterUsername { get; set; }
    }
}