using System.Drawing;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerPanelSettingsForm));
            this.playerPanelSettings = new System.Windows.Forms.Panel();
            this.savePlayerPanelSettingsButton = new System.Windows.Forms.Button();
            this.playerHeightLabel = new System.Windows.Forms.Label();
            this.playerHeightTextBox = new System.Windows.Forms.TextBox();
            this.playerWidthLabel = new System.Windows.Forms.Label();
            this.playerWidthTextBox = new System.Windows.Forms.TextBox();
            this.autosizeCheckButton = new System.Windows.Forms.CheckBox();
            this.autosizeLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
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
            this.savePlayerPanelSettingsButton.BackColor = System.Drawing.Color.Transparent;
            this.savePlayerPanelSettingsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("savePlayerPanelSettingsButton.BackgroundImage")));
            this.savePlayerPanelSettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.savePlayerPanelSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.savePlayerPanelSettingsButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.savePlayerPanelSettingsButton.ForeColor = System.Drawing.Color.White;
            this.savePlayerPanelSettingsButton.Location = new System.Drawing.Point(314, 315);
            this.savePlayerPanelSettingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.savePlayerPanelSettingsButton.Name = "savePlayerPanelSettingsButton";
            this.savePlayerPanelSettingsButton.Size = new System.Drawing.Size(270, 65);
            this.savePlayerPanelSettingsButton.TabIndex = 8;
            this.savePlayerPanelSettingsButton.UseVisualStyleBackColor = false;
            // 
            // playerHeightLabel
            // 
            this.playerHeightLabel.AutoSize = true;
            this.playerHeightLabel.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerHeightLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.playerHeightLabel.Location = new System.Drawing.Point(77, 175);
            this.playerHeightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerHeightLabel.Name = "playerHeightLabel";
            this.playerHeightLabel.Size = new System.Drawing.Size(176, 34);
            this.playerHeightLabel.TabIndex = 7;
            this.playerHeightLabel.Text = "Player height";
            // 
            // playerHeightTextBox
            // 
            this.playerHeightTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.playerHeightTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playerHeightTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerHeightTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(224)))));
            this.playerHeightTextBox.Location = new System.Drawing.Point(367, 175);
            this.playerHeightTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.playerHeightTextBox.MaxLength = 4;
            this.playerHeightTextBox.Name = "playerHeightTextBox";
            this.playerHeightTextBox.Size = new System.Drawing.Size(145, 33);
            this.playerHeightTextBox.TabIndex = 6;
            this.playerHeightTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.playerHeightTextBox_KeyPress);
            // 
            // playerWidthLabel
            // 
            this.playerWidthLabel.AutoSize = true;
            this.playerWidthLabel.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerWidthLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.playerWidthLabel.Location = new System.Drawing.Point(77, 125);
            this.playerWidthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerWidthLabel.Name = "playerWidthLabel";
            this.playerWidthLabel.Size = new System.Drawing.Size(166, 34);
            this.playerWidthLabel.TabIndex = 5;
            this.playerWidthLabel.Text = "Player width";
            // 
            // playerWidthTextBox
            // 
            this.playerWidthTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.playerWidthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playerWidthTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerWidthTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(224)))));
            this.playerWidthTextBox.Location = new System.Drawing.Point(367, 125);
            this.playerWidthTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.playerWidthTextBox.MaxLength = 4;
            this.playerWidthTextBox.Name = "playerWidthTextBox";
            this.playerWidthTextBox.Size = new System.Drawing.Size(145, 33);
            this.playerWidthTextBox.TabIndex = 4;
            this.playerWidthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.playerWidthTextBox_KeyPress);
            // 
            // autosizeCheckButton
            // 
            this.autosizeCheckButton.AutoSize = true;
            this.autosizeCheckButton.BackColor = System.Drawing.Color.Transparent;
            this.autosizeCheckButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.autosizeCheckButton.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autosizeCheckButton.ForeColor = System.Drawing.Color.Transparent;
            this.autosizeCheckButton.Location = new System.Drawing.Point(492, 84);
            this.autosizeCheckButton.Margin = new System.Windows.Forms.Padding(4);
            this.autosizeCheckButton.Name = "autosizeCheckButton";
            this.autosizeCheckButton.Size = new System.Drawing.Size(18, 17);
            this.autosizeCheckButton.TabIndex = 3;
            this.autosizeCheckButton.UseVisualStyleBackColor = false;
            this.autosizeCheckButton.CheckedChanged += new System.EventHandler(this.autosizeCheckButton_CheckedChanged);
            // 
            // autosizeLabel
            // 
            this.autosizeLabel.AutoSize = true;
            this.autosizeLabel.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autosizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.autosizeLabel.Location = new System.Drawing.Point(77, 75);
            this.autosizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autosizeLabel.Name = "autosizeLabel";
            this.autosizeLabel.Size = new System.Drawing.Size(196, 34);
            this.autosizeLabel.TabIndex = 2;
            this.autosizeLabel.Text = "Autosize mode";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 280);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(79, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 315);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(103, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 350);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(138, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // PlayerPanelSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(582, 378);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playerHeightTextBox);
            this.Controls.Add(this.savePlayerPanelSettingsButton);
            this.Controls.Add(this.playerWidthLabel);
            this.Controls.Add(this.playerHeightLabel);
            this.Controls.Add(this.playerWidthTextBox);
            this.Controls.Add(this.autosizeCheckButton);
            this.Controls.Add(this.autosizeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(600, 425);
            this.MinimumSize = new System.Drawing.Size(600, 425);
            this.Name = "PlayerPanelSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Player Panel Settings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
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

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}