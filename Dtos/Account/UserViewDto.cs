﻿namespace HeThongMoiGioiDoCu.Dtos.Account
{
    public class UserViewDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public DateTime UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
