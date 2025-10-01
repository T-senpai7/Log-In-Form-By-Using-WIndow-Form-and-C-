using System;
using System.Security.Cryptography;
using System.Text;

namespace UserManagementSystem
{
    /// <summary>
    /// Class hỗ trợ mã hóa mật khẩu sử dụng SHA-256
    /// Được Claude Code đề xuất hướng giải 
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Mã hóa mật khẩu bằng SHA-256
        /// </summary>
        /// <param name="password">Mật khẩu cần mã hóa</param>
        /// <returns>Chuỗi mật khẩu đã mã hóa dạng hexadecimal</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống", nameof(password));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                // Chuyển mật khẩu thành byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                
                // Tính hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                
                // Chuyển hash thành chuỗi hexadecimal
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// So sánh mật khẩu người dùng nhập với mật khẩu đã hash
        /// </summary>
        /// <param name="inputPassword">Mật khẩu người dùng nhập</param>
        /// <param name="hashedPassword">Mật khẩu đã hash trong database</param>
        /// <returns>True nếu khớp, False nếu không khớp</returns>
        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string inputHashed = HashPassword(inputPassword);
            return inputHashed.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}