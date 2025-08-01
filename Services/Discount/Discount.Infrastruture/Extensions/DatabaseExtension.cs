using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastruture.Extensions
{
    public static class DatabaseExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var logger = services.GetRequiredService<ILogger<TContext>>();

                    logger.LogInformation("Migrating database started");

                    ApplyMigration(configuration);

                    logger.LogInformation("Migrating database completed");
                }
                catch (Exception ex)
                {
                    // Log the error (uncomment ex variable name and write a log.)
                    throw;
                }
            }

            return host;
        }

        private static void ApplyMigration(IConfiguration configuration)
        {
            const int maxRetries = 10;
            const int delayMs = 2000;
            int retry = 0;

            while (true)
            {
                try
                {
                    using var connection = new NpgsqlConnection(configuration.GetConnectionString("Postgres"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                AmountOff DECIMAL(10, 2))";
                    command.ExecuteNonQuery();


                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, AmountOff) VALUES('IPhone X', 'IPhone Discount', 150);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, AmountOff) VALUES('Samsung 10', 'Samsung Discount', 100);";
                    command.ExecuteNonQuery();

                    break; // Success, exit loop
                }
                catch (NpgsqlException ex) when (retry < maxRetries)
                {
                    retry++;
                    Console.WriteLine($"Database connection failed (attempt {retry}): {ex.Message}");
                    Thread.Sleep(delayMs);
                }
            }
        }
    }
}
