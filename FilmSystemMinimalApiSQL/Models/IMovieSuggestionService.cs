namespace FilmSystemMinimalApiSQL.Models
{
    public interface IMovieSuggestionService
    {
        Task<IEnumerable<string>> GetMovieSuggestions(int genreId);
    }
}
