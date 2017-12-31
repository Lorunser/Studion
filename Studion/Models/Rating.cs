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
        [Key, Column(Order = 0)]
        public int NoteID { get; set; }
        [Key, Column(Order = 1)]
        public int RaterID { get; set; }

        public int Stars { get; set; }
    }
}