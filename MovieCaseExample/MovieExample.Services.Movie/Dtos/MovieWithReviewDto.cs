namespace MovieExample.Services.Movie.Dtos
{
    public class MovieWithReviewDto
    {
        public MovieDto Movie { get; set; }

        public ReviewDto Review { get; set; }
    }
}
