using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class GenreList
    {
        [Key]
        public int GenreId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
