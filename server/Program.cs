using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using server.Data;
using server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Connects to the database
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register UserRepository for handling user data
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Add support for controllers
builder.Services.AddControllers();
//Enable API documentation with Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() // Accepts requests from anywhere
               .AllowAnyMethod() // Allows all HTTP methods (GET, POST, etc.)
               .AllowAnyHeader();// Allows all headers
    });
});

var app = builder.Build();

// Enable Swagger for API testing in developmente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS
app.UseAuthorization(); // Use authorization for protected routes

// Use CORS policy
app.UseCors("AllowAll");

app.MapControllers(); // Maps the controllers to the application
app.Run(); //starts the application