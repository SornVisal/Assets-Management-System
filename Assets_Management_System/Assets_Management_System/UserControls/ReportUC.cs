using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using Assets_Management_System.Services;
using Assets_Management_System.Models;

namespace Assets_Management_System.UserControls
{
    public partial class ReportUC : UserControl
    {
        private AssetService assetService = new AssetService();
        private ReportService reportService = new ReportService();
        private int currentRowIndex = 0; // Tracking for PDF paging

        public ReportUC()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            labelTitle.ForeColor = Color.FromArgb(31, 41, 55); 
            
            // Clear and add items to ensure order
            cbReportType.Items.Clear();
            cbReportType.Items.AddRange(new object[] {
                "Current Asset Assignment",
                "Transaction History",
                "All Assets",
                "Assigned Assets",
                "Available Assets",
                "Assets Under Repair",
                "Retired Assets"
            });
            cbReportType.SelectedIndex = 0;

            // Default dates
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;

            ToggleFilters();
            lblSummary.Text = "Select a report type and click Generate.";
            lblCount.Text = "Total Records: 0";
        }

        private void ToggleFilters()
        {
            bool showDates = cbReportType.Text == "Transaction History";
            lblFrom.Visible = dtpFrom.Visible = showDates;
            lblTo.Visible = dtpTo.Visible = showDates;
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleFilters();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = cbReportType.Text;
                DataTable dt = null;

                // Validate Date Range if applicable
                if (reportType == "Transaction History")
                {
                    if (dtpFrom.Value.Date > dtpTo.Value.Date)
                    {
                        MessageBox.Show("'From' date cannot be after 'To' date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Fetch Data using ReportService
                switch (reportType)
                {
                    case "Current Asset Assignment":
                        dt = reportService.GetCurrentAssignments();
                        break;
                    case "Transaction History":
                        dt = reportService.GetTransactionHistory(dtpFrom.Value, dtpTo.Value);
                        break;
                    case "All Assets":
                        dt = reportService.GetAssetsByStatus();
                        break;
                    case "Assigned Assets":
                        dt = reportService.GetAssetsByStatus(AssetStatus.Assigned);
                        break;
                    case "Available Assets":
                        dt = reportService.GetAssetsByStatus(AssetStatus.Available);
                        break;
                    case "Assets Under Repair":
                        dt = reportService.GetAssetsByStatus(AssetStatus.Repair);
                        break;
                    case "Retired Assets":
                        dt = reportService.GetAssetsByStatus(AssetStatus.Retired);
                        break;
                }

                dgvReport.DataSource = dt;
                
                int count = dt?.Rows.Count ?? 0;
                lblCount.Text = $"Total Records: {count}";
                
                string dateRange = reportType == "Transaction History" 
                    ? $" from {dtpFrom.Value:yyyy-MM-dd} to {dtpTo.Value:yyyy-MM-dd}" 
                    : "";
                lblSummary.Text = $"Showing {reportType}{dateRange}";

                if (count == 0)
                {
                    MessageBox.Show("No records found for this report and filters.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export. Please generate a report first.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv)|*.csv";
            sfd.FileName = cbReportType.Text.Replace(" ", "_") + "_" + DateTime.Now.ToString("yyyyMMdd");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToCSV(sfd.FileName);
                    MessageBox.Show("Report exported successfully!", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting report: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCSV(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // 1. Add Report Metadata Header (Professional improvement F)
            sb.AppendLine("\"REPORT TITLE:\",\"" + cbReportType.Text + "\"");
            sb.AppendLine("\"GENERATED ON:\",\"" + DateTime.Now.ToString("f") + "\"");
            if (cbReportType.Text == "Transaction History")
            {
                sb.AppendLine("\"DATE RANGE:\",\"" + dtpFrom.Value.ToShortDateString() + " to " + dtpTo.Value.ToShortDateString() + "\"");
            }
            sb.AppendLine("\"TOTAL RECORDS:\",\"" + dgvReport.Rows.Count + "\"");
            sb.AppendLine(); // Empty line separator

            // 2. Data Headers
            var headers = dgvReport.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .Select(c => "\"" + c.HeaderText.Replace("\"", "\"\"") + "\"");
            sb.AppendLine(string.Join(",", headers));

            // 3. Data Rows
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>()
                        .Where(c => c.OwningColumn.Visible)
                        .Select(c => {
                            string val = c.Value?.ToString() ?? "";
                            return "\"" + val.Replace("\"", "\"\"") + "\"";
                        });
                    sb.AppendLine(string.Join(",", cells));
                }
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export. Please generate a report first.", "PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentRowIndex = 0; 
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            pd.BeginPrint += (s, ev) => { currentRowIndex = 0; };
            pd.PrintPage += new PrintPageEventHandler(PrintReportPage);

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.WindowState = FormWindowState.Maximized;
            ppd.ShowDialog();
        }

        private void PrintReportPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 10);
            Font footerFont = new Font("Segoe UI", 8, FontStyle.Italic);
            
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float rowHeight = 30;
            
            // Draw Title
            g.DrawString(cbReportType.Text, titleFont, Brushes.Black, x, y);
            y += 40;

            // Draw Metadata
            string metadata = $"Generated on: {DateTime.Now:f} | Total Records: {dgvReport.Rows.Count}";
            if (cbReportType.Text == "Transaction History")
                metadata += $" | Range: {dtpFrom.Value:d} - {dtpTo.Value:d}";
            
            g.DrawString(metadata, bodyFont, Brushes.Gray, x, y);
            y += 40;

            // Get visible columns
            var columns = dgvReport.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
            float totalWidth = e.MarginBounds.Width;
            float colWidth = totalWidth / columns.Count;

            // Draw Headers
            g.FillRectangle(new SolidBrush(Color.FromArgb(15, 23, 42)), x, y, totalWidth, rowHeight);
            for (int i = 0; i < columns.Count; i++)
            {
                g.DrawString(columns[i].HeaderText, headerFont, Brushes.White, x + (i * colWidth) + 5, y + 5);
            }
            y += rowHeight;

            // Draw Rows
            for (int i = currentRowIndex; i < dgvReport.Rows.Count; i++)
            {
                DataGridViewRow row = dgvReport.Rows[i];
                if (row.IsNewRow) continue;

                if (y + rowHeight > e.MarginBounds.Bottom - 40)
                {
                    currentRowIndex = i; 
                    e.HasMorePages = true;
                    return;
                }

                if (i % 2 == 1)
                    g.FillRectangle(new SolidBrush(Color.FromArgb(249, 250, 251)), x, y, totalWidth, rowHeight);

                for (int j = 0; j < columns.Count; j++)
                {
                    string val = row.Cells[columns[j].Index].Value?.ToString() ?? "";
                    RectangleF cellRect = new RectangleF(x + (j * colWidth) + 5, y + 5, colWidth - 10, rowHeight - 10);
                    g.DrawString(val, bodyFont, Brushes.Black, cellRect);
                }
                
                g.DrawLine(new Pen(Color.FromArgb(229, 231, 235)), x, y + rowHeight, x + totalWidth, y + rowHeight);
                y += rowHeight;
            }

            // Footer
            g.DrawString("Asset Management System - Professional Report", footerFont, Brushes.DarkGray, x, e.MarginBounds.Bottom + 10);

            e.HasMorePages = false;
            currentRowIndex = 0; 
        }

        private void FormatGrid()
        {
            dgvReport.BackgroundColor = Color.White;
            dgvReport.BorderStyle = BorderStyle.None;
            dgvReport.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReport.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvReport.GridColor = Color.FromArgb(239, 243, 248);
            dgvReport.RowHeadersVisible = false;
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.ColumnHeadersHeight = 45;
            dgvReport.RowTemplate.Height = 40;
            dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255); 
            dgvReport.DefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            if (dgvReport.Columns.Count == 0) return;

            // Common Hide
            if (dgvReport.Columns["Id"] != null) dgvReport.Columns["Id"].Visible = false;
            if (dgvReport.Columns["AssetId"] != null) dgvReport.Columns["AssetId"].Visible = false;
            if (dgvReport.Columns["ImagePath"] != null) dgvReport.Columns["ImagePath"].Visible = false;

            // Formatting
            if (dgvReport.Columns["Price"] != null) dgvReport.Columns["Price"].DefaultCellStyle.Format = "C2";
            if (dgvReport.Columns["TransactionDate"] != null) dgvReport.Columns["TransactionDate"].DefaultCellStyle.Format = "g";
            if (dgvReport.Columns["PurchaseDate"] != null) dgvReport.Columns["PurchaseDate"].DefaultCellStyle.Format = "d";

            // Headers
            if (dgvReport.Columns["AssetCode"] != null) dgvReport.Columns["AssetCode"].HeaderText = "Asset Code";
            if (dgvReport.Columns["AssetName"] != null) dgvReport.Columns["AssetName"].HeaderText = "Asset Name";
            if (dgvReport.Columns["EmployeeName"] != null) dgvReport.Columns["EmployeeName"].HeaderText = "Employee";
            if (dgvReport.Columns["TransactionType"] != null) dgvReport.Columns["TransactionType"].HeaderText = "Type";
            if (dgvReport.Columns["AssignedTo"] != null) dgvReport.Columns["AssignedTo"].HeaderText = "Assigned To";
            if (dgvReport.Columns["AssignedDate"] != null) dgvReport.Columns["AssignedDate"].DefaultCellStyle.Format = "d";

            dgvReport.ReadOnly = true;
        }
    }
}
