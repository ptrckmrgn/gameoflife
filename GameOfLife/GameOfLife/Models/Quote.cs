using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameOfLife.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Author { get; set; }
    }
}