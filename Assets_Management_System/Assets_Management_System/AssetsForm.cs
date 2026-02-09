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
using static Assets_Management_System.AssetsForm;

namespace Assets_Management_System
{
    public partial class AssetsForm : Form
    {
        private BindingList<Asset> assets;
        public AssetsForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }


        private void DgvAssets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count > 0)
            {
                var selectedAsset = (Asset)dgvAssets.SelectedRows[0].DataBoundItem;
                txtAssetName.Text = selectedAsset.Name;
                if (cbCategory.Items.Contains(selectedAsset.Category))
                    cbCategory.SelectedItem = selectedAsset.Category;
                nudQuantity.Value = selectedAsset.Quantity;
                txtPrice.Text = selectedAsset.Price.ToString();
            }
        }
        private void InitializeCustomComponents()
        {
            assets = new BindingList<Asset>();

            // Map existing columns to properties
            dgvAssets.AutoGenerateColumns = false;
            if (dgvAssets.Columns["Column1"] != null) dgvAssets.Columns["Column1"].DataPropertyName = "Name";
            if (dgvAssets.Columns["Column2"] != null) dgvAssets.Columns["Column2"].DataPropertyName = "Category";
            if (dgvAssets.Columns["Column3"] != null) dgvAssets.Columns["Column3"].DataPropertyName = "Quantity";
            if (dgvAssets.Columns["Column4"] != null) dgvAssets.Columns["Column4"].DataPropertyName = "Price";
            if (dgvAssets.Columns["Column5"] != null) dgvAssets.Columns["Column5"].DataPropertyName = "TotalValue";

            dgvAssets.DataSource = assets;

            // Format columns
            if (dgvAssets.Columns["Column4"] != null)
                dgvAssets.Columns["Column4"].DefaultCellStyle.Format = "C2";
            if (dgvAssets.Columns["Column5"] != null)
            {
                dgvAssets.Columns["Column5"].DefaultCellStyle.Format = "C2";
                dgvAssets.Columns["Column5"].ReadOnly = true;
            }

            // Populate categories if empty (Designer might have "Select category")
            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(new string[] { "Select category", "Electronics", "Furniture", "Stationery", "Vehicles", "Other" });
            cbCategory.SelectedIndex = 0;

            // NumericUpDown formatting
            nudQuantity.Minimum = 0;
            nudQuantity.Maximum = 10000;

            // Handle grid selection change to populate fields
            dgvAssets.SelectionChanged += DgvAssets_SelectionChanged;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var asset = new Asset
                {
                    Name = txtAssetName.Text.Trim(),
                    Category = cbCategory.SelectedItem.ToString(),
                    Quantity = (int)nudQuantity.Value,
                    Price = decimal.Parse(txtPrice.Text)
                };

                assets.Add(asset);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding asset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAssets.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an asset to update.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput()) return;

                var selectedAsset = (Asset)dgvAssets.SelectedRows[0].DataBoundItem;
                selectedAsset.Name = txtAssetName.Text.Trim();
                selectedAsset.Category = cbCategory.SelectedItem.ToString();
                selectedAsset.Quantity = (int)nudQuantity.Value;
                selectedAsset.Price = decimal.Parse(txtPrice.Text);

                dgvAssets.Refresh();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating asset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAssets.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an asset to delete.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to delete this asset?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var selectedAsset = (Asset)dgvAssets.SelectedRows[0].DataBoundItem;
                    assets.Remove(selectedAsset);
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting asset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            dgvAssets.ClearSelection();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtAssetName.Text))
            {
                MessageBox.Show("Asset Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cbCategory.SelectedIndex <= 0) // Index 0 is "Select category"
            {
                MessageBox.Show("Please select a valid category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nudQuantity.Value <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Price must be a valid number greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void ClearFields()
        {
            txtAssetName.Text = "";
            cbCategory.SelectedIndex = 0;
            nudQuantity.Value = 0;
            txtPrice.Text = "";
        }
        public class Asset
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal TotalValue => Quantity * Price;
        }
    }
}
