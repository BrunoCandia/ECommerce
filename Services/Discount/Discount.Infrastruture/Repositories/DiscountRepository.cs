using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastruture.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Postgres"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, AmountOff) VALUES (@ProductName, @Description, @AmountOff)",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, AmountOff = coupon.AmountOff });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscountAsync(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Postgres"));

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscountByProductNameAsync(string productName)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Postgres")))
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

                if (coupon is null)
                {
                    return new Coupon
                    {
                        Id = 0,
                        ProductName = "No Discount",
                        Description = "No Discount Available",
                        AmountOff = 0
                    };
                }

                return coupon;
            }
        }

        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Postgres"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, AmountOff = @AmountOff WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, AmountOff = coupon.AmountOff, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
