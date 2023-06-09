﻿using FilmSystemMinimalApiSQL.Data;
using FilmSystemMinimalApiSQL.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace FilmSystemMinimalApiSQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSingleton<IMovieSuggestionService, MovieSuggestionService>();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
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

            //Get all Person from Syetem
            app.MapGet("/api/AllPerson", async (DataContext context) => await context.Persons.ToListAsync());


            //Get all genres associated with a specific person by ID
            app.MapGet("/api/Person/{PersonID}/PersonChoise", async (DataContext context, int PersonID) =>
             {
                 var choise = await context.PersonChoises.Where(x => x.PersonID == PersonID).ToListAsync();
                 return Results.Ok(choise);
             });

            //Get all videos linked to a specific person
            app.MapGet("/api/Person/{PersonID}/Movie",
            async (DataContext context, int PersonID) =>
            {
                var movieLink = await context.Movies.Where(x => x.PersonID == PersonID).ToListAsync();
                return Results.Ok(movieLink);
            });

            // Enter "rating" on films linked to a person
            // "/api/Person/{personId}/Add/Movie/{movieId}/rating{rating}
            app.MapPost("/api/Movie/rating/{rating}", async (DataContext context, int personId, int movieId, decimal rating) =>
            {
                var movie = context.Movies.Where(x => x.PersonID == personId && x.ID == movieId).FirstOrDefault();
                movie.Rating = rating; 
                context.Movies.Update(movie);
                await context.SaveChangesAsync();
                return Results.Created($"/api/Person/{movie.PersonID}/Add/Movie/{movie.ID}/rating{movie.Rating}", movie);
            });

            // Retrieve  "rating" on films linked to a person
            app.MapGet("/api/Movie/{ID}/Person/{personId}", async (DataContext context, int ID, int personId) =>
            {
                var movie = context.Movies.Where(x => x.PersonID == personId && x.ID == ID).FirstOrDefault();

                return movie is Movie mov ? Results.Ok(mov) : Results.NotFound("Genre are not available./");
            });

            //Connect a person to a new genre
            app.MapPost("/api/Person/PersonChoise", async (DataContext context, string Name, int GenreID) =>
            {
                var person = await context.Persons.SingleOrDefaultAsync(p => p.Name == Name);
                if (person == null)
                {
                    return Results.NotFound();
                }

                var personSelect = context.PersonChoises;
                personSelect.Add(new PersonChoise { PersonID = person.ID, GenreID = GenreID });
                await context.SaveChangesAsync();
                return Results.Created($"/api/Person/PersonChoise{person.ID}", person);
            });

            //Insert new links for a specific person and a specific genre
            app.MapPost("/api/MovieLink", async (DataContext context, string movieName, string link, int personId, int genreId) =>
            {
                var movieEnter = await context.Movies.SingleOrDefaultAsync(p => p.Name == movieName);

                var movieLink = await context.Movies.SingleOrDefaultAsync(p => p.Link == link);

                var personSelect = await context.Persons.SingleOrDefaultAsync(p => p.ID == personId);
                if (personSelect == null)
                {
                    return Results.NotFound();
                }

                var genreSelect = await context.Genres.SingleOrDefaultAsync(p => p.ID == genreId);
                if (genreSelect == null)
                {
                    return Results.NotFound();
                }

                var insertMovieLink = context.Movies;
                insertMovieLink.Add(new Movie { Name = movieName, Link = link, PersonID = personId, GenreID = genreId} );
                await context.SaveChangesAsync();
                return Results.Created($"/api/MovieLink", link);
            });

            //TMDB-Inq
            app.MapGet("/tmdb/api/moviesuggestions/{genreId}", async (IMovieSuggestionService movieSuggestionService, int genreId) =>
            {
                var suggestions = await movieSuggestionService.GetMovieSuggestions(genreId);
                dynamic data = JsonConvert.SerializeObject(suggestions);
                return data;
            });

            app.Run();
        }

    }
}