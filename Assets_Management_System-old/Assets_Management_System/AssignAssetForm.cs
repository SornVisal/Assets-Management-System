using Assets_Management_System.Models;
using Assets_Management_System.Services;
using System;
using System.Windows.Forms;

namespace Assets_Management_System.Forms
{
    public partial class AssignAssetForm : Form
    {
        private readonly Asset asset;
        private readonly AssetService assetService = new AssetService();
        private readonly TransactionService transactionService = new TransactionService();

        public AssignAssetForm(Asset asset)
        {
            InitializeComponent();
            this.asset = asset;
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployee.Text))
            {
                MessageBox.Show("Employee name required");
                return;
            }

            // Update asset
            asset.Status = "Assigned";
            assetService.Update(asset);

            // Log transaction
            transactionService.Add(new TransactionClass
            {
                AssetId = asset.Id,
                TransactionType = "ASSIGN",
                EmployeeName = txtEmployee.Text,
                TransactionDate = DateTime.Now,
                Note = "Assigned asset"
            });

            MessageBox.Show("Asset assigned successfully");
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
