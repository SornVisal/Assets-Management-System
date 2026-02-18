using Assets_Management_System.Models;
using Assets_Management_System.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Assets_Management_System.Services;
using System.Drawing;

namespace Assets_Management_System
{
    public partial class AssetsForm : Form
    {
        private AssetService service = new AssetService();
        private BindingSource bindingSource = new BindingSource();
        private NewAssetsPanel newAssetsPanel;

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
                    Category = "Laptop",
                    SerialNumber = "SN001",
                    PurchaseDate = DateTime.Today,
                    Price = 1299.99m,
                    Status = "Available"
                });

                service.Add(new Asset
                {
                    Name = "Office Chair",
                    Category = "Furniture",
                    SerialNumber = "SN002",
                    PurchaseDate = DateTime.Today,
                    Price = 189.50m,
                    Status = "Available"
                });
            }

            newAssetsPanel = new NewAssetsPanel();
            newAssetsPanel.OnSaveCompleted += (s, evt) => HideFormPanel();
            newAssetsPanel.OnCancelled += (s, evt) => HideFormPanel();

            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                bindingSource.DataSource = service.GetAll();
                dgvAssets.DataSource = bindingSource;

                // VERY IMPORTANT
                dgvAssets.AutoGenerateColumns = false;
                dgvAssets.Columns.Clear();

                // 👇👇👇 THIS IS WHERE YOUR CODE GOES 👇👇👇
                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "AssetCode",
                    HeaderText = "Asset Code",
                    Width = 100
                });

                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 40
                });

                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Name",
                    HeaderText = "Asset Name",
                    Width = 150
                });

                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Category",
                    HeaderText = "Category",
                    Width = 100
                });

                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SerialNumber",
                    HeaderText = "Serial #",
                    Width = 110
                });

                var purchaseDateColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PurchaseDate",
                    HeaderText = "Purchase Date",
                    Width = 110
                };
                purchaseDateColumn.DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvAssets.Columns.Add(purchaseDateColumn);

                var priceColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Price",
                    HeaderText = "Price",
                    Width = 90
                };
                priceColumn.DefaultCellStyle.Format = "$#,##0.00";
                dgvAssets.Columns.Add(priceColumn);

                dgvAssets.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Status",
                    HeaderText = "Status",
                    Width = 90
                });

                // grid behavior (you already had this part)
                dgvAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAssets.MultiSelect = false;
                dgvAssets.ReadOnly = true;
                dgvAssets.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowFormPanel(Asset asset)
        {
            dgvAssets.Visible = false;

            if (!pnlContent.Controls.Contains(newAssetsPanel))
            {
                pnlContent.Controls.Add(newAssetsPanel);
                newAssetsPanel.Dock = DockStyle.Fill;
            }

            newAssetsPanel.Visible = true;   // 🔥 ADD THIS
            newAssetsPanel.Initialize(service, asset);
            newAssetsPanel.BringToFront();
        }

        private void HideFormPanel()
        {
            dgvAssets.Visible = true;
            newAssetsPanel.Visible = false;
            LoadGrid();
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            ShowFormPanel(null);
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to edit.");
                return;
            }

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            ShowFormPanel(asset); // EDIT MODE
        }

        private void btnDelete_Click_2(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            if (asset == null)
                return;

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

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }

        private void dgvAssets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}