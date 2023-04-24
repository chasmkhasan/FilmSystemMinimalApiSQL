using FilmSystemMinimalApiSQL.Models;
using Microsoft.EntityFrameworkCore;
using static FilmSystemMinimalApiSQL.Models.PersonChoise;

namespace FilmSystemMinimalApiSQL.Data
{
    public class DataContext : DbContext
    {
        //Database connection
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonChoise> PersonChoises { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
