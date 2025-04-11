using System.Data;
using System.Runtime.InteropServices;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using Microsoft.Data.SqlClient;

namespace HeThongMoiGioiDoCu.Repository.UserRepo
{
    public class ClientRepository : IClientRepository
    {
        private static Users MapToUser(DataRow row)
        {
            return new Users
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
                Balance = row["Balance"] != DBNull.Value ? Convert.ToDecimal(row["Balance"]) : 0,
                TotalPosts = row["TotalPosts"] != DBNull.Value ? Convert.ToInt32(row["TotalPosts"]) : 0,
                TotalPurchases = row["TotalPurchases"] != DBNull.Value ? Convert.ToInt32(row["TotalPurchases"]) : 0,
                Rating = row["Rating"] != DBNull.Value ? Convert.ToDouble(row["Rating"]) : 0.0,
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : string.Empty,
                Role = Convert.ToInt32(row["Role"]),
                IsVerified = row["IsVerified"] != DBNull.Value ? Convert.ToBoolean(row["IsVerified"]) : true,
                LastLoginAt = row["LastLoginAt"] != DBNull.Value ? Convert.ToDateTime(row["LastLoginAt"]) : null,
                UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : null,
                CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : null
            };
        }
        public async Task<Users> GetUserByEmailAsync(string email)
        {
            string sql = "SELECT * FROM Users WHERE (Role = 2 OR Role = 3) AND Gmail = @Gmail";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Gmail", email)
            };
            var user = await DBHelper.Instance.GetRecordsAsync(sql, parameters);
            if (user != null && user.Rows.Count > 0)
            {                                 
                var us = MapToUser(user.Rows[0]);
                string s = "SELECT PermissionID FROM RolePermissions WHERE RoleID = " + us.Role;
                var permissions = await DBHelper.Instance.GetRecordsAsync(s);
                foreach (DataRow permission in permissions.Rows)
                {
                    us.Permissions.Add(Convert.ToInt32(permission["PermissionID"]));
                }
                return us;
            }
            return null;
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            string sql = "SELECT * FROM Users WHERE UserID  = @UserID AND (Role = 2 OR Role = 3)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", SqlDbType.Int) { Value = id }
            };
            var user = await DBHelper.Instance.GetRecordsAsync(sql, parameters);
            if (user != null && user.Rows.Count > 0)
            {
                var us = MapToUser(user.Rows[0]);
                string s = "SELECT PermissionID FROM RolePermissions WHERE RoleID = " + us.Role;
                var permissions = await DBHelper.Instance.GetRecordsAsync(s);
                foreach (DataRow permission in permissions.Rows)
                {
                    us.Permissions.Add(Convert.ToInt32(permission["PermissionID"]));
                }
                return us;
            }

            return null;
        }

        public async Task<List<Users>> GetAllUserAsync()
        {
            string sql = "SELECT * FROM Users WHERE (Role = 2 OR Role = 3)";

            var users = await DBHelper.Instance.GetRecordsAsync(sql);

            if (users == null || users.Rows.Count == 0)
            {
                return new List<Users>();
            }

            List<Users> list = new List<Users>();

            foreach (DataRow row in users.Rows)
            {
                list.Add(MapToUser(row)); 
            }

            return list; 
        }

        public async Task AddUserAsync(Users user)
        {
            string sql = "INSERT INTO Users (Username, Gmail, PasswordHash, Name, Gender, Balance, TotalPosts, Rating, Status, BirthOfDate, PhoneNumber, Address, AvatarUrl, Role, IsVerified) " +
                         "VALUES (@Username, @Gmail, @PasswordHash, @Name, @Gender, @Balance, @TotalPosts, @Rating, @Status, @BirthOfDate, @PhoneNumber, @Address, @AvatarUrl, @Role, @IsVerified)";

            var parameters = new[]
            {
                new SqlParameter("@Username", SqlDbType.NVarChar) { Value = user.Username },
                new SqlParameter("@Gmail", SqlDbType.NVarChar) { Value = user.Gmail },
                new SqlParameter("@PasswordHash", SqlDbType.NVarChar) { Value = user.PasswordHash },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = user.Name },
                new SqlParameter("@Gender", SqlDbType.NVarChar) { Value = user.Gender },
                new SqlParameter("@Balance", SqlDbType.Decimal) { Value = user.Balance },
                new SqlParameter("@TotalPosts", SqlDbType.Int) { Value = user.TotalPosts },
                new SqlParameter("@Rating", SqlDbType.Float) { Value = user.Rating },
                new SqlParameter("@Status", SqlDbType.NVarChar) { Value = user.Status },
                new SqlParameter("@BirthOfDate", SqlDbType.Date) { Value = user.BirthOfDate.HasValue ? (object)user.BirthOfDate.Value : DBNull.Value },
                new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = user.PhoneNumber },
                new SqlParameter("@Address", SqlDbType.NVarChar) { Value = user.Address },
                new SqlParameter("@AvatarUrl", SqlDbType.NVarChar) { Value = user.AvatarUrl },
                new SqlParameter("@Role", SqlDbType.Int) { Value = user.Role },
                new SqlParameter("@IsVerified", SqlDbType.Bit) { Value = user.IsVerified }
            };

            await DBHelper.Instance.ExecuteDBAsync(sql, parameters);      
        }


        public async Task UpdateLastLoginAsync(int id)
        {
            string sql = @"UPDATE Users SET LastLoginAt = GETDATE() WHERE UserID = @UserID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", id)
            };
            await DBHelper.Instance.ExecuteDBAsync(sql, parameters);
        }

        public async Task UpdateUserAsync(Users user)
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

            await DBHelper.Instance.ExecuteDBAsync(sql, parameters);
        }

        public async Task<List<Users>> SearchUserAsync(Users user)
        {
            string sql = "SELECT * FROM Users WHERE 1=1 ";
            List<SqlParameter> listsql = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(user.Username))
            {
                sql += "AND Username LIKE @Username ";
                listsql.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = "%" + user.Username + "%" });
            }

            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                sql += "AND Name LIKE @Name ";
                listsql.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = "%" + user.Name + "%" });
            }

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                sql += "AND PhoneNumber LIKE @PhoneNumber ";
                listsql.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = "%" + user.PhoneNumber + "%" });
            }

            if (!string.IsNullOrWhiteSpace(user.Gmail))
            {
                sql += "AND Gmail LIKE @Gmail ";
                listsql.Add(new SqlParameter("@Gmail", SqlDbType.NVarChar) { Value = "%" + user.Gmail + "%" });
            }

            sql += "AND (Role = 2 OR Role = 3) AND Status = 'Active'";

            var users = await DBHelper.Instance.GetRecordsAsync(sql, listsql.ToArray());

            List<Users> list = new List<Users>();

            foreach (DataRow row in users.Rows)
            {
                list.Add(MapToUser(row));
            }

            return list;
        }

        public async Task RegisterSeller(int id)
        {
            if (id != 1 && id != 3 && id != 4) {
                string sql = "UPDATE Users SET Role = 3 WHERE UserID = @UserID";
                var parameters = new SqlParameter[] {
                    new SqlParameter("UserID", id),
                };

                await DBHelper.Instance.ExecuteDBAsync(sql, parameters);
            }
        }

        public async Task ResetPasswordAsync(string newPassword, int id)
        {
            string sql = "UPDATE User SET PasswordHash = @PasswordHash WHERE UserID = @UserID";
            var parameters = new SqlParameter[] {
                new SqlParameter("PasswordHash", newPassword),
                new SqlParameter("UserID", id)
            };

            await DBHelper.Instance.ExecuteDBAsync(sql, parameters);
        }
    }
}
