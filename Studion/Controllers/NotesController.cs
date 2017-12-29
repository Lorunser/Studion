using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.Models;
using Studion.ViewModels;

namespace Studion.Controllers
{
    public class NotesController : Controller
    {
        // GET: Notes/{NoteID}
        // if NoteID not set then revert to default
        [Route("Notes/{NoteID?}")]
        public ActionResult Display(int? NoteID)
        {
            NotesDisplayViewModel ndvm = new NotesDisplayViewModel();

            if (NoteID == null)
            {
                ndvm.Default();
            }
            else
            {
                throw new NotImplementedException("Implement SQL queries to load notes page");
            }

            return View(ndvm);
        }

        // GET: Notes/Search
        public ActionResult Search()
        {
            throw new NotImplementedException();
        }
    }
}