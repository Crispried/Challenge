namespace Challange.Forms
{
    partial class BroadcastForm
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
            this.broadcastPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.broadcastPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // broadcastPictureBox
            // 
            this.broadcastPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.broadcastPictureBox.Location = new System.Drawing.Point(0, 0);
            this.broadcastPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.broadcastPictureBox.Name = "broadcastPictureBox";
            this.broadcastPictureBox.Size = new System.Drawing.Size(739, 506);
            this.broadcastPictureBox.TabIndex = 0;
            this.broadcastPictureBox.TabStop = false;
            // 
            // BroadcastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 506);
            this.Controls.Add(this.broadcastPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BroadcastForm";
            this.Text = "Broadcast";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.broadcastPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox broadcastPictureBox;
    }
}