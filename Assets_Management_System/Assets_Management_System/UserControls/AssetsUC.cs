using System;
using System.Drawing;
using System.Windows.Forms;
using Assets_Management_System;
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
            
            // Setup search box - leave it completely empty
            txtAssetName.Clear();
            txtAssetName.TextChanged += TxtAssetName_TextChanged;
            
            // Initialize search status
            UpdateSearchStatus(bindingSource.Count, false);
        }

        private void LoadGrid()
        {
            try
            {
                // Get data first to check if there are any assets
                var assets = service.GetAll();
                
                if (assets == null || assets.Count == 0)
                {
                    MessageBox.Show("No assets found in database. Try adding a new asset.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dgvAssets.AutoGenerateColumns = false;
                dgvAssets.Columns.Clear();

                // Setup columns BEFORE binding data
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
                    Visible = false
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

                // NOW bind the data AFTER columns are set up
                bindingSource.DataSource = assets;
                dgvAssets.DataSource = bindingSource;
                
                System.Diagnostics.Debug.WriteLine($"Grid loaded with {assets.Count} assets");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading grid:\n\n{ex.Message}\n\nStack: {ex.StackTrace}", "Grid Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadGrid Exception: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public void RefreshGrid()
        {
            LoadGrid();
        }

        private void TxtAssetName_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtAssetName.Text.Trim();
            
            // If search box is empty, remove filter and show all
            if (string.IsNullOrEmpty(searchText))
            {
                bindingSource.RemoveFilter();
                UpdateSearchStatus(bindingSource.Count, false);
                return;
            }
            
            // Apply filter for actual search text
            FilterBySearchText(searchText);
            UpdateSearchStatus(bindingSource.Count, true);
        }

        private void FilterBySearchText(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                bindingSource.RemoveFilter();
                return;
            }

            try
            {
                // Escape single quotes for SQL
                string escapedSearch = searchText.Replace("'", "''");
                
                string filter = $"AssetCode LIKE '%{escapedSearch}%' OR " +
                               $"Name LIKE '%{escapedSearch}%' OR " +
                               $"SerialNumber LIKE '%{escapedSearch}%' OR " +
                               $"Category LIKE '%{escapedSearch}%'";
                
                bindingSource.Filter = filter;
                System.Diagnostics.Debug.WriteLine($"Search filter applied: {filter}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Filter error: {ex.Message}");
                bindingSource.RemoveFilter();
            }
        }

        private void UpdateSearchStatus(int count, bool isSearching)
        {
            if (lblSearchStatus == null) return;
            
            if (isSearching)
            {
                lblSearchStatus.Text = count == 0 
                    ? "No results found" 
                    : $"Showing {count} result{(count != 1 ? "s" : "")}";
            }
            else
            {
                lblSearchStatus.Text = $"Showing all {count} asset{(count != 1 ? "s" : "")}";
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

            var confirmDialog = new ConfirmationDialog(
                "Delete Confirmation",
                $"Are you sure you want to delete:\n\nAsset: {asset.Name}\nSerial: {asset.SerialNumber}\nPrice: ${asset.Price:F2}",
                isConfirmation: true);
            
            var result = confirmDialog.ShowDialog(this.ParentForm ?? Form.ActiveForm);

            if (result == DialogResult.Yes)
            {
                service.Delete(asset.Id);
                LoadGrid();
                
                var successDialog = new ConfirmationDialog(
                    "Success",
                    "Asset deleted successfully!",
                    isConfirmation: false,
                    isSuccess: true);
                successDialog.ShowDialog(this.ParentForm ?? Form.ActiveForm);
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
            if (asset.Status != AssetStatus.Available)
            {
                MessageBox.Show("Only 'Available' assets can be assigned.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (asset.Status != AssetStatus.Assigned)
            {
                MessageBox.Show("Only 'Assigned' assets can be returned.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowReturnPanel(asset);
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0) return;

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;
            
            if (asset.Status == AssetStatus.Retired)
            {
                MessageBox.Show("Cannot repair a retired asset.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmDialog = new ConfirmationDialog(
                "Confirm Repair",
                $"Send {asset.Name} to repair?",
                isConfirmation: true);
            
            var result = confirmDialog.ShowDialog(this.ParentForm ?? Form.ActiveForm);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    service.SendToRepair(asset.Id, "Sent to repair from Asset List");
                    LoadGrid();
                    
                    var successDialog = new ConfirmationDialog(
                        "Success",
                        "Asset status updated to Repair.",
                        isConfirmation: false,
                        isSuccess: true);
                    successDialog.ShowDialog(this.ParentForm ?? Form.ActiveForm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRetire_Click(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0) return;

            var asset = dgvAssets.SelectedRows[0].DataBoundItem as Asset;

            var confirmDialog = new ConfirmationDialog(
                "Confirm Retirement",
                $"Are you sure you want to RETIRE {asset.Name}?\nThis action is FINAL and cannot be undone.",
                isConfirmation: true);
            
            var result = confirmDialog.ShowDialog(this.ParentForm ?? Form.ActiveForm);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    service.RetireAsset(asset.Id, "Asset retired from Asset List");
                    LoadGrid();
                    
                    var successDialog = new ConfirmationDialog(
                        "Success",
                        "Asset has been retired.",
                        isConfirmation: false,
                        isSuccess: true);
                    successDialog.ShowDialog(this.ParentForm ?? Form.ActiveForm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmsAsset_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Get current mouse position relative to Grid
            Point pos = dgvAssets.PointToClient(Control.MousePosition);
            var hit = dgvAssets.HitTest(pos.X, pos.Y);

            // 1. Only allow opening if clicking on a Cell
            if (hit.Type != DataGridViewHitTestType.Cell)
            {
                e.Cancel = true;
                return;
            }

            // 2. Only allow opening if clicking on the "Status" column
            if (dgvAssets.Columns[hit.ColumnIndex].DataPropertyName != "Status")
            {
                e.Cancel = true;
                return;
            }

            // Select the row that was right-clicked so logic works correctly
            dgvAssets.ClearSelection();
            dgvAssets.Rows[hit.RowIndex].Selected = true;

            var asset = dgvAssets.Rows[hit.RowIndex].DataBoundItem as Asset;
            if (asset == null) return;

            // Enable/Disable based on status
            tsmAssign.Enabled = (asset.Status == AssetStatus.Available);
            tsmReturn.Enabled = (asset.Status == AssetStatus.Assigned);
            tsmRepair.Enabled = (asset.Status != AssetStatus.Retired && asset.Status != AssetStatus.Repair);
            tsmRetire.Enabled = (asset.Status != AssetStatus.Retired);
            
            tsmEdit.Enabled = true; 
            tsmDelete.Enabled = true;
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
