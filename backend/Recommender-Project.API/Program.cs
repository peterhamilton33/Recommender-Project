using Recommender_Project.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Build paths to CSV files in the DataSets folder
var csvCollaborativePath = Path.Combine(builder.Environment.ContentRootPath, "DataSets", "collaborative_filtering_recommendations.csv");
var csvContentPath = Path.Combine(builder.Environment.ContentRootPath, "DataSets", "content_filtering_recommendations.csv");

// Load collaborative filtering recommendations
var collaborativeRecommendations = CsvLoader.LoadCollaborativeRecommendations(csvCollaborativePath);
// Load content filtering recommendations
var contentRecommendations = CsvLoader.LoadContentRecommendations(csvContentPath);

// Register both lists with DI
builder.Services.AddSingleton(collaborativeRecommendations);
builder.Services.AddSingleton(contentRecommendations);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
