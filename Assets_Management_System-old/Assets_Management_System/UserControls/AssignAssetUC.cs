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
                txtEmployee.Clear();
                txtNotes.Clear();
                dtAssignDate.Value = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployee.Text))
            {
                MessageBox.Show("Please enter the employee name.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Create Transaction record
                transactionService.Add(new TransactionClass
                {
                    AssetId = currentAsset.Id,
                    TransactionType = "Assign",
                    EmployeeName = txtEmployee.Text.Trim(),
                    TransactionDate = dtAssignDate.Value,
                    Note = txtNotes.Text.Trim()
                });

                // 2. Update Asset Status
                assetService.AssignAsset(currentAsset.Id);

                MessageBox.Show("Asset assigned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnAssignCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning asset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
