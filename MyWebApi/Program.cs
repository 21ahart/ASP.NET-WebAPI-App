using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
//add controllers to handle requests
builder.Services.AddControllers();

//database config (use mysql via pomelo provider)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// use https for data security in transit
app.UseHttpsRedirection();
//send http request to appropiate controller
app.MapControllers();

/*
 app.MapGet("/api/data", () =>
{
    return Results.Json(new { });
})
.WithName("GetData");
*/

app.Run();