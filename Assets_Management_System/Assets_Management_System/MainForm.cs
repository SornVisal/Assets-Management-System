using System;
using System.Drawing;
using System.Windows.Forms;
using Assets_Management_System.UserControls;

namespace Assets_Management_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Seed Data if empty
            var seeder = new Services.DataSeeder();
            seeder.Seed();

            // Default View
            SetActiveButton(btnDashboard);
            AddUserControl(new DashboardUC());
        }

        private void AddUserControl(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(uc);
            uc.BringToFront();
        }

        // Navigation Handlers
        private void SetActiveButton(Button activeBtn)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.ForeColor = Color.FromArgb(148, 163, 184); // Dimmed slate
                    btn.BackColor = Color.Transparent;
                }
            }

            if (activeBtn != null && activeBtn != btnLogout)
            {
                activeBtn.ForeColor = Color.White;
                activeBtn.BackColor = Color.FromArgb(30, 41, 59); // Slightly lighter navy/slate
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            AddUserControl(new DashboardUC());
        }

        private void btnAssets_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAssets);
            AddUserControl(new AssetsUC());
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnEmployees);
            AddUserControl(new EmployeesUC());
        }

        private void btnStockTransaction_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnStockTransaction);
            AddUserControl(new TransactionUC());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnReport);
            AddUserControl(new ReportUC());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form login = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm)
                {
                    login = f;
                    break;
                }
            }

            if (login != null)
            {
                login.Show();
                this.Close();
            }
            else
            {
                LoginForm newLogin = new LoginForm();
                newLogin.Show();
                this.Close();
            }
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
