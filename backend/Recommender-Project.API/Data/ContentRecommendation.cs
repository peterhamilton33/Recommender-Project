﻿using System.ComponentModel;

namespace Recommender_Project.API.Data
{
    public class ContentRecommendation
    {
        [TypeConverter(typeof(StringConverter))]
        public string contentId { get; set; }
        public string recommendation1 { get; set; }
        public string recommendation2 { get; set; }
        public string recommendation3 { get; set; }
        public string recommendation4 { get; set; }
        public string recommendation5 { get; set; }
    }
}
