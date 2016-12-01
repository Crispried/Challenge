namespace Challange.Forms
{
    partial class ChallangeSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChallangeSettingsForm));
            this.futureSecondsTextBox = new System.Windows.Forms.TextBox();
            this.pastSecondsLabel = new System.Windows.Forms.Label();
            this.futureSecondsLabel = new System.Windows.Forms.Label();
            this.pastSecondsTextBox = new System.Windows.Forms.TextBox();
            this.saveChallangeSettingsButton = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // futureSecondsTextBox
            // 
            this.futureSecondsTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.futureSecondsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.futureSecondsTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.futureSecondsTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(224)))));
            this.futureSecondsTextBox.Location = new System.Drawing.Point(363, 123);
            this.futureSecondsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.futureSecondsTextBox.MaxLength = 2;
            this.futureSecondsTextBox.Name = "futureSecondsTextBox";
            this.futureSecondsTextBox.Size = new System.Drawing.Size(142, 33);
            this.futureSecondsTextBox.TabIndex = 10;
            this.futureSecondsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.futureSecondsTextBox_KeyPress);
            // 
            // pastSecondsLabel
            // 
            this.pastSecondsLabel.AutoSize = true;
            this.pastSecondsLabel.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pastSecondsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.pastSecondsLabel.Location = new System.Drawing.Point(73, 73);
            this.pastSecondsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pastSecondsLabel.Name = "pastSecondsLabel";
            this.pastSecondsLabel.Size = new System.Drawing.Size(176, 34);
            this.pastSecondsLabel.TabIndex = 9;
            this.pastSecondsLabel.Text = "Past seconds";
            // 
            // futureSecondsLabel
            // 
            this.futureSecondsLabel.AutoSize = true;
            this.futureSecondsLabel.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.futureSecondsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.futureSecondsLabel.Location = new System.Drawing.Point(73, 123);
            this.futureSecondsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.futureSecondsLabel.Name = "futureSecondsLabel";
            this.futureSecondsLabel.Size = new System.Drawing.Size(205, 34);
            this.futureSecondsLabel.TabIndex = 11;
            this.futureSecondsLabel.Text = "Future seconds";
            // 
            // pastSecondsTextBox
            // 
            this.pastSecondsTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.pastSecondsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pastSecondsTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pastSecondsTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(224)))));
            this.pastSecondsTextBox.Location = new System.Drawing.Point(363, 73);
            this.pastSecondsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.pastSecondsTextBox.MaxLength = 2;
            this.pastSecondsTextBox.Name = "pastSecondsTextBox";
            this.pastSecondsTextBox.Size = new System.Drawing.Size(141, 33);
            this.pastSecondsTextBox.TabIndex = 8;
            this.pastSecondsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pastSecondsTextBox_KeyPress);
            // 
            // saveChallangeSettingsButton
            // 
            this.saveChallangeSettingsButton.BackColor = System.Drawing.Color.Transparent;
            this.saveChallangeSettingsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveChallangeSettingsButton.BackgroundImage")));
            this.saveChallangeSettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveChallangeSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveChallangeSettingsButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveChallangeSettingsButton.ForeColor = System.Drawing.Color.White;
            this.saveChallangeSettingsButton.Location = new System.Drawing.Point(313, 265);
            this.saveChallangeSettingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveChallangeSettingsButton.Name = "saveChallangeSettingsButton";
            this.saveChallangeSettingsButton.Size = new System.Drawing.Size(270, 65);
            this.saveChallangeSettingsButton.TabIndex = 12;
            this.saveChallangeSettingsButton.UseVisualStyleBackColor = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 300);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(138, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 265);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(103, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 230);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(79, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // ChallangeSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(582, 328);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.saveChallangeSettingsButton);
            this.Controls.Add(this.futureSecondsTextBox);
            this.Controls.Add(this.pastSecondsLabel);
            this.Controls.Add(this.futureSecondsLabel);
            this.Controls.Add(this.pastSecondsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(600, 375);
            this.MinimumSize = new System.Drawing.Size(600, 375);
            this.Name = "ChallangeSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Challange Settings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox futureSecondsTextBox;
        private System.Windows.Forms.Label pastSecondsLabel;
        private System.Windows.Forms.Label futureSecondsLabel;
        private System.Windows.Forms.TextBox pastSecondsTextBox;
        private System.Windows.Forms.Button saveChallangeSettingsButton;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}