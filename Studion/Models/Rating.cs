using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Rating
    {
        public int NoteID { get; set; }
        public int RaterID { get; set; }

        public int Stars { get; set; }
    }
}