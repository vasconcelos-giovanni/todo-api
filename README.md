# TodoAPI

## 📌 Project dea

TodoAPI is a REST API with basic CRUD operations for a Todo app, which I've made following the tutorial: [How to Build CRUD Operations with .NET Core – A Todo API Handbook](freecodecamp.org/news/build-cr87836a7f-7f61-4ff5-895a-02a1dd5ca594ud-operations-with-dotnet-core-handbook/#step-11), by [Isaiah Cliford Opoku](https://www.freecodecamp.org/news/author/isaiahcliffordopoku/).

## 💡 Key Features

- CRUD operations with meaningful responses.

## 🔧 Technologies

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

- ASP.NET Core 8
- Entity Framework

## 📚 My Learning Journey

I have come from Laravel and this project was my first experience with ASP.NET Core, building a controller-based REST API.

I have learned how to set up database context, to work with exception middleware and logging, contracts with DTOs (Data Transfer Objects), models with Entity Framework ORM, and to give meaningful HTTP responses.

## Setting up local enviroment

Dependencies:
- .NET ^8.0 SDK

After cloning the repository and entering its root directory, run the below for installing dependencies:
```sh
dotnet restore
```

Now set up `appsettings.json` and `appsettings.Development.json`.
```sh
mv example.appsettings.json appsettings.json

mv example.appsettings.Development.json appsettings.Development.json
```

Set up your database "ConnectionString" in `appsettings.json`.

Run the migrations.
```sh
dotnet ef database update
```

Run the API.
```sh
dotnet run
```

## My learning questions

> P.s.: These are personal questions for helping myself to improve.

- There are comments starting with "QUESTION!!!" throughout the project.
- Can I define manually my routes paths?
- How to see all my routes?
- What about seeders and factories?
- What about policies, authentication, and authorization?
- How to scape characters in JSON responses?