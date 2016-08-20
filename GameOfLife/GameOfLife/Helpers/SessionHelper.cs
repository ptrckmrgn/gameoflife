using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfLife.Helpers
{
    public class SessionHelper
    {
        // Add a game to the session
        public static int AddTemplateToSession(Template template)
        {
            // Create new game
            Game game = new Game(template);
            
            // Get session games
            List<Game> games = GetAllGamesFromSession();

            // Increment ID
            if (games.Count > 0)
            {
                game.Id = games.Last().Id + 1;
            }
            else
            {
                game.Id = 1;
            }

            // Add game to session
            games.Add(game);
            HttpContext.Current.Session["games"] = games;

            return game.Id;
        }

        // Add a game to the session
        public static int AddGameToSession(Game game)
        {
            // Get session games
            List<Game> games = GetAllGamesFromSession();

            // Increment ID
            if (games.Count > 0)
            {
                game.Id = games.Last().Id + 1;
            }
            else
            {
                game.Id = 1;
            }

            // Add game to session
            games.Add(game);
            HttpContext.Current.Session["games"] = games;

            return game.Id;
        }

        // Get all games from the session
        public static List<Game> GetAllGamesFromSession()
        {
            List<Game> session = (List<Game>)HttpContext.Current.Session["games"];
            return session != null ? new List<Game>(session) : new List<Game>();
        }

        // Get game from the session by id
        public static Game GetGameFromSession(int id)
        {
            List<Game> games = GetAllGamesFromSession();

            Game game = games.Where(g => g.Id == id).FirstOrDefault();
            return game;
        }

        // Delete game from the session by id
        public static void DeleteGameFromSession(int id)
        {
            List<Game> games = GetAllGamesFromSession();

            Game game = games.Where(g => g.Id == id).FirstOrDefault();
            games.Remove(game);
            HttpContext.Current.Session["games"] = games;
        }

        // Count games in the session
        public static int CountGamesInSession()
        {
            List<Game> games = GetAllGamesFromSession();

            return games.Count;
        }
    }
}