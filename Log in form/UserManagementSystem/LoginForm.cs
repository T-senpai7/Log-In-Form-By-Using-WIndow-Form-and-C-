using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Cấu hình Form
            this.Text = "Đăng nhập hệ thống";
            this.Size = new Size(450, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 244, 248);

   
            Panel mainPanel = new Panel
            {
                Size = new Size(400, 320),
                Location = new Point(25, 20),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

          
            mainPanel.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 220), 2),
                    new Rectangle(0, 0, mainPanel.Width - 1, mainPanel.Height - 1));
            };

      
            Label titleLabel = new Label
            {
                Text = "ĐĂNG NHẬP",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                AutoSize = true,
                Location = new Point(130, 20)
            };

   
            Label usernameLabel = new Label
            {
                Text = "Tên đăng nhập:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(30, 80)
            };


            TextBox usernameTextBox = new TextBox
            {
                Name = "txtUsername",
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 30),
                Location = new Point(30, 105),
                MaxLength = 50
            };

      
            Label passwordLabel = new Label
            {
                Text = "Mật khẩu:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(30, 145)
            };

 
            TextBox passwordTextBox = new TextBox
            {
                Name = "txtPassword",
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 30),
                Location = new Point(30, 170),
                MaxLength = 100,
                UseSystemPasswordChar = true
            };

           
            CheckBox showPasswordCheckBox = new CheckBox
            {
                Text = "Hiển thị mật khẩu",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(30, 205)
            };

            showPasswordCheckBox.CheckedChanged += (s, e) =>
            {
                passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
            };

            Button loginButton = new Button
            {
                Text = "ĐĂNG NHẬP",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(340, 40),
                Location = new Point(30, 235),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += (s, e) => LoginButton_Click(usernameTextBox, passwordTextBox);

            
            LinkLabel registerLink = new LinkLabel
            {
                Text = "Chưa có tài khoản? Đăng ký ngay",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(105, 285),
                LinkColor = Color.FromArgb(41, 128, 185)
            };
            registerLink.LinkClicked += RegisterLink_LinkClicked;

            // Thêm các control vào panel 
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(usernameLabel);
            mainPanel.Controls.Add(usernameTextBox);
            mainPanel.Controls.Add(passwordLabel);
            mainPanel.Controls.Add(passwordTextBox);
            mainPanel.Controls.Add(showPasswordCheckBox);
            mainPanel.Controls.Add(loginButton);
            mainPanel.Controls.Add(registerLink);

            this.Controls.Add(mainPanel);

            // Xử lý Enter button nhấn 
            usernameTextBox.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    passwordTextBox.Focus();
                    e.Handled = true;
                }
            };

            passwordTextBox.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    LoginButton_Click(usernameTextBox, passwordTextBox);
                    e.Handled = true;
                }
            };
        }

        private void LoginButton_Click(TextBox usernameTextBox, TextBox passwordTextBox)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            // check dữ liệu input 
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usernameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBox.Focus();
                return;
            }

            try
            {
                // Đăng nhập - login
                bool loginSuccess = DatabaseHelper.LoginUser(username, password, out string errorMessage, out UserInfo userInfo);

                if (loginSuccess)
                {
                    MessageBox.Show($"Đăng nhập thành công!\nChào mừng {userInfo.FullName ?? userInfo.Username}!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở form chính 
                    MainForm mainForm = new MainForm(userInfo);
                    mainForm.FormClosed += (s, args) => this.Show();
                    this.Hide();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    passwordTextBox.Clear();
                    passwordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.FormClosed += (s, args) => this.Show();
            this.Hide();
            registerForm.Show();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(450, 380);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
        }
    }
}