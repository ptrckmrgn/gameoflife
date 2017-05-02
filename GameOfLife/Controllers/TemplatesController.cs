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
using Newtonsoft.Json;

namespace GameOfLife.Controllers
{
    [Authorize]
    public class TemplatesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Templates
        [AllowAnonymous]
        public ActionResult Index(string search)
        {
            IQueryable<Template> templates = db.Templates.Include(u => u.User);

            if (!String.IsNullOrWhiteSpace(search))
            {
                templates = templates.Where(t => t.Name.Contains(search));
            }

            ViewBag.SearchTerm = search;
            ViewBag.Count = templates.Count();

            return View(templates);
        }

        // GET: Templates/Create
        public ActionResult Create()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        // POST: Templates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Height,Width,Cells,UserId")] Template template)
        {
            if (ModelState.IsValid)
            {
                // Assign user
                ApplicationUser user = db.Users.Find(template.UserId);
                template.User = user;

                // Convert cells to uppercase for consistency
                template.Cells = template.Cells.ToUpper(); 

                // Save template
                db.Templates.Add(template);
                db.SaveChanges();
                return RedirectToAction("MyTemplates");
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
            ViewBag.UserId = User.Identity.GetUserId();
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Height,Width,Cells,UserId")] Template template)
        {
            if (ModelState.IsValid)
            {
                // Assign user
                ApplicationUser user = db.Users.Find(template.UserId);
                template.User = user;

                db.Entry(template).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyTemplates");
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

            // Check user is authorized to delete
            string userId = User.Identity.GetUserId();
            if (template.User.Id != userId)
            {
                return RedirectToAction("Forbidden", "Error");
            }

            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = db.Templates.Find(id);
            
            // Check user is authorized to delete
            string userId = User.Identity.GetUserId();
            if (template.User.Id != userId)
            {
                return RedirectToAction("Forbidden", "Error");
            }

            // Delete
            db.Templates.Remove(template);
            db.SaveChanges();

            return RedirectToAction("MyTemplates");
        }

        // GET: Templates/MyTemplates
        public ActionResult MyTemplates()
        {
            string userId = User.Identity.GetUserId();
            IQueryable<Template> templates = db.Templates.Include(u => u.User).Where(u => u.User.Id == userId);

            ViewBag.Count = templates.Count();

            return View(templates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
