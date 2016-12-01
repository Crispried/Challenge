namespace Challange.Forms
{
    partial class RewindSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RewindSettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveRewindSettings = new System.Windows.Forms.Button();
            this.rewindForwardTextBox = new System.Windows.Forms.TextBox();
            this.rewindBackwardTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.label1.Location = new System.Drawing.Point(60, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rewind forward on";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.label2.Location = new System.Drawing.Point(60, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rewind backward on";
            // 
            // saveRewindSettings
            // 
            this.saveRewindSettings.BackColor = System.Drawing.Color.Transparent;
            this.saveRewindSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveRewindSettings.BackgroundImage")));
            this.saveRewindSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveRewindSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveRewindSettings.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveRewindSettings.ForeColor = System.Drawing.Color.White;
            this.saveRewindSettings.Location = new System.Drawing.Point(312, 265);
            this.saveRewindSettings.Name = "saveRewindSettings";
            this.saveRewindSettings.Size = new System.Drawing.Size(270, 65);
            this.saveRewindSettings.TabIndex = 2;
            this.saveRewindSettings.UseVisualStyleBackColor = false;
            // 
            // rewindForwardTextBox
            // 
            this.rewindForwardTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.rewindForwardTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rewindForwardTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rewindForwardTextBox.Location = new System.Drawing.Point(350, 75);
            this.rewindForwardTextBox.MaxLength = 2;
            this.rewindForwardTextBox.Name = "rewindForwardTextBox";
            this.rewindForwardTextBox.Size = new System.Drawing.Size(60, 33);
            this.rewindForwardTextBox.TabIndex = 3;
            this.rewindForwardTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rewindForwardTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rewindForwardTextBox_KeyPress);
            // 
            // rewindBackwardTextBox
            // 
            this.rewindBackwardTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.rewindBackwardTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rewindBackwardTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rewindBackwardTextBox.Location = new System.Drawing.Point(350, 127);
            this.rewindBackwardTextBox.MaxLength = 2;
            this.rewindBackwardTextBox.Name = "rewindBackwardTextBox";
            this.rewindBackwardTextBox.Size = new System.Drawing.Size(60, 33);
            this.rewindBackwardTextBox.TabIndex = 4;
            this.rewindBackwardTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rewindBackwardTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rewindBackwardTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.label3.Location = new System.Drawing.Point(416, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 34);
            this.label3.TabIndex = 5;
            this.label3.Text = "frames";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.label4.Location = new System.Drawing.Point(416, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "frames";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 300);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(138, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 18;
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
            this.pictureBox2.TabIndex = 17;
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
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // RewindSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(582, 328);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rewindBackwardTextBox);
            this.Controls.Add(this.rewindForwardTextBox);
            this.Controls.Add(this.saveRewindSettings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(600, 375);
            this.MinimumSize = new System.Drawing.Size(600, 375);
            this.Name = "RewindSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rewind Settings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveRewindSettings;
        private System.Windows.Forms.TextBox rewindForwardTextBox;
        private System.Windows.Forms.TextBox rewindBackwardTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}