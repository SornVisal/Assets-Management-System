using System;
using System.Drawing;
using System.Windows.Forms;
using Assets_Management_System.Models;
using Assets_Management_System.Services;

namespace Assets_Management_System.UserControls
{
    public partial class ReturnAssetUC : UserControl
    {
        private Asset currentAsset;
        private AssetService assetService = new AssetService();
        private TransactionService transactionService = new TransactionService();

        public event EventHandler OnReturnCompleted;
        public event EventHandler OnCancelled;

        public ReturnAssetUC()
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
                dtReturnDate.Value = DateTime.Now;
                txtNotes.Clear();
                
                // Show current employee assigned
                var lastTx = transactionService.GetAll()
                    .FindLast(t => t.AssetId == asset.Id && t.TransactionType == "Assign");
                lblCurrentlyWith.Text = lastTx != null ? lastTx.EmployeeName : "Unknown";
                
                cbCondition.SelectedIndex = 0; // Default: Good
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string condition = cbCondition.Text; 

                lblMessage.ForeColor = Color.FromArgb(37, 99, 235);
                lblMessage.Text = "Processing return...";
                btnSave.Enabled = false;

                // Use the new atomic method
                assetService.ReturnAsset(
                    currentAsset.Id,
                    lblCurrentlyWith.Text,
                    condition,
                    txtNotes.Text.Trim(),
                    dtReturnDate.Value
                );

                string status = condition == "Good" ? "Available" : "Repair";
                lblMessage.ForeColor = Color.FromArgb(16, 185, 129);
                lblMessage.Text = $"Returned! Status: {status}. Closing...";
                
                await System.Threading.Tasks.Task.Delay(1200);
                
                btnSave.Enabled = true;
                OnReturnCompleted?.Invoke(this, EventArgs.Empty);
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

