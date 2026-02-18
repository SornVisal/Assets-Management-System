using Assets_Management_System.Models;
using Assets_Management_System.Services;
using System;
using System.Windows.Forms;

namespace Assets_Management_System.Forms
{
    public partial class NewAssetsPanel : UserControl
    {
        private AssetService service;
        private Asset currentAsset;

        public event EventHandler OnSaveCompleted;
        public event EventHandler OnCancelled;

        public NewAssetsPanel()
        {
            InitializeComponent();
        }

        public void Initialize(AssetService service, Asset asset)
        {
            this.service = service;
            LoadCategories();
            ClearForm();

            currentAsset = asset;

            if (asset != null)
                LoadAsset(asset);
        }

        private void LoadCategories()
        {
            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(new string[]
            {
                "PC","Laptop","Printer","Router","Switch",
                "Monitor","Furniture","Vehicle","Other"
            });
            cbCategory.SelectedIndex = -1;
            cbCategory.Text = "Select category";
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtSerial.Clear();
            txtPrice.Text = "0.00";
            txtNotes.Clear();
            dtPurchase.Value = DateTime.Now;
        }

        private void LoadAsset(Asset a)
        {
            txtName.Text = a.Name;
            cbCategory.Text = a.Category;
            txtSerial.Text = a.SerialNumber;
            dtPurchase.Value = a.PurchaseDate;
            txtPrice.Text = a.Price.ToString("F2");
            txtNotes.Text = a.Notes;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Asset name required");
                return;
            }

            if (cbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Select category");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price");
                return;
            }

            if (currentAsset == null)
            {
                service.Add(new Asset
                {
                    Name = txtName.Text,
                    Category = cbCategory.Text,
                    SerialNumber = txtSerial.Text,
                    PurchaseDate = dtPurchase.Value,
                    Price = price,
                    Status = "Available",
                    Notes = txtNotes.Text
                });
            }
            else
            {
                currentAsset.Name = txtName.Text;
                currentAsset.Category = cbCategory.Text;
                currentAsset.SerialNumber = txtSerial.Text;
                currentAsset.PurchaseDate = dtPurchase.Value;
                currentAsset.Price = price;
                currentAsset.Notes = txtNotes.Text;

                service.Update(currentAsset);
            }

            OnSaveCompleted?.Invoke(this, EventArgs.Empty);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
