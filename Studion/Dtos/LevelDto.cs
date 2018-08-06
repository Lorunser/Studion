using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studion.Models;

namespace Studion.Dtos
{
    public class LevelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public LevelDto(Level levelInDb)
        {
            this.Id = levelInDb.LevelID;
            this.Name = levelInDb.LevelName;
        }
    }
}