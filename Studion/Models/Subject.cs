using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }

        public string SubjectName { get; set; }

        public void Default()
        {
            SubjectID = 0;
            SubjectName = "Computing";
        }
    }
}