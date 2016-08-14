using GameOfLife.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfLife.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string search)
        {
            IQueryable<Template> templates = db.Templates.Include(u => u.User);

            if (!String.IsNullOrWhiteSpace(search))
            {
                templates = templates.Where(t => t.Name.Contains(search));
            }

            ViewBag.SearchTerm = search;

            return View(templates);
        }

        public ActionResult Rules()
        {
            return View();
        }
    }
}