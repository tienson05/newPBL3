using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;

namespace HeThongMoiGioiDoCu.Mapper
{
    public static class UserMappers
    {
        public static User MapToUser(this SignupDto signupDto, string hashedPassword)
        {
            return new User
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
                Role = "Buyer",
                Balance = 0,
                TotalPosts = 0,
                TotalPurchases = 0,
                Rating = 0,
                Status = "Active",
            };
        }

        public static User MapToUser(this UpdateUserDto updateUserDto, int id)
        {
            return new User
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

        public static UpdateUserDto MapToUpdateUserDto(this User user)
        {
            return new UpdateUserDto
            {
                UserID = user.UserID,
                Username = user.Username,
                Gmail = user.Gmail,
                Name = user.Name,
                Gender = user.Gender,
                BirthOfDate = user.BirthOfDate,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
            };
        }

        public static UserViewDto MapToUserViewDto(this User user)
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
            };
        }

        public static User MapToUser(this CreateUserDto createUserDto, string hashedPassword)
        {
            return new User
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
                Role = createUserDto.Role,
                IsVerified = createUserDto.IsVerified,
            };
        }

        public static UpdateUserOfAdminDto MapToUpdateUserOfAdminDto(this User user)
        {
            return new UpdateUserOfAdminDto
            {
                Username = user.Username,
                Gmail = user.Gmail,
                Name = user.Name,
                Gender = user.Gender,
                BirthOfDate = user.BirthOfDate,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
                Status = user.Status,
                Role = user.Role,
                IsVerified = user.IsVerified,
                UserID = user.UserID
            };
        }

        public static User MapToUser(this UpdateUserOfAdminDto updateUserOfAdminDto, int id)
        {
            return new User
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

        public static User MapToUser(this SearchUserDto searchUserDto) {
            return new User
            {
                Username = searchUserDto.UserName,
                Gmail = searchUserDto.Gmail,
                Name = searchUserDto.Name,
                PhoneNumber = searchUserDto.PhoneNumber,
            };
        }
    }
}
