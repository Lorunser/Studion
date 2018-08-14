using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studion.Dtos
{
    public class RatingDto
    {
        [Required]
        public int NoteID { get; set; }

        public string RaterID { get; set; }

        [Required]
        [Range(1,5)]
        public int Stars { get; set; }
    }
}