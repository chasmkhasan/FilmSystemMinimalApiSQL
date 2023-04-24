# Filmsystemet or FilmSystem
This project has been created for understanding of different type of API(Application Programming Interface). This project has fully focused on REST Architecture but project has been created by Minimal API but had option to created by MVC as well. 
In this project has SQL-Database and has been taken API key from TMDB(Millions of movies, TV shows and people to discover. Explore now.)

# Structure of Code:
Project has been generated by 3 layers of logic. Those are Models, Data, Business logic. Since project has been created through Entity Framework so, data migration has been interconnected.

## Structure of Project:
|   Tasks     |   Framwork    |  Effect  |
|-----|--------|-------|
|Database |   SQL   | Database has been created through EF Migration
|Model | VS C# & .NET Core 6   | GenreList, Movie, UserChoice, UserList
|Connection |  JSON   |  LocalHost Server
|Web-API |    REST-API(Minimal)     |  Get, Post, Put, Delete
|Demon-Loan |    Swagger     |  WeatherForecast

# Dependencies:
=> MicrosoftEntityFrameworkCore (6)
=> MicrosoftEntityFrameworkSQLServer (6)
=> MicrosoftEntityFrameworkTools (6)
=> Newtonsoft.Json (13)

#Database Diagram is below:
![Database Diagram](https://github.com/chasmkhasan/FilmSystemMinimalApiSQL/blob/bc786c2422e89b88cdcdc73eae9947f14881f37c/FilmSystemMinimalApiSQL/FilmSystemDiagram.jpeg)

# Test API via Insomnia
=> Get all people in the system
https://localhost:7159/api/AllPerson - Get

=>Get all genres associated with a specific person
https://localhost:7159/api/Person/1/PersonChoise - Get

=>Download all videos linked to a specific person
https://localhost:7159/api/Person/1/Movie - Get

=>Enter and Retrieve "rating" on films linked to a person
https://localhost:7159/api/Person/1/Add/Movie/1/rating6 - Post
https://localhost:7159/api/Movie/1/Person/1 - Get

=>Connect a person to a new genre
https://localhost:7159/api/Person/PersonChoise?Name=MK&GenreID=1 - Post

=>Insert new links for a specific person and a specific genre
https://localhost:7159/api/Movie - Post

# Reflection:
Minimal API is primery stag of learning and understanding of API. MVC is much more secure and profeeisonal. I have learned how does work about API. In this project API has been called only primerykey that is why I have faced problem to get other information. I couldn't reached how to call foreign key in my business logic. I will implement and try to sort it out foreign key request and will try to focused on MVC. 
I haven't done repository pattern yet because this project has very little time understand. I will focused on.

## Creator: Welcome to visit my link:

- [MK Hasan - Github](https://github.com/chasmkhasan)
- [MK Hasan - LinkedIN](linkedin.com/in/md-kamrul-hasan-b72b1931)
- [MK Hasan - WebPage](chasmkhasan.github.io/Dynamic-CV/)


