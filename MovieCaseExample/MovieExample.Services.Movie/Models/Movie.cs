using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieExample.Services.Movie.Models
{
    public class Movie
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public int MovieId { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public string Overview { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Popularity { get; set; }

        public string PosterPath { get; set; }

        public string ReleaseDate { get; set; }

        public string Title { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal VoteAverage { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int VoteCount { get; set; }
    }
}
