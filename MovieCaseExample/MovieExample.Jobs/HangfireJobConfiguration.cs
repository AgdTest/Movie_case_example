using Hangfire;
using MovieExample.Jobs.Jobs;

namespace MovieExample.Jobs
{
    public static class HangfireJobConfiguration
    {
        public static void ConfigureJobs(this WebApplication app)
        {
            app.Services.CreateScope();

            // Collect movies every hour
            RecurringJob.AddOrUpdate<MovieCollectorJob>(j => j.CollectMovies(), "0 * * * *");
        }
    }
}
