namespace MovieExample.Services.Movie.Dtos
{
    public class RecommendMovieDto
    {
        public int MovieId { get; set; }

        public string SenderEmail { get; set; }

        public string ReceiverEmail { get; set; }
    }
}
