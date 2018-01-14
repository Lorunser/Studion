using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Level : IComparable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LevelID { get; set; }

        [Required]
        [MaxLength(40)]
        public string LevelName { get; set; }

        public int CompareTo(object obj)
        {
            Level other = obj as Level;
            return this.LevelName.CompareTo(other.LevelName);
        }
    }
}