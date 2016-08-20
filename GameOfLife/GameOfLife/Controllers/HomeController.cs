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

        public ActionResult Index(bool? redirect)
        {
            if (redirect.HasValue && redirect.Value)
            {
                return RedirectToActionPermanent("Index", new { redirect = null as object });
            }

            return View();
        }
    }
}