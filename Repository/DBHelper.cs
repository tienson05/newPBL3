using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongMoiGioiDoCu.Repository
{
    public class DBHelper
    {
        private static DBHelper _Instance;

        //private SqlConnection cnn;
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
            //cnn = new SqlConnection(connectionString);
        }

        public async Task<DataTable> GetRecordsAsync(string sql)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cnn))
                {
                    DataTable dt = new DataTable();
                    await cnn.OpenAsync();
                    da.Fill(dt);
                    //cnn.Close();
                    return dt;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }

        }
        public async Task<DataTable> GetRecordsAsync(string sql, params SqlParameter[] p)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection cnn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {                 
                    cmd.Parameters.AddRange(p);     
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        await cnn.OpenAsync();  // Mở kết nối
                        da.Fill(dt); // Điền dữ liệu vào DataTable
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;  
            }          
        }


        // Thực thi câu lệnh SQL mà không trả về kết quả (ví dụ: INSERT, UPDATE, DELETE).
        public async Task ExecuteDBAsync(string sql)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    await cnn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    cnn.Close();
                }                   
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }         
        }

        public async Task ExecuteDBAsync(string sql, params SqlParameter[] p)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddRange(p);
                    await cnn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
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
