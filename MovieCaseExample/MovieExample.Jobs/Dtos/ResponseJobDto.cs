namespace MovieExample.Jobs.Dtos
{
    public class ResponseJobDto
    {
        public int Page { get; set; }
        public IEnumerable<MovieJobDto> Results { get; set; }

        public int Total_Pages { get; set; }

        public int Total_Results { get; set; }
    }
}
