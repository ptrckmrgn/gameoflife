using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfLife.Controllers
{
    public class ErrorController : BaseController
    {
        // TODO : Error pages

        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        // GET: Error/PageNotFound
        public ActionResult PageNotFound()
        {
            return View();
        }

        // GET: Error/Forbidden
        public ActionResult Forbidden()
        {
            return View();
        }
    }
}
