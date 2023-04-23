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

            //Get all Person Information Items
            app.MapGet("/api/UserList", async (DataContext context) => await context.UserLists.ToListAsync());

            //Create New Person Added Items 
            app.MapPost("/api/UserList/Add New Person", async (DataContext context, UserList userList) =>
            {
                context.UserLists.Add(userList);
                await context.SaveChangesAsync();
                return Results.Created($"/api/userList/{userList.UserId}", userList);
            });


           







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

            

            

            //Get User Choice Items by id
            app.MapGet("/api/UserChoice/{ChoiceId}", async (DataContext context, int ChoiceId) =>
                await context.UserChoices.FindAsync(ChoiceId) is UserChoice userChoice ? Results.Ok(userChoice) : Results.NotFound("User is not found ./"));

            //Get User Choice Items by FkUserId
            app.MapGet("/api/UserChoice/{FkUserIdUserId}", async (DataContext context, int FkUserIdUserId) =>
                await context.UserChoices.FindAsync(FkUserIdUserId) is UserChoice userChoice ? Results.Ok(userChoice) : Results.NotFound("User is not found ./"));

            ////Updating User Choice Items
            //app.MapPut("/api/UserChoice/{FkUserIdUserId}", async (DataContext context, UserChoice userChoice, int FkUserIdUserId) =>
            //{
            //    var userChoiceFromDb = await context.UserChoices.FindAsync(FkUserIdUserId);

            //    if (userChoiceFromDb != null)
            //    {
            //        userChoiceFromDb.ChoiceId = userChoice.ChoiceId;
            //        userChoiceFromDb.FkUserId = userChoice.FkUserId;
            //        userChoiceFromDb.FkGenreId = userChoice.FkGenreId;

            //        //await context.SaveChangesAsync();
            //        return Results.Ok(userChoice);
            //    }
            //    return Results.NotFound("userChoice not found");
            //});

            //Get all Movie List
            app.MapGet("/api/Movie store", async (DataContext context) => await context.Movies.ToListAsync());

            //Get MovieList Items by id
            app.MapGet("/api/Movie/{MovieId}", async (DataContext context, int MovieId) =>
                await context.Movies.FindAsync(MovieId) is Movie movie ? Results.Ok(movie) : Results.NotFound("Movie is not found ./"));

            app.Run();
        }
    }
}