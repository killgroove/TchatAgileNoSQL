using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TchatAgileNoSQL.Controllers
{
    public class TchatController : Controller
    {
        // GET: Tchat
        public ActionResult Index()
        {
            return View();
        }
    }
}