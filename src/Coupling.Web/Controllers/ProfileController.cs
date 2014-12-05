using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Coupling.Web.Models.Profile;

namespace Coupling.Web.Controllers
{
    public class ProfileController : Controller
    {

        public ProfileController()
        {
            
        }

        //
        // GET: /Profiles/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Personal(PersonalInformationModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new SecurityException("Access denied. Not authenticated.");
            }

            if (ModelState.IsValid)
            {
                
            }

            return RedirectToAction("Index", "Profile");
        }

    }
}
