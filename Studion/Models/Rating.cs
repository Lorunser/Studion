using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Rating
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("note")]
        public int NoteID { get; set; }
        public Note note { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("rater")]
        public string RaterID { get; set; }
        public ApplicationUser rater { get; set; }

        [Required]
        public int Stars { get; set; }
    }
}