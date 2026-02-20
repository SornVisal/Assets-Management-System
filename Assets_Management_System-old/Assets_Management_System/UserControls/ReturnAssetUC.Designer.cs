namespace Assets_Management_System.UserControls
{
    partial class ReturnAssetUC
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

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblAssetLabel = new System.Windows.Forms.Label();
            this.lblAssetName = new System.Windows.Forms.Label();
            this.lblCodeLabel = new System.Windows.Forms.Label();
            this.lblAssetCode = new System.Windows.Forms.Label();
            this.lblWithLabel = new System.Windows.Forms.Label();
            this.lblCurrentlyWith = new System.Windows.Forms.Label();
            this.lblConditionLabel = new System.Windows.Forms.Label();
            this.cbCondition = new System.Windows.Forms.ComboBox();
            this.lblDateLabel = new System.Windows.Forms.Label();
            this.dtReturnDate = new System.Windows.Forms.DateTimePicker();
            this.lblNotesLabel = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.txtNotes);
            this.pnlMain.Controls.Add(this.lblNotesLabel);
            this.pnlMain.Controls.Add(this.dtReturnDate);
            this.pnlMain.Controls.Add(this.lblDateLabel);
            this.pnlMain.Controls.Add(this.cbCondition);
            this.pnlMain.Controls.Add(this.lblConditionLabel);
            this.pnlMain.Controls.Add(this.lblCurrentlyWith);
            this.pnlMain.Controls.Add(this.lblWithLabel);
            this.pnlMain.Controls.Add(this.lblAssetCode);
            this.pnlMain.Controls.Add(this.lblCodeLabel);
            this.pnlMain.Controls.Add(this.lblAssetName);
            this.pnlMain.Controls.Add(this.lblAssetLabel);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Location = new System.Drawing.Point(20, 20);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(500, 580);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblDescription);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(147, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Return Asset";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDescription.Location = new System.Drawing.Point(17, 45);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(306, 19);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Release asset from employee back to inventory.";
            // 
            // lblAssetLabel
            // 
            this.lblAssetLabel.AutoSize = true;
            this.lblAssetLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAssetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblAssetLabel.Location = new System.Drawing.Point(25, 100);
            this.lblAssetLabel.Name = "lblAssetLabel";
            this.lblAssetLabel.Size = new System.Drawing.Size(48, 19);
            this.lblAssetLabel.TabIndex = 1;
            this.lblAssetLabel.Text = "Asset:";
            // 
            // lblAssetName
            // 
            this.lblAssetName.AutoSize = true;
            this.lblAssetName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblAssetName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblAssetName.Location = new System.Drawing.Point(25, 120);
            this.lblAssetName.Name = "lblAssetName";
            this.lblAssetName.Size = new System.Drawing.Size(94, 21);
            this.lblAssetName.TabIndex = 2;
            this.lblAssetName.Text = "Asset Name";
            // 
            // lblCodeLabel
            // 
            this.lblCodeLabel.AutoSize = true;
            this.lblCodeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblCodeLabel.Location = new System.Drawing.Point(250, 100);
            this.lblCodeLabel.Name = "lblCodeLabel";
            this.lblCodeLabel.Size = new System.Drawing.Size(47, 19);
            this.lblCodeLabel.TabIndex = 3;
            this.lblCodeLabel.Text = "Code:";
            // 
            // lblAssetCode
            // 
            this.lblAssetCode.AutoSize = true;
            this.lblAssetCode.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblAssetCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblAssetCode.Location = new System.Drawing.Point(250, 120);
            this.lblAssetCode.Name = "lblAssetCode";
            this.lblAssetCode.Size = new System.Drawing.Size(76, 21);
            this.lblAssetCode.TabIndex = 4;
            this.lblAssetCode.Text = "AST-0000";
            // 
            // lblWithLabel
            // 
            this.lblWithLabel.AutoSize = true;
            this.lblWithLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWithLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblWithLabel.Location = new System.Drawing.Point(25, 160);
            this.lblWithLabel.Name = "lblWithLabel";
            this.lblWithLabel.Size = new System.Drawing.Size(103, 19);
            this.lblWithLabel.TabIndex = 5;
            this.lblWithLabel.Text = "Currently with:";
            // 
            // lblCurrentlyWith
            // 
            this.lblCurrentlyWith.AutoSize = true;
            this.lblCurrentlyWith.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblCurrentlyWith.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblCurrentlyWith.Location = new System.Drawing.Point(25, 185);
            this.lblCurrentlyWith.Name = "lblCurrentlyWith";
            this.lblCurrentlyWith.Size = new System.Drawing.Size(122, 21);
            this.lblCurrentlyWith.TabIndex = 6;
            this.lblCurrentlyWith.Text = "Employee Name";
            // 
            // lblConditionLabel
            // 
            this.lblConditionLabel.AutoSize = true;
            this.lblConditionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConditionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblConditionLabel.Location = new System.Drawing.Point(25, 230);
            this.lblConditionLabel.Name = "lblConditionLabel";
            this.lblConditionLabel.Size = new System.Drawing.Size(122, 19);
            this.lblConditionLabel.TabIndex = 7;
            this.lblConditionLabel.Text = "Return Condition:";
            // 
            // cbCondition
            // 
            this.cbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCondition.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbCondition.FormattingEnabled = true;
            this.cbCondition.Items.AddRange(new object[] {
            "Good",
            "Broken / Damage"});
            this.cbCondition.Location = new System.Drawing.Point(25, 255);
            this.cbCondition.Name = "cbCondition";
            this.cbCondition.Size = new System.Drawing.Size(450, 28);
            this.cbCondition.TabIndex = 8;
            // 
            // lblDateLabel
            // 
            this.lblDateLabel.AutoSize = true;
            this.lblDateLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblDateLabel.Location = new System.Drawing.Point(25, 305);
            this.lblDateLabel.Name = "lblDateLabel";
            this.lblDateLabel.Size = new System.Drawing.Size(91, 19);
            this.lblDateLabel.TabIndex = 9;
            this.lblDateLabel.Text = "Return Date:";
            // 
            // dtReturnDate
            // 
            this.dtReturnDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtReturnDate.Location = new System.Drawing.Point(25, 330);
            this.dtReturnDate.Name = "dtReturnDate";
            this.dtReturnDate.Size = new System.Drawing.Size(450, 27);
            this.dtReturnDate.TabIndex = 10;
            // 
            // lblNotesLabel
            // 
            this.lblNotesLabel.AutoSize = true;
            this.lblNotesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNotesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblNotesLabel.Location = new System.Drawing.Point(25, 380);
            this.lblNotesLabel.Name = "lblNotesLabel";
            this.lblNotesLabel.Size = new System.Drawing.Size(51, 19);
            this.lblNotesLabel.TabIndex = 11;
            this.lblNotesLabel.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNotes.Location = new System.Drawing.Point(25, 405);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(450, 80);
            this.txtNotes.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(355, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Confirm Return";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.btnCancel.Location = new System.Drawing.Point(245, 510);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ReturnAssetUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ReturnAssetUC";
            this.Size = new System.Drawing.Size(964, 650);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblAssetLabel;
        private System.Windows.Forms.Label lblAssetName;
        private System.Windows.Forms.Label lblCodeLabel;
        private System.Windows.Forms.Label lblAssetCode;
        private System.Windows.Forms.Label lblWithLabel;
        private System.Windows.Forms.Label lblCurrentlyWith;
        private System.Windows.Forms.Label lblConditionLabel;
        private System.Windows.Forms.ComboBox cbCondition;
        private System.Windows.Forms.Label lblDateLabel;
        private System.Windows.Forms.DateTimePicker dtReturnDate;
        private System.Windows.Forms.Label lblNotesLabel;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
