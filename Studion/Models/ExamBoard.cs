using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class ExamBoard : IComparable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamBoardID { get; set; }

        [Required]
        [MaxLength(40)]
        public string ExamBoardName { get; set; }

        public int CompareTo(object obj)
        {
            ExamBoard other = obj as ExamBoard;
            return this.ExamBoardName.CompareTo(other.ExamBoardName);
        }
    }
}