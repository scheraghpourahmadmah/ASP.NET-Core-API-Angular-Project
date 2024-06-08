using API.Extensions;
using API.Middleware;

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

app.Run();
