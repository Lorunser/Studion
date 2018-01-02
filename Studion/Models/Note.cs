using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID { get; set; }

        [ForeignKey("author")]
        public string AuthorID { get; set; }
        public ApplicationUser author { get; set; }

        [ForeignKey("subject")]
        public int SubjectID { get; set; }
        public Subject subject { get; set; }

        [ForeignKey("level")]
        public int LevelID { get; set; }
        public Level level { get; set; }

        [ForeignKey("examBoard")]
        public int ExamBoardID { get; set; }
        public ExamBoard examBoard { get; set; }

        [MaxLength(40, ErrorMessage = "Title must be 40 characters or less")]
        public string Title { get; set; }

        public DateTime UploadTime { get; set; }

        public void Default()
        {
            NoteID = 0;
            AuthorID = "0";
            SubjectID = 0;
            LevelID = 0;
            ExamBoardID = 0;
            Title = "AQA A-Level Computing";
        }
    }
}