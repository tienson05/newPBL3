namespace HeThongMoiGioiDoCu.Dtos.Account
{
    public class UpdateUserOfAdminDto
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Gmail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime? BirthOfDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
    }
}
