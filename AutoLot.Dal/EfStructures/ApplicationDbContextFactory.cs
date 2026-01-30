using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;



namespace AutoLot.Dal.EfStructures
{
    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var provider = config["ProviderName"];
            //if (!Enum.TryParse<DataProvider>(config["ProviderName"], out var provider))
            //    throw new NotImplementedException("This database provider is not implemented");
            
            var connectionString = config[$"{provider}:ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);

        }
    }
}
