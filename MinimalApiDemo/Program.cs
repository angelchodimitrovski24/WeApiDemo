using MinimalApiDemo;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Dummy user data
var users = new List<Users>
        {
            new Users { Id = 1, Name = "Gabriel", Age = 30, Email = "gabriel@example.com" },
            new Users { Id = 2, Name = "Hristina", Age = 36, Email = "hristina@example.com" },
            new Users { Id = 3, Name = "Angelcho", Age = 40, Email = "angelchod@example.com"}
        };

// Endpoint to get all users
app.MapGet("/users", () => users);

// Endpoint to get an individual user by ID
app.MapGet("/users/{id}", (HttpContext context) =>
{
    int id = int.Parse(context.Request.RouteValues["id"].ToString());
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user != null)
    {
        return context.Response.WriteAsJsonAsync(user);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return context.Response.WriteAsync("User not found.");
    }
});

app.Run();