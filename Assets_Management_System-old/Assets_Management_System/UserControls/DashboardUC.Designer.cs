namespace Assets_Management_System.UserControls
{
    partial class DashboardUC
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelRecent = new System.Windows.Forms.Panel();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.labelRecent = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.cardValue = new System.Windows.Forms.Panel();
            this.lblValueCount = new System.Windows.Forms.Label();
            this.lblValueLabel = new System.Windows.Forms.Label();
            this.cardAssigned = new System.Windows.Forms.Panel();
            this.lblAssignedCount = new System.Windows.Forms.Label();
            this.lblAssignedLabel = new System.Windows.Forms.Label();
            this.cardAvailable = new System.Windows.Forms.Panel();
            this.lblAvailableCount = new System.Windows.Forms.Label();
            this.lblAvailableLabel = new System.Windows.Forms.Label();
            this.panelRecent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.panelCards.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.cardValue.SuspendLayout();
            this.cardAssigned.SuspendLayout();
            this.cardAvailable.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRecent
            // 
            this.panelRecent.BackColor = System.Drawing.Color.White;
            this.panelRecent.Controls.Add(this.dgvRecent);
            this.panelRecent.Controls.Add(this.labelRecent);
            this.panelRecent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRecent.Location = new System.Drawing.Point(0, 150);
            this.panelRecent.Name = "panelRecent";
            this.panelRecent.Padding = new System.Windows.Forms.Padding(20);
            this.panelRecent.Size = new System.Drawing.Size(964, 500);
            this.panelRecent.TabIndex = 1;
            // 
            // dgvRecent
            // 
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.AllowUserToResizeRows = false;
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecent.ColumnHeadersHeight = 40;
            this.dgvRecent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRecent.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecent.EnableHeadersVisualStyles = false;
            this.dgvRecent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvRecent.Location = new System.Drawing.Point(20, 60);
            this.dgvRecent.MultiSelect = false;
            this.dgvRecent.Name = "dgvRecent";
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvRecent.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Tai Le", 10F);
            this.dgvRecent.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvRecent.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dgvRecent.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.dgvRecent.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvRecent.RowTemplate.Height = 45;
            this.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.Size = new System.Drawing.Size(924, 420);
            this.dgvRecent.TabIndex = 1;
            // 
            // labelRecent
            // 
            this.labelRecent.AutoSize = true;
            this.labelRecent.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelRecent.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecent.Location = new System.Drawing.Point(20, 20);
            this.labelRecent.Name = "labelRecent";
            this.labelRecent.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.labelRecent.Size = new System.Drawing.Size(200, 37);
            this.labelRecent.TabIndex = 0;
            this.labelRecent.Text = "Recent Transactions";
            // 
            // panelCards
            // 
            this.panelCards.ColumnCount = 4;
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.Controls.Add(this.cardAvailable, 3, 0);
            this.panelCards.Controls.Add(this.cardAssigned, 2, 0);
            this.panelCards.Controls.Add(this.cardValue, 1, 0);
            this.panelCards.Controls.Add(this.cardTotal, 0, 0);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 0);
            this.panelCards.Name = "panelCards";
            this.panelCards.RowCount = 1;
            this.panelCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelCards.Size = new System.Drawing.Size(964, 150);
            this.panelCards.TabIndex = 0;
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.White;
            this.cardTotal.Controls.Add(this.lblTotalCount);
            this.cardTotal.Controls.Add(this.lblTotalLabel);
            this.cardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotal.Location = new System.Drawing.Point(3, 3);
            this.cardTotal.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Size = new System.Drawing.Size(228, 137);
            this.cardTotal.TabIndex = 0;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalCount.Location = new System.Drawing.Point(15, 50);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(37, 41);
            this.lblTotalCount.TabIndex = 1;
            this.lblTotalCount.Text = "0";
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalLabel.Location = new System.Drawing.Point(15, 15);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(93, 21);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "Total Assets";
            // 
            // cardValue
            // 
            this.cardValue.BackColor = System.Drawing.Color.White;
            this.cardValue.Controls.Add(this.lblValueCount);
            this.cardValue.Controls.Add(this.lblValueLabel);
            this.cardValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardValue.Location = new System.Drawing.Point(244, 3);
            this.cardValue.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.cardValue.Name = "cardValue";
            this.cardValue.Size = new System.Drawing.Size(228, 137);
            this.cardValue.TabIndex = 1;
            // 
            // lblValueCount
            // 
            this.lblValueCount.AutoSize = true;
            this.lblValueCount.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueCount.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblValueCount.Location = new System.Drawing.Point(15, 50);
            this.lblValueCount.Name = "lblValueCount";
            this.lblValueCount.Size = new System.Drawing.Size(55, 41);
            this.lblValueCount.TabIndex = 1;
            this.lblValueCount.Text = "$0";
            // 
            // lblValueLabel
            // 
            this.lblValueLabel.AutoSize = true;
            this.lblValueLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblValueLabel.Location = new System.Drawing.Point(15, 15);
            this.lblValueLabel.Name = "lblValueLabel";
            this.lblValueLabel.Size = new System.Drawing.Size(87, 21);
            this.lblValueLabel.TabIndex = 0;
            this.lblValueLabel.Text = "Total Value";
            // 
            // cardAssigned
            // 
            this.cardAssigned.BackColor = System.Drawing.Color.White;
            this.cardAssigned.Controls.Add(this.lblAssignedCount);
            this.cardAssigned.Controls.Add(this.lblAssignedLabel);
            this.cardAssigned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAssigned.Location = new System.Drawing.Point(485, 3);
            this.cardAssigned.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.cardAssigned.Name = "cardAssigned";
            this.cardAssigned.Size = new System.Drawing.Size(228, 137);
            this.cardAssigned.TabIndex = 2;
            // 
            // lblAssignedCount
            // 
            this.lblAssignedCount.AutoSize = true;
            this.lblAssignedCount.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignedCount.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblAssignedCount.Location = new System.Drawing.Point(15, 50);
            this.lblAssignedCount.Name = "lblAssignedCount";
            this.lblAssignedCount.Size = new System.Drawing.Size(37, 41);
            this.lblAssignedCount.TabIndex = 1;
            this.lblAssignedCount.Text = "0";
            // 
            // lblAssignedLabel
            // 
            this.lblAssignedLabel.AutoSize = true;
            this.lblAssignedLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignedLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblAssignedLabel.Location = new System.Drawing.Point(15, 15);
            this.lblAssignedLabel.Name = "lblAssignedLabel";
            this.lblAssignedLabel.Size = new System.Drawing.Size(73, 21);
            this.lblAssignedLabel.TabIndex = 0;
            this.lblAssignedLabel.Text = "Assigned";
            // 
            // cardAvailable
            // 
            this.cardAvailable.BackColor = System.Drawing.Color.White;
            this.cardAvailable.Controls.Add(this.lblAvailableCount);
            this.cardAvailable.Controls.Add(this.lblAvailableLabel);
            this.cardAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAvailable.Location = new System.Drawing.Point(726, 3);
            this.cardAvailable.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.cardAvailable.Name = "cardAvailable";
            this.cardAvailable.Size = new System.Drawing.Size(228, 137);
            this.cardAvailable.TabIndex = 3;
            // 
            // lblAvailableCount
            // 
            this.lblAvailableCount.AutoSize = true;
            this.lblAvailableCount.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCount.ForeColor = System.Drawing.Color.Teal;
            this.lblAvailableCount.Location = new System.Drawing.Point(15, 50);
            this.lblAvailableCount.Name = "lblAvailableCount";
            this.lblAvailableCount.Size = new System.Drawing.Size(37, 41);
            this.lblAvailableCount.TabIndex = 1;
            this.lblAvailableCount.Text = "0";
            // 
            // lblAvailableLabel
            // 
            this.lblAvailableLabel.AutoSize = true;
            this.lblAvailableLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblAvailableLabel.Location = new System.Drawing.Point(15, 15);
            this.lblAvailableLabel.Name = "lblAvailableLabel";
            this.lblAvailableLabel.Size = new System.Drawing.Size(73, 21);
            this.lblAvailableLabel.TabIndex = 0;
            this.lblAvailableLabel.Text = "Available";
            // 
            // DashboardUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.panelRecent);
            this.Controls.Add(this.panelCards);
            this.Name = "DashboardUC";
            this.Size = new System.Drawing.Size(964, 650);
            this.panelRecent.ResumeLayout(false);
            this.panelRecent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.panelCards.ResumeLayout(false);
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.cardValue.ResumeLayout(false);
            this.cardValue.PerformLayout();
            this.cardAssigned.ResumeLayout(false);
            this.cardAssigned.PerformLayout();
            this.cardAvailable.ResumeLayout(false);
            this.cardAvailable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRecent;
        private System.Windows.Forms.Label labelRecent;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.TableLayoutPanel panelCards;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Panel cardValue;
        private System.Windows.Forms.Label lblValueCount;
        private System.Windows.Forms.Label lblValueLabel;
        private System.Windows.Forms.Panel cardAssigned;
        private System.Windows.Forms.Label lblAssignedCount;
        private System.Windows.Forms.Label lblAssignedLabel;
        private System.Windows.Forms.Panel cardAvailable;
        private System.Windows.Forms.Label lblAvailableCount;
        private System.Windows.Forms.Label lblAvailableLabel;
    }
}
