﻿using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public decimal Rating { get; set; }
        public int PersonID { get; set; }
        public int GenreID { get; set; }

        public virtual Person Person { get; set; }
        public virtual Genre Genre { get; set; }

    }
}
