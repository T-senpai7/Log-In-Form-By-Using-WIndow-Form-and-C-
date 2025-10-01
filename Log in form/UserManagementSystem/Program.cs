using System;
using System.Windows.Forms;

namespace UserManagementSystem
{
    static class Program
    {
        /// <summary>
        /// Entry point chính của app 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Kiểm tra kết nối database trước khi chạy ứng dụng
                if (!DatabaseHelper.TestConnection())
                {
                    MessageBox.Show(
                        "Không thể kết nối đến cơ sở dữ liệu!\n\n" +
                        "Vui lòng kiểm tra:\n" +
                        "1. SQL Server đã được khởi động\n" +
                        "2. Database 'UserManagementDB' đã được tạo\n" +
                        "3. Connection string trong DatabaseHelper.cs đúng\n\n" +
                        "Connection String hiện tại:\n" +
                        "Data Source=.;Initial Catalog=UserManagementDB;Integrated Security=True",
                        "Lỗi kết nối Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Chạy form log in
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi nghiêm trọng:\n\n{ex.Message}\n\nỨng dụng sẽ đóng.",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}