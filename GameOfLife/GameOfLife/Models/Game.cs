using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GameOfLife.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(1, 100)]
        public int Height { get; set; }

        [Required, Range(1, 100)]
        public int Width { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Cells { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PlayerId { get; set; }

        const char Alive = 'O';
        const char Dead = 'X';

        public Game()
        { }

        public Game(Template template)
        {
            this.Name = template.Name;
            this.Height = template.Height;
            this.Width = template.Width;
            this.Cells = template.Cells;
            this.User = template.User;
            this.UserId = template.User.Id;
        }

        //public Game(string playerId)
        //{
        //    PlayerId = playerId;
        //}

        // One returns the 'tick' of the game
        public void TakeTurn()
        {
            StringBuilder nextCells = new StringBuilder(Cells);

            // loop through each cell
            int pos;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    pos = j + (i * Width);
                    // get next state of each cell
                    nextCells[pos] = CellFate(i, j);
                }
            }

            // take turn
            Cells = nextCells.ToString();
        }

        // Checks the fate of a cell based on the game rules.
        public char CellFate(int row, int col)
        {
            int neighbours = 0;

            // loop through nine cells surrounding to determine live neighbours
            int pos;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    pos = (j + col) + ((i + row) * Width);
                    try
                    {
                        if (Cells[pos] == Alive)
                        {
                            neighbours++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { } // ignore out of range cells
                }
            }

            // return result based on rules:
            // 1. any live cell with fewer than two live neighbours dies, as if caused by under-population.
            // 2. any live cell with two or three live neighbours lives on to the next generation.
            // 3. any live cell with more than three live neighbours dies, as if by over-population.
            // 4. any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            if (Cells[col + (row * Width)] == Alive)
            {
                neighbours--; // adjust to account for cell being counted as neighbour

                if (neighbours < 2 || neighbours > 3)
                {
                    return Dead;
                }
                else
                {
                    return Alive;
                }
            }
            else // dead cell
            {
                if (neighbours == 3)
                {
                    return Alive;
                }
                else
                {
                    return Dead;
                }
            }
        }
    }
}