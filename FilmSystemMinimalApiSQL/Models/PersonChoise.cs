using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class PersonChoise
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int GenreID { get; set; }

        public virtual Person Person { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
