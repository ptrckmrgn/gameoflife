using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameOfLife.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GameOfLife.Controllers
{
    public class TemplatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Templates
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

        // GET: Templates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // GET: Templates/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Height,Width,Cells")] Template template)
        {
            if (ModelState.IsValid)
            {
                // Assign user
                string userId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userId);
                template.User = user;

                db.Templates.Add(template);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(template);
        }

        // GET: Templates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Height,Width,Cells")] Template template)
        {
            if (ModelState.IsValid)
            {
                db.Entry(template).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(template);
        }

        // GET: Templates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = db.Templates.Find(id);
            db.Templates.Remove(template);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Templates/MyTemplates
        public ActionResult MyTemplates(string search)
        {
            string userId = User.Identity.GetUserId();
            IQueryable<Template> templates = db.Templates.Include(u => u.User).Where(u => u.User.Id == userId);

            if (!String.IsNullOrWhiteSpace(search))
            {
                templates = templates.Where(t => t.Name.Contains(search));
            }

            ViewBag.Context = "MyTemplates";
            ViewBag.SearchTerm = search;

            return View("Index", templates);
        }
    }
}
