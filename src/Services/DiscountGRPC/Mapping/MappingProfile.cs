using AutoMapper;
using DiscountGRPC.Models;
using DiscountGRPC.Protos;

namespace DiscountGRPC.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Coupon, CouponRequest>().ReverseMap();
        }
        
    }
}
