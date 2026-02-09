using Assets_Management_System;  // ← CORRECT
using Assets_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Management_System
{
    public partial class DashboardForm : Form
    {

        public DashboardForm()
        {
            InitializeComponent();
        }
        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.White;

            btn.MouseEnter += (s, e) =>
                btn.BackColor = Color.FromArgb(235, 239, 244);

            btn.MouseLeave += (s, e) =>
                btn.BackColor = Color.White;
        }

        private void LoadForm(Form f)
        {
            panelMain.Controls.Clear();

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;

            panelMain.Controls.Add(f);
            f.Show();
        }
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            StyleButton(btnDashboard);
            StyleButton(btnAssets);
            StyleButton(btnStockTransaction);
            StyleButton(btnReport);
            StyleButton(btnLogout);
            LoadForm(new ReportForm()); // or AssetsForm or HomeForm
        }
        private void ActivateButton(Button active)
        {
            foreach (Control c in panelSidebar.Controls)
                if (c is Button b)
                    b.BackColor = Color.White; // reset

            active.BackColor = Color.FromArgb(235, 239, 244); // light gray highlight
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDashboard);
            panelMain.Controls.Clear();
        }

        private void btnAssets_Click(object sender, EventArgs e)
        {
            ActivateButton(btnAssets);
            LoadForm(new AssetsForm());
        }

        private void btnStockTransaction_Click(object sender, EventArgs e)
        {
            ActivateButton(btnStockTransaction);
            LoadForm(new TransactionForm());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(btnReport);
            LoadForm(new ReportForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLogout);
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DashboardForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
