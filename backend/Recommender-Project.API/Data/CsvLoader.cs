using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Recommender_Project.API.Data
{
    public static class CsvLoader
    {
        public static List<CollaborativeRecommendation> LoadCollaborativeRecommendations(string csvFilePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                MissingFieldFound = null, // Skip if field missing
                HeaderValidated = null    // Skip if header doesn't perfectly match
            };

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<CollaborativeRecommendation>().ToList();
            return records;
        }

        public static List<ContentRecommendation> LoadContentRecommendations(string csvFilePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<ContentRecommendation>().ToList();
            return records;
        }
    }
}