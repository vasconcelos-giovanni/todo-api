
// program.cs
using TodoAPI.AppDataContext;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automapping (DTO -> Model)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Add this line

// User defined DI
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); // Add this line
builder.Services.AddSingleton<TodoDbContext>(); // Add this line

/* -------------------------------------------------------------------------- */
/*                            Error Handling: START                           */
/* -------------------------------------------------------------------------- */
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // Add this line

builder.Services.AddProblemDetails();  // Add this line

// Adding of login 
builder.Services.AddLogging();  //  Add this line
/* -------------------------------------------------------------------------- */
/*                             Error Handling: END                            */
/* -------------------------------------------------------------------------- */

var app = builder.Build();

{
    using var scope = app.Services.CreateScope(); // Add this line
    var context = scope.ServiceProvider; // Add this line
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
