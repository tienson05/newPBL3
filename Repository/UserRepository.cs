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
            sql += user.BirthOfDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "NULL" + "', '";
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
            if (user != null && user.Rows.Count > 0)
            {
                foreach (DataRow row in user.Rows)
                {
                    return new User
                    {
                        UserID = row["UserID"] != DBNull.Value ? Convert.ToInt32(row["UserID"]) : 0,
                        Username = row["Username"] != DBNull.Value ? row["Username"].ToString() : string.Empty,
                        PasswordHash = row["PasswordHash"] != DBNull.Value ? row["PasswordHash"].ToString() : string.Empty,
                        Gmail = row["Gmail"] != DBNull.Value ? row["Gmail"].ToString() : string.Empty,
                        Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty,
                        Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : string.Empty,
                        BirthOfDate = row["BirthOfDate"] != DBNull.Value ? Convert.ToDateTime(row["BirthOfDate"]) : null,
                        PhoneNumber = row["PhoneNumber"] != DBNull.Value ? row["PhoneNumber"].ToString() : string.Empty,
                        Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : string.Empty,
                        AvatarUrl = row["AvatarUrl"] != DBNull.Value ? row["AvatarUrl"].ToString() : string.Empty,
                        Balance = row["Balance"] != DBNull.Value ? Convert.ToDouble(row["Balance"]) : 0.0,
                        TotalPosts = row["TotalPosts"] != DBNull.Value ? Convert.ToInt32(row["TotalPosts"]) : 0,
                        TotalPurchases = row["TotalPurchases"] != DBNull.Value ? Convert.ToInt32(row["TotalPurchases"]) : 0,
                        Rating = row["Rating"] != DBNull.Value ? Convert.ToDouble(row["Rating"]) : 0.0,
                        Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : string.Empty,
                        Role = row["Role"] != DBNull.Value ? row["Role"].ToString() : string.Empty,
                        IsVerified = row["IsVerified"] != DBNull.Value ? Convert.ToBoolean(row["IsVerified"]) : true,
                        LastLoginAt = row["LastLoginAt"] != DBNull.Value ? Convert.ToDateTime(row["LastLoginAt"]) : null,
                        UpdateAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : null,
                        CreateAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : null
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
                        UserID = row["UserID"] != DBNull.Value ? Convert.ToInt32(row["UserID"]) : 0,
                        Username = row["Username"] != DBNull.Value ? row["Username"].ToString() : string.Empty,
                        PasswordHash = row["PasswordHash"] != DBNull.Value ? row["PasswordHash"].ToString() : string.Empty,
                        Gmail = row["Gmail"] != DBNull.Value ? row["Gmail"].ToString() : string.Empty,
                        Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty,
                        Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : string.Empty,
                        BirthOfDate = row["BirthOfDate"] != DBNull.Value ? Convert.ToDateTime(row["BirthOfDate"]) : null,
                        PhoneNumber = row["PhoneNumber"] != DBNull.Value ? row["PhoneNumber"].ToString() : string.Empty,
                        Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : string.Empty,
                        AvatarUrl = row["AvatarUrl"] != DBNull.Value ? row["AvatarUrl"].ToString() : string.Empty,
                        Balance = row["Balance"] != DBNull.Value ? Convert.ToDouble(row["Balance"]) : 0.0,
                        TotalPosts = row["TotalPosts"] != DBNull.Value ? Convert.ToInt32(row["TotalPosts"]) : 0,
                        TotalPurchases = row["TotalPurchases"] != DBNull.Value ? Convert.ToInt32(row["TotalPurchases"]) : 0,
                        Rating = row["Rating"] != DBNull.Value ? Convert.ToDouble(row["Rating"]) : 0.0,
                        Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : string.Empty,
                        Role = row["Role"] != DBNull.Value ? row["Role"].ToString() : string.Empty,
                        IsVerified = row["IsVerified"] != DBNull.Value ? Convert.ToBoolean(row["IsVerified"]) : true,
                        LastLoginAt = row["LastLoginAt"] != DBNull.Value ? Convert.ToDateTime(row["LastLoginAt"]) : null,
                        UpdateAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : null,
                        CreateAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : null
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
