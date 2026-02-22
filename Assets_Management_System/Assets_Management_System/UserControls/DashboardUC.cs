using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Assets_Management_System.Services;
using Assets_Management_System.Models;

namespace Assets_Management_System.UserControls
{
    public partial class DashboardUC : UserControl
    {
        private AssetService assetService = new AssetService();
        private TransactionService transactionService = new TransactionService();

        public DashboardUC()
        {
            InitializeComponent();
            this.Load += new EventHandler(DashboardUC_Load);
        }

        private void DashboardUC_Load(object sender, EventArgs e)
        {
            labelRecent.ForeColor = Color.FromArgb(31, 41, 55); 
            lblTotalLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblValueLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblAssignedLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblAvailableLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblRepairLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblRetireLabel.ForeColor = Color.FromArgb(107, 114, 128);

            LoadStats();
            LoadRecentTransactions();
        }

        private void LoadStats()
        {
            try
            {
                var assets = assetService.GetAll();

                // 1. Total Assets
                lblTotalCount.Text = assets.Count.ToString();

                // 2. Total Value
                decimal totalValue = assets.Sum(a => a.Price);
                lblValueCount.Text = "$" + totalValue.ToString("N0");

                // 3. Assigned
                int assigned = assets.Count(a => a.Status == AssetStatus.Assigned);
                lblAssignedCount.Text = assigned.ToString();

                // 4. Available
                int available = assets.Count(a => a.Status == AssetStatus.Available);
                lblAvailableCount.Text = available.ToString();

                // 5. In Repair
                int inRepair = assets.Count(a => a.Status == AssetStatus.Repair);
                lblRepairCount.Text = inRepair.ToString();

                // 6. Retired
                int retired = assets.Count(a => a.Status == AssetStatus.Retired);
                lblRetireCount.Text = retired.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message);
            }
        }

        private void LoadRecentTransactions()
        {
            try
            {
                var transactions = transactionService.GetAll();
                
                // Get top 5 recent
                var recent = transactions.OrderByDescending(t => t.TransactionDate).Take(5).ToList();

                dgvRecent.DataSource = recent;
                FormatGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FormatGrid()
        {
            // ALWAYS apply theme even if empty
            dgvRecent.BackgroundColor = Color.White;
            dgvRecent.BorderStyle = BorderStyle.None;
            dgvRecent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRecent.GridColor = Color.FromArgb(239, 243, 248);
            dgvRecent.RowHeadersVisible = false;
            dgvRecent.EnableHeadersVisualStyles = false;
            dgvRecent.ColumnHeadersHeight = 45;
            dgvRecent.RowTemplate.Height = 45;
            dgvRecent.DefaultCellStyle.BackColor = Color.White;
            dgvRecent.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); 
            dgvRecent.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255); 
            dgvRecent.DefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
            dgvRecent.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvRecent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvRecent.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvRecent.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            if (dgvRecent.Columns.Count == 0) return;

            dgvRecent.AutoGenerateColumns = false;

            // Hide IDs
            if (dgvRecent.Columns["Id"] != null) dgvRecent.Columns["Id"].Visible = false;
            if (dgvRecent.Columns["AssetId"] != null) dgvRecent.Columns["AssetId"].Visible = false;

            // Rename and Order
            if (dgvRecent.Columns["TransactionDate"] != null)
            {
                dgvRecent.Columns["TransactionDate"].HeaderText = "Date";
                dgvRecent.Columns["TransactionDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvRecent.Columns["TransactionDate"].Width = 100;
                dgvRecent.Columns["TransactionDate"].DisplayIndex = 0;
            }

            if (dgvRecent.Columns["AssetName"] != null)
            {
                dgvRecent.Columns["AssetName"].HeaderText = "Asset";
                dgvRecent.Columns["AssetName"].Width = 150;
                dgvRecent.Columns["AssetName"].DisplayIndex = 1;
            }

            if (dgvRecent.Columns["TransactionType"] != null)
            {
                dgvRecent.Columns["TransactionType"].HeaderText = "Type";
                dgvRecent.Columns["TransactionType"].Width = 100;
                dgvRecent.Columns["TransactionType"].DisplayIndex = 2;
            }

            if (dgvRecent.Columns["EmployeeName"] != null)
            {
                dgvRecent.Columns["EmployeeName"].HeaderText = "User";
                dgvRecent.Columns["EmployeeName"].Width = 120;
                dgvRecent.Columns["EmployeeName"].DisplayIndex = 3;
            }
            dgvRecent.ReadOnly = true;
        }
    }
}
