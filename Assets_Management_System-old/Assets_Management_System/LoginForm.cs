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
                    // con.Open(); // DbHelper already opens the connection

                    // Postgres table/column names are case-insensitive by default if unquoted
                    // We assume tables are created as 'users' (or 'Users' which casts to 'users')
                    // 'user' is a reserved keyword, but 'users' is not.
                    string sql =
                        "SELECT COUNT(*) FROM Users WHERE Username=@u AND Password=@p";

                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("u", username);
                        cmd.Parameters.AddWithValue("p", password);

                        // Use long because COUNT returns bigint in Postgres
                        long count = (long)cmd.ExecuteScalar(); 

                        if (count > 0)
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
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Database connection error.";
                btnLogin.Enabled = true;
                // Optional: Console.WriteLine(ex.Message);
            }
        }
    }
}
