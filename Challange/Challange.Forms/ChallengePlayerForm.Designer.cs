namespace Challange.Forms
{
    partial class ChallengePlayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChallengePlayerForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rewindForward = new System.Windows.Forms.Button();
            this.rewindBackward = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.challengeTrackBar = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.challengeFormSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.rewindOnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.challengeTrackBar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.rewindForward);
            this.panel1.Controls.Add(this.rewindBackward);
            this.panel1.Controls.Add(this.startButton);
            this.panel1.Controls.Add(this.stopButton);
            this.panel1.Controls.Add(this.challengeTrackBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 468);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 85);
            this.panel1.TabIndex = 0;
            // 
            // rewindForward
            // 
            this.rewindForward.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rewindForward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rewindForward.BackgroundImage")));
            this.rewindForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rewindForward.Location = new System.Drawing.Point(721, 35);
            this.rewindForward.Name = "rewindForward";
            this.rewindForward.Size = new System.Drawing.Size(49, 50);
            this.rewindForward.TabIndex = 4;
            this.rewindForward.UseVisualStyleBackColor = true;
            // 
            // rewindBackward
            // 
            this.rewindBackward.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rewindBackward.BackColor = System.Drawing.Color.Transparent;
            this.rewindBackward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rewindBackward.BackgroundImage")));
            this.rewindBackward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rewindBackward.Location = new System.Drawing.Point(651, 35);
            this.rewindBackward.Name = "rewindBackward";
            this.rewindBackward.Size = new System.Drawing.Size(49, 50);
            this.rewindBackward.TabIndex = 3;
            this.rewindBackward.UseVisualStyleBackColor = false;
            // 
            // startButton
            // 
            this.startButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.startButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("startButton.BackgroundImage")));
            this.startButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.startButton.Location = new System.Drawing.Point(82, 35);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(49, 50);
            this.startButton.TabIndex = 2;
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.stopButton.BackColor = System.Drawing.Color.Transparent;
            this.stopButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stopButton.BackgroundImage")));
            this.stopButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stopButton.Location = new System.Drawing.Point(12, 35);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(49, 50);
            this.stopButton.TabIndex = 1;
            this.stopButton.UseVisualStyleBackColor = false;
            // 
            // challengeTrackBar
            // 
            this.challengeTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.challengeTrackBar.BackColor = System.Drawing.Color.DimGray;
            this.challengeTrackBar.Location = new System.Drawing.Point(3, 3);
            this.challengeTrackBar.Maximum = 50;
            this.challengeTrackBar.Name = "challengeTrackBar";
            this.challengeTrackBar.Size = new System.Drawing.Size(776, 56);
            this.challengeTrackBar.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(782, 440);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.challengeFormSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(782, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // challengeFormSettings
            // 
            this.challengeFormSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rewindOnSetting});
            this.challengeFormSettings.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.challengeFormSettings.Name = "challengeFormSettings";
            this.challengeFormSettings.Size = new System.Drawing.Size(82, 24);
            this.challengeFormSettings.Text = "Settings";
            // 
            // rewindOnSetting
            // 
            this.rewindOnSetting.Name = "rewindOnSetting";
            this.rewindOnSetting.Size = new System.Drawing.Size(181, 26);
            this.rewindOnSetting.Text = "Rewind On";
            // 
            // ChallengePlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ChallengePlayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Challenge Player";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.challengeTrackBar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem challengeFormSettings;
        private System.Windows.Forms.Button rewindForward;
        private System.Windows.Forms.Button rewindBackward;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.TrackBar challengeTrackBar;
        private System.Windows.Forms.ToolStripMenuItem rewindOnSetting;
    }
}