using System;
using System.Text.Json.Serialization;

namespace HeThongMoiGioiDoCu.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Gmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime? BirthOfDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public int TotalPosts { get; set; } = 0;
        public int TotalPurchases { get; set; } = 0;
        public double Rating { get; set; } = 0;
        public string Status { get; set; } = "Active";
        public int Role { get; set; } = 2;
        public bool IsVerified { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }

        public List<int> Permissions { get; set; } = new List<int>();

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
