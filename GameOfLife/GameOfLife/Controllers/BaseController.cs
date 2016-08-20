using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfLife.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Load quote into viewbag
            IQueryable<Quote> quotes = db.Quotes.Where(q => q.Id > 0);
            int rnd = new Random().Next(1, quotes.Count() + 1);
            ViewBag.Quote = quotes.Where(q => q.Id == rnd).First();

            base.OnActionExecuting(filterContext);
        }
    }
}