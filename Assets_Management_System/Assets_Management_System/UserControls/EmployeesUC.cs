using System;
using System.Drawing;
using System.Windows.Forms;
using Assets_Management_System.Models;
using Assets_Management_System.Services;

namespace Assets_Management_System.UserControls
{
    public partial class EmployeesUC : UserControl
    {
        private EmployeeService service = new EmployeeService();
        private BindingSource bindingSource = new BindingSource();
        private EmployeeEntryUC entryPanel;

        public EmployeesUC()
        {
            InitializeComponent();
            InitializeEntryPanel();
            LoadGrid();
        }

        private void InitializeEntryPanel()
        {
            entryPanel = new EmployeeEntryUC();
            entryPanel.Dock = DockStyle.Fill;
            entryPanel.Visible = false;
            entryPanel.OnSaveCompleted += (s, e) => {
                entryPanel.Visible = false;
                dgvEmployees.Visible = true;
                LoadGrid();
            };
            entryPanel.OnCancel += (s, e) => {
                entryPanel.Visible = false;
                dgvEmployees.Visible = true;
            };
            this.Controls.Add(entryPanel);
        }

        private void LoadGrid()
        {
            try
            {
                var list = service.GetAll();
                
                dgvEmployees.AutoGenerateColumns = false;
                dgvEmployees.Columns.Clear();

                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Visible = false
                });

                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "FullName",
                    HeaderText = "Full Name",
                    Width = 200
                });

                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Department",
                    HeaderText = "Department",
                    Width = 120
                });

                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Position",
                    HeaderText = "Position",
                    Width = 150
                });

                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Email",
                    HeaderText = "Email",
                    Width = 180
                });

                dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PhoneNumber",
                    HeaderText = "Phone",
                    Width = 120
                });

                bindingSource.DataSource = list;
                dgvEmployees.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ShowEntryGroup(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;
            var emp = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;
            ShowEntryGroup(emp);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;
            var emp = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;

            if (MessageBox.Show($"Delete employee {emp.FullName}?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                service.Delete(emp.Id);
                LoadGrid();
            }
        }

        private void ShowEntryGroup(Employee emp)
        {
            dgvEmployees.Visible = false;
            entryPanel.Initialize(emp);
            entryPanel.Visible = true;
            entryPanel.BringToFront();
        }
    }
}
