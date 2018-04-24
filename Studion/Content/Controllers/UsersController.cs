using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.ViewModels;
using Studion.Models;

namespace Studion.Controllers
{
    public class UsersController : Controller
    {
        //database initialisation
        #region
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        // GET: Users/{UserID}
        [Route("Users/{UserID}")]
        public ActionResult ProfilePage(string UserID)
        {
            UsersProfileViewModel upvm = new UsersProfileViewModel(UserID, _context);
            return View(upvm);
        }
    }
}