using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class Person
    {
        public Person()
        {
            Genres = new List<Genre>();
            Movies = new List<Movie>();
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual List<Genre> Genres { get; set; }
        public virtual List<Movie> Movies { get; set; }
    }
}
