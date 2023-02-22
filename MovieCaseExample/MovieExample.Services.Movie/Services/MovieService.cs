using AutoMapper;
using AutoMapper.Configuration;
using MongoDB.Driver;
using MovieExample.Services.Movie.Dtos;
using MovieExample.Services.Movie.Models;
using MovieExample.Services.Movie.Settings;
using MovieExample.Shared;
using System.Net;

namespace MovieExample.Services.Movie.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMongoCollection<MovieExample.Services.Movie.Models.Movie> _movieCollection;

        private readonly IMongoCollection<MovieExample.Services.Movie.Models.Review> _reviewCollection;

        private readonly IMapper _mapper;

        public MovieService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _movieCollection = database.GetCollection<MovieExample.Services.Movie.Models.Movie>(databaseSettings.MovieCollectionName);
            _reviewCollection = database.GetCollection<MovieExample.Services.Movie.Models.Review>(databaseSettings.ReviewCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<MovieDto>>> GetAllAsync()
        {
            var movies = await _movieCollection.Find(movie => true).ToListAsync();

            return Response<List<MovieDto>>.Success(_mapper.Map<List<MovieDto>>(movies), 200);
        }

        public async Task<Response<MovieDto>> GetByIdAsync(int id)
        {
            var movie = await _movieCollection.Find<MovieExample.Services.Movie.Models.Movie>(x => x.MovieId == id).FirstOrDefaultAsync();

            if (movie == null)
            {
                return Response<MovieDto>.Fail("Movie not found", 404);
            }

            return Response<MovieDto>.Success(_mapper.Map<MovieDto>(movie), 200);
        }

        public async Task<Response<ReviewDto>> GetReviewByIdAsync(int movieId)
        {
            var review = await _reviewCollection.Find<MovieExample.Services.Movie.Models.Review>(x => x.Movie == movieId).FirstOrDefaultAsync();

            return Response<ReviewDto>.Success(_mapper.Map<ReviewDto>(review), 200);
        }

        public async Task<Response<ReviewDto>> WriteMovieReview(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);

            var filter = Builders<MovieExample.Services.Movie.Models.Review>.Filter.Where(x => x.Movie == review.Movie);

            var update = Builders<MovieExample.Services.Movie.Models.Review>.Update
                            .Set(x => x.Comment, review.Comment)
                            .Set(x => x.Rating, review.Rating);

            var options = new UpdateOptions { IsUpsert = true };

            await _reviewCollection.UpdateOneAsync(filter, update, options);

            return Response<ReviewDto>.Success(_mapper.Map<ReviewDto>(review), 200);
        }

        public async Task<Response<IEnumerable<MovieDto>>> CreateOrUpdateAsync(IEnumerable<MovieDto> movieDtos)
        {
            var movies = _mapper.Map<IEnumerable<MovieExample.Services.Movie.Models.Movie>>(movieDtos);

            foreach (var movie in movies)
            {
                var filter = Builders<MovieExample.Services.Movie.Models.Movie>.Filter.Eq(x => x.MovieId, movie.MovieId);
                var update = Builders<MovieExample.Services.Movie.Models.Movie>.Update
                    .Set(x => x.PosterPath, movie.PosterPath)
                    .Set(x => x.Title, movie.Title)
                    .Set(x => x.Popularity, movie.Popularity)
                    .Set(x => x.OriginalTitle, movie.OriginalTitle)
                    .Set(x => x.OriginalLanguage, movie.OriginalLanguage)
                    .Set(x => x.Overview, movie.Overview)
                    .Set(x => x.ReleaseDate, movie.ReleaseDate)
                    .Set(x => x.VoteAverage, movie.VoteAverage)
                    .Set(x => x.VoteCount, movie.VoteCount);
                var options = new UpdateOptions { IsUpsert = true };

                await _movieCollection.UpdateOneAsync(filter, update, options);
            }

            return Response<IEnumerable<MovieDto>>.Success(_mapper.Map<IEnumerable<MovieDto>>(movies), 200);
        }
    }
}
