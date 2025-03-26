using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using System.Data;

namespace HeThongMoiGioiDoCu.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
            string sql = "INSERT INTO users (Username, Email, PasswordHash, Fullname, Gender, DateOfBirth, PhoneNumber, Address) VALUES ('";
            sql += user.Username + "', '";
            sql += user.Email + "', '";
            sql += user.PasswordHash + "', '";
            sql += user.Fullname + "', '";
            sql += (user.Gender ? "1" : "0") + "', '"; 
            sql += user.DateOfBirth.ToString("yyyy-MM-dd") + "', '";
            sql += user.PhoneNumber + "', '";
            sql += user.Address + "')";

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
    }
}
