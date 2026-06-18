using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class AuthenticateDTO
    {
        [Required(ErrorMessage = "نام کاربری ضروری میباشد")]
        public string MobileOrEmailOrUsername { get; set; }
        [Required(ErrorMessage = "رمز عبور ضروری مییباشد")]
        public string Password { get; set; }
    }
}
