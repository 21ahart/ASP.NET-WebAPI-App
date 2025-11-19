using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using YamlDotNet.Serialization;

var builder = WebApplication.CreateBuilder(args);


//add controllers to handle requests
builder.Services.AddControllers();
//add NSwag/OpenAPI document generation
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "MyWebApi - Students API";
    config.Version = "v1";

});

//database config (use mysql via pomelo provider)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //serve swagger JSON
    app.UseOpenApi();
    //UI for our tests
    app.UseSwaggerUi(Settings =>
    {
        Settings.Path = "/swagger";
        Settings.DocumentPath = "/swagger/v1/swagger.json";
    });
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