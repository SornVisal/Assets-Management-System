namespace Assets_Management_System.UserControls
{
    partial class AssignAssetUC
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblAssetLabel = new System.Windows.Forms.Label();
            this.lblAssetName = new System.Windows.Forms.Label();
            this.lblCodeLabel = new System.Windows.Forms.Label();
            this.lblAssetCode = new System.Windows.Forms.Label();
            this.lblEmployeeLabel = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.lblDateLabel = new System.Windows.Forms.Label();
            this.dtAssignDate = new System.Windows.Forms.DateTimePicker();
            this.lblNotesLabel = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.lblMessage);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.txtNotes);
            this.pnlMain.Controls.Add(this.lblNotesLabel);
            this.pnlMain.Controls.Add(this.dtAssignDate);
            this.pnlMain.Controls.Add(this.lblDateLabel);
            this.pnlMain.Controls.Add(this.cmbEmployee);
            this.pnlMain.Controls.Add(this.lblEmployeeLabel);
            this.pnlMain.Controls.Add(this.lblAssetCode);
            this.pnlMain.Controls.Add(this.lblCodeLabel);
            this.pnlMain.Controls.Add(this.lblAssetName);
            this.pnlMain.Controls.Add(this.lblAssetLabel);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Location = new System.Drawing.Point(20, 20);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(500, 550);
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
            this.lblTitle.Size = new System.Drawing.Size(144, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Assign Asset";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblDescription.Location = new System.Drawing.Point(17, 45);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(264, 19);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Assign this asset to an employee or user.";
            // 
            // lblAssetLabel
            // 
            this.lblAssetLabel.AutoSize = true;
            this.lblAssetLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAssetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblAssetLabel.Location = new System.Drawing.Point(20, 100);
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
            this.lblAssetName.Location = new System.Drawing.Point(20, 120);
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
            // lblEmployeeLabel
            // 
            this.lblEmployeeLabel.AutoSize = true;
            this.lblEmployeeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmployeeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblEmployeeLabel.Location = new System.Drawing.Point(20, 160);
            this.lblEmployeeLabel.Name = "lblEmployeeLabel";
            this.lblEmployeeLabel.Size = new System.Drawing.Size(124, 19);
            this.lblEmployeeLabel.TabIndex = 5;
            this.lblEmployeeLabel.Text = "Employee / User:";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbEmployee.Location = new System.Drawing.Point(20, 185);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(460, 28);
            this.cmbEmployee.TabIndex = 6;
            // 
            // lblDateLabel
            // 
            this.lblDateLabel.AutoSize = true;
            this.lblDateLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblDateLabel.Location = new System.Drawing.Point(20, 230);
            this.lblDateLabel.Name = "lblDateLabel";
            this.lblDateLabel.Size = new System.Drawing.Size(123, 19);
            this.lblDateLabel.TabIndex = 7;
            this.lblDateLabel.Text = "Assignment Date:";
            // 
            // dtAssignDate
            // 
            this.dtAssignDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtAssignDate.Location = new System.Drawing.Point(20, 255);
            this.dtAssignDate.Name = "dtAssignDate";
            this.dtAssignDate.Size = new System.Drawing.Size(460, 27);
            this.dtAssignDate.TabIndex = 8;
            // 
            // lblNotesLabel
            // 
            this.lblNotesLabel.AutoSize = true;
            this.lblNotesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNotesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblNotesLabel.Location = new System.Drawing.Point(20, 300);
            this.lblNotesLabel.Name = "lblNotesLabel";
            this.lblNotesLabel.Size = new System.Drawing.Size(51, 19);
            this.lblNotesLabel.TabIndex = 9;
            this.lblNotesLabel.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNotes.Location = new System.Drawing.Point(20, 325);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(460, 100);
            this.txtNotes.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(360, 480);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Assign Now";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblMessage.Location = new System.Drawing.Point(20, 440);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 19);
            this.lblMessage.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.btnCancel.Location = new System.Drawing.Point(250, 480);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AssignAssetUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "AssignAssetUC";
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
        private System.Windows.Forms.Label lblEmployeeLabel;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Label lblDateLabel;
        private System.Windows.Forms.DateTimePicker dtAssignDate;
        private System.Windows.Forms.Label lblNotesLabel;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMessage;
    }
}
