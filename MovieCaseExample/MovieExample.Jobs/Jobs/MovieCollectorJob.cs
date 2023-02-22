using Hangfire;
using MassTransit;
using MovieExample.Jobs.Dtos;
using MovieExample.Shared.Messages;

namespace MovieExample.Jobs.Jobs
{
    public class MovieCollectorJob : IMovieCollectorJob
    {
        // For testing purposes, only get 20 pages of popular movies
        const int pageCount = 20;

        private readonly IConfiguration _config;

        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MovieCollectorJob(IConfiguration config, ISendEndpointProvider sendEndpointProvider)
        {
            _config = config;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [Queue("default")]
        [JobDisplayName("Collect the popular movies from themoviedb API")]
        public async Task CollectMovies()
        {
            string apiBaseUrl = _config.GetValue<string>("TheMovieDbSettings:ApiBaseUrl");
            string apiKey = _config.GetValue<string>("TheMovieDbSettings:ApiKey");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiBaseUrl);

            var sendEndPoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-movie-service"));

            var createMovieMessageCommand = new CreateMovieMessageCommand();

            for (int i = 1; i <= pageCount; i++)
            {
                HttpResponseMessage response = client.GetAsync($"/3/movie/popular?api_key={apiKey}&page={i}").Result;

                // Force to fail job if response status code is not success
                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($"The job failed, response status code {response.StatusCode}");

                var content = response.Content.ReadFromJsonAsync<ResponseJobDto>().Result;

                if (content != null && content.Results.Any())
                {
                    foreach (var movieJobDto in content.Results)
                    {
                        MovieItem movieItem = new MovieItem();

                        movieItem.Id = movieJobDto.Id;
                        movieItem.ReleaseDate = movieJobDto.Release_Date;
                        movieItem.VoteCount = movieJobDto.Vote_Count;
                        movieItem.VoteAverage = movieJobDto.Vote_Average;
                        movieItem.PosterPath = movieJobDto.Poster_Path;
                        movieItem.Overview = movieJobDto.Overview;
                        movieItem.Popularity = movieJobDto.Popularity;
                        movieItem.OriginalLanguage = movieJobDto.Original_Language;
                        movieItem.OriginalTitle = movieJobDto.Original_Title;
                        movieItem.Title = movieJobDto.Title;

                        createMovieMessageCommand.MovieItems.Add(movieItem);
                    }
                }
            }

            await sendEndPoint.Send<CreateMovieMessageCommand>(createMovieMessageCommand);
        }
    }
}
