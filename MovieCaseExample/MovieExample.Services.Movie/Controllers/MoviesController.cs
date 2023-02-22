using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieExample.Services.Movie.Dtos;
using MovieExample.Services.Movie.Services;
using MovieExample.Shared.Messages;
using System.Text;

namespace MovieExample.Services.Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MoviesController(IMovieService movieService, ISendEndpointProvider sendEndpointProvider)
        {
            _movieService = movieService;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize = 20)
        {
            var movies = await _movieService.GetAllAsync();

            var paginatedFilms = movies.Data.Take(pageSize).ToList();

            return new ObjectResult(paginatedFilms) { StatusCode = movies.StatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            var review = await _movieService.GetReviewByIdAsync(id);

            MovieWithReviewDto movieWithReviewDto = new MovieWithReviewDto()
            {
                Movie = movie.Data,
                Review = review.Data
            };

            return new ObjectResult(movieWithReviewDto) { StatusCode = movie.StatusCode };
        }

        [HttpPost]
        [Route("/api/[controller]/WriteMovieReview")]
        public async Task<IActionResult> WriteMovieReview(ReviewDto reviewDto)
        {
            var review = await _movieService.WriteMovieReview(reviewDto);

            return new ObjectResult(review) { StatusCode = review.StatusCode };
        }

        [HttpPost]
        [Route("/api/[controller]/RecommendMovie")]
        public async Task<IActionResult> RecommendMovie(RecommendMovieDto recommendMovieDto)
        {
            var sendEndPoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:send-email-service"));

            var movie = await _movieService.GetByIdAsync(recommendMovieDto.MovieId);

            if (movie == null)
                return NotFound();

            StringBuilder? sb = new StringBuilder();
            sb.AppendLine($"Title : {movie.Data.Title}");
            sb.AppendLine();
            sb.AppendLine($"Release Date : {movie.Data.ReleaseDate}");
            sb.AppendLine();
            sb.AppendLine($"Overview : {movie.Data.Overview}");
            sb.AppendLine();
            sb.AppendLine($"Vote Average : {movie.Data.VoteAverage}");
            sb.AppendLine();
            sb.AppendLine($"Vote Count : {movie.Data.VoteCount}");

            string body = sb.ToString();
            sb = null;

            var emailMessageCommand = new EmailMessageCommand() 
            { 
                SenderEmail = recommendMovieDto.SenderEmail,
                ReceiverEmail = recommendMovieDto.ReceiverEmail,
                Subject = "Movie recommendation",
                Body = body
            };

            await sendEndPoint.Send<EmailMessageCommand>(emailMessageCommand);

            return NoContent();
        }
    }
}
