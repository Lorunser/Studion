using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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