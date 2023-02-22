namespace MovieExample.Services.Movie.Settings
{
    public interface IDatabaseSettings
    {
        public string MovieCollectionName { get; set; }
        public string ReviewCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
