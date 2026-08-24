using Microsoft.EntityFrameworkCore;
using MyNumsApi.Data;
using MyNumsApi.Endpoints;
using MyNumsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// MyNumsService depends on AppDbContext which is registered as scoped by AddDbContext.
// Register the service as scoped so it can consume the scoped DbContext.
builder.Services.AddScoped<IMyNumsService, MyNumsService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("MyNumsDB"));
});

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseDefaultFiles();   // serves index.html at "/"
app.UseStaticFiles();    // serves files from .3/wwwroot

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Use the CORS policy
app.MapMyNumsEndpoints();

app.Run();
