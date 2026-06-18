using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "رمز عبور جدید را وارد نمایید")]
        public string NewPassword { get; set; }
    }
}
