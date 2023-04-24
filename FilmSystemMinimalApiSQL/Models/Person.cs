using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
