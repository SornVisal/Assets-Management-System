using System;
using Npgsql;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assets_Management_System.Services;

namespace Assets_Management_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
            this.VisibleChanged += new EventHandler(LoginForm_VisibleChanged);
            lblMessage.Text = ""; // Clear on start
        }

        private void LoginForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                txtUser.Clear();
                txtPass.Clear();
                lblMessage.Text = "";
                btnLogin.Enabled = true;
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            // basic validation
            if (username == "" || password == "")
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Please enter both username and password.";
                return;
            }

            lblMessage.ForeColor = Color.FromArgb(37, 99, 235);
            lblMessage.Text = "Authenticating...";
            btnLogin.Enabled = false; // Prevent double click

            try
            {
                using (var con = DbHelper.GetConnection())
                {
                    // Get password from database
                    string sql = "SELECT Password FROM Users WHERE Username=@u LIMIT 1";
                    string storedPassword = null;

                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("u", username);
                        var result = cmd.ExecuteScalar();
                        storedPassword = result?.ToString();
                    }

                    // Check password - support both hashed (new) and plain text (legacy)
                    bool isValidPassword = false;
                    
                    if (!string.IsNullOrEmpty(storedPassword))
                    {
                        // Try hashed password first (new method)
                        if (PasswordHelper.VerifyPassword(password, storedPassword))
                        {
                            isValidPassword = true;
                        }
                        // Fall back to plain text (legacy - for existing users)
                        else if (storedPassword == password)
                        {
                            isValidPassword = true;
                            // TODO: Prompt user to change password to secure hash
                        }
                    }

                    if (isValidPassword)
                    {
                        lblMessage.ForeColor = Color.FromArgb(16, 185, 129); // Success Green
                        lblMessage.Text = "Login successful! Redirecting...";

                        // Small delay so user can see the message
                        await Task.Delay(800);

                        MainForm dash = new MainForm();
                        dash.Show();

                        this.Hide();
                    }
                    else
                    {
                        lblMessage.ForeColor = Color.FromArgb(239, 68, 68); // Error Red
                        lblMessage.Text = "Invalid username or password.";
                        txtPass.Clear();
                        txtUser.Focus();
                        btnLogin.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Connection error: " + ex.Message;
                btnLogin.Enabled = true;
            }
        }
    }
}
