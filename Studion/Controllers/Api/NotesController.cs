using Studion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

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
        public Note CreateNote(Note note)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Notes.Add(note);
            _context.SaveChanges();

            return note;
        }
        #endregion

        // RETRIEVE
        #region
        // GET /api/notes
        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes.ToList();
        }

        // GET /api/notes/<noteID>
        [HttpGet]
        public Note GetNote(int noteID)
        {
            var note = _context.Notes
                .Include(n => n.author)
                .Include(n => n.subject)
                .Include(n => n.level)
                .Include(n => n.examBoard)
                .Include(n => n.ratings)
                .SingleOrDefault(n => n.NoteID == noteID);

            if(note == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return note;
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
    }
}
