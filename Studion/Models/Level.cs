using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class Level
    {
        public int LevelID { get; set; }

        public string LevelName { get; set; }

        public void Default()
        {
            LevelID = 0;
            LevelName = "A-Level";
        }
    }
}