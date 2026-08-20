using DNSLab.Web.DTOs.Repositories.Coupon;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface ICouponRepository
    {
        public Task<CouponDTO?> Validate(string code, long totalAmount);
    }
}
