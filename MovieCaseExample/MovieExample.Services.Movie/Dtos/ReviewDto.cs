using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieExample.Services.Movie.Dtos
{
    public class ReviewDto
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }

        public int Movie { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }
    }
}
