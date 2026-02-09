using Assets_Management_System.Forms;
using Assets_Management_System.Models;
using Assets_Management_System.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Assets_Management_System
{
    public partial class AssetsForm : Form
    {
        private AssetService service = new AssetService();
        private BindingSource bindingSource = new BindingSource();

        public AssetsForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Size = new System.Drawing.Size(1400, 700);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void AssetsForm_Load(object sender, EventArgs e)
        {
            if (service.GetAll().Count == 0)
            {
                service.Add(new Asset
                {
                    Name = "Laptop Dell XPS",
                    Category = "Electronics",
                    SerialNumber = "SN001",
                    Price = 1299.99m,
                    Status = "Available"
                });
                service.Add(new Asset
                {
                    Name = "Office Chair",
                    Category = "Furniture",
                    SerialNumber = "SN002",
                    Price = 189.50m,
                    Status = "Available"
                });
                service.Add(new Asset
                {
                    Name = "Company Car - Toyota",
                    Category = "Vehicles",
                    SerialNumber = "SN003",
                    Price = 35000m,
                    EmployeeName = "John Smith",
                    Status = "Assigned"
                });
            }
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                bindingSource.DataSource = service.GetAll();
                dgvAssets.DataSource = bindingSource;

                dgvAssets.AutoGenerateColumns = false;
                dgvAssets.Columns.Clear();

                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Width = 40 });
                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Asset Name", Width = 150 });
                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Category", Width = 100 });
                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SerialNumber", HeaderText = "Serial #", Width = 110 });

                var purchaseDateColumn = new DataGridViewTextBoxColumn { DataPropertyName = "PurchaseDate", HeaderText = "Purchase Date", Width = 110 };
                purchaseDateColumn.DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvAssets.Columns.Add(purchaseDateColumn);

                var priceColumn = new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Price", Width = 90 };
                priceColumn.DefaultCellStyle.Format = "$#,##0.00";
                dgvAssets.Columns.Add(priceColumn);

                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EmployeeName", HeaderText = "Employee", Width = 120 });
                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 90 });
                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ImagePath", HeaderText = "Image Path", Width = 150 });
                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Notes", HeaderText = "Notes", Width = 150 });

                // ===== STYLING =====
                dgvAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAssets.MultiSelect = false;
                dgvAssets.ReadOnly = true;
                dgvAssets.AllowUserToAddRows = false;
                dgvAssets.RowHeadersWidth = 30;

                // Header styling
                dgvAssets.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204); // Dark Blue
                dgvAssets.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvAssets.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

                // Row styling
                dgvAssets.DefaultCellStyle.BackColor = Color.White;
                dgvAssets.DefaultCellStyle.ForeColor = Color.Black;
                dgvAssets.DefaultCellStyle.Font = new Font("Arial", 9);

                // Alternate row color
                dgvAssets.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 250); // Light Blue
                dgvAssets.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

                // Selection styling
                dgvAssets.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 255); // Bright Blue
                dgvAssets.DefaultCellStyle.SelectionForeColor = Color.White;

                // Border
                dgvAssets.GridColor = Color.LightGray;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            NewAssets form = new NewAssets(service, null);
            form.ShowDialog();
            LoadGrid();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedAsset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
                if (selectedAsset != null)
                {
                    NewAssets form = new NewAssets(service, selectedAsset);
                    form.ShowDialog();
                    LoadGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click_2(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
                if (asset == null)
                {
                    MessageBox.Show("Error: Could not retrieve selected asset.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete:\n\nAsset: {asset.Name}\nSerial: {asset.SerialNumber}\nPrice: ${asset.Price:F2}",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    service.Delete(asset.Id);
                    LoadGrid();
                    MessageBox.Show("Asset deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting asset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }
    }
}