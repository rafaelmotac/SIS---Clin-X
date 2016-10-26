using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMC.TCC.Clinica.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Acesso()
        {
            return View();
        }
    }
}