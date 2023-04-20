using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string? MovieName { get; set; }
        public string? MovieLink { get; set; }
        public decimal Rating { get; set; }

        public virtual UserList FkUserId { get; set; }
        public virtual GenreList FkGenreId { get; set; }
    }
}
