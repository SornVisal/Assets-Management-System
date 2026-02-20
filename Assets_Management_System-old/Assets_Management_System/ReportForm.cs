using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Assets_Management_System.Services;

namespace Assets_Management_System
{
    public partial class ReportForm : Form
    {
        private AssetService assetService = new AssetService();

        public ReportForm()
        {
            InitializeComponent();
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
                        filteredList = filteredList.Where(a => a.Status == "Repair"); // Check exact string usage
                        break;
                    case "Retired Assets":
                        filteredList = filteredList.Where(a => a.Status == "Retired");
                        break;
                }

                var list = filteredList.ToList();
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

        private void FormatGrid()
        {
            if (dgvReport.Columns.Count == 0) return;
            
            dgvReport.AutoGenerateColumns = false;

            // Simple common formatting
            if (dgvReport.Columns["Id"] != null) dgvReport.Columns["Id"].Visible = false;
            
            if (dgvReport.Columns["Price"] != null)
                dgvReport.Columns["Price"].DefaultCellStyle.Format = "c"; // Currency

            if (dgvReport.Columns["PurchaseDate"] != null)
                dgvReport.Columns["PurchaseDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
        }
    }
}
