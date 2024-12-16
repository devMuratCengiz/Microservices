using Dapper;
using Microservice.Shared.Dtos;
using Microservices.Services.Discount.Dtos;
using Npgsql;
using System.Data;

namespace Microservices.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _connection.ExecuteAsync("Delete from discount where id=@Id", new { Id = id });
            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<ResultDiscountDto>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<ResultDiscountDto>("Select * from discount");
            return Response<List<ResultDiscountDto>>.Success(discounts.ToList(),200);
        }

        public async Task<Response<ResultDiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _connection.QueryAsync<ResultDiscountDto>("Select * from discount where code=@Code and userId=@UserId", new
            {
                code = code,
                userId = userId
            });
            var hasDiscount = discount.FirstOrDefault();
            return hasDiscount == null ? Response<ResultDiscountDto>.Fail("Discount not found.", 404) : Response<ResultDiscountDto>.Success(hasDiscount,200);
        }

        public async Task<Response<ResultDiscountDto>> GetById(int id)
        {
            var discount = (await _connection.QueryAsync<ResultDiscountDto>("Select * from discount where id=@Id", new { Id=id })).SingleOrDefault();
            if (discount == null)
            {
                return Response<ResultDiscountDto>.Fail("Discount not found", 404);
            }
            return Response<ResultDiscountDto>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(CreateDiscountDto createDiscountDto)
        {
            var saveStatus = await _connection.ExecuteAsync("Insert into discount (userId,rate,code) values (@userId,@rate,@code)",createDiscountDto);
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("An error occured while adding", 500);
        }

        public async Task<Response<NoContent>> Update(UpdateDiscountDto updateDiscountDto)
        {
            var status = await _connection.ExecuteAsync("Update discount set userId=@userId,code=@code,rate=@rate where id=@Id", new
            {
                Id = updateDiscountDto.Id,
                userId = updateDiscountDto.UserId,
                code = updateDiscountDto.Code,
                rate = updateDiscountDto.Rate
            });

            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
