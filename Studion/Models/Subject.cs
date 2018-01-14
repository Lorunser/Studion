using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Subject : IComparable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectID { get; set; }

        [Required]
        [MaxLength(40)]
        public string SubjectName { get; set; }

        public int CompareTo(object obj)
        {
            Subject other = obj as Subject;
            return this.SubjectName.CompareTo(other.SubjectName);
        }
    }
}