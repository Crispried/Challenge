namespace Challange.Forms
{
    partial class PlayerPanelSettingsForm
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
            this.playerPanelSettings = new System.Windows.Forms.Panel();
            this.savePlayerPanelSettingsButton = new System.Windows.Forms.Button();
            this.playerHeightLabel = new System.Windows.Forms.Label();
            this.playerHeightTextBox = new System.Windows.Forms.TextBox();
            this.playerWidthLabel = new System.Windows.Forms.Label();
            this.playerWidthTextBox = new System.Windows.Forms.TextBox();
            this.autosizeCheckButton = new System.Windows.Forms.CheckBox();
            this.autosizeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playerPanelSettings
            // 
            this.playerPanelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerPanelSettings.Location = new System.Drawing.Point(0, 0);
            this.playerPanelSettings.Name = "playerPanelSettings";
            this.playerPanelSettings.Size = new System.Drawing.Size(334, 316);
            this.playerPanelSettings.TabIndex = 0;
            // 
            // savePlayerPanelSettingsButton
            // 
            this.savePlayerPanelSettingsButton.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.savePlayerPanelSettingsButton.Location = new System.Drawing.Point(184, 178);
            this.savePlayerPanelSettingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.savePlayerPanelSettingsButton.Name = "savePlayerPanelSettingsButton";
            this.savePlayerPanelSettingsButton.Size = new System.Drawing.Size(80, 62);
            this.savePlayerPanelSettingsButton.TabIndex = 8;
            this.savePlayerPanelSettingsButton.Text = "Save";
            this.savePlayerPanelSettingsButton.UseVisualStyleBackColor = true;
            // 
            // playerHeightLabel
            // 
            this.playerHeightLabel.AutoSize = true;
            this.playerHeightLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerHeightLabel.Location = new System.Drawing.Point(14, 122);
            this.playerHeightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerHeightLabel.Name = "playerHeightLabel";
            this.playerHeightLabel.Size = new System.Drawing.Size(154, 29);
            this.playerHeightLabel.TabIndex = 7;
            this.playerHeightLabel.Text = "Player height";
            // 
            // playerHeightTextBox
            // 
            this.playerHeightTextBox.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerHeightTextBox.Location = new System.Drawing.Point(184, 119);
            this.playerHeightTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.playerHeightTextBox.MaxLength = 4;
            this.playerHeightTextBox.Name = "playerHeightTextBox";
            this.playerHeightTextBox.Size = new System.Drawing.Size(80, 34);
            this.playerHeightTextBox.TabIndex = 6;
            this.playerHeightTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.playerHeightTextBox_KeyPress);
            // 
            // playerWidthLabel
            // 
            this.playerWidthLabel.AutoSize = true;
            this.playerWidthLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerWidthLabel.Location = new System.Drawing.Point(14, 67);
            this.playerWidthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerWidthLabel.Name = "playerWidthLabel";
            this.playerWidthLabel.Size = new System.Drawing.Size(148, 29);
            this.playerWidthLabel.TabIndex = 5;
            this.playerWidthLabel.Text = "Player width";
            // 
            // playerWidthTextBox
            // 
            this.playerWidthTextBox.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerWidthTextBox.Location = new System.Drawing.Point(184, 64);
            this.playerWidthTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.playerWidthTextBox.MaxLength = 4;
            this.playerWidthTextBox.Name = "playerWidthTextBox";
            this.playerWidthTextBox.Size = new System.Drawing.Size(79, 34);
            this.playerWidthTextBox.TabIndex = 4;
            this.playerWidthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.playerWidthTextBox_KeyPress);
            // 
            // autosizeCheckButton
            // 
            this.autosizeCheckButton.AutoSize = true;
            this.autosizeCheckButton.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autosizeCheckButton.Location = new System.Drawing.Point(218, 29);
            this.autosizeCheckButton.Margin = new System.Windows.Forms.Padding(4);
            this.autosizeCheckButton.Name = "autosizeCheckButton";
            this.autosizeCheckButton.Size = new System.Drawing.Size(18, 17);
            this.autosizeCheckButton.TabIndex = 3;
            this.autosizeCheckButton.UseVisualStyleBackColor = true;
            this.autosizeCheckButton.CheckedChanged += new System.EventHandler(this.autosizeCheckButton_CheckedChanged);
            // 
            // autosizeLabel
            // 
            this.autosizeLabel.AutoSize = true;
            this.autosizeLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autosizeLabel.Location = new System.Drawing.Point(14, 22);
            this.autosizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autosizeLabel.Name = "autosizeLabel";
            this.autosizeLabel.Size = new System.Drawing.Size(170, 29);
            this.autosizeLabel.TabIndex = 2;
            this.autosizeLabel.Text = "Autosize mode";
            // 
            // PlayerPanelSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.playerHeightTextBox);
            this.Controls.Add(this.savePlayerPanelSettingsButton);
            this.Controls.Add(this.playerWidthLabel);
            this.Controls.Add(this.playerHeightLabel);
            this.Controls.Add(this.playerWidthTextBox);
            this.Controls.Add(this.autosizeCheckButton);
            this.Controls.Add(this.autosizeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "PlayerPanelSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Player Panel Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel playerPanelSettings;
        private System.Windows.Forms.Button savePlayerPanelSettingsButton;
        private System.Windows.Forms.Label playerHeightLabel;
        private System.Windows.Forms.TextBox playerHeightTextBox;
        private System.Windows.Forms.Label playerWidthLabel;
        private System.Windows.Forms.TextBox playerWidthTextBox;
        private System.Windows.Forms.CheckBox autosizeCheckButton;
        private System.Windows.Forms.Label autosizeLabel;

        #endregion
    }
}