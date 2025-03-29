using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongMoiGioiDoCu.Repository
{
    public class DBHelper
    {
        private static DBHelper _Instance;

        private SqlConnection cnn;
        private static string connectionString = @"Server=TIENSON;Database=OldGoodsMarketplace;Integrated Security=True;TrustServerCertificate=True;";

        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DBHelper(connectionString);
                return _Instance;
            }

            private set { }
        }

        private DBHelper(string s)
        {
            cnn = new SqlConnection(connectionString);
        }

        public DataTable GetRecords(string sql)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
                DataTable dt = new DataTable();
                cnn.Open();
                da.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }

        }

        // Thực thi câu lệnh SQL mà không trả về kết quả (ví dụ: INSERT, UPDATE, DELETE).
        public void ExecuteDB(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }

        public void ExecuteDB(string sql, params SqlParameter[] p)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddRange(p);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;  
            }
        }

    }
}
