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
            this.SuspendLayout();
            // 
            // camerasListBox
            // 
            this.camerasListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.camerasListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camerasListBox.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.camerasListBox.FormattingEnabled = true;
            this.camerasListBox.ItemHeight = 18;
            this.camerasListBox.Location = new System.Drawing.Point(0, 0);
            this.camerasListBox.Name = "camerasListBox";
            this.camerasListBox.Size = new System.Drawing.Size(484, 262);
            this.camerasListBox.TabIndex = 0;
            // 
            // CamerasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.camerasListBox);
            this.MaximumSize = new System.Drawing.Size(500, 300);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "CamerasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CamerasForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox camerasListBox;
    }
}