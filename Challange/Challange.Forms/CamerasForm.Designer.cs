namespace Challange.Forms
{
    partial class CamerasForm
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
            this.camerasListBox = new System.Windows.Forms.ListBox();
            this.noConnectedCameraLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // camerasListBox
            // 
            this.camerasListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.camerasListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camerasListBox.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.camerasListBox.FormattingEnabled = true;
            this.camerasListBox.ItemHeight = 24;
            this.camerasListBox.Location = new System.Drawing.Point(0, 0);
            this.camerasListBox.Margin = new System.Windows.Forms.Padding(4);
            this.camerasListBox.Name = "camerasListBox";
            this.camerasListBox.Size = new System.Drawing.Size(643, 311);
            this.camerasListBox.TabIndex = 0;
            // 
            // noConnectedCameraLabel
            // 
            this.noConnectedCameraLabel.AutoSize = true;
            this.noConnectedCameraLabel.BackColor = System.Drawing.Color.Transparent;
            this.noConnectedCameraLabel.Font = new System.Drawing.Font("Georgia", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noConnectedCameraLabel.Location = new System.Drawing.Point(12, 96);
            this.noConnectedCameraLabel.Name = "noConnectedCameraLabel";
            this.noConnectedCameraLabel.Size = new System.Drawing.Size(579, 86);
            this.noConnectedCameraLabel.TabIndex = 1;
            this.noConnectedCameraLabel.Text = "There are not any connected\r\ncameras :(";
            this.noConnectedCameraLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.noConnectedCameraLabel.Visible = false;
            // 
            // CamerasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 311);
            this.Controls.Add(this.noConnectedCameraLabel);
            this.Controls.Add(this.camerasListBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(661, 358);
            this.MinimumSize = new System.Drawing.Size(661, 358);
            this.Name = "CamerasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CamerasForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox camerasListBox;
        private System.Windows.Forms.Label noConnectedCameraLabel;
    }
}