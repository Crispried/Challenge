﻿namespace Challange.Forms
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
            this.playerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerPanelSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeAxis = new System.Windows.Forms.Panel();
            this.challangeTimeAxis = new ChallangeTimeAxis.TimeAxis();
            this.elapsedTimeFromStart = new System.Windows.Forms.Label();
            this.addChallange = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menu.SuspendLayout();
            this.timeAxis.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerPanel
            // 
            this.playerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerPanel.AutoScroll = true;
            this.playerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.playerPanel.Location = new System.Drawing.Point(0, 27);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(784, 494);
            this.playerPanel.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(784, 24);
            this.menu.TabIndex = 2;
            this.menu.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerPanelSettings});
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // playerPanelSettings
            // 
            this.playerPanelSettings.Name = "playerPanelSettings";
            this.playerPanelSettings.Size = new System.Drawing.Size(153, 22);
            this.playerPanelSettings.Text = "Player panel";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // timeAxis
            // 
            this.timeAxis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.timeAxis.Controls.Add(this.challangeTimeAxis);
            this.timeAxis.Controls.Add(this.elapsedTimeFromStart);
            this.timeAxis.Controls.Add(this.addChallange);
            this.timeAxis.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timeAxis.Location = new System.Drawing.Point(0, 501);
            this.timeAxis.Name = "timeAxis";
            this.timeAxis.Size = new System.Drawing.Size(784, 61);
            this.timeAxis.TabIndex = 3;
            // 
            // challangeTimeAxis
            // 
            this.challangeTimeAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.challangeTimeAxis.Location = new System.Drawing.Point(3, 13);
            this.challangeTimeAxis.Name = "challangeTimeAxis";
            this.challangeTimeAxis.Size = new System.Drawing.Size(635, 23);
            this.challangeTimeAxis.TabIndex = 0;
            // 
            // elapsedTimeFromStart
            // 
            this.elapsedTimeFromStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elapsedTimeFromStart.AutoSize = true;
            this.elapsedTimeFromStart.Location = new System.Drawing.Point(12, 39);
            this.elapsedTimeFromStart.Name = "elapsedTimeFromStart";
            this.elapsedTimeFromStart.Size = new System.Drawing.Size(0, 13);
            this.elapsedTimeFromStart.TabIndex = 0;
            // 
            // addChallange
            // 
            this.addChallange.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.addChallange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.addChallange.Location = new System.Drawing.Point(644, 3);
            this.addChallange.Name = "addChallange";
            this.addChallange.Size = new System.Drawing.Size(128, 55);
            this.addChallange.TabIndex = 1;
            this.addChallange.Text = "Challange";
            this.addChallange.UseVisualStyleBackColor = false;
            this.addChallange.Click += new System.EventHandler(this.addChallange_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(661, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.timeAxis);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.playerPanel);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Challange";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.timeAxis.ResumeLayout(false);
            this.timeAxis.PerformLayout();
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
        private ChallangeTimeAxis.TimeAxis challangeTimeAxis;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

