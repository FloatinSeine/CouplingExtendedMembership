using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coupling.Web.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profiles/

        public ActionResult Index()
        {
            return View();
        }

    }
}
