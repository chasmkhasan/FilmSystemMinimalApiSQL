using FilmSystemMinimalApiSQL.Data;
using FilmSystemMinimalApiSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemMinimalApiSQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //Get to Try out the routing
            app.MapGet("/Welcome Note or Greeting", () => "Welcome to Film System Minimal API SQL and TMDB");

            //Get all people in the system
            app.MapGet("/api/UserList", async (DataContext context) => await context.UserLists.ToListAsync());

            //Create New Person Added Items 
            app.MapPost("/api/UserList/Add New Person", async (DataContext context, UserList userList) =>
            {
                context.UserLists.Add(userList);
                await context.SaveChangesAsync();
                return Results.Created($"/api/userList/{userList.UserId}", userList);
            });

            //Get see Rating Items
            app.MapGet("/api/Movie/Rating Status/Person", async (DataContext context) => await context.Movies.ToListAsync());

            //Add Movie Link Connection with spacific person and spacific genre.
            app.MapPost("/api/Movie/Add Link and Give Rating", async (DataContext context, Movie movie) =>
            {
                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return Results.Created($"/api/movie/{movie.MovieId}", movie);
            });

            //Get UserChoice Items by id
            app.MapGet("/api/UserChoice/{ChoiceId}", async (DataContext context, int ChoiceId) =>
                await context.UserChoices.FindAsync(ChoiceId) is UserChoice userChoice ? Results.Ok(userChoice) : Results.NotFound("UserChoice item not found ./"));

            //Get Movie Items by id
            app.MapGet("/api/Movie/{MovieId}", async (DataContext context, int MovieId) =>
                await context.Movies.FindAsync(MovieId) is Movie movie ? Results.Ok(movie) : Results.NotFound("Movie not found ./"));

            app.Run();
        }
    }
}