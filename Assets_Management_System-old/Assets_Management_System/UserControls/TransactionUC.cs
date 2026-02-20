using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Assets_Management_System.Models;
using Assets_Management_System.Services;

namespace Assets_Management_System.UserControls
{
    public partial class TransactionUC : UserControl
    {
        private TransactionService service = new TransactionService();
        private List<TransactionClass> allTransactions = new List<TransactionClass>();

        public TransactionUC()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.TransactionUC_Load);
            this.txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilters();
                e.SuppressKeyPress = true; // Prevent beep
            }
        }

        private void TransactionUC_Load(object sender, EventArgs e)
        {
            lblTitle.ForeColor = Color.FromArgb(31, 41, 55); 

             // First time load: Fetch everything
            allTransactions = service.GetAll();

            // Setup Defaults
            dtFrom.Value = DateTime.Today.AddDays(-30);
            dtTo.Value = DateTime.Today;
            cbType.SelectedIndex = 0; // "All"

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = (allTransactions ?? new List<TransactionClass>()).AsEnumerable();

            // 1. Search (Asset Name)
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string kw = txtSearch.Text.ToLower();
                filtered = filtered.Where(t => 
                    (t.AssetName != null && t.AssetName.ToLower().Contains(kw)) || 
                    (t.EmployeeName != null && t.EmployeeName.ToLower().Contains(kw)));
            }

            // 2. Type
            if (cbType.SelectedIndex > 0) // 0 is All
            {
                string selectedType = cbType.Text;
                filtered = filtered.Where(t => t.TransactionType.Equals(selectedType, StringComparison.OrdinalIgnoreCase));
            }

            // 3. Date Range (Inclusive)
            // Start of From date to End of To date
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date.AddDays(1).AddTicks(-1);

            filtered = filtered.Where(t => t.TransactionDate >= from && t.TransactionDate <= to);

            // Bind to Grid
            dgvTransactions.DataSource = filtered.ToList();
            FormatGrid();
        }

        private void FormatGrid()
        {
            // ALWAYS apply theme even if empty
            dgvTransactions.BackgroundColor = Color.White;
            dgvTransactions.BorderStyle = BorderStyle.None;
            dgvTransactions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTransactions.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTransactions.GridColor = Color.FromArgb(239, 243, 248);
            dgvTransactions.RowHeadersVisible = false;
            dgvTransactions.EnableHeadersVisualStyles = false;
            dgvTransactions.ColumnHeadersHeight = 45;
            dgvTransactions.RowTemplate.Height = 45;
            dgvTransactions.DefaultCellStyle.BackColor = Color.White;
            dgvTransactions.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); 
            dgvTransactions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255); 
            dgvTransactions.DefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
            dgvTransactions.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            if (dgvTransactions.Columns.Count == 0) return;

            dgvTransactions.AutoGenerateColumns = false;
            
            // Hide IDs
            if (dgvTransactions.Columns["Id"] != null) dgvTransactions.Columns["Id"].Visible = false;
            if (dgvTransactions.Columns["AssetId"] != null) dgvTransactions.Columns["AssetId"].Visible = false;

            // Rename and Order
            if (dgvTransactions.Columns["TransactionDate"] != null)
            {
                dgvTransactions.Columns["TransactionDate"].HeaderText = "Date";
                dgvTransactions.Columns["TransactionDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgvTransactions.Columns["TransactionDate"].Width = 140;
                dgvTransactions.Columns["TransactionDate"].DisplayIndex = 0;
            }

            if (dgvTransactions.Columns["AssetName"] != null)
            {
                dgvTransactions.Columns["AssetName"].HeaderText = "Asset";
                dgvTransactions.Columns["AssetName"].Width = 200;
                dgvTransactions.Columns["AssetName"].DisplayIndex = 1;
            }

            if (dgvTransactions.Columns["TransactionType"] != null)
            {
                dgvTransactions.Columns["TransactionType"].HeaderText = "Type";
                dgvTransactions.Columns["TransactionType"].Width = 100;
                dgvTransactions.Columns["TransactionType"].DisplayIndex = 2;
            }

            if (dgvTransactions.Columns["EmployeeName"] != null)
            {
                dgvTransactions.Columns["EmployeeName"].HeaderText = "Employee / User";
                dgvTransactions.Columns["EmployeeName"].Width = 150;
                dgvTransactions.Columns["EmployeeName"].DisplayIndex = 3;
            }

            if (dgvTransactions.Columns["Note"] != null)
            {
                dgvTransactions.Columns["Note"].HeaderText = "Note";
                dgvTransactions.Columns["Note"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvTransactions.Columns["Note"].DisplayIndex = 4;
            }

            dgvTransactions.ReadOnly = true;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cbType.SelectedIndex = 0;
            dtFrom.Value = DateTime.Today.AddDays(-30);
            dtTo.Value = DateTime.Today;
            
            // Re-apply without calling Load again, just filter defaults
            ApplyFilters();
        }
    }
}
