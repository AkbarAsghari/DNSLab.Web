namespace DNSLab.Web.DTOs.Repositories.Accounts
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public string? Mobile { get; set; }
        public bool IsMobileApproved { get; set; }
        public string Email { get; set; }
        public bool IsEmailApproved { get; set; }
        public string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
