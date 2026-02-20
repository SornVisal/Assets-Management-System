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
                dtReturnDate.Value = DateTime.Now;
                txtNotes.Clear();
                
                // Show current employee assigned
                var lastTx = transactionService.GetAll()
                    .FindLast(t => t.AssetId == asset.Id && t.TransactionType == "Assign");
                lblCurrentlyWith.Text = lastTx != null ? lastTx.EmployeeName : "Unknown";
                
                cbCondition.SelectedIndex = 0; // Default: Good
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string status = cbCondition.Text == "Good" ? "Available" : "Repair";

                // 1. Log Transaction
                transactionService.Add(new TransactionClass
                {
                    AssetId = currentAsset.Id,
                    TransactionType = "Return",
                    EmployeeName = lblCurrentlyWith.Text,
                    TransactionDate = dtReturnDate.Value,
                    Note = $"Returned as {cbCondition.Text}. " + txtNotes.Text.Trim()
                });

                // 2. Update Asset Status
                if (status == "Available")
                    assetService.UpdateStatus(currentAsset.Id, "Available");
                else
                    assetService.UpdateStatus(currentAsset.Id, "Repair");

                MessageBox.Show($"Asset returned successfully! Status is now: {status}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnReturnCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing return: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}

