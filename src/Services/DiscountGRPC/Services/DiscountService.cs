using AutoMapper;
using DiscountGRPC.Models;
using DiscountGRPC.Protos;
using DiscountGRPC.Repository;
using Grpc.Core;

namespace DiscountGRPC.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        ICouponRepository _couponRepository;
        ILogger<DiscountService> _logger;
        IMapper _mapper;
        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper) 
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper= mapper;
        }

        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if(coupon==null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            }
            _logger.LogInformation("Discount is retrieved for ProductName: {ProductName},Amount: {Amount}",coupon.ProductName,coupon.Amount);
           // return new CouponRequest { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };
           return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon=_mapper.Map<Coupon>(request);
            bool isSaved = await _couponRepository.CreateDiscount(coupon);
            if (isSaved)
            {
                _logger.LogInformation("Discount is saved successfuly ProductName: {ProductName}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount creation is failed");
            }
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isUpdated= await _couponRepository.UpdateDiscount(coupon);
            if (isUpdated)
            {
                _logger.LogInformation("Discount is updated successfully , ProductName: {ProductName}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount updation is failed");
            }
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool isDeleted = await _couponRepository.DeleteDiscount(request.ProductId);
            if(isDeleted)
            {
                _logger.LogInformation("Discount is deleted, ProductId: {ProductId}",request.ProductId);
            }
            else
            {
                _logger.LogInformation("Discount delation is failed");
            }

            return new DeleteDiscountResponse() { Success = isDeleted };
        }

    }
}
