using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameOfLife.Models;
using GameOfLife.Helpers;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameOfLife.Controllers
{
    [Authorize]
    public class SavedGamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Show all the user's games in the database
        // GET: SavedGames
        public ActionResult Index()
        {
            // Get games from database (associated with user)
            string playerId = User.Identity.GetUserId();
            IQueryable<Game> games = db.Games.Where(u => u.PlayerId == playerId);

            foreach (Game game in games)
            {
                string userId = game.UserId;
                game.User = db.Users.Find(userId);
            }

            ViewBag.Count = games.Count();

            return View(games);
        }

        // Save a game from the session to the database (and remove from session)
        // GET: Games/Save/{SessionId}
        public ActionResult Save(int id)
        {
            // Get (and delete) game from session
            Game game;
            try
            {
                game = SessionHelper.GetGameFromSession(id);
                SessionHelper.DeleteGameFromSession(id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            // Assign user
            string playerId = User.Identity.GetUserId();
            game.PlayerId = playerId;

            // Remove game user because of validation bug - TODO: Fix validation bug...
            game.User = null;

            // Save template
            db.Games.Add(game);
            SaveChanges(db);

            // Add game user because of validation bug (see above
            string userId = game.UserId;
            game.User = db.Users.Find(userId);

            return RedirectToAction("Index");
        }

        // Move a saved game from the database to the session
        // GET: SavedGames/Resume/{SessionId}
        public ActionResult Reload(int? id)
        {
            // Check template validity
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Find and delete from database
            Game game = db.Games.Find(id);
            string userId = game.UserId;

            db.Games.Remove(game);
            db.SaveChanges();

            if (game == null)
            {
                return HttpNotFound();
            }

            // Re-add user (because of bug in Save() above)
            game.UserId = userId;
            game.User = db.Users.Find(userId);

            // Add to session
            int sessionId = SessionHelper.AddGameToSession(game);

            return RedirectToAction("Play", "Games", new { id = sessionId });
        }
        
        // Delete a saved game from the database
        // GET: SavedGames/Delete/5
        public ActionResult Delete(int? id)
        {
            // Check game validity
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            // Check user can delete
            string userId = User.Identity.GetUserId();
            if (game.User.Id != userId)
            {
                RedirectToAction("Forbidden", "Error");
            }

            // Delete from database
            db.Games.Remove(game);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
