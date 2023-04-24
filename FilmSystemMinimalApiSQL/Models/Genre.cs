using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
