using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HeThongMoiGioiDoCu.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
            string sql = "INSERT INTO Users (Username, Gmail, PasswordHash, Name, Gender, Balance, TotalPosts, Rating, Status, BirthOfDate, PhoneNumber, Address, AvatarUrl, Role, IsVerified) VALUES ('";
            sql += user.Username + "', '";
            sql += user.Gmail + "', '";
            sql += user.PasswordHash + "', '";
            sql += user.Name + "', '";
            sql += user.Gender + "', '";
            sql += user.Balance + "', '";
            sql += user.TotalPosts + "', '";
            sql += user.Rating + "', '";
            sql += user.Status + "', '";
            sql += user.BirthOfDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '";
            sql += user.PhoneNumber + "', '";
            sql += user.Address + "', '";
            sql += user.AvatarUrl + "', '";
            sql += user.Role + "', '";
            sql += user.IsVerified + "')";

            DBHelper.Instance.ExecuteDB(sql);
        }

        public async Task DeleteUserAsync(int id)
        {
            string sql = "DELETE FROM Users WHERE UserID = '" + id + "'";
            DBHelper.Instance.ExecuteDB(sql);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            string sql = "SELECT * FROM Users WHERE Gmail = '" + email + "'";
            var user = DBHelper.Instance.GetRecords(sql);
            if(user != null && user.Rows.Count > 0)
            {
                foreach (DataRow row in user.Rows)
                {
                    return new User
                    {
                        Username = row["Username"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        Gmail = row["Gmail"].ToString(),
                        Name = row["Name"].ToString(),
                    };
                }
            }
            return null;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            string sql = "SELECT * FROM Users WHERE UserID  = " + id + "";
            var user = DBHelper.Instance.GetRecords(sql);
            if (user != null && user.Rows.Count > 0 )
            {
                foreach (DataRow row in user.Rows)
                {
                    return new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        Username = row["Username"].ToString(),
                        Gmail = row["Gmail"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        Name = row["Name"].ToString(), 
                        Gender = row["Gender"].ToString(),
                        BirthOfDate = Convert.ToDateTime(row["BirthOfDate"]),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Address = row["Address"].ToString(),
                        AvatarUrl = row["AvatarUrl"].ToString(),
                        Status = row["Status"].ToString(),
                        Role = row["Role"].ToString(),
                        Balance = Convert.ToDouble(row["Balance"]),
                        TotalPosts = Convert.ToInt32(row["TotalPosts"]),
                        TotalPurchases = Convert.ToInt32(row["TotalPurchases"]),
                        Rating = Convert.ToDouble(row["Rating"]),
                        IsVerified = Convert.ToBoolean(row["IsVerified"]),
                        //LastLoginAt = Convert.ToDateTime(row["LastLoginAt"]),
                        UpdateAt = Convert.ToDateTime(row["UpdatedAt"]),
                        CreateAt = Convert.ToDateTime(row["CreatedAt"]),
                    };
                }
            }
            
            return null;
        }

        public async Task UpdateUserAsync(User user)
        {
            string sql = @"UPDATE Users SET
                    Username = @Username,
                    Gmail = @Gmail,
                    Name = @Name,
                    Gender = @Gender,
                    BirthOfDate = @BirthOfDate,
                    PhoneNumber = @PhoneNumber,
                    Address = @Address,
                    AvatarUrl = @AvatarUrl,
                    UpdatedAt = GETDATE()
                    WHERE UserID = @UserID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Gmail", user.Gmail),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Gender", user.Gender),
                new SqlParameter("@BirthOfDate", user.BirthOfDate),
                new SqlParameter("@PhoneNumber", user.PhoneNumber),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@AvatarUrl", user.AvatarUrl),
                new SqlParameter("@UserID", user.UserID)
            };

            DBHelper.Instance.ExecuteDB(sql, parameters);
        }

        public async Task UpdateUserOfAdmin(User user)
        {
            string sql = @"UPDATE Users SET
                    Username = @Username,
                    Gmail = @Gmail,
                    Name = @Name,
                    Gender = @Gender,
                    BirthOfDate = @BirthOfDate,
                    PhoneNumber = @PhoneNumber,
                    Address = @Address,
                    ProfilePictureUrl = @AvatarUrl,
                    Status = @Status,
                    Role = @Role,
                    UpdatedAt = GETDATE()
                    WHERE UserID = @UserID";
            var parameters = new SqlParameter[] {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Gmail", user.Gmail),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Gender", user.Gender),
                new SqlParameter("@BirthOfDate", user.BirthOfDate),
                new SqlParameter("@PhoneNumber", user.PhoneNumber),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@AvatarUrl", user.AvatarUrl),
                new SqlParameter("@Role", user.Role),
                new SqlParameter("@Status", user.Status),
                new SqlParameter("@UserID", user.UserID)
            };

            DBHelper.Instance.ExecuteDB (sql, parameters);
        }
    }
}
