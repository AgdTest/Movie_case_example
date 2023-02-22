using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MovieExample.Services.Movie.Models
{
    public class Review
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public int Movie { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }
    }
}
