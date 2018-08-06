using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Studion.Models;
using Studion.Dtos;

namespace Studion.Controllers.Api
{
    public class LevelController : ApiController
    {
        //database initialisation
        #region
        private ApplicationDbContext _context;

        public LevelController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // RETRIEVE
        public IHttpActionResult GetLevels()
        {
            IEnumerable<Level> levelsInDb = _context.Levels.ToList();
            List<LevelDto> levelsDto = new List<LevelDto>();

            foreach(var levelInDb in levelsInDb)
            {
                levelsDto.Add(new LevelDto(levelInDb));
            }

            return Ok(levelsDto);
        }
    }
}
