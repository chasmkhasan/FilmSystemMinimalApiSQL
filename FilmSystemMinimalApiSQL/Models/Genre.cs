using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class Genre
    {
        public Genre()
        {
            Movies = new List<Movie>();
            People = new List<Person>();
        }
        [Key]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public virtual List<Movie> Movies { get; }
        public virtual List<Person> People { get; }
    }
}
