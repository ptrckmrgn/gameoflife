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

namespace GameOfLife.Controllers
{
    public class GamesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Show all games in the session
        // GET: Games
        public ActionResult Index()
        {
            // Get games from session
            List<Game> games = SessionHelper.GetAllGamesFromSession();

            ViewBag.Count = games.Count();

            return View(games);
        }

        // Play a template game
        // GET: Games/Load/{TemplateId}
        public ActionResult Load(int? id)
        {
            // Check template validity
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }

            int sessionId = SessionHelper.AddTemplateToSession(template);

            return RedirectToAction("Play", new { id = sessionId });
        }

        // Load game from session
        // GET: Games/Play/{SessionId}
        public ActionResult Play(int id)
        {
            Game game = SessionHelper.GetGameFromSession(id);

            return View(game);
        }

        // Take next turn of the template
        // POST: Games/NextTurn/{SessionId}
        [HttpPost]
        public string PlayTurn(int id)
        {
            string test = id.ToString();

            Game game = SessionHelper.GetGameFromSession(id);

            game.TakeTurn();

            return game.Cells;
        }

        // Delete a game from the session
        // GET: Games/Delete/{SessionId}
        public ActionResult Delete(int id)
        {
            // Get session games
            List<Game> games = SessionHelper.GetAllGamesFromSession();

            // Delete game from session
            SessionHelper.DeleteGameFromSession(id);

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
    }
}
