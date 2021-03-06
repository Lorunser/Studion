﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.Models;
using Studion.ViewModels;
using System.Data.Entity; // get include working
using Microsoft.AspNet.Identity;
using System.IO;

namespace Studion.Controllers
{
    public class NotesController : Controller
    {
        #region Database
        private ApplicationDbContext _context;

        public NotesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion 

        // GET: Notes/Display/{NoteID}
        [Route("Notes/Display/{NoteID}")]
        public ActionResult Display(int NoteID)
        {
            NotesDisplayViewModel ndvm = new NotesDisplayViewModel(NoteID, _context);

            //code to load rating
            if (Request.IsAuthenticated)
            {
                var raterID = User.Identity.GetUserId();
                int stars;

                try
                {
                    Rating rating = _context.Ratings.Single(r => r.NoteID == NoteID && r.RaterID == raterID);
                    stars = rating.Stars;
                }
                catch
                {
                    // no rating made
                    stars = 0;
                }

                ndvm.Rating = stars;
            }

            return View(ndvm);
        }

        // GET: Notes/Download/{NoteID}
        [Route("Notes/Download/{NoteID}")]
        public ActionResult Download(int NoteID)
        {
            //returns file for download
            string filename = NoteID + ".pdf";

            string pathToSubDir = ControllerContext.HttpContext.Server.MapPath("~/Documents/");
            string filepath = Path.Combine(pathToSubDir, filename);

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = false
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            //if user is pressing download button rather than google docs then increment download counter
            if(Request.IsAuthenticated)
            {
                var note = _context.Notes.Single(n => n.NoteID == NoteID);
                note.Downloads++; // increment
                _context.SaveChanges();
            }

            return File(filedata, contentType);
        }

        // GET: Notes/Search
        // first time called
        public ActionResult Search()
        {
            var nsvm = new NotesSearchViewModel(_context);
            return View(nsvm);
        }

        // GET: Notes/Upload
        // first time called
        [Authorize]
        public ActionResult Upload()
        {
            var viewModel = new NoteFormViewModel(_context);
            return View(viewModel);
        }

        //on form submit
        [HttpPost]
        [Authorize]
        public ActionResult Upload(NoteFormViewModel nfvm, HttpPostedFileBase upload = null)
        {

            if (ModelState.IsValid)
            {
                if (upload.ContentLength < 20000000) // ContentLength returns integer number of bytes >> fail if exceeded 20 million
                {
                    string pathToSubDir = ControllerContext.HttpContext.Server.MapPath("~/Documents/");
                    nfvm.SaveToDatabase(_context, User.Identity, upload, pathToSubDir);
                    return RedirectToAction("Display", "Notes", new { NoteID = nfvm.Note.NoteID });
                }
                else
                {
                    ModelState.AddModelError("", "Exceeded maximum content length");
                }
            }

            nfvm.GenLists(_context); // must be called to repopulate lists
            return View(nfvm);
        }


        // GET: Notes/Edit/{NoteID}
        [Route("Notes/Edit/{NoteID}")]
        [Authorize]
        public ActionResult Edit(int NoteID)
        {
            var note = _context.Notes.Single(n => n.NoteID == NoteID);

            if (note == null) // no note with given id
            {
                return HttpNotFound();
            }

            var identity = User.Identity;
            var userID = identity.GetUserId();
            if (userID == note.AuthorID) // add additional or statemenet for if admin
            {
                //get from db
                var nfvm = new NoteFormViewModel(_context);
                //set properties
                nfvm.SetProps(note);
                //return view
                return View("Upload", nfvm);
            }

            return HttpNotFound(); // forbidden page would be better
        }
    }
}