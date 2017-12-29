using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Studion.ViewModels;

namespace Studion.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users/{UserID?}
        [Route("Users/{UserID?}")]
        public ActionResult ProfilePage(int? UserID)
        {
            UsersProfileViewModel upvm = new UsersProfileViewModel();

            if (UserID == null)
            {
                upvm.Default();
            }
            else
            {
                throw new NotImplementedException("Implement SQL queries to load user page");
            }

            return View(upvm);
        }
    }
}