using System.Globalization;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Recommender_Project.API.Data
{
    public static class CsvLoader
    {
        public static List<CollaborativeRecommendation> LoadCollaborativeRecommendations(string csvFilePath)
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CollaborativeRecommendation>().ToList();
            return records;
        }

        public static List<ContentRecommendation> LoadContentRecommendations(string csvFilePath)
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<ContentRecommendation>().ToList();
            return records;
        }
    }
}
