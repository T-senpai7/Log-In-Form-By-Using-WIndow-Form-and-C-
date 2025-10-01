using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserManagementSystem
{
    public partial class RegisterForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtFullName;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private CheckBox chkShowPassword;
        private Button btnRegister;
        private LinkLabel linkLogin;

        public RegisterForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Cấu hình Form
            this.Text = "Đăng ký tài khoản";
            this.Size = new Size(450, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // Panel chính
            Panel mainPanel = new Panel
            {
                Size = new Size(400, 490),
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
                Text = "ĐĂNG KÝ TÀI KHOẢN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                AutoSize = true,
                Location = new Point(80, 20)
            };

            // Username
            Label usernameLabel = new Label
            {
                Text = "Tên đăng nhập: *",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(30, 70)
            };

            txtUsername = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 30),
                Location = new Point(30, 95),
                MaxLength = 50
            };

            // Email
            Label emailLabel = new Label
            {
                Text = "Email: *",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(30, 135)
            };

            txtEmail = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 30),
                Location = new Point(30, 160),
                MaxLength = 100
            };

          
            Label fullNameLabel = new Label
            {
                Text = "Họ và tên:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(30, 200)
            };

            txtFullName = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 30),
                Location = new Point(30, 225),
                MaxLength = 100
            };

            
            Label passwordLabel = new Label
            {
                Text = "Mật khẩu: * (tối thiểu 6 ký tự)",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(30, 265)
            };

            txtPassword = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 30),
                Location = new Point(30, 290),
                MaxLength = 100,
                UseSystemPasswordChar = true
            };

            
            Label confirmPasswordLabel = new Label
            {
                Text = "Xác nhận mật khẩu: *",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(30, 330)
            };

            txtConfirmPassword = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 30),
                Location = new Point(30, 355),
                MaxLength = 100,
                UseSystemPasswordChar = true
            };

            /
            chkShowPassword = new CheckBox
            {
                Text = "Hiển thị mật khẩu",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(30, 390)
            };

            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;

        
            btnRegister = new Button
            {
                Text = "ĐĂNG KÝ",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(340, 40),
                Location = new Point(30, 420),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            // Login Link
            linkLogin = new LinkLabel
            {
                Text = "Đã có tài khoản? Đăng nhập",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(115, 465),
                LinkColor = Color.FromArgb(46, 204, 113)
            };
            linkLogin.LinkClicked += LinkLogin_LinkClicked;

            // Thêm các control vào panel
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(usernameLabel);
            mainPanel.Controls.Add(txtUsername);
            mainPanel.Controls.Add(emailLabel);
            mainPanel.Controls.Add(txtEmail);
            mainPanel.Controls.Add(fullNameLabel);
            mainPanel.Controls.Add(txtFullName);
            mainPanel.Controls.Add(passwordLabel);
            mainPanel.Controls.Add(txtPassword);
            mainPanel.Controls.Add(confirmPasswordLabel);
            mainPanel.Controls.Add(txtConfirmPassword);
            mainPanel.Controls.Add(chkShowPassword);
            mainPanel.Controls.Add(btnRegister);
            mainPanel.Controls.Add(linkLogin);

            // Thêm panel vào form
            this.Controls.Add(mainPanel);

            // Xử lý phím Enter để chuyển TextBox
            txtUsername.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtEmail.Focus();
                    e.Handled = true;
                }
            };

            txtEmail.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtFullName.Focus();
                    e.Handled = true;
                }
            };

            txtFullName.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtPassword.Focus();
                    e.Handled = true;
                }
            };

            txtPassword.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtConfirmPassword.Focus();
                    e.Handled = true;
                }
            };

            txtConfirmPassword.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    BtnRegister_Click(btnRegister, EventArgs.Empty);
                    e.Handled = true;
                }
            };
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ form
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            
            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }

            
            if (!DatabaseHelper.IsValidEmail(email))
            {
                MessageBox.Show("Email không đúng định dạng.\nVí dụ: example@domain.com", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            try
            {
                
                bool registerSuccess = DatabaseHelper.RegisterUser(username, password, email, fullName, out string errorMessage);

                if (registerSuccess)
                {
                    MessageBox.Show("Đăng ký tài khoản thành công!\nBạn có thể đăng nhập ngay bây giờ.",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Đăng ký thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}\n\nVui lòng kiểm tra kết nối database.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LinkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(450, 550);
            this.Name = "RegisterForm";
            this.ResumeLayout(false);
        }
    }
}