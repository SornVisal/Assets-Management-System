using Assets_Management_System.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Assets_Management_System
{
    public partial class AssetsForm : Form
    {
        private BindingList<Asset> assets;
        private readonly string[] Categories = new string[]
        {
            "Electronics", "Furniture", "Stationery", "Vehicles", "Office Equipment", "Other"
        };

        public AssetsForm()
        {
            InitializeComponent();
            InitializeCustomComponents();

            // These lines fix size + no maximize + centered
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Try one of these sizes (start with the first)
            this.Size = new Size(1200, 750);       // ← most common good size
                                                   // this.Size = new Size(1280, 800);
                                                   // this.Size = new Size(1360, 768);    // fits many laptops perfectly

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void InitializeCustomComponents()
        {
            // Initialize data source
            assets = new BindingList<Asset>();
            dgvAssets.DataSource = assets;

            // Manual column mapping (must match exact column names in Designer)
            ConfigureDataGridViewColumns();

            // Setup ComboBox
            cbCategory.Items.Clear();
            cbCategory.Items.Add("— Select Category —");
            cbCategory.Items.AddRange(Categories);
            cbCategory.SelectedIndex = 0;
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // NumericUpDown settings
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 100000;
            nudQuantity.Increment = 1;

            // Price textbox: only allow numbers and one decimal point
            txtPrice.KeyPress += TxtPrice_KeyPress;

            // Selection changed → fill fields
            dgvAssets.SelectionChanged += DgvAssets_SelectionChanged;

            // Double-click row to edit (very user-friendly)
            dgvAssets.DoubleClick += DgvAssets_DoubleClick;

            // Better column headers
            dgvAssets.Columns["Column1"].HeaderText = "Asset Name";
            dgvAssets.Columns["Column2"].HeaderText = "Category";
            dgvAssets.Columns["Column3"].HeaderText = "Quantity";
            dgvAssets.Columns["Column4"].HeaderText = "Unit Price";
            dgvAssets.Columns["Column5"].HeaderText = "Total Value";

            // Select full row + better selection mode
            dgvAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAssets.MultiSelect = false;
            dgvAssets.ReadOnly = true;
            dgvAssets.AllowUserToAddRows = false;
            dgvAssets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigureDataGridViewColumns()
        {
            dgvAssets.AutoGenerateColumns = false;

            dgvAssets.Columns["Column1"].DataPropertyName = "Name";
            dgvAssets.Columns["Column2"].DataPropertyName = "Category";
            dgvAssets.Columns["Column3"].DataPropertyName = "Quantity";
            dgvAssets.Columns["Column4"].DataPropertyName = "Price";
            dgvAssets.Columns["Column5"].DataPropertyName = "TotalValue";

            // Currency formatting
            dgvAssets.Columns["Column4"].DefaultCellStyle.Format = "$#,##0.00";
            dgvAssets.Columns["Column5"].DefaultCellStyle.Format = "$#,##0.00";
            dgvAssets.Columns["Column5"].DefaultCellStyle.Font = new Font(dgvAssets.Font, FontStyle.Bold);
            dgvAssets.Columns["Column5"].DefaultCellStyle.ForeColor = Color.DarkGreen;
        }

        private void DgvAssets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count > 0)
            {
                var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
                if (asset != null)
                {
                    txtAssetName.Text = asset.Name;
                    cbCategory.SelectedItem = asset.Category;
                    nudQuantity.Value = asset.Quantity;
                    txtPrice.Text = asset.Price.ToString("F2");
                }
            }
        }

        private void DgvAssets_DoubleClick(object sender, EventArgs e)
        {
            btnUpdate.PerformClick(); // Optional: auto focus update when double-click
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var newAsset = new Asset
            {
                Name = txtAssetName.Text.Trim(),
                Category = cbCategory.SelectedItem.ToString(),
                Quantity = (int)nudQuantity.Value,
                Price = decimal.Parse(txtPrice.Text)
            };

            assets.Add(newAsset);
            ClearInputFields();
            MessageBox.Show("Asset added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            var selectedAsset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            if (selectedAsset == null) return;

            selectedAsset.Name = txtAssetName.Text.Trim();
            selectedAsset.Category = cbCategory.SelectedItem.ToString();
            selectedAsset.Quantity = (int)nudQuantity.Value;
            selectedAsset.Price = decimal.Parse(txtPrice.Text);

            dgvAssets.Refresh();
            ClearInputFields();
            MessageBox.Show("Asset updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;

            var result = MessageBox.Show(
                $"Are you sure you want to delete:\n\n{asset.Name} ({asset.Category})\nQuantity: {asset.Quantity} × ${asset.Price:F2} = ${asset.TotalValue:F2}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                assets.Remove(asset);
                ClearInputFields();
                MessageBox.Show("Asset deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            dgvAssets.ClearSelection();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtAssetName.Text))
            {
                ShowError("Please enter Asset Name.");
                txtAssetName.Focus();
                return false;
            }

            if (cbCategory.SelectedIndex <= 0)
            {
                ShowError("Please select a valid category.");
                cbCategory.Focus();
                return false;
            }

            if (nudQuantity.Value < 1)
            {
                ShowError("Quantity must be at least 1.");
                nudQuantity.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                ShowError("Please enter a valid price greater than 0.");
                txtPrice.Focus();
                txtPrice.SelectAll();
                return false;
            }

            return true;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearInputFields()
        {
            txtAssetName.Clear();
            cbCategory.SelectedIndex = 0;
            nudQuantity.Value = 1;
            txtPrice.Clear();
            txtAssetName.Focus();
        }

        // Allow only numbers and one decimal point in Price field
        private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
                e.Handled = true;
        }

        // Optional: Add some sample data on first load (remove later if you connect to DB)
        private void AssetsForm_Load(object sender, EventArgs e)
        {
            if (assets.Count == 0)
            {
                assets.Add(new Asset { Name = "Laptop Dell XPS", Category = "Electronics", Quantity = 10, Price = 1299.99m });
                assets.Add(new Asset { Name = "Office Chair", Category = "Furniture", Quantity = 25, Price = 189.50m });
                assets.Add(new Asset { Name = "Company Car - Toyota", Category = "Vehicles", Quantity = 3, Price = 35000m });
            }
        }

        // ==================== ASSET MODEL (Inner Class) ====================
        public class Asset
        {
            public string Name { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public decimal Price { get; set; }

            // Calculated property
            public decimal TotalValue => Quantity * Price;

            public override string ToString() => $"{Name} ({Quantity} × ${Price:F2})";
        }
    }
}