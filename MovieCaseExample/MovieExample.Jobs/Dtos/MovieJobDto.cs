namespace MovieExample.Jobs.Dtos
{
    public class MovieJobDto
    {
        public bool Adult { get; set; }

        public string Backdrop_Path { get; set; }

        public int[] Genre_Ids { get; set; }

        public int Id { get; set; }

        public string Original_Language { get; set; }

        public string Original_Title { get; set; }

        public string Overview { get; set; }

        public decimal Popularity { get; set; }

        public string Poster_Path { get; set; }

        public string Release_Date { get; set; }

        public string Title { get; set; }

        public bool Video { get; set; }

        public decimal Vote_Average { get; set; }

        public int Vote_Count { get; set; }
    }
}
