using MediaBrowser.Model.ProcessRun.Metrics;

namespace HeThongMoiGioiDoCu.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Gmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthOfDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public double Balance { get; set; }
        public int TotalPosts { get; set; }
        public int TotalPurchases { get; set; }
        public double Rating { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public DateTime LastLoginAt { get; set; }
        public DateTime  UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
