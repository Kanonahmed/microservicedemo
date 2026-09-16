using Dapper;
using DiscountGRPC.Models;
using Npgsql;

namespace DiscountGRPC.Repository
{
    public class CouponRepository : ICouponRepository
    {
        IConfiguration _configuration;
        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
            var affected = await connection.ExecuteAsync("INSERT INTO Coupon(Id,ProductId,ProductName,Description,Amount) VALUES(@Id,@ProductId,@ProductName,@Description,@Amount)",new {Id=coupon.Id,ProductId=coupon.ProductId,ProductName=coupon.ProductName,Description=coupon.Description,Amount=coupon.Amount});
            if(affected>0)
            {
                return true;

            }
            return false;
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductId=@ProductId", new { ProductId = productId });
            if (affected > 0)
            {
                return true;

            }
            return false;
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductId=@ProductId", new {ProductId=productId});
            if(coupon==null)
            {
                return new Coupon() { Amount=0, ProductName="No Discount"};
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
            var affected = await connection.ExecuteAsync("UPDATE Coupon SET Id=@Id,ProductId=@ProductId,ProductName=@ProductName,Description=@Description,Amount=@Amount", new { Id = coupon.Id, ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected > 0)
            {
                return true;

            }
            return false;
        }
    }
}
