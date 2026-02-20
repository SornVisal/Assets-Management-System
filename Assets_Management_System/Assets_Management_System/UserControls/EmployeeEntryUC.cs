using System;
using System.Drawing;
using System.Windows.Forms;
using Assets_Management_System.Models;
using Assets_Management_System.Services;

namespace Assets_Management_System.UserControls
{
    public partial class EmployeeEntryUC : UserControl
    {
        private Employee currentEmployee;
        private EmployeeService service = new EmployeeService();

        public event EventHandler OnSaveCompleted;
        public event EventHandler OnCancel;

        public EmployeeEntryUC()
        {
            InitializeComponent();
        }

        public void Initialize(Employee emp)
        {
            currentEmployee = emp;
            lblMessage.Text = ""; // Clear
            if (emp == null)
            {
                lblTitle.Text = "New Employee";
                txtFullName.Clear();
                txtDepartment.Clear();
                txtPosition.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
            }
            else
            {
                lblTitle.Text = "Edit Employee";
                txtFullName.Text = emp.FullName;
                txtDepartment.Text = emp.Department;
                txtPosition.Text = emp.Position;
                txtEmail.Text = emp.Email;
                txtPhone.Text = emp.PhoneNumber;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Full Name is required.";
                return;
            }

            var emp = currentEmployee ?? new Employee();
            emp.FullName = txtFullName.Text.Trim();
            emp.Department = txtDepartment.Text.Trim();
            emp.Position = txtPosition.Text.Trim();
            emp.Email = txtEmail.Text.Trim();
            emp.PhoneNumber = txtPhone.Text.Trim();

            try
            {
                lblMessage.ForeColor = Color.FromArgb(37, 99, 235);
                lblMessage.Text = "Saving...";
                btnSave.Enabled = false;

                if (currentEmployee == null) service.Add(emp);
                else service.Update(emp);

                lblMessage.ForeColor = Color.FromArgb(16, 185, 129);
                lblMessage.Text = "Saved successfully! Closing...";
                
                await System.Threading.Tasks.Task.Delay(900);
                
                btnSave.Enabled = true;
                OnSaveCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Error: " + ex.Message;
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke(this, EventArgs.Empty);
        }
    }
}
