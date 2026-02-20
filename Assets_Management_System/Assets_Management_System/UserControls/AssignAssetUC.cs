using System;
using System.Drawing;
using System.Windows.Forms;
using Assets_Management_System.Models;
using Assets_Management_System.Services;

namespace Assets_Management_System.UserControls
{
    public partial class AssignAssetUC : UserControl
    {
        private Asset currentAsset;
        private AssetService assetService = new AssetService();
        private TransactionService transactionService = new TransactionService();

        public event EventHandler OnAssignCompleted;
        public event EventHandler OnCancelled;

        public AssignAssetUC()
        {
            InitializeComponent();
        }

        public void Initialize(Asset asset)
        {
            this.currentAsset = asset;
            if (asset != null)
            {
                lblAssetName.Text = asset.Name;
                lblAssetCode.Text = asset.AssetCode;
                lblMessage.Text = ""; // Clear message
                LoadEmployees();
                txtNotes.Clear();
                dtAssignDate.Value = DateTime.Now;
            }
        }

        private void LoadEmployees()
        {
            var empService = new EmployeeService();
            cmbEmployee.DataSource = empService.GetAll();
            cmbEmployee.DisplayMember = "FullName";
            cmbEmployee.ValueMember = "Id";
            cmbEmployee.SelectedIndex = -1;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedItem == null)
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Please select an employee.";
                return;
            }

            var selectedEmp = cmbEmployee.SelectedItem as Employee;

            try
            {
                lblMessage.ForeColor = Color.FromArgb(37, 99, 235);
                lblMessage.Text = "Processing assignment...";
                btnSave.Enabled = false;

                // Use the new atomic method that handles both status and transaction record
                assetService.AssignAsset(
                    currentAsset.Id, 
                    selectedEmp.FullName, 
                    txtNotes.Text.Trim(), 
                    dtAssignDate.Value
                );

                lblMessage.ForeColor = Color.FromArgb(16, 185, 129);
                lblMessage.Text = "Asset assigned successfully! Closing...";
                
                await System.Threading.Tasks.Task.Delay(1000);
                
                btnSave.Enabled = true;
                OnAssignCompleted?.Invoke(this, EventArgs.Empty);
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
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
