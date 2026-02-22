using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assets_Management_System
{
    public partial class ConfirmationDialog : Form
    {
        public DialogResult UserResult { get; set; }
        private Timer autoCloseTimer;

        public ConfirmationDialog(string title, string message, bool isConfirmation = true, bool isSuccess = false)
        {
            InitializeComponent();
            this.Text = title;
            lblTitle.Text = title;
            lblMessage.Text = message;

            if (isSuccess)
            {
                btnYes.BackColor = Color.FromArgb(34, 197, 94); // Green
                btnYes.Text = "OK";
                btnNo.Visible = false;
                btnYes.Location = new System.Drawing.Point(145, 203);
                btnYes.Size = new System.Drawing.Size(80, 32);
                picIcon.Image = SystemIcons.Information.ToBitmap();
                this.BackColor = Color.FromArgb(245, 247, 250);
                
                // Auto-close success dialog after 2 seconds
                autoCloseTimer = new Timer();
                autoCloseTimer.Interval = 2000; // 2 seconds
                autoCloseTimer.Tick += (s, e) => {
                    autoCloseTimer.Stop();
                    autoCloseTimer.Dispose();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };
                autoCloseTimer.Start();
            }
            else if (isConfirmation)
            {
                btnYes.BackColor = Color.FromArgb(37, 99, 235); // Blue
                btnYes.Text = "Yes";
                btnNo.BackColor = Color.FromArgb(203, 213, 225); // Gray
                btnNo.Text = "No";
                btnNo.Visible = true;
                picIcon.Image = SystemIcons.Question.ToBitmap();
                this.BackColor = Color.FromArgb(245, 247, 250);
            }

            UserResult = DialogResult.Cancel;
        }

        private void InitializeComponent()
        {
            this.pnlOverlay = new System.Windows.Forms.Panel();
            this.pnlDialog = new System.Windows.Forms.Panel();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.pnlOverlay.SuspendLayout();
            this.pnlDialog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOverlay
            // 
            this.pnlOverlay.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.pnlOverlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOverlay.Location = new System.Drawing.Point(0, 0);
            this.pnlOverlay.Name = "pnlOverlay";
            this.pnlOverlay.Size = new System.Drawing.Size(371, 250);
            this.pnlOverlay.TabIndex = 0;
            this.pnlOverlay.Visible = false;
            // 
            // pnlDialog
            // 
            this.pnlDialog.BackColor = System.Drawing.Color.White;
            this.pnlDialog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDialog.Controls.Add(this.picIcon);
            this.pnlDialog.Controls.Add(this.lblTitle);
            this.pnlDialog.Controls.Add(this.lblMessage);
            this.pnlDialog.Controls.Add(this.btnYes);
            this.pnlDialog.Controls.Add(this.btnNo);
            this.pnlDialog.Location = new System.Drawing.Point(0, 0);
            this.pnlDialog.Margin = new System.Windows.Forms.Padding(1);
            this.pnlDialog.Name = "pnlDialog";
            this.pnlDialog.Padding = new System.Windows.Forms.Padding(12);
            this.pnlDialog.Size = new System.Drawing.Size(371, 250);
            this.pnlDialog.TabIndex = 1;
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.White;
            this.picIcon.Location = new System.Drawing.Point(12, 12);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(35, 35);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 4;
            this.picIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(50, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMessage.Location = new System.Drawing.Point(12, 55);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(345, 130);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message";
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.FlatAppearance.BorderSize = 0;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(192, 203);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(80, 32);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.FlatAppearance.BorderSize = 0;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            this.btnNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnNo.Location = new System.Drawing.Point(277, 203);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(80, 32);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // ConfirmationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(371, 250);
            this.Controls.Add(this.pnlDialog);
            this.Controls.Add(this.pnlOverlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfirmationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm";
            this.pnlOverlay.ResumeLayout(false);
            this.pnlDialog.ResumeLayout(false);
            this.pnlDialog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            UserResult = DialogResult.Yes;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            UserResult = DialogResult.No;
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private System.Windows.Forms.Panel pnlOverlay;
        private System.Windows.Forms.Panel pnlDialog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.PictureBox picIcon;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                autoCloseTimer?.Stop();
                autoCloseTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

