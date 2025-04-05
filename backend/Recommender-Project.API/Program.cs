using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Recommender_Project.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS for any origin (you can restrict it later to specific domains)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Load CSV files
var collaborativeRecommendations = CsvLoader.LoadCollaborativeRecommendations("data/DataSets/collaborative_filtering_recommendations.csv");
var contentRecommendations = CsvLoader.LoadContentRecommendations("data/DataSets/content_filtering_recommendations.csv");

// Make recommendations accessible via DI
builder.Services.AddSingleton(collaborativeRecommendations);
builder.Services.AddSingleton(contentRecommendations);

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Serve static files from the "data" folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "data")),
    RequestPath = "/data"
});

app.UseAuthorization();
app.MapControllers();
app.Run();