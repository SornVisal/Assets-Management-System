using Assets_Management_System.Services;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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

            if (asset != null)
                LoadData(asset);
        }

        // =========================
        // Load dropdown values
        // =========================
        private void LoadCombos()
        {
            cbCategory2.Items.AddRange(new string[]
            {
                "PC", "Laptop", "Printer", "Router",
                "Switch", "Monitor", "Other"
            });

            cbStatus.Items.AddRange(new string[]
            {
                "Available", "Assigned", "Repair", "Retired"
            });

            // simple employee list (you can replace later)
            cbEmployee.Items.AddRange(new string[]
            {
                "None", "Employee 1", "Employee 2", "Employee 3"
            });

            cbCategory2.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            cbEmployee.SelectedIndex = 0;
        }

        // =========================
        // Load data for editing
        // =========================
        private void LoadData(Asset a)
        {
            txtName.Text = a.Name;
            cbCategory2.Text = a.Category;
            txtSerial.Text = a.SerialNumber;
            dtPurchase.Value = a.PurchaseDate;
            txtPrice.Text = a.Price.ToString();
            cbEmployee.Text = a.EmployeeName;
            cbStatus.Text = a.Status;
            txtNotes.Text = a.Notes;

            imagePath = a.ImagePath;

            if (File.Exists(imagePath))
                pictureBoxImage.Image = Image.FromFile(imagePath);
        }

        // =========================
        // Upload image
        // =========================
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagePath = ofd.FileName;
                pictureBoxImage.Image = Image.FromFile(imagePath);
            }
        }

        // =========================
        // Save button
        // =========================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Asset name is required");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price");
                return;
            }

            Asset asset = new Asset()
            {
                Name = txtName.Text,
                Category = cbCategory2.Text,
                SerialNumber = txtSerial.Text,
                PurchaseDate = dtPurchase.Value,
                Price = price,
                EmployeeName = cbEmployee.Text,
                Status = cbStatus.Text,
                Notes = txtNotes.Text,
                ImagePath = imagePath
            };

            if (editingAsset == null)
            {
                service.Add(asset);
            }
            else
            {
                asset.Id = editingAsset.Id;
                service.Update(asset);
            }

            this.Close();
        }

        // =========================
        // Cancel
        // =========================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================
        // Disable employee if not assigned
        // =========================
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEmployee.Enabled = cbStatus.Text == "Assigned";
        }
    }
}
