using Assets_Management_System.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Assets_Management_System.Services;

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

                dgvAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAssets.MultiSelect = false;
                dgvAssets.ReadOnly = true;
                dgvAssets.AllowUserToAddRows = false;
                dgvAssets.RowHeadersWidth = 30;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
                MessageBox.Show("Select an asset first");
                return;
            }

            var selectedAsset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            if (selectedAsset != null)
            {
                NewAssets form = new NewAssets(service, selectedAsset);
                form.ShowDialog();
                LoadGrid();
            }
        }

        private void btnDelete_Click_2(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an asset first");
                return;
            }

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            if (MessageBox.Show($"Delete {asset.Name}?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                service.Delete(asset.Id);
                LoadGrid();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }
    }
}