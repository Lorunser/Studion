﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class ExamBoard
    {
        public int ExamBoardID { get; set; }

        public string ExamBoardName { get; set; }

        public void Default()
        {
            ExamBoardID = 0;
            ExamBoardName = "AQA";
        }
    }
}