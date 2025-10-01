using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace UserManagementSystem
{
    public class DatabaseHelper
    {
        // Connection string 
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=UserManagementDB;Integrated Security=True";

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

 
  
        public static bool RegisterUser(string username, string password, string email, string fullName, out string errorMessage)
        {
            errorMessage = string.Empty;

            
            if (!ValidateRegistrationData(username, password, email, out errorMessage))
            {
                return false;
            }

           
            if (IsUsernameExists(username))
            {
                errorMessage = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.";
                return false;
            }

            if (IsEmailExists(email))
            {
                errorMessage = "Email đã được đăng ký. Vui lòng sử dụng email khác.";
                return false;
            }

            try
            {
                // Mã hóa mật khẩu - Claude đề xuất SHA 256 structure
                string hashedPassword = PasswordHelper.HashPassword(password);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = @"INSERT INTO Users (Username, Password, Email, FullName, CreatedDate) 
                                   VALUES (@Username, @Password, @Email, @FullName, @CreatedDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@FullName", string.IsNullOrEmpty(fullName) ? DBNull.Value : (object)fullName);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi đăng ký: {ex.Message}";
                return false;
            }
        }


        /// Đăng nhập người dùng
        public static bool LoginUser(string username, string password, out string errorMessage, out UserInfo userInfo)
        {
            errorMessage = string.Empty;
            userInfo = null;

           
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessage = "Vui lòng nhập tên đăng nhập.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Vui lòng nhập mật khẩu.";
                return false;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = @"SELECT UserId, Username, Password, Email, FullName, CreatedDate 
                                   FROM Users WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashedPassword = reader["Password"].ToString();

                                // Kiểm tra mật khẩu
                                if (PasswordHelper.VerifyPassword(password, storedHashedPassword))
                                {
                                    // Lấy thông tin người dùng
                                    userInfo = new UserInfo
                                    {
                                        UserId = Convert.ToInt32(reader["UserId"]),
                                        Username = reader["Username"].ToString(),
                                        Email = reader["Email"].ToString(),
                                        FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : string.Empty,
                                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                                    };

                                    // Cập nhật thời gian đăng nhập cuối
                                    UpdateLastLoginDate(userInfo.UserId);

                                    return true;
                                }
                                else
                                {
                                    errorMessage = "Mật khẩu không chính xác.";
                                    return false;
                                }
                            }
                            else
                            {
                                errorMessage = "Tên đăng nhập không tồn tại.";
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi đăng nhập: {ex.Message}";
                return false;
            }
        }

      ///KKểm tra username đã tồn tại chưa
    
        private static bool IsUsernameExists(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool IsEmailExists(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static void UpdateLastLoginDate(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "UPDATE Users SET LastLoginDate = @LastLoginDate WHERE UserId = @UserId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@LastLoginDate", DateTime.Now);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
       
            }
        }

        private static bool ValidateRegistrationData(string username, string password, string email, out string errorMessage)
        {
            errorMessage = string.Empty;

        
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessage = "Tên đăng nhập không được để trống.";
                return false;
            }

            if (username.Length < 3 || username.Length > 50)
            {
                errorMessage = "Tên đăng nhập phải có từ 3 đến 50 ký tự.";
                return false;
            }

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                errorMessage = "Tên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới.";
                return false;
            }

            // Check pass 
            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Mật khẩu không được để trống.";
                return false;
            }

            if (password.Length < 6)
            {
                errorMessage = "Mật khẩu phải có ít nhất 6 ký tự.";
                return false;
            }

            // Check t.tin email
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "Email không được để trống.";
                return false;
            }

            if (!IsValidEmail(email))
            {
                errorMessage = "Email không đúng định dạng.";
                return false;
            }

            return true;
        }

    /// Định dạng mail
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Hệ thống pattern email 
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}