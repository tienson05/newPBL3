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
            string sql = "INSERT INTO users (Username, Email, PasswordHash, Fullname, Gender, DateOfBirth, PhoneNumber, Address, ProfilePictureurl, Role, IsActive) VALUES ('";
            sql += user.Username + "', '";
            sql += user.Email + "', '";
            sql += user.PasswordHash + "', '";
            sql += user.Fullname + "', '";
            sql += (user.Gender ? "1" : "0") + "', '"; 
            sql += user.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss") + "', '";
            sql += user.PhoneNumber + "', '";
            sql += user.Address + "', '";
            sql += user.ProfilePictureUrl + "', '";
            sql += user.Role + "', '";
            sql += user.IsActive + "')";

            DBHelper.Instance.ExecuteDB(sql);
        }

        public async Task DeleteUserAsync(int id)
        {
            string sql = "DELETE FROM Users WHERE Id = '" + id + "'";
            DBHelper.Instance.ExecuteDB(sql);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            string sql = "SELECT * FROM users WHERE email = '" + email + "'";
            var user = DBHelper.Instance.GetRecords(sql);
            if(user != null && user.Rows.Count > 0)
            {
                foreach (DataRow row in user.Rows)
                {
                    return new User
                    {
                        Username = row["Username"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        Email = row["Email"].ToString(),
                        Fullname = row["Fullname"].ToString(),
                    };
                }
            }
            return null;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            string sql = "SELECT * FROM Users WHERE Id  = " + id + "";
            var user = DBHelper.Instance.GetRecords(sql);
            if (user != null && user.Rows.Count > 0 )
            {
                foreach (DataRow row in user.Rows)
                {
                    return new User
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        Fullname = row["Fullname"].ToString(), 
                        Gender = Convert.ToBoolean(row["Gender"]),
                        DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Address = row["Address"].ToString(),
                        ProfilePictureUrl = row["ProfilePictureUrl"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        Role = row["Role"].ToString(),
                        UpdateAt = Convert.ToDateTime(row["UpdateAt"]),
                        CreateAt = Convert.ToDateTime(row["UpdateAt"]),
                    };
                }
            }
            
            return null;
        }

        public async Task UpdateUserAsync(User user)
        {
            string sql = @"UPDATE Users SET
                    Username = @Username,
                    Email = @Email,
                    Fullname = @Fullname,
                    Gender = @Gender,
                    DateOfBirth = @DateOfBirth,
                    PhoneNumber = @PhoneNumber,
                    Address = @Address,
                    ProfilePictureUrl = @ProfilePictureUrl,
                    UpdateAt = GETDATE()
                    WHERE Id = @Id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Fullname", user.Fullname),
                new SqlParameter("@Gender", user.Gender),
                new SqlParameter("@DateOfBirth", user.DateOfBirth),
                new SqlParameter("@PhoneNumber", user.PhoneNumber),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@ProfilePictureUrl", user.ProfilePictureUrl),
                new SqlParameter("@Id", user.Id)
            };

            DBHelper.Instance.ExecuteDB(sql, parameters);
        }

        public async Task UpdateUserOfAdmin(User user)
        {
            string sql = @"UPDATE Users SET
                    Username = @Username,
                    Email = @Email,
                    Fullname = @Fullname,
                    Gender = @Gender,
                    DateOfBirth = @DateOfBirth,
                    PhoneNumber = @PhoneNumber,
                    Address = @Address,
                    ProfilePictureUrl = @ProfilePictureUrl,
                    IsActive = @IsActive,
                    Role = @Role,
                    UpdateAt = GETDATE()
                    WHERE Id = @Id";
            var parameters = new SqlParameter[] {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Fullname", user.Fullname),
                new SqlParameter("@Gender", user.Gender),
                new SqlParameter("@DateOfBirth", user.DateOfBirth),
                new SqlParameter("@PhoneNumber", user.PhoneNumber),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@ProfilePictureUrl", user.ProfilePictureUrl),
                new SqlParameter("@Role", user.Role),
                new SqlParameter("@IsActive", user.IsActive),
                new SqlParameter("@Id", user.Id)
            };

            DBHelper.Instance.ExecuteDB (sql, parameters);
        }
    }
}
