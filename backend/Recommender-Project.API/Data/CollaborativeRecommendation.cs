namespace Recommender_Project.API.Data
{
    public class CollaborativeRecommendation
    {
        public string articleId { get; set; } = string.Empty;  // <- the actual ID from the CSV
        public string article { get; set; } = string.Empty;    // <- the title of the article
        public string recommendation1 { get; set; } = string.Empty;
        public string recommendation2 { get; set; } = string.Empty;
        public string recommendation3 { get; set; } = string.Empty;
        public string recommendation4 { get; set; } = string.Empty;
        public string recommendation5 { get; set; } = string.Empty;
    }
}