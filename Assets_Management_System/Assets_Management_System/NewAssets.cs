using Assets_Management_System.Services;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Assets_Management_System.Models;

namespace Assets_Management_System
{
    public partial class NewAssets : Form
    {
        private AssetService service;
        private Asset editingAsset = null;
        private string imagePath = "";

        public NewAssets(AssetService s, Asset asset = null)
        {
            InitializeComponent();

            service = s;
            editingAsset = asset;

            LoadCombos();

            // Set title based on operation
            if (asset != null)
            {
                this.Text = "Edit Asset";
                LoadData(asset);
            }
            else
            {
                this.Text = "Create a New Asset";
            }
        }

        private void LoadCombos()
        {
            // Categories
            cbCategory2.Items.AddRange(new string[]
            {
                "PC", "Laptop", "Printer", "Router",
                "Switch", "Monitor", "Furniture", "Vehicles", "Other"
            });

            // Status
            cbStatus.Items.AddRange(new string[]
            {
                "Available", "Assigned", "Repair", "Retired"
            });

            // Employees
            cbEmployee.Items.AddRange(new string[]
            {
                "None", "Employee 1", "Employee 2", "Employee 3", "John Smith", "Jane Doe"
            });

            cbCategory2.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            cbEmployee.SelectedIndex = 0;
        }

        private void LoadData(Asset a)
        {
            txtName.Text = a.Name ?? "";
            cbCategory2.Text = a.Category ?? "PC";
            txtSerial.Text = a.SerialNumber ?? "";
            dtPurchase.Value = a.PurchaseDate != DateTime.MinValue ? a.PurchaseDate : DateTime.Now;
            txtPrice.Text = a.Price.ToString("F2");
            cbEmployee.Text = a.EmployeeName ?? "None";
            cbStatus.Text = a.Status ?? "Available";
            txtNotes.Text = a.Notes ?? "";

            imagePath = a.ImagePath ?? "";

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    pictureBoxImage.Image = Image.FromFile(imagePath);
                }
                catch
                {
                    pictureBoxImage.Image = null;
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.png;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagePath = ofd.FileName;
                try
                {
                    pictureBoxImage.Image = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                    imagePath = "";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Asset name is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Invalid price format", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            try
            {
                Asset asset = new Asset()
                {
                    Name = txtName.Text.Trim(),
                    Category = cbCategory2.Text,
                    SerialNumber = txtSerial.Text.Trim(),
                    PurchaseDate = dtPurchase.Value,
                    Price = price,
                    EmployeeName = cbEmployee.Text,
                    Status = cbStatus.Text,
                    Notes = txtNotes.Text.Trim(),
                    ImagePath = imagePath
                };

                if (editingAsset == null)
                {
                    service.Add(asset);
                    MessageBox.Show("Asset added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    asset.Id = editingAsset.Id;
                    service.Update(asset);
                    MessageBox.Show("Asset updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving asset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Only enable employee selection if status is "Assigned"
            cbEmployee.Enabled = cbStatus.Text == "Assigned";
            if (cbStatus.Text != "Assigned")
                cbEmployee.SelectedIndex = 0; // Reset to "None"
        }

        private void NewAssets_Load(object sender, EventArgs e)
        {

        }
    }
}