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

        public RecommendationsController(
            List<CollaborativeRecommendation> collaborativeRecommendations,
            List<ContentRecommendation> contentRecommendations)
        {
            _collaborativeRecommendations = collaborativeRecommendations;
            _contentRecommendations = contentRecommendations;
        }

        // GET: api/Recommendations/collaborative
        [HttpGet("collaborative")]
        public IActionResult GetAllCollaborativeRecommendations()
        {
            var result = _collaborativeRecommendations.Select(r => new
            {
                articleId = r.articleId,
                article = r.article,
                recommendations = new List<string>
                {
                    r.recommendation1,
                    r.recommendation2,
                    r.recommendation3,
                    r.recommendation4,
                    r.recommendation5
                }
            }).ToList();

            return Ok(result);
        }

        // GET: api/Recommendations/collaborative/{articleId}
        [HttpGet("collaborative/{articleId}")]
        public IActionResult GetCollaborativeRecommendations(string articleId)
        {
            var recommendation = _collaborativeRecommendations
                .FirstOrDefault(r => r.articleId == articleId);

            if (recommendation == null)
            {
                return NotFound($"No collaborative recommendations found for article ID: {articleId}");
            }

            var result = new
            {
                articleId = recommendation.articleId,
                article = recommendation.article,
                recommendations = new List<string>
                {
                    recommendation.recommendation1,
                    recommendation.recommendation2,
                    recommendation.recommendation3,
                    recommendation.recommendation4,
                    recommendation.recommendation5
                }
            };

            return Ok(result);
        }

        // ✅ This one was missing before
        // GET: api/Recommendations/content
        [HttpGet("content")]
        public IActionResult GetAllContentRecommendations()
        {
            var result = _contentRecommendations.Select(r => new
            {
                contentId = r.contentId,
                recommendations = new List<string>
                {
                    r.recommendation1,
                    r.recommendation2,
                    r.recommendation3,
                    r.recommendation4,
                    r.recommendation5
                }
            }).ToList();

            return Ok(result);
        }

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
