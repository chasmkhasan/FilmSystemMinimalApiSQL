using FilmSystemMinimalApiSQL.Data;
using FilmSystemMinimalApiSQL.Models;
using Microsoft.EntityFrameworkCore;
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
            app.MapGet("/", () => "Welcome to Film System Minimal API SQL and TMDB");

            ////Get all Genre Items
            //app.MapGet("/api/Genre", async (DataContext context) => await context.Genres.ToListAsync());

            ////Get Genre Items by id
            //app.MapGet("/api/Genre/{ID}", async (DataContext context, int ID) =>
            //    await context.Genres.FindAsync(ID) is Genre genre ? Results.Ok(genre) : Results.NotFound("Genre are not available./"));

            ////Create Genre Items 
            //app.MapPost("/api/Genre", async (DataContext context, Genre genre) =>
            //{
            //    context.Genres.Add(genre);
            //    await context.SaveChangesAsync();
            //    return Results.Created($"/api/genre/{genre.ID}", genre);
            //});

            ////Updating Genre Items
            //app.MapPut("/api/Genre/{ID}", async (DataContext context, Genre genre, int ID) =>
            //{
            //    var genreFromDb = await context.Genres.FindAsync(ID);

            //    if (genreFromDb != null)
            //    {
            //        genreFromDb.Title = genre.Title;
            //        genreFromDb.Description = genre.Description;

            //        await context.SaveChangesAsync();
            //        return Results.Ok(genre);
            //    }
            //    return Results.NotFound("genre not found");
            //});


            ////Deleting Genre Items
            //app.MapDelete("/api/Genre/{ID}", async (DataContext context, int ID) =>
            //{
            //    var genreFromDb = await context.Genres.FindAsync(ID);

            //    if (genreFromDb != null)
            //    {
            //        context.Remove(genreFromDb);
            //        await context.SaveChangesAsync();
            //        return Results.Ok();
            //    }
            //    return Results.NotFound("Genre not found");
            //});

            //Get all Person Items
            app.MapGet("/api/Person", async (DataContext context) => await context.Persons.ToListAsync());

            ////Get Person Items by id
            //app.MapGet("/api/Person/{ID}", async (DataContext context, int ID) =>
            //    await context.Persons.FindAsync(ID) is Person person ? Results.Ok(person) : Results.NotFound("Person are not Found./"));

            //Create Person Items 
            app.MapPost("/api/Person", async (DataContext context, Person person) =>
            {
                context.Persons.Add(person);
                await context.SaveChangesAsync();
                return Results.Created($"/api/person/{person.ID}", person);
            });

            ////Updating Person Items
            //app.MapPut("/api/Person/{ID}", async (DataContext context, Person person, int ID) =>
            //{
            //    var personFromDb = await context.Persons.FindAsync(ID);

            //    if (personFromDb != null)
            //    {
            //        personFromDb.Name = person.Name;
            //        personFromDb.Email = person.Email;

            //        await context.SaveChangesAsync();
            //        return Results.Ok(person);
            //    }
            //    return Results.NotFound("Person not found");
            //});


            ////Deleting Person Items
            //app.MapDelete("/api/Person/{ID}", async (DataContext context, int ID) =>
            //{
            //    var personFromDb = await context.Persons.FindAsync(ID);

            //    if (personFromDb != null)
            //    {
            //        context.Remove(personFromDb);
            //        await context.SaveChangesAsync();
            //        return Results.Ok();
            //    }
            //    return Results.NotFound("Person not found");
            //});

            //Get all PersonChoise Items
            //app.MapGet("/api/PersonChoise", async (DataContext context) => await context.PersonChoises.ToListAsync());

            //Get PersonChoise Items by id
            app.MapGet("/api/Person/{PersonID}/PersonChoise", async (DataContext context, int PersonID) =>
            {
                var choise = await context.PersonChoises.Where(x => x.PersonID == PersonID).ToListAsync();
                return Results.Ok(choise);
            });

            //Create PersonChoise Items 
            app.MapPost("/api/PersonChoise", async (DataContext context, PersonChoise personChoise) =>
                {
                    context.PersonChoises.Add(personChoise);
                    await context.SaveChangesAsync();
                    return Results.Created($"/api/PersonChoise/{personChoise.ID}", personChoise);
                });

            //Updating PersonChoise Items
            app.MapPut("/api/PersonChoise/{ID}", async (DataContext context, PersonChoise personChoise, int ID) =>
            {
                var personChoiseFromDb = await context.PersonChoises.FindAsync(ID);

                if (personChoiseFromDb != null)
                {
                    personChoiseFromDb.PersonID = personChoise.PersonID;
                    personChoiseFromDb.GenreID = personChoise.GenreID;

                    await context.SaveChangesAsync();
                    return Results.Ok(personChoise);
                }
                return Results.NotFound("PersonChoise not found");
            });


            ////Deleting PersonChoise Items
            //app.MapDelete("/api/PersonChoise/{ID}", async (DataContext context, int ID) =>
            //{
            //    var personChoiseFromDb = await context.PersonChoises.FindAsync(ID);

            //    if (personChoiseFromDb != null)
            //    {
            //        context.Remove(personChoiseFromDb);
            //        await context.SaveChangesAsync();
            //        return Results.Ok();
            //    }
            //    return Results.NotFound("PersonChoise not found");
            //});

            //Get all Movie Items
            app.MapGet("/api/Movie", async (DataContext context) => await context.Movies.ToListAsync());

            //Get Movie Items by id
            app.MapGet("/api/Person/{PersonID}/Movie", async (DataContext context, int PersonID) =>
            {
                var movieLink = await context.Movies.Where(x => x.PersonID == PersonID).ToListAsync();
                return Results.Ok(movieLink);
            });

            //Create Movie Items 
            app.MapPost("/api/Movie", async (DataContext context, Movie movie) =>
            {
                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return Results.Created($"/api/Movie/{movie.ID}", movie);
            });

            ////Updating Movie Items
            //app.MapPut("/api/Movie/{ID}", async (DataContext context, Movie movie, int ID) =>
            //{
            //    var movieFromDb = await context.Movies.FindAsync(ID);

            //    if (movieFromDb != null)
            //    {
            //        movieFromDb.Name = movie.Name;
            //        movieFromDb.Link = movie.Link;
            //        movieFromDb.Rating = movie.Rating;
            //        movieFromDb.PersonID = movie.PersonID;
            //        movieFromDb.GenreID = movie.GenreID;

            //        await context.SaveChangesAsync();
            //        return Results.Ok(movie);
            //    }
            //    return Results.NotFound("Movie not found");
            //});



            app.Run();
        }
    }
}