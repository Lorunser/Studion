using Studion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Studion.Dtos;

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
        // POST /api/notes
        [HttpPost]
        public NoteDto CreateNote(NoteDto noteDto)
        {
            //check valid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //instantiate note object
            var noteInDb = new Note();

            //map properties
            noteInDb.AuthorID = noteDto.AuthorID;
            noteInDb.SubjectID = noteDto.SubjectID;
            noteInDb.ExamBoardID = noteDto.ExamBoardID;
            noteInDb.LevelID = noteDto.LevelID;

            noteInDb.Title = noteDto.Title;

            //assign props
            noteInDb.Downloads = 0;
            noteInDb.UploadTime = DateTime.Now;

            //save to database
            _context.Notes.Add(noteInDb);
            _context.SaveChanges();

            //extract NoteID
            noteDto.NoteID = noteInDb.NoteID;

            return noteDto;
        }
        #endregion

        // RETRIEVE
        #region
        // GET /api/notes
        [HttpGet]
        public IEnumerable<NoteDto> GetNotes()
        {
            return _context.Notes.ToList();
        }

        // GET /api/notes/<noteID>
        [HttpGet]
        public NoteDto GetNote(int noteID)
        {
            Note noteInDb = GetFullNoteFromDb(noteID);
            NoteDto noteDto = ToNoteDto(noteInDb);
            return noteDto;
        }

        //Determine route
        [HttpGet]
        public IEnumerable<Note> SearchNotes(int? subjectID, int? examBoardID, int? levelID)
        {
            //call to database
            var notes = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings);

            //filters
            if (subjectID != null)
                notes = notes.Where(n => n.SubjectID == subjectID);

            if (examBoardID != null)
                notes = notes.Where(n => n.ExamBoardID == examBoardID);

            if (levelID != null)
                notes = notes.Where(n => n.LevelID == levelID);

            //turn into list
            return notes.ToList();
        }
        #endregion

        // UPDATE
        #region
        // PUT /api/notes/<id>
        [HttpPut]
        public Note UpdateNote(int noteID, Note note)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // get note from database
            var noteInDb = _context.Notes.Single(n => n.NoteID == noteID);

            if (noteInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            noteInDb.ExamBoardID = note.ExamBoardID;
            noteInDb.LevelID = note.LevelID;
            noteInDb.SubjectID = note.SubjectID;
            noteInDb.Title = note.Title;

            _context.SaveChanges();
            return noteInDb;

            //remember to update file
        }
        #endregion

        // DELETe
        #region
        // DELETE /api/notes/<id>
        [HttpDelete]
        public void DeleteNote(int noteID)
        {
            // get note from database
            var noteInDb = _context.Notes.Single(n => n.NoteID == noteID);

            if (noteInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Notes.Remove(noteInDb);
            _context.SaveChanges();

            //remember to delete file
        }
        #endregion

        // HELPER Methods
        #region
        private static Note ToNoteInDb(NoteDto noteDto)
        {
            var noteInDb = new Note();

            //map foreign keys
            noteInDb.AuthorID = noteDto.AuthorID;
            noteInDb.SubjectID = noteDto.SubjectID;
            noteInDb.ExamBoardID = noteDto.ExamBoardID;
            noteInDb.LevelID = noteDto.LevelID;

            //map props
            noteInDb.Title = noteDto.Title;

            //assign props not from Dto
            noteInDb.Downloads = 0;
            noteInDb.UploadTime = DateTime.Now;

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

            if (note == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return note;
        }
        #endregion
    }
}
