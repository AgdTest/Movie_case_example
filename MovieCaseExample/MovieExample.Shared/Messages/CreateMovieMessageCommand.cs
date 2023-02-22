using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieExample.Shared.Messages
{
    public class CreateMovieMessageCommand
    {
        public CreateMovieMessageCommand()
        {
            MovieItems = new List<MovieItem>();
        }

        public List<MovieItem> MovieItems { get; set; }
    }

    public class MovieItem
    {
        public int Id { get; set; }

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
