using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class ExamBoard
    {
        [Key]
        public int ExamBoardID { get; set; }

        [Required]
        [MaxLength(40)]
        public string ExamBoardName { get; set; }

        public void Default()
        {
            ExamBoardID = 0;
            ExamBoardName = "AQA";
        }
    }
}