using System;
using System.Drawing;
using System.Windows.Forms;
using Assets_Management_System.Models;
using Assets_Management_System.Services;


namespace Assets_Management_System.UserControls
{
    public partial class AssetsUC : UserControl
    {
        private AssetService service = new AssetService();
        private BindingSource bindingSource = new BindingSource();
        private NewAssetsUC newAssetsPanel;
        private AssignAssetUC assignAssetUC;
        private ReturnAssetUC returnAssetUC;

        public AssetsUC()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.AssetsUC_Load);

            // Initialize Form Panels
            newAssetsPanel = new NewAssetsUC();
            newAssetsPanel.OnSaveCompleted += (s, evt) => HideFormPanel();
            newAssetsPanel.OnCancelled += (s, evt) => HideFormPanel();
            newAssetsPanel.Dock = DockStyle.Fill;

            assignAssetUC = new AssignAssetUC();
            assignAssetUC.OnAssignCompleted += (s, evt) => HideFormPanel();
            assignAssetUC.OnCancelled += (s, evt) => HideFormPanel();
            assignAssetUC.Dock = DockStyle.Fill;

            returnAssetUC = new ReturnAssetUC();
            returnAssetUC.OnReturnCompleted += (s, evt) => HideFormPanel();
            returnAssetUC.OnCancelled += (s, evt) => HideFormPanel();
            returnAssetUC.Dock = DockStyle.Fill;
        }

        private void AssetsUC_Load(object sender, EventArgs e)
        {
            lblTitle.ForeColor = Color.FromArgb(31, 41, 55); 
            lblSubtitle.ForeColor = Color.FromArgb(107, 114, 128);

             // Initial Data Check (Optional, assuming service handles it or it was done in Dashboard)
             if (service.GetAll().Count == 0)
             {
                 // Add dummy data if needed, or rely on existing data
             }

            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgvAssets.AutoGenerateColumns = false;

                bindingSource.DataSource = service.GetAll();
                dgvAssets.DataSource = bindingSource;
                
                dgvAssets.Columns.Clear();

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
                    Width = 40,
                    Visible = false // Hide ID usually
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

                dgvAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAssets.MultiSelect = false;
                dgvAssets.ReadOnly = true;
                dgvAssets.AllowUserToAddRows = false;
                dgvAssets.RowHeadersVisible = false;

                // PREMIUM STYLING FOR HIGH CONTRAST
                dgvAssets.BackgroundColor = Color.White;
                dgvAssets.BorderStyle = BorderStyle.None;
                dgvAssets.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvAssets.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvAssets.GridColor = Color.FromArgb(239, 243, 248);

                // Row style - using explicit dark colors
                dgvAssets.DefaultCellStyle.BackColor = Color.White;
                dgvAssets.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); 
                dgvAssets.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255); 
                dgvAssets.DefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
                dgvAssets.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

                // Header style
                dgvAssets.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
                dgvAssets.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dgvAssets.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvAssets.ColumnHeadersHeight = 45;
                dgvAssets.EnableHeadersVisualStyles = false;

                dgvAssets.RowTemplate.Height = 45;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading grid: " + ex.Message);
            }
        }

        private void ShowFormPanel(Asset asset)
        {
            dgvAssets.Visible = false;
            // Also hide label10 (Asset List title) if we want clean look, or keep it
            
            if (!pnlContent.Controls.Contains(newAssetsPanel))
            {
                pnlContent.Controls.Add(newAssetsPanel);
            }

            newAssetsPanel.Visible = true;
            newAssetsPanel.Initialize(service, asset);
            newAssetsPanel.BringToFront();
        }

        private void ShowAssignPanel(Asset asset)
        {
            dgvAssets.Visible = false;
            if (!pnlContent.Controls.Contains(assignAssetUC))
            {
                pnlContent.Controls.Add(assignAssetUC);
            }
            assignAssetUC.Visible = true;
            assignAssetUC.Initialize(asset);
            assignAssetUC.BringToFront();
        }

        private void ShowReturnPanel(Asset asset)
        {
            dgvAssets.Visible = false;
            if (!pnlContent.Controls.Contains(returnAssetUC))
            {
                pnlContent.Controls.Add(returnAssetUC);
            }
            returnAssetUC.Visible = true;
            returnAssetUC.Initialize(asset);
            returnAssetUC.BringToFront();
        }

        private void HideFormPanel()
        {
            newAssetsPanel.Visible = false;
            if (assignAssetUC != null) assignAssetUC.Visible = false;
            if (returnAssetUC != null) returnAssetUC.Visible = false;
            dgvAssets.Visible = true;
            LoadGrid();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ShowFormPanel(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to edit.");
                return;
            }

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            ShowFormPanel(asset); 
        }

        private void btnDelete_Click(object sender, EventArgs e)
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

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to assign.");
                return;
            }

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            if (asset.Status == "Assigned")
            {
                MessageBox.Show("This asset is already assigned.", "Already Assigned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowAssignPanel(asset);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset to return.");
                return;
            }

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            if (asset.Status != "Assigned")
            {
                MessageBox.Show("Only 'Assigned' assets can be returned.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowReturnPanel(asset);
        }

        private void txtAssetName_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtAssetName.Text.Trim().ToLower();
            var allAssets = service.GetAll();
            
            if (string.IsNullOrEmpty(keyword))
            {
                bindingSource.DataSource = allAssets;
            }
            else
            {
                var filtered = allAssets.FindAll(a => 
                    (a.Name != null && a.Name.ToLower().Contains(keyword)) || 
                    (a.AssetCode != null && a.AssetCode.ToLower().Contains(keyword)) ||
                    (a.SerialNumber != null && a.SerialNumber.ToLower().Contains(keyword))
                );
                bindingSource.DataSource = filtered;
            }
        }
    }
}
