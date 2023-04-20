using System.ComponentModel.DataAnnotations;

namespace FilmSystemMinimalApiSQL.Models
{
    public class UserChoice
    {
        [Key]
        public int ChoiceId { get; set; }
        public virtual UserList FkUserId { get; set; }
        public virtual GenreList FkGenreId { get; set; }
    }
}
