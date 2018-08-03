using Studion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Dtos
{
    public class NoteDto
    {
        //primary key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID { get; set; }

        //foreign keys and navigation properties
        [Required]
        public string AuthorID { get; set; }
        public string AuthorName { get; set; }

        [Required]
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }

        [Required]
        public int LevelID { get; set; }
        public string LevelName { get; set; }

        [Required]
        public int ExamBoardID { get; set; }
        public string ExamBoardName { get; set; }

        //properties
        [MaxLength(40, ErrorMessage = "Title must be 40 characters or less")]
        public string Title { get; set; }

        public int Downloads { get; set; }
        public DateTime UploadTime { get; set; }

        //display props
        public double AverageRating { get; set; }
    }
}