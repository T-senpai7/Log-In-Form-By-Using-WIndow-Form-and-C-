using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserManagementSystem
{
    public partial class MainForm : Form
    {
        private UserInfo currentUser;

        public MainForm(UserInfo userInfo)
        {
            InitializeComponent();
            currentUser = userInfo;
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Cấu hình Form
            this.Text = "Trang chủ - User Management System";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

          
            Panel headerPanel = new Panel
            {
                Size = new Size(700, 80),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(52, 73, 94),
                Dock = DockStyle.Top
            };

            Label headerLabel = new Label
            {
                Text = "QUẢN LÝ NGƯỜI DÙNG",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(30, 25)
            };

            headerPanel.Controls.Add(headerLabel);

         
            Panel userInfoPanel = new Panel
            {
                Size = new Size(640, 280),
                Location = new Point(30, 110),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            userInfoPanel.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 220), 2),
                    new Rectangle(0, 0, userInfoPanel.Width - 1, userInfoPanel.Height - 1));
            };

            // Tiêu đề thông tin
            Label infoTitleLabel = new Label
            {
                Text = "THÔNG TIN CÁ NHÂN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            
            Label avatarLabel = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI", 60),
                AutoSize = true,
                Location = new Point(270, 60)
            };

            Label userIdLabel = new Label
            {
                Text = "ID Người dùng:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, 150)
            };

            Label userIdValue = new Label
            {
                Text = currentUser.UserId.ToString(),
                Font = new Font("Segoe UI", 11),
                AutoSize = true,
                Location = new Point(200, 150),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

           
            Label usernameLabel = new Label
            {
                Text = "Tên đăng nhập:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, 180)
            };

            Label usernameValue = new Label
            {
                Text = currentUser.Username,
                Font = new Font("Segoe UI", 11),
                AutoSize = true,
                Location = new Point(200, 180),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

           
            Label emailLabel = new Label
            {
                Text = "Email:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, 210)
            };

            Label emailValue = new Label
            {
                Text = currentUser.Email,
                Font = new Font("Segoe UI", 11),
                AutoSize = true,
                Location = new Point(200, 210),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            
            Label fullNameLabel = new Label
            {
                Text = "Họ và tên:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, 240)
            };

            Label fullNameValue = new Label
            {
                Text = string.IsNullOrEmpty(currentUser.FullName) ? "(Chưa cập nhật)" : currentUser.FullName,
                Font = new Font("Segoe UI", 11),
                AutoSize = true,
                Location = new Point(200, 240),
                ForeColor = string.IsNullOrEmpty(currentUser.FullName) ? Color.Gray : Color.FromArgb(52, 152, 219)
            };

            
            userInfoPanel.Controls.Add(infoTitleLabel);
            userInfoPanel.Controls.Add(avatarLabel);
            userInfoPanel.Controls.Add(userIdLabel);
            userInfoPanel.Controls.Add(userIdValue);
            userInfoPanel.Controls.Add(usernameLabel);
            userInfoPanel.Controls.Add(usernameValue);
            userInfoPanel.Controls.Add(emailLabel);
            userInfoPanel.Controls.Add(emailValue);
            userInfoPanel.Controls.Add(fullNameLabel);
            userInfoPanel.Controls.Add(fullNameValue);

            
            Button logoutButton = new Button
            {
                Text = "ĐĂNG XUẤT",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(200, 45),
                Location = new Point(250, 410),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Click += LogoutButton_Click;

            // Thêm các control vào form
            this.Controls.Add(headerPanel);
            this.Controls.Add(userInfoPanel);
            this.Controls.Add(logoutButton);

            // Xử lý sự kiện đóng form (gán với main form) 
            this.FormClosing += MainForm_FormClosing;
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Đã đăng xuất thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đảm bảo không đóng ứng dụng khi đóng MainForm
            Application.Exit();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.ResumeLayout(false);
        }
    }
}