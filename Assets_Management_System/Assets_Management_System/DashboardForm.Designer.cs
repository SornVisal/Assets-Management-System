namespace Assets_Management_System
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnStockTransaction = new System.Windows.Forms.Button();
            this.btnAssets = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.White;
            this.panelSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSidebar.Controls.Add(this.label1);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnReport);
            this.panelSidebar.Controls.Add(this.btnStockTransaction);
            this.panelSidebar.Controls.Add(this.btnAssets);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(220, 450);
            this.panelSidebar.TabIndex = 0;
            this.panelSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSidebar_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "📦 AssetPro";
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.Black;
            this.btnLogout.Location = new System.Drawing.Point(-1, 408);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(220, 41);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.White;
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            this.btnReport.Location = new System.Drawing.Point(-1, 219);
            this.btnReport.Name = "btnReport";
            this.btnReport.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnReport.Size = new System.Drawing.Size(220, 41);
            this.btnReport.TabIndex = 4;
            this.btnReport.Text = "📊 Reports";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnStockTransaction
            // 
            this.btnStockTransaction.BackColor = System.Drawing.Color.White;
            this.btnStockTransaction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStockTransaction.FlatAppearance.BorderSize = 0;
            this.btnStockTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockTransaction.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStockTransaction.ForeColor = System.Drawing.Color.Black;
            this.btnStockTransaction.Location = new System.Drawing.Point(-1, 172);
            this.btnStockTransaction.Name = "btnStockTransaction";
            this.btnStockTransaction.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnStockTransaction.Size = new System.Drawing.Size(220, 41);
            this.btnStockTransaction.TabIndex = 3;
            this.btnStockTransaction.Text = "🔄️ Stock Transactions";
            this.btnStockTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockTransaction.UseVisualStyleBackColor = false;
            this.btnStockTransaction.Click += new System.EventHandler(this.btnStockTransaction_Click);
            // 
            // btnAssets
            // 
            this.btnAssets.BackColor = System.Drawing.Color.White;
            this.btnAssets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAssets.FlatAppearance.BorderSize = 0;
            this.btnAssets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssets.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAssets.ForeColor = System.Drawing.Color.Black;
            this.btnAssets.Location = new System.Drawing.Point(-1, 125);
            this.btnAssets.Name = "btnAssets";
            this.btnAssets.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAssets.Size = new System.Drawing.Size(220, 41);
            this.btnAssets.TabIndex = 2;
            this.btnAssets.Text = "📦 Assets";
            this.btnAssets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAssets.UseVisualStyleBackColor = false;
            this.btnAssets.Click += new System.EventHandler(this.btnAssets_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.White;
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.Black;
            this.btnDashboard.Location = new System.Drawing.Point(-1, 78);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(220, 41);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "🪟 Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(824, 450);
            this.panelMain.TabIndex = 1;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(824, 450);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelMain);
            this.Name = "DashboardForm";
            this.Text = "Form1";
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnStockTransaction;
        private System.Windows.Forms.Button btnAssets;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnLogout;
    }
}

