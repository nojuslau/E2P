using E2P.AppSettingsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace E2P.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.ConfigureWritable<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
            var serviceProvider = services.BuildServiceProvider();
            var writableOptions = serviceProvider.GetRequiredService<IWritableOptions<ApplicationSettings>>();
            var connectionString = configuration.GetSection("ApplicationSettings:ConnectionStrings")["DefaultConnection"];
            optionsBuilder.UseSqlite(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options, writableOptions);
        }
    }
}
