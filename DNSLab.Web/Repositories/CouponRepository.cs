using DNSLab.Web.DTOs.Repositories.Coupon;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class CouponRepository(IHttpServiceProvider _HttpServiceProvider) : ICouponRepository
    {
        const string APIController = "Coupon";

        public async Task<CouponDTO?> Validate(string code, long totalAmount)
        {
            return await _HttpServiceProvider.Get<CouponDTO?>($"{APIController}/Validate?code={code}&totalAmount={totalAmount}");
        }
    }
}
