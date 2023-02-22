using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MovieExample.Services.Movie.Dtos
{
    public class MovieDto
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }

        public int MovieId { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public string Overview { get; set; }

        public decimal Popularity { get; set; }

        public string PosterPath { get; set; }

        public string ReleaseDate { get; set; }

        public string Title { get; set; }

        public decimal VoteAverage { get; set; }

        public int VoteCount { get; set; }
    }
}
