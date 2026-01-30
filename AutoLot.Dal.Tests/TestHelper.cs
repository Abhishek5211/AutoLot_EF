using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Tests
{
    public class TestHelper
    {
       public static IConfiguration GetConfiguration() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.testing.json", optional: false, reloadOnChange: true)
            .Build();


       public static ApplicationDbContext GetContext(IConfiguration configuration)
       {
           var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
           var connectionString = configuration.GetConnectionString("AutoLot");
            optionsBuilder.UseSqlServer(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }

        public static ApplicationDbContext GetSecondContext(ApplicationDbContext old, IDbContextTransaction transaction)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(old.Database.GetConnectionString());
            var newContext = new ApplicationDbContext(optionsBuilder.Options);
            newContext.Database.UseTransaction(transaction.GetDbTransaction());
            return newContext;
        }
    }
}
