namespace Challange.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.playerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerPanelSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.challangeSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ftpSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.rewindSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawTestPlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeAxis = new System.Windows.Forms.Panel();
            this.challengeRecordingImage = new System.Windows.Forms.PictureBox();
            this.viewLastChallengeButton = new System.Windows.Forms.Button();
            this.challengeTimeAxis = new ChallangeTimeAxis.TimeAxis();
            this.elapsedTimeFromStart = new System.Windows.Forms.Label();
            this.addChallange = new System.Windows.Forms.Button();
            this.toolBox = new System.Windows.Forms.ToolStrip();
            this.startStreamButton = new System.Windows.Forms.ToolStripButton();
            this.stopStreamButton = new System.Windows.Forms.ToolStripButton();
            this.openGameFolderButton = new System.Windows.Forms.ToolStripButton();
            this.openDevicesListButton = new System.Windows.Forms.ToolStripButton();
            this.menu.SuspendLayout();
            this.timeAxis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.challengeRecordingImage)).BeginInit();
            this.toolBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerPanel
            // 
            this.playerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerPanel.AutoScroll = true;
            this.playerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.playerPanel.Location = new System.Drawing.Point(13, 85);
            this.playerPanel.Margin = new System.Windows.Forms.Padding(4);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(980, 493);
            this.playerPanel.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(67, 4);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.menu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.tempToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(1006, 29);
            this.menu.TabIndex = 2;
            this.menu.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerPanelSettings,
            this.challangeSettings,
            this.ftpSettings,
            this.rewindSettings});
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(82, 25);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // playerPanelSettings
            // 
            this.playerPanelSettings.Name = "playerPanelSettings";
            this.playerPanelSettings.Size = new System.Drawing.Size(176, 26);
            this.playerPanelSettings.Text = "Player panel";
            // 
            // challangeSettings
            // 
            this.challangeSettings.Name = "challangeSettings";
            this.challangeSettings.Size = new System.Drawing.Size(176, 26);
            this.challangeSettings.Text = "Challange";
            // 
            // ftpSettings
            // 
            this.ftpSettings.Name = "ftpSettings";
            this.ftpSettings.Size = new System.Drawing.Size(176, 26);
            this.ftpSettings.Text = "FTP";
            // 
            // rewindSettings
            // 
            this.rewindSettings.Name = "rewindSettings";
            this.rewindSettings.Size = new System.Drawing.Size(176, 26);
            this.rewindSettings.Text = "Rewind";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 25);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // tempToolStripMenuItem
            // 
            this.tempToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawTestPlayersToolStripMenuItem});
            this.tempToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.tempToolStripMenuItem.Name = "tempToolStripMenuItem";
            this.tempToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.tempToolStripMenuItem.Text = "Temp";
            // 
            // drawTestPlayersToolStripMenuItem
            // 
            this.drawTestPlayersToolStripMenuItem.Name = "drawTestPlayersToolStripMenuItem";
            this.drawTestPlayersToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.drawTestPlayersToolStripMenuItem.Text = "DrawTestPlayers";
            this.drawTestPlayersToolStripMenuItem.Click += new System.EventHandler(this.drawTestPlayersToolStripMenuItem_Click);
            // 
            // timeAxis
            // 
            this.timeAxis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.timeAxis.Controls.Add(this.challengeRecordingImage);
            this.timeAxis.Controls.Add(this.viewLastChallengeButton);
            this.timeAxis.Controls.Add(this.challengeTimeAxis);
            this.timeAxis.Controls.Add(this.elapsedTimeFromStart);
            this.timeAxis.Controls.Add(this.addChallange);
            this.timeAxis.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timeAxis.Location = new System.Drawing.Point(0, 586);
            this.timeAxis.Margin = new System.Windows.Forms.Padding(4);
            this.timeAxis.Name = "timeAxis";
            this.timeAxis.Size = new System.Drawing.Size(1006, 135);
            this.timeAxis.TabIndex = 3;
            // 
            // challengeRecordingImage
            // 
            this.challengeRecordingImage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.challengeRecordingImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.challengeRecordingImage.Image = ((System.Drawing.Image)(resources.GetObject("challengeRecordingImage.Image")));
            this.challengeRecordingImage.Location = new System.Drawing.Point(736, 70);
            this.challengeRecordingImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.challengeRecordingImage.Name = "challengeRecordingImage";
            this.challengeRecordingImage.Size = new System.Drawing.Size(270, 65);
            this.challengeRecordingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.challengeRecordingImage.TabIndex = 3;
            this.challengeRecordingImage.TabStop = false;
            this.challengeRecordingImage.Visible = false;
            // 
            // viewLastChallengeButton
            // 
            this.viewLastChallengeButton.BackColor = System.Drawing.Color.Transparent;
            this.viewLastChallengeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("viewLastChallengeButton.BackgroundImage")));
            this.viewLastChallengeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.viewLastChallengeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.viewLastChallengeButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewLastChallengeButton.ForeColor = System.Drawing.Color.White;
            this.viewLastChallengeButton.Location = new System.Drawing.Point(0, 70);
            this.viewLastChallengeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.viewLastChallengeButton.Name = "viewLastChallengeButton";
            this.viewLastChallengeButton.Size = new System.Drawing.Size(270, 65);
            this.viewLastChallengeButton.TabIndex = 2;
            this.viewLastChallengeButton.UseVisualStyleBackColor = false;
            this.viewLastChallengeButton.Visible = false;
            // 
            // challengeTimeAxis
            // 
            this.challengeTimeAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.challengeTimeAxis.Location = new System.Drawing.Point(14, 19);
            this.challengeTimeAxis.Margin = new System.Windows.Forms.Padding(5);
            this.challengeTimeAxis.Name = "challengeTimeAxis";
            this.challengeTimeAxis.Size = new System.Drawing.Size(978, 28);
            this.challengeTimeAxis.TabIndex = 0;
            // 
            // elapsedTimeFromStart
            // 
            this.elapsedTimeFromStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.elapsedTimeFromStart.AutoSize = true;
            this.elapsedTimeFromStart.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elapsedTimeFromStart.ForeColor = System.Drawing.SystemColors.Window;
            this.elapsedTimeFromStart.Location = new System.Drawing.Point(441, 90);
            this.elapsedTimeFromStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.elapsedTimeFromStart.Name = "elapsedTimeFromStart";
            this.elapsedTimeFromStart.Size = new System.Drawing.Size(0, 24);
            this.elapsedTimeFromStart.TabIndex = 0;
            // 
            // addChallange
            // 
            this.addChallange.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.addChallange.BackColor = System.Drawing.Color.Transparent;
            this.addChallange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("addChallange.BackgroundImage")));
            this.addChallange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addChallange.Cursor = System.Windows.Forms.Cursors.Default;
            this.addChallange.Enabled = false;
            this.addChallange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addChallange.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addChallange.ForeColor = System.Drawing.Color.White;
            this.addChallange.Location = new System.Drawing.Point(736, 70);
            this.addChallange.Margin = new System.Windows.Forms.Padding(4);
            this.addChallange.Name = "addChallange";
            this.addChallange.Size = new System.Drawing.Size(270, 65);
            this.addChallange.TabIndex = 1;
            this.addChallange.UseVisualStyleBackColor = false;
            // 
            // toolBox
            // 
            this.toolBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.toolBox.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBox.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startStreamButton,
            this.stopStreamButton,
            this.openGameFolderButton,
            this.openDevicesListButton});
            this.toolBox.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolBox.Location = new System.Drawing.Point(0, 29);
            this.toolBox.Name = "toolBox";
            this.toolBox.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.toolBox.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolBox.Size = new System.Drawing.Size(1006, 35);
            this.toolBox.TabIndex = 4;
            this.toolBox.Text = "toolStrip1";
            // 
            // startStreamButton
            // 
            this.startStreamButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startStreamButton.Image = ((System.Drawing.Image)(resources.GetObject("startStreamButton.Image")));
            this.startStreamButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.startStreamButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startStreamButton.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.startStreamButton.Name = "startStreamButton";
            this.startStreamButton.Size = new System.Drawing.Size(33, 32);
            this.startStreamButton.Text = "Start stream";
            // 
            // stopStreamButton
            // 
            this.stopStreamButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopStreamButton.Enabled = false;
            this.stopStreamButton.Image = ((System.Drawing.Image)(resources.GetObject("stopStreamButton.Image")));
            this.stopStreamButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stopStreamButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopStreamButton.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.stopStreamButton.Name = "stopStreamButton";
            this.stopStreamButton.Size = new System.Drawing.Size(32, 32);
            this.stopStreamButton.Text = "Stop stream";
            // 
            // openGameFolderButton
            // 
            this.openGameFolderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openGameFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("openGameFolderButton.Image")));
            this.openGameFolderButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGameFolderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openGameFolderButton.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.openGameFolderButton.Name = "openGameFolderButton";
            this.openGameFolderButton.Size = new System.Drawing.Size(31, 32);
            this.openGameFolderButton.Text = "Open game folder";
            // 
            // openDevicesListButton
            // 
            this.openDevicesListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openDevicesListButton.Image = ((System.Drawing.Image)(resources.GetObject("openDevicesListButton.Image")));
            this.openDevicesListButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openDevicesListButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openDevicesListButton.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.openDevicesListButton.Name = "openDevicesListButton";
            this.openDevicesListButton.Size = new System.Drawing.Size(38, 32);
            this.openDevicesListButton.Text = "Opens list with all conncected devices";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.toolBox);
            this.Controls.Add(this.timeAxis);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.playerPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Challange";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.timeAxis.ResumeLayout(false);
            this.timeAxis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.challengeRecordingImage)).EndInit();
            this.toolBox.ResumeLayout(false);
            this.toolBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel playerPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel timeAxis;
        private System.Windows.Forms.ToolStripMenuItem playerPanelSettings;
        private System.Windows.Forms.Label elapsedTimeFromStart;
        private System.Windows.Forms.Button addChallange;
        private ChallangeTimeAxis.TimeAxis challengeTimeAxis;
        private System.Windows.Forms.ToolStrip toolBox;
        private System.Windows.Forms.ToolStripButton startStreamButton;
        private System.Windows.Forms.ToolStripButton stopStreamButton;
        private System.Windows.Forms.ToolStripMenuItem challangeSettings;
        private System.Windows.Forms.ToolStripButton openGameFolderButton;
        private System.Windows.Forms.ToolStripButton openDevicesListButton;
        private System.Windows.Forms.Button viewLastChallengeButton;
        private System.Windows.Forms.PictureBox challengeRecordingImage;
        private System.Windows.Forms.ToolStripMenuItem tempToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawTestPlayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ftpSettings;
        private System.Windows.Forms.ToolStripMenuItem rewindSettings;
    }
}

