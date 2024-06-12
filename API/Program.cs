using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicatiionServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//(Exception handling middleware)
app.UseMiddleware<ExceptionMiddleware>();


app.UseHttpsRedirection();


//2 Access to the data from the API
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
//Do you have valid token
app.UseAuthentication();
//Ok now you have valid token now what are you allow to do
app.UseAuthorization();

app.MapControllers();


//Access to All the services that we have in the program classes
using var scop = app.Services.CreateScope();
var services = scop.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch(Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
