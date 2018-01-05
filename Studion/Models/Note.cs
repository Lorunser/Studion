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
        //primary key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID { get; set; }

        //foreign keys and navigation properties
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

        //properties
        [MaxLength(40, ErrorMessage = "Title must be 40 characters or less")]
        public string Title { get; set; }

        public int Downloads { get; set; }

        public DateTime UploadTime { get; set; }

        //nav list properties
        public List<Rating> ratings { get; set; }
        public List<Comment> comments { get; set; }

        //methods
        public double GetAvRating()
        {
            double sum = 0;
            int n = ratings.Count();

            for (int i = 0; i < n; i++)
            {
                sum += ratings[i].Stars;
            }

            double av = sum / n;
            return av;
        }
    }
}