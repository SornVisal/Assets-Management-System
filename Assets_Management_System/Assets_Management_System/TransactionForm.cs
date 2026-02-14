using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing; // REQUIRED for Color, Size, Padding
using System.Windows.Forms;
using Assets_Management_System.Models;

namespace Assets_Management_System
{
    public partial class TransactionForm : Form
    {
        // Fields to hold data
        private BindingList<Asset> _sourceAssets;
        private BindingList<TransactionClass> _transactionHistory;

        // UI Controls declaration
        private Panel panelTop;
        private ComboBox cbAssetName;
        private ComboBox cbTransType;
        private NumericUpDown nudQuantity;
        private TextBox txtNotes;
        private Button btnProcess;
        private DataGridView dgvTransactions;

        // Constructor accepts the asset list from Form1
        public TransactionForm(BindingList<Asset> assets)
        {
            InitializeComponent();
            _sourceAssets = assets;
            _transactionHistory = new BindingList<TransactionClass>();
            SetupDesign(); // Call the design method
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {
            // Logic for when form loads (optional)
        }

        // --- DESIGN CODE ---
        private void SetupDesign()
        {
            // Form Settings
            this.Text = "Stock Transactions";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // 1. TOP PANEL DESIGN
            panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 180;
            panelTop.BackColor = Color.FromArgb(240, 240, 240); // Light gray background
            panelTop.Padding = new Padding(20);

            // Asset Selection
            Label lblAsset = new Label() { Text = "Asset Name:", Left = 20, Top = 20, Width = 120 };
            cbAssetName = new ComboBox() { Left = 150, Top = 20, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cbAssetName.DataSource = _sourceAssets;
            cbAssetName.DisplayMember = "Name"; // Displays the Name property from Asset class

            // Transaction Type
            Label lblType = new Label() { Text = "Type:", Left = 20, Top = 60, Width = 120 };
            cbTransType = new ComboBox() { Left = 150, Top = 60, Width = 250 };
            cbTransType.Items.AddRange(new string[] { "IN (Add Stock)", "OUT (Remove Stock)" });
            cbTransType.SelectedIndex = 0;

            // Quantity
            Label lblQty = new Label() { Text = "Quantity:", Left = 20, Top = 100, Width = 120 };
            nudQuantity = new NumericUpDown() { Left = 150, Top = 100, Width = 100, Minimum = 1, Maximum = 100000 };

            // Notes
            Label lblNotes = new Label() { Text = "Notes:", Left = 20, Top = 140, Width = 120 };
            txtNotes = new TextBox() { Left = 150, Top = 140, Width = 250 };

            // Process Button
            btnProcess = new Button() { Text = "PROCESS TRANSACTION", Left = 420, Top = 60, Width = 130, Height = 80 };
            btnProcess.BackColor = Color.FromArgb(0, 122, 204); // Blue color
            btnProcess.ForeColor = Color.White;
            btnProcess.FlatStyle = FlatStyle.Flat;
            btnProcess.Click += BtnProcess_Click;

            // Add controls to the Panel
            panelTop.Controls.AddRange(new Control[] {
                lblAsset, cbAssetName,
                lblType, cbTransType,
                lblQty, nudQuantity,
                lblNotes, txtNotes,
                btnProcess
            });

            // 2. BOTTOM GRID DESIGN
            dgvTransactions = new DataGridView();
            dgvTransactions.Dock = DockStyle.Fill;
            dgvTransactions.AutoGenerateColumns = true;
            dgvTransactions.ReadOnly = true;
            dgvTransactions.AllowUserToAddRows = false;
            dgvTransactions.BackgroundColor = Color.White;
            dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransactions.DataSource = _transactionHistory;

            // Add Panel and Grid to Form
            this.Controls.Add(dgvTransactions); // Fills the rest of the space
            this.Controls.Add(panelTop);        // Sits at the top
        }

        // --- BUTTON CLICK LOGIC ---
        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (cbAssetName.SelectedItem == null)
            {
                MessageBox.Show("Please select an asset.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Asset selectedAsset = (Asset)cbAssetName.SelectedItem;
            int qty = (int)nudQuantity.Value;
            string type = cbTransType.SelectedIndex == 0 ? "IN" : "OUT";

            // Update Asset Quantity Logic
            if (type == "IN")
            {
                selectedAsset.Quantity += qty;
            }
            else
            {
                if (selectedAsset.Quantity < qty)
                {
                    MessageBox.Show($"Insufficient stock. Only {selectedAsset.Quantity} available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                selectedAsset.Quantity -= qty;
            }

            // Create Transaction Record
            TransactionClass trans = new TransactionClass
            {
                AssetName = selectedAsset.Name,
                TransactionType = type,
                Quantity = qty,
                Remarks = txtNotes.Text
            };

            _transactionHistory.Add(trans);

            MessageBox.Show("Transaction Recorded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNotes.Clear();
        }
    }
}