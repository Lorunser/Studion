using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Note
    {
        [Key]
        public int NoteID { get; set; }

        public int AuthorID { get; set; }
        public int SubjectID { get; set; }
        public int LevelID { get; set; }
        public int ExamBoardID { get; set; }
          
        public string Title { get; set; }

        public DateTime UploadTime { get; set; }

        public void Default()
        {
            NoteID = 0;
            AuthorID = 0;
            SubjectID = 0;
            LevelID = 0;
            ExamBoardID = 0;
            Title = "AQA A-Level Computing";
        }
    }
}