using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Mapper
{
    public static class UserMappers
    {
        public static SignupDto MapToUserDto(this User userModel)
        {
            return new SignupDto
            {
                Username = userModel.Username,
                Password = userModel.PasswordHash,
                Gmail = userModel.Gmail
            };
        }

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
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
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

        public static UpdateUserDto MapToUpdateUserDto(this User user) {
            return new UpdateUserDto
            {
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

        public static UserViewDto MapToUserViewDto(this User user) {
            return new UserViewDto {
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
                UpdateAt = user.UpdateAt,
                CreateAt = user.CreateAt,
            };
        }
    }
}
