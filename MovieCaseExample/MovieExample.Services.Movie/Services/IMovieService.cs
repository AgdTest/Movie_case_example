using MovieExample.Services.Movie.Dtos;
using MovieExample.Shared;

namespace MovieExample.Services.Movie.Services
{
    public interface IMovieService
    {
        Task<Response<List<MovieDto>>> GetAllAsync();

        Task<Response<MovieDto>> GetByIdAsync(int id);

        Task<Response<ReviewDto>> GetReviewByIdAsync(int movieId);

        Task<Response<ReviewDto>> WriteMovieReview(ReviewDto reviewDto);

        Task<Response<IEnumerable<MovieDto>>> CreateOrUpdateAsync(IEnumerable<MovieDto> movieDtos);
    }
}
