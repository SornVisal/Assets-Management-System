using System;
using System.Data.SqlClient;
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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            // basic validation
            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter username and password");
                return;
            }

            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                {
                    con.Open();

                    string sql =
                        "SELECT COUNT(*) FROM Users WHERE Username=@u AND Password=@p";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", password);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Welcome, " + username);

                            DashboardForm dash = new DashboardForm();
                            dash.Show();

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password");
                            txtPass.Clear();
                            txtUser.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message);
            }
        }
    }
}
