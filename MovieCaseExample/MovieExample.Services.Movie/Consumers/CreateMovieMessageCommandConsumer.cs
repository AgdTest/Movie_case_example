using AutoMapper;
using MassTransit;
using MovieExample.Services.Movie.Dtos;
using MovieExample.Services.Movie.Services;
using MovieExample.Shared.Messages;

namespace MovieExample.Services.Movie.Consumers
{
    public class CreateMovieMessageCommandConsumer : IConsumer<CreateMovieMessageCommand>
    {
        private readonly IMovieService _movieService;

        public CreateMovieMessageCommandConsumer(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task Consume(ConsumeContext<CreateMovieMessageCommand> context)
        {
            List<MovieDto> movieDtos = new List<MovieDto>();

            context.Message.MovieItems.ForEach(x =>
            {
                MovieDto movieDto = new MovieDto();
                movieDto.Popularity = x.Popularity;
                movieDto.PosterPath = x.PosterPath;
                movieDto.Overview = x.Overview;
                movieDto.ReleaseDate = x.ReleaseDate;
                movieDto.VoteAverage = x.VoteAverage;
                movieDto.VoteCount = x.VoteCount;
                movieDto.MovieId = x.Id;
                movieDto.OriginalLanguage = x.OriginalLanguage;
                movieDto.OriginalTitle = x.OriginalTitle;
                movieDto.Title = x.Title;

                movieDtos.Add(movieDto);
            });

            await _movieService.CreateOrUpdateAsync(movieDtos);
        }
    }
}
