using Studion.Models;
using Studion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Studion.Controllers.Api
{
    public class SubjectController : ApiController
    {
        //database initialisation
        #region
        private ApplicationDbContext _context;

        public SubjectController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // RETRIEVE
        public IHttpActionResult GetSubjects()
        {
            IEnumerable<Subject> subjectsInDb = _context.Subjects.ToList();
            List<SubjectDto> subjectsDto = new List<SubjectDto>();

            foreach (var subjectInDb in subjectsInDb)
            {
                subjectsDto.Add(new SubjectDto(subjectInDb));
            }

            return Ok(subjectsDto);
        }
    }
}
