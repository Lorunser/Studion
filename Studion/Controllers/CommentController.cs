using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.Models;
using Studion.ViewModels;
using Microsoft.AspNet.Identity;

namespace Studion.Controllers
{
    public class CommentController : Controller
    {
        //database initialisation
        #region
        private ApplicationDbContext _context;

        public CommentController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: Comment/Save
        public ActionResult Save(NotesDisplayViewModel ndvm)
        {
            var userID = User.Identity.GetUserId();
            ndvm.SaveToDatabase(_context, userID);
            return View();
        }
    }
}