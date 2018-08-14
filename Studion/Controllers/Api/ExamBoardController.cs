using Studion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Studion.Dtos;

namespace Studion.Controllers.Api
{
    public class ExamBoardController : ApiController
    {
        #region Database
        private ApplicationDbContext _context;

        public ExamBoardController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        #region RETRIEVE
        public IHttpActionResult GetExamBoards()
        {
            IEnumerable<ExamBoard> examBoardsInDb = _context.ExamBoards.ToList();
            List<ExamBoardDto> examBoardsDto = new List<ExamBoardDto>();

            foreach(var examBoardInDb in examBoardsInDb)
            {
                examBoardsDto.Add(new ExamBoardDto(examBoardInDb));
            }

            return Ok(examBoardsDto);
        }
        #endregion

        // IMPLEMENT CUD manually through database admin
    }
}
