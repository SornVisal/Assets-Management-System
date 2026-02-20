using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using Assets_Management_System.Services;

namespace Assets_Management_System.UserControls
{
    public partial class ReportUC : UserControl
    {
        private AssetService assetService = new AssetService();
        private int currentRowIndex = 0; // Tracking for PDF paging

        public ReportUC()
        {
            InitializeComponent();
            labelTitle.ForeColor = Color.FromArgb(31, 41, 55); 
            cbReportType.SelectedIndex = 0;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                var assets = assetService.GetAll();
                string reportType = cbReportType.Text;

                var filteredList = assets.AsEnumerable();

                switch (reportType)
                {
                    case "All Assets":
                        // No filter
                        break;
                    case "Assigned Assets":
                        filteredList = filteredList.Where(a => a.Status == "Assigned");
                        break;
                    case "Available Assets":
                        filteredList = filteredList.Where(a => a.Status == "Available");
                        break;
                    case "Assets Under Repair":
                        filteredList = filteredList.Where(a => a.Status == "Repair");
                        break;
                    case "Retired Assets":
                        filteredList = filteredList.Where(a => a.Status == "Retired");
                        break;
                }

                var list = filteredList.ToList();
                dgvReport.DataSource = null; // Reset to allow column renaming
                dgvReport.DataSource = list;
                
                if (list.Count == 0)
                {
                    MessageBox.Show("No records found for this report.");
                }

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message);
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
            sfd.Filter = "CSV File (*.csv)|*.csv|Excel Workbook (*.csv)|*.csv";
            sfd.FileName = "Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

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

            // Headers
            var headers = dgvReport.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .Select(c => "\"" + c.HeaderText.Replace("\"", "\"\"") + "\"");
            sb.AppendLine(string.Join(",", headers));

            // Rows
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>()
                        .Where(c => c.OwningColumn.Visible)
                        .Select(c => {
                            string val = c.Value?.ToString() ?? "";
                            // Escape quotes and wrap in quotes
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
            pd.BeginPrint += (s, ev) => { currentRowIndex = 0; }; // Official reset point
            pd.PrintPage += new PrintPageEventHandler(PrintReportPage);

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.WindowState = FormWindowState.Maximized;

            if (ppd.ShowDialog() == DialogResult.OK)
            {
                // The dialog itself handles printing to PDF if the user selects the driver
                // But usually we just show the preview and let them print
            }
        }

        private void PrintReportPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 10);
            
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float rowHeight = 30;
            
            // Draw Title
            g.DrawString("Asset Management Report: " + cbReportType.Text, titleFont, Brushes.Black, x, y);
            y += 50;
            g.DrawString("Generated on: " + DateTime.Now.ToString("f"), bodyFont, Brushes.Gray, x, y);
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

                // Check if we need a new page
                if (y + rowHeight > e.MarginBounds.Bottom)
                {
                    currentRowIndex = i; // Save progress
                    e.HasMorePages = true;
                    return;
                }

                // Alternate row colors
                if (i % 2 == 1)
                    g.FillRectangle(new SolidBrush(Color.FromArgb(249, 250, 251)), x, y, totalWidth, rowHeight);

                for (int j = 0; j < columns.Count; j++)
                {
                    string val = row.Cells[columns[j].Index].Value?.ToString() ?? "";
                    
                    // Simple text clipping / truncation
                    RectangleF cellRect = new RectangleF(x + (j * colWidth) + 5, y + 5, colWidth - 10, rowHeight - 10);
                    g.DrawString(val, bodyFont, Brushes.Black, cellRect);
                }
                
                // Border line
                g.DrawLine(new Pen(Color.FromArgb(229, 231, 235)), x, y + rowHeight, x + totalWidth, y + rowHeight);
                
                y += rowHeight;
            }

            e.HasMorePages = false;
            currentRowIndex = 0; // All done
        }

        private void FormatGrid()
        {
            // ALWAYS apply theme even if empty
            dgvReport.BackgroundColor = Color.White;
            dgvReport.BorderStyle = BorderStyle.None;
            dgvReport.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReport.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvReport.GridColor = Color.FromArgb(239, 243, 248);
            dgvReport.RowHeadersVisible = false;
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.ColumnHeadersHeight = 45;
            dgvReport.RowTemplate.Height = 45;
            dgvReport.DefaultCellStyle.BackColor = Color.White;
            dgvReport.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); 
            dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255); 
            dgvReport.DefaultCellStyle.SelectionForeColor = Color.FromArgb(37, 99, 235);
            dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            if (dgvReport.Columns.Count == 0) return;
            
            dgvReport.AutoGenerateColumns = false;

            // Simple common formatting
            if (dgvReport.Columns["Id"] != null) dgvReport.Columns["Id"].Visible = false;
            
            if (dgvReport.Columns["Price"] != null)
                dgvReport.Columns["Price"].DefaultCellStyle.Format = "c"; // Currency

            if (dgvReport.Columns["PurchaseDate"] != null)
                dgvReport.Columns["PurchaseDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            // Rename columns for better look
            if (dgvReport.Columns["AssetCode"] != null) dgvReport.Columns["AssetCode"].HeaderText = "Code";
            if (dgvReport.Columns["Name"] != null) dgvReport.Columns["Name"].HeaderText = "Asset Name";
            if (dgvReport.Columns["ImagePath"] != null) dgvReport.Columns["ImagePath"].Visible = false;

            dgvReport.ReadOnly = true;
        }
    }
}
