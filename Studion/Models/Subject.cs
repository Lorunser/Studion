using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [Required]
        [MaxLength(40)]
        public string SubjectName { get; set; }

        public void Default()
        {
            SubjectID = 0;
            SubjectName = "Computing";
        }
    }
}