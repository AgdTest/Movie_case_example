using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;

namespace MovieExample.Jobs
{
    public static class HangfireService
    {
        public static void ConfigureHangfireService(this WebApplicationBuilder builder)
        {
            var mongoUrlBuilder = new MongoUrlBuilder(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            mongoUrlBuilder.DatabaseName = builder.Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new MigrateMongoMigrationStrategy(),
                        BackupStrategy = new CollectionMongoBackupStrategy()
                    },
                    Prefix = "movies.hangfire",
                    CheckConnection = false,
                })
            );

            builder.Services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Movies.Hangfire";
            });

        }
    }
}
