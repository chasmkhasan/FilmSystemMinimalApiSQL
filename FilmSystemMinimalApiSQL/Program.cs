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
            app.MapGet("/", () => "Welcome to Film System Minimal API SQL and TMDB");

            //Get all GenreList Items
            app.MapGet("/api/GenreList", async (DataContext context) => await context.GenreLists.ToListAsync());

            //Get GenreList Items by id
            app.MapGet("/api/GenreList/{GenreId}", async (DataContext context, int GenreId) =>
                await context.GenreLists.FindAsync(GenreId) is GenreList genreList ? Results.Ok(genreList) : Results.NotFound("GenreList item not found ./"));

            //Create GenreList Items 
            app.MapPost("/api/GenreList", async (DataContext context, GenreList genreList) =>
            {
                context.GenreLists.Add(genreList);
                await context.SaveChangesAsync();
                return Results.Created($"/api/genreList/{genreList.GenreId}", genreList);
            });

            //Updating GenreList Items
            app.MapPut("/api/GenreList/{GenreId}", async (DataContext context, GenreList genreList, int GenreId) =>
            {
                var genreListFromDb = await context.GenreLists.FindAsync(GenreId);

                if (genreListFromDb != null)
                {
                    genreListFromDb.Title = genreList.Title;
                    genreListFromDb.Description = genreList.Description;

                    await context.SaveChangesAsync();
                    return Results.Ok(genreList);
                }
                return Results.NotFound("genreList not found");
            });


            //Deleting GenreList Items
            app.MapDelete("/api/GenreList/{GenreId}", async (DataContext context, int GenreId) =>
            {
                var genreListFromDb = await context.GenreLists.FindAsync(GenreId);

                if (genreListFromDb != null)
                {
                    context.Remove(genreListFromDb);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                return Results.NotFound("GenreList not found");
            });

            app.Run();
        }
    }
}