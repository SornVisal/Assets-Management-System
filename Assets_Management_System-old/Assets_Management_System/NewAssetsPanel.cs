using Assets_Management_System.Models;
using Assets_Management_System.Services;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Assets_Management_System.Forms
{
    public partial class NewAssetsPanel : UserControl
    {
        private AssetService service;
        private Asset currentAsset = null;
        private string imagePath = "";

        public event EventHandler OnSaveCompleted;
        public event EventHandler OnCancelled;

        public NewAssetsPanel()
        {
            InitializeComponent();
        }

        // ===============================
        // INITIALIZE (CALLED FROM AssetsForm)
        // ===============================
        public void Initialize(AssetService service, Asset asset)
        {
            this.service = service;

            LoadCategories();   // 🔴 MUST be first
            ClearForm();        // 🔴 then reset form

            currentAsset = asset;

            if (asset == null)
            {
                txtCode.Text = "(Auto)";
            }
            else
            {
                LoadAsset(asset);
            }
        }

        // ===============================
        // LOAD CATEGORIES
        // ===============================
        private void LoadCategories()
        {
            cbCategory.Items.Clear();

            cbCategory.Items.AddRange(new string[]
            {
                "PC", "Laptop", "Printer", "Router",
                "Switch", "Monitor", "Furniture", "Vehicles", "Other"
            });

            cbCategory.SelectedIndex = -1;
            cbCategory.Text = "Select category";
        }

        // ===============================
        // LOAD ASSET (EDIT MODE)
        // ===============================
        private void LoadAsset(Asset asset)
        {
            txtCode.Text = asset.AssetCode;
            txtName.Text = asset.Name;
            cbCategory.Text = asset.Category;
            txtSerial.Text = asset.SerialNumber;
            dtPurchase.Value = asset.PurchaseDate;
            txtPrice.Text = asset.Price.ToString("F2");
            txtNotes.Text = asset.Notes;

            imagePath = asset.ImagePath ?? "";

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

        // ===============================
        // CLEAR FORM (ADD MODE)
        // ===============================
        private void ClearForm()
        {
            txtName.Text = "";
            cbCategory.SelectedIndex = -1;
            cbCategory.Text = "Select category";
            txtSerial.Text = "";
            dtPurchase.Value = DateTime.Now;
            txtPrice.Text = "0.00";
            txtNotes.Text = "";
            pictureBoxImage.Image = null;
            imagePath = "";
        }

        // ===============================
        // UPLOAD IMAGE
        // ===============================
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.jpg;*.png;*.jpeg;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagePath = ofd.FileName;
                    pictureBoxImage.Image = Image.FromFile(imagePath);
                }
            }
        }

        // ===============================
        // SAVE
        // ===============================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Asset name is required.");
                return;
            }

            if (cbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            if (currentAsset == null)
            {
                // ADD
                var asset = new Asset
                {
                    Name = txtName.Text,
                    Category = cbCategory.Text,
                    SerialNumber = txtSerial.Text,
                    PurchaseDate = dtPurchase.Value,
                    Price = price,
                    Status = "Available",
                    Notes = txtNotes.Text,
                    ImagePath = imagePath
                };

                service.Add(asset);
            }
            else
            {
                // EDIT
                currentAsset.Name = txtName.Text;
                currentAsset.Category = cbCategory.Text;
                currentAsset.SerialNumber = txtSerial.Text;
                currentAsset.PurchaseDate = dtPurchase.Value;
                currentAsset.Price = price;
                currentAsset.Notes = txtNotes.Text;
                currentAsset.ImagePath = imagePath;

                service.Update(currentAsset);
            }

            OnSaveCompleted?.Invoke(this, EventArgs.Empty);
        }

        // ===============================
        // CANCEL
        // ===============================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
