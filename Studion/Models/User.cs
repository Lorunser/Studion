using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studion.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public void Default()
        {
            UserID = 0;
            Username = "Lorunser";
        }
    }
}