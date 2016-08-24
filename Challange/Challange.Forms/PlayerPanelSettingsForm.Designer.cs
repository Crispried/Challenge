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
            this.numberOfPlayersLabel = new System.Windows.Forms.Label();
            this.numberOfPlayersTextBox = new System.Windows.Forms.TextBox();
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
            this.savePlayerPanelSettingsButton.Location = new System.Drawing.Point(220, 235);
            this.savePlayerPanelSettingsButton.Name = "savePlayerPanelSettingsButton";
            this.savePlayerPanelSettingsButton.Size = new System.Drawing.Size(60, 50);
            this.savePlayerPanelSettingsButton.TabIndex = 8;
            this.savePlayerPanelSettingsButton.Text = "Save";
            this.savePlayerPanelSettingsButton.UseVisualStyleBackColor = true;
            // 
            // playerHeightLabel
            // 
            this.playerHeightLabel.AutoSize = true;
            this.playerHeightLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerHeightLabel.Location = new System.Drawing.Point(24, 176);
            this.playerHeightLabel.Name = "playerHeightLabel";
            this.playerHeightLabel.Size = new System.Drawing.Size(122, 23);
            this.playerHeightLabel.TabIndex = 7;
            this.playerHeightLabel.Text = "Player height";
            // 
            // playerHeightTextBox
            // 
            this.playerHeightTextBox.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerHeightTextBox.Location = new System.Drawing.Point(219, 170);
            this.playerHeightTextBox.Name = "playerHeightTextBox";
            this.playerHeightTextBox.Size = new System.Drawing.Size(61, 29);
            this.playerHeightTextBox.TabIndex = 6;
            // 
            // playerWidthLabel
            // 
            this.playerWidthLabel.AutoSize = true;
            this.playerWidthLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerWidthLabel.Location = new System.Drawing.Point(24, 126);
            this.playerWidthLabel.Name = "playerWidthLabel";
            this.playerWidthLabel.Size = new System.Drawing.Size(117, 23);
            this.playerWidthLabel.TabIndex = 5;
            this.playerWidthLabel.Text = "Player width";
            // 
            // playerWidthTextBox
            // 
            this.playerWidthTextBox.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerWidthTextBox.Location = new System.Drawing.Point(219, 120);
            this.playerWidthTextBox.Name = "playerWidthTextBox";
            this.playerWidthTextBox.Size = new System.Drawing.Size(60, 29);
            this.playerWidthTextBox.TabIndex = 4;
            // 
            // autosizeCheckButton
            // 
            this.autosizeCheckButton.AutoSize = true;
            this.autosizeCheckButton.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autosizeCheckButton.Location = new System.Drawing.Point(219, 85);
            this.autosizeCheckButton.Name = "autosizeCheckButton";
            this.autosizeCheckButton.Size = new System.Drawing.Size(15, 14);
            this.autosizeCheckButton.TabIndex = 3;
            this.autosizeCheckButton.UseVisualStyleBackColor = true;
            this.autosizeCheckButton.CheckedChanged += new System.EventHandler(this.autosizeCheckButton_CheckedChanged);
            // 
            // autosizeLabel
            // 
            this.autosizeLabel.AutoSize = true;
            this.autosizeLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autosizeLabel.Location = new System.Drawing.Point(24, 76);
            this.autosizeLabel.Name = "autosizeLabel";
            this.autosizeLabel.Size = new System.Drawing.Size(134, 23);
            this.autosizeLabel.TabIndex = 2;
            this.autosizeLabel.Text = "Autosize mode";
            // 
            // numberOfPlayersLabel
            // 
            this.numberOfPlayersLabel.AutoSize = true;
            this.numberOfPlayersLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numberOfPlayersLabel.Location = new System.Drawing.Point(24, 36);
            this.numberOfPlayersLabel.Name = "numberOfPlayersLabel";
            this.numberOfPlayersLabel.Size = new System.Drawing.Size(167, 23);
            this.numberOfPlayersLabel.TabIndex = 1;
            this.numberOfPlayersLabel.Text = "Number of players";
            // 
            // numberOfPlayersTextBox
            // 
            this.numberOfPlayersTextBox.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numberOfPlayersTextBox.Location = new System.Drawing.Point(219, 30);
            this.numberOfPlayersTextBox.MaxLength = 3;
            this.numberOfPlayersTextBox.Name = "numberOfPlayersTextBox";
            this.numberOfPlayersTextBox.Size = new System.Drawing.Size(61, 29);
            this.numberOfPlayersTextBox.TabIndex = 0;
            // 
            // PlayerPanelSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(334, 316);
            this.Controls.Add(this.playerHeightTextBox);
            this.Controls.Add(this.savePlayerPanelSettingsButton);
            this.Controls.Add(this.playerWidthLabel);
            this.Controls.Add(this.playerHeightLabel);
            this.Controls.Add(this.playerWidthTextBox);
            this.Controls.Add(this.autosizeCheckButton);
            this.Controls.Add(this.autosizeLabel);
            this.Controls.Add(this.numberOfPlayersLabel);
            this.Controls.Add(this.numberOfPlayersTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(350, 350);
            this.MinimumSize = new System.Drawing.Size(350, 350);
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
        private System.Windows.Forms.Label numberOfPlayersLabel;
        private System.Windows.Forms.TextBox numberOfPlayersTextBox;

        #endregion
    }
}