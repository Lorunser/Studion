using Studion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Studion.Dtos;
using System.Web;
using System.IO;
using Microsoft.AspNet.Identity;


namespace Studion.Controllers.Api
{
    public class NotesController : ApiController
    {
        private ApplicationDbContext _context;

        //constructor
        public NotesController()
        {
            _context = new ApplicationDbContext();
        }

        // CREATE
        #region
        [HttpPost]
        [Route("api/notes")]
        public IHttpActionResult CreateNote(NoteDto noteDto, HttpPostedFileBase upload)
        {
            //check valid
            if (!ModelState.IsValid)
                return BadRequest();

            //create note object
            var noteInDb = ToNoteInDb(noteDto, newNote: true);            

            //save to database
            _context.Notes.Add(noteInDb);
            _context.SaveChanges();

            //extract NoteID
            noteDto.NoteID = noteInDb.NoteID;

            //save file to server
            string path = GetPath(noteInDb.NoteID);
            upload.SaveAs(path);

            //return ok
            return Created(new Uri(Request.RequestUri + "/" + noteDto.NoteID), noteDto);
        }
        #endregion

        // RETRIEVE
        #region
        [HttpGet]
        [Route("api/notes/{noteID}")]
        public IHttpActionResult GetNote(int noteID)
        {
            //retrieve note from db
            Note noteInDb = GetFullNoteFromDb(noteID);

            //check exists
            if(noteInDb == null)
                return NotFound();

            //convert
            NoteDto noteDto = ToNoteDto(noteInDb);
            return Ok(noteDto);
        }

    
        [HttpGet]
        [Route("api/notes")]
        public IHttpActionResult GetNotes()
        {
            return SearchNotes(null, null, null);
        }

        [HttpGet]
        [Route("api/notes/search")]
        public IHttpActionResult SearchNotes(int? subjectID, int? examBoardID, int? levelID, string authorID = null)
        {
            //call to database
            var notesInDb = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings);

            //filters
            if (subjectID != null)
                notesInDb = notesInDb.Where(n => n.SubjectID == subjectID);

            if (examBoardID != null)
                notesInDb = notesInDb.Where(n => n.ExamBoardID == examBoardID);

            if (levelID != null)
                notesInDb = notesInDb.Where(n => n.LevelID == levelID);

            if (authorID != null)
                notesInDb = notesInDb.Where(n => n.AuthorID == authorID);

            //turn into list of Dto's
            IEnumerable<NoteDto> notesDto = ToNoteDtoList(notesInDb);

            //success
            return Ok(notesDto);
        }
        #endregion

        // UPDATE
        #region
        [HttpPut]
        [Route("api/notes")]
        public IHttpActionResult UpdateNote(NoteDto noteDto, HttpPostedFileBase upload = null)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // get note from database
            var noteInDb = _context.Notes.Single(n => n.NoteID == noteDto.NoteID);

            if (noteInDb == null)
                return NotFound();

            var identity = User.Identity;

            //allowed
            if (identity.GetUserId() == noteInDb.AuthorID)
            {
                //assign props
                noteInDb = ToNoteInDb(noteDto);

                //save to database
                _context.SaveChanges();

                //save file
                string path = GetPath(noteInDb.NoteID);
                upload.SaveAs(path);

                //return ok
                return Ok(noteInDb);
            }

            //denied
            return Unauthorized();
        }
        #endregion

        // DELETE
        #region
        // DELETE /api/notes/<id>
        [HttpDelete]
        [Route("api/notes/{noteID}")]
        public IHttpActionResult DeleteNote(int noteID)
        { 
            // get note from database
            var noteInDb = _context.Notes.Single(n => n.NoteID == noteID);

            // access allowed
            var identity = User.Identity;
            if(identity.GetUserId() == noteInDb.AuthorID) // also allow for admin
            {
                if (noteInDb == null)
                    NotFound();

                //delete file
                string path = GetPath(noteInDb.NoteID);
                File.Delete(path);

                //remove from db
                _context.Notes.Remove(noteInDb);
                _context.SaveChanges();

                //success
                return Ok();
            }

            //not allowed
            return Unauthorized();
        }
        #endregion

        // HELPER Methods
        #region
        private Note ToNoteInDb(NoteDto noteDto, bool newNote = false)
        {
            var noteInDb = new Note();

            //map foreign keys
            noteInDb.AuthorID = noteDto.AuthorID;
            noteInDb.SubjectID = noteDto.SubjectID;
            noteInDb.ExamBoardID = noteDto.ExamBoardID;
            noteInDb.LevelID = noteDto.LevelID;

            //map props
            noteInDb.Title = noteDto.Title;

            if(newNote)
            {
                //assign props not from Dto
                noteInDb.Downloads = 0;
                noteInDb.UploadTime = DateTime.Now;
            }

            return noteInDb;
        }

        private NoteDto ToNoteDto(Note noteInDb)
        {
            var noteDto = new NoteDto();

            //assign ID
            noteDto.NoteID = noteInDb.NoteID;

            //assign foreign keys
            noteDto.AuthorID = noteInDb.AuthorID;
            noteDto.AuthorName = noteInDb.author.UserName;

            noteDto.SubjectID = noteInDb.SubjectID;
            noteDto.SubjectName = noteInDb.subject.SubjectName;

            noteDto.ExamBoardID = noteInDb.ExamBoardID;
            noteDto.ExamBoardName = noteInDb.examBoard.ExamBoardName;

            noteDto.LevelID = noteInDb.LevelID;
            noteDto.LevelName = noteInDb.level.LevelName;

            //map props
            noteDto.Title = noteInDb.Title;
            noteDto.Downloads = noteInDb.Downloads;
            noteDto.UploadTime = noteInDb.UploadTime;

            //determine props
            noteDto.AverageRating = noteInDb.GetAvRating();

            //return
            return noteDto;
        }

        private Note GetFullNoteFromDb(int noteID)
        {
            var note = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings)
                .SingleOrDefault(n => n.NoteID == noteID);

            return note;
        }

        private IEnumerable<NoteDto> ToNoteDtoList(IEnumerable<Note> notesInDb)
        {
            //instantiate list
            List<NoteDto> notesDto = new List<NoteDto>();

            //iterate and convert
            foreach (var noteInDb in notesInDb)
            {
                notesDto.Add(ToNoteDto(noteInDb));
            }

            //return list
            return notesDto;
        }

        private string GetPath(int noteID)
        {
            string pathToSubDir = System.Web.Hosting.HostingEnvironment.MapPath("~/Documents/");
            return Path.Combine(pathToSubDir, Convert.ToString(noteID) + ".pdf");
        }
        #endregion
    }
}
