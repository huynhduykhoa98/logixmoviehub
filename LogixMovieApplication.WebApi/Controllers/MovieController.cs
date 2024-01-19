using LogixMovieApplication.WebApi.Interfaces;
using LogixMovieApplication.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogixMovieApplication.WebApi.Controllers
{
    [Authorize]
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IMovieReactionService _movieReactionService;
        private readonly ILogger<MovieController> _logger;
        public MovieController(ILogger<MovieController> logger, IMovieService movieService, IMovieReactionService movieReactionService) : base(logger)
        {
            _logger = logger;
            _movieService = movieService;
            _movieReactionService = movieReactionService;
        }

        [HttpGet]
        [Route("/api/movie/list")]
        public async Task<IActionResult> List()
        {
            _logger.LogInformation("GET Movie/List .{0}", DateTime.UtcNow.ToLongTimeString());
            try
            {
                var movies = await _movieService.ListMovie();
                var movieReactions = await _movieReactionService.GetMovieReactions(movies.Select(x => x.Id).ToList());
                var results = movies.Select(movie => new ViewModels.MovieVM()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    CreateDate = movie.CreatedDate,
                    Thumbnail = movie.Thumbnail,
                    DislikeCount = movieReactions.Where(react => react.MovieId == movie.Id && react.MovieReactionType == Enums.MovieReactionTypeEnum.Dislike).Count(),
                    LikeCount = movieReactions.Where(react => react.MovieId == movie.Id && react.MovieReactionType == Enums.MovieReactionTypeEnum.Like).Count(),
                    Disliked = movieReactions.Where(react => react.MovieId == movie.Id && react.MovieReactionType == Enums.MovieReactionTypeEnum.Dislike).Any(x => x.UserId == LoggedInUserId),
                    Liked = movieReactions.Where(react => react.MovieId == movie.Id && react.MovieReactionType == Enums.MovieReactionTypeEnum.Like).Any(x => x.UserId == LoggedInUserId),
                }); ;
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("GET Movie/List message:{0}. {1}", ex.Message, DateTime.UtcNow.ToLongTimeString());
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost]
        [Route("/api/movie/reaction")]
        public async Task<IActionResult> Reaction([FromBody] MovieReactionVM model)
        {
            try
            {
                _logger.LogInformation("GET Movie/Reaction UID: {0}.{1}", LoggedInUserId, DateTime.UtcNow.ToLongTimeString());
                await _movieReactionService.CreateOrUpdateReaction(LoggedInUserId, model.MovieId, model.MovieReactionType);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("POST Movie/Reaction message:{0}. {1}", ex.Message, DateTime.UtcNow.ToLongTimeString());
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
