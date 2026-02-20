using Assets_Management_System.Models;
using Assets_Management_System.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assets_Management_System.UserControls
{
    public partial class NewAssetsUC : UserControl
    {
        private AssetService service;
        private Asset currentAsset;

        public event EventHandler OnSaveCompleted;
        public event EventHandler OnCancelled;

        public NewAssetsUC()
        {
            InitializeComponent();
        }

        public void Initialize(AssetService service, Asset asset)
        {
            this.service = service;
            this.currentAsset = asset;
            
            LoadDropdowns();
            ClearForm();

            if (asset != null)
            {
                lblTitle.Text = "Edit Asset";
                lblSubtitle.Text = "Update information for " + asset.AssetCode;
                LoadAsset(asset);
                
                // Show status but make it read-only during edit
                cbStatus.Visible = true;
                lblStatus.Visible = true;
                cbStatus.Enabled = false; // Status should be changed via Transactions

                if (asset.Status == AssetStatus.Retired)
                {
                    btnSave.Enabled = false;
                    lblSubtitle.Text += " (RETIRED - READ ONLY)";
                    lblSubtitle.ForeColor = Color.Red;
                }
                else
                {
                    btnSave.Enabled = true;
                    lblSubtitle.ForeColor = Color.FromArgb(101, 117, 139);
                }
            }
            else
            {
                lblTitle.Text = "Create New Asset";
                lblSubtitle.Text = "Enter information to add a new asset. It will start as 'Available'.";
                lblSubtitle.ForeColor = Color.FromArgb(101, 117, 139);
                cbStatus.Visible = false;
                lblStatus.Visible = false;
                btnSave.Enabled = true;
            }
        }

        private void LoadDropdowns()
        {
            // Category
            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(new string[]
            {
                "PC","Laptop","Printer","Router","Switch",
                "Monitor","Furniture","Vehicle","Mobile Phone", "Tablet", "Other"
            });
            cbCategory.SelectedIndex = -1;

            // Status
            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new string[] 
            { 
                AssetStatus.Available, 
                AssetStatus.Assigned, 
                AssetStatus.Repair, 
                AssetStatus.Retired 
            });
            cbStatus.SelectedIndex = 0; // Default to Available
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtSerial.Clear();
            txtPrice.Text = "0.00";
            txtNotes.Clear();
            dtPurchase.Value = DateTime.Now;
            cbCategory.SelectedIndex = -1;
            cbStatus.SelectedIndex = 0;
            if (pictureBoxImage.Image != null)
            {
                pictureBoxImage.Image.Dispose();
                pictureBoxImage.Image = null;
            }
        }

        private void LoadAsset(Asset a)
        {
            txtName.Text = a.Name;
            cbCategory.Text = a.Category;
            txtSerial.Text = a.SerialNumber;
            dtPurchase.Value = a.PurchaseDate;
            txtPrice.Text = a.Price.ToString("F2");
            txtNotes.Text = a.Notes;
            cbStatus.Text = a.Status;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Asset name is required.";
                return;
            }

            if (cbCategory.SelectedIndex == -1 && string.IsNullOrWhiteSpace(cbCategory.Text))
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Please select a category.";
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Please enter a valid price.";
                return;
            }

            try
            {
                lblMessage.ForeColor = Color.FromArgb(37, 99, 235);
                lblMessage.Text = "Saving asset...";
                btnSave.Enabled = false;

                if (currentAsset == null)
                {
                    service.Add(new Asset
                    {
                        Name = txtName.Text.Trim(),
                        Category = cbCategory.Text,
                        SerialNumber = txtSerial.Text.Trim(),
                        PurchaseDate = dtPurchase.Value,
                        Price = price,
                        Status = AssetStatus.Available, // Hardcoded for new assets
                        Notes = txtNotes.Text.Trim()
                    });
                }
                else
                {
                    currentAsset.Name = txtName.Text.Trim();
                    currentAsset.Category = cbCategory.Text;
                    currentAsset.SerialNumber = txtSerial.Text.Trim();
                    currentAsset.PurchaseDate = dtPurchase.Value;
                    currentAsset.Price = price;
                    currentAsset.Notes = txtNotes.Text.Trim();
                    // We don't update Status here; it's handled by Assign/Return transactions

                    service.Update(currentAsset);
                }

                lblMessage.ForeColor = Color.FromArgb(16, 185, 129);
                lblMessage.Text = "Asset saved successfully! Closing...";
                
                await System.Threading.Tasks.Task.Delay(1000);
                
                btnSave.Enabled = true;
                OnSaveCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.FromArgb(239, 68, 68);
                lblMessage.Text = "Error: " + ex.Message;
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxImage.Image = Image.FromFile(ofd.FileName);
                }
            }
        }
    }
}
