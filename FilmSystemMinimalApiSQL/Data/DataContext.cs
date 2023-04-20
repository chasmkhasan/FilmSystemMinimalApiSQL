using FilmSystemMinimalApiSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemMinimalApiSQL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<GenreList> GenreLists => Set<GenreList>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<UserChoice> UserChoices => Set<UserChoice>();
        public DbSet<UserList> UserLists => Set<UserList>();

    }
}
