using Microsoft.AspNetCore.Mvc;
using Recommender_Project.API.Data;
using System.Collections.Generic;
using System.Linq;

namespace Recommender_Project.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly List<CollaborativeRecommendation> _collaborativeRecommendations;
        private readonly List<ContentRecommendation> _contentRecommendations;

        // Both recommendation lists are injected via DI.
        public RecommendationsController(
            List<CollaborativeRecommendation> collaborativeRecommendations,
            List<ContentRecommendation> contentRecommendations)
        {
            _collaborativeRecommendations = collaborativeRecommendations;
            _contentRecommendations = contentRecommendations;
        }

        // Endpoint for collaborative recommendations
        // GET: api/Recommendations/collaborative/{articleId}
        [HttpGet("collaborative/{articleId}")]
        public IActionResult GetCollaborativeRecommendations(string articleId)
        {
            var recommendation = _collaborativeRecommendations
                .FirstOrDefault(r => r.article == articleId);

            if (recommendation == null)
            {
                return NotFound($"No collaborative recommendations found for article ID: {articleId}");
            }

            var result = new List<string>
            {
                recommendation.recommendation1,
                recommendation.recommendation2,
                recommendation.recommendation3,
                recommendation.recommendation4,
                recommendation.recommendation5
            };

            return Ok(result);
        }

        // Endpoint for content recommendations
        // GET: api/Recommendations/content/{contentId}
        [HttpGet("content/{contentId}")]
        public IActionResult GetContentRecommendations(string contentId)
        {
            var recommendation = _contentRecommendations
                .FirstOrDefault(r => r.contentId == contentId);

            if (recommendation == null)
            {
                return NotFound($"No content recommendations found for content ID: {contentId}");
            }

            var result = new List<string>
            {
                recommendation.recommendation1,
                recommendation.recommendation2,
                recommendation.recommendation3,
                recommendation.recommendation4,
                recommendation.recommendation5
            };

            return Ok(result);
        }
    }
}
