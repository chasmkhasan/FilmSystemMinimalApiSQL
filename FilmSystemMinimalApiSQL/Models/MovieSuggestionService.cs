using Newtonsoft.Json;
using System.Text.Json;

namespace FilmSystemMinimalApiSQL.Models
{
    public class MovieSuggestionService : IMovieSuggestionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public MovieSuggestionService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            //_httpClient.BaseAddress = new Uri(_config["TMDB:BaseUrl"]);
        }

        public async Task<IEnumerable<string>> GetMovieSuggestions(int genreId)
        {
            var suggestions = new List<string>();
            string url = $"https://api.themoviedb.org/3/discover/movie?api_key={_config.GetConnectionString("b15bc7bd4c5ec20893fdabc51b2dbe5f")}&with_genres={genreId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MovieResponse>(jsonContent);

                foreach (var movie in data.Results)
                {
                    suggestions.Add(movie.Title);
                }
            }

            return suggestions;
        }

        public class MovieResponse
        {
            public int Page { get; set; }
            public List<MovieResult> Results { get; set; }
            public int TotalPages { get; set; }
            public int TotalResults { get; set; }
        }

        public class MovieResult
        {
            public bool Adult { get; set; }
            public string BackdropPath { get; set; }
            public List<int> GenreIds { get; set; }
            public int Id { get; set; }
            public string OriginalLanguage { get; set; }
            public string OriginalTitle { get; set; }
            public string Overview { get; set; }
            public double Popularity { get; set; }
            public string PosterPath { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Title { get; set; }
            public bool Video { get; set; }
            public double VoteAverage { get; set; }
            public int VoteCount { get; set; }
        }
    }
}
