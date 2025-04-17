using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;

namespace HeThongMoiGioiDoCu.Mapper
{
    public static class UserMappers
    {
        public static Users MapToUser(this SignupDto signupDto, string hashedPassword)
        {
            return new Users
            {
                Username = signupDto.Username,
                Gmail = signupDto.Gmail,
                PasswordHash = hashedPassword,
                Gender = signupDto.Gender,
                Address = signupDto.Address,
                BirthOfDate = signupDto.BirthOfDate,
                Name = signupDto.Name,
                PhoneNumber = signupDto.PhoneNumber,
                AvatarUrl = signupDto.AvatarUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsVerified = true,
                Role = 2,
                Balance = 0,
                TotalPosts = 0,
                TotalPurchases = 0,
                Rating = 0,
                Status = "Active",
            };
        }

        public static Users MapToUser(this UpdateUserDto updateUserDto, int id)
        {
            return new Users
            {
                UserID = id,
                Username = updateUserDto.Username,
                Gmail = updateUserDto.Gmail,
                Name = updateUserDto.Name,
                Gender = updateUserDto.Gender,
                BirthOfDate = updateUserDto.BirthOfDate,
                PhoneNumber = updateUserDto.PhoneNumber,
                Address = updateUserDto.Address,
                AvatarUrl = updateUserDto.AvatarUrl,
            };
        }
        public static Users MapToUser(this CreateUserDto createUserDto, string hashedPassword)
        {
            return new Users
            {
                Username = createUserDto.Username,
                Gmail = createUserDto.Gmail,
                PasswordHash = hashedPassword,
                Name = createUserDto.Name,
                Gender = createUserDto.Gender,
                BirthOfDate = createUserDto.BirthOfDate,
                PhoneNumber = createUserDto.PhoneNumber,
                Address = createUserDto.Address,
                AvatarUrl = createUserDto.AvatarUrl,
                Status = createUserDto.Status,
                Role = 4,
                IsVerified = createUserDto.IsVerified,
                Permissions = createUserDto.Permissions,
            };
        }

        public static Users MapToUser(this UpdateUserOfAdminDto updateUserOfAdminDto, int id)
        {
            return new Users
            {
                UserID = id,
                Username = updateUserOfAdminDto.Username,
                Gmail = updateUserOfAdminDto.Gmail,
                Name = updateUserOfAdminDto.Name,
                Gender = updateUserOfAdminDto.Gender,
                BirthOfDate = updateUserOfAdminDto.BirthOfDate,
                PhoneNumber = updateUserOfAdminDto.PhoneNumber,
                Address = updateUserOfAdminDto.Address,
                AvatarUrl = updateUserOfAdminDto.AvatarUrl,
                Status = updateUserOfAdminDto.Status,
                Role = updateUserOfAdminDto.Role,
                IsVerified = updateUserOfAdminDto.IsVerified,
            };
        }

        public static Users MapToUser(this SearchUserDto searchUserDto) {
            return new Users
            {
                Username = searchUserDto.UserName,
                Gmail = searchUserDto.Gmail,
                Name = searchUserDto.Name,
                PhoneNumber = searchUserDto.PhoneNumber,
            };
        }

        public static UserViewDto MapToUserViewDto(this Users user)
        {
            return new UserViewDto
            {
                UserID = user.UserID,
                Name = user.Name,
                Gmail = user.Gmail,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
                Username = user.Username,
                Gender = user.Gender,
                BirthOfDate = user.BirthOfDate,
                Balance = user.Balance,
                TotalPosts = user.TotalPosts,
                TotalPurchases = user.TotalPurchases,
                Rating = user.Rating,
                Role = user.Role,
                UpdatedAt = user.UpdatedAt,
                CreatedAt = user.CreatedAt,
                Permissions = user.Permissions,
            };
        }
    }
}
