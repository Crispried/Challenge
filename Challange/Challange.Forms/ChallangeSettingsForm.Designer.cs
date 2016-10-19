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
            this.futureSecondsTextBox = new System.Windows.Forms.TextBox();
            this.pastSecondsLabel = new System.Windows.Forms.Label();
            this.futureSecondsLabel = new System.Windows.Forms.Label();
            this.pastSecondsTextBox = new System.Windows.Forms.TextBox();
            this.saveChallangeSettingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // futureSecondsTextBox
            // 
            this.futureSecondsTextBox.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.futureSecondsTextBox.Location = new System.Drawing.Point(300, 106);
            this.futureSecondsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.futureSecondsTextBox.MaxLength = 2;
            this.futureSecondsTextBox.Name = "futureSecondsTextBox";
            this.futureSecondsTextBox.Size = new System.Drawing.Size(80, 34);
            this.futureSecondsTextBox.TabIndex = 10;
            this.futureSecondsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.futureSecondsTextBox_KeyPress);
            // 
            // pastSecondsLabel
            // 
            this.pastSecondsLabel.AutoSize = true;
            this.pastSecondsLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pastSecondsLabel.Location = new System.Drawing.Point(40, 52);
            this.pastSecondsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pastSecondsLabel.Name = "pastSecondsLabel";
            this.pastSecondsLabel.Size = new System.Drawing.Size(148, 29);
            this.pastSecondsLabel.TabIndex = 9;
            this.pastSecondsLabel.Text = "Past seconds";
            // 
            // futureSecondsLabel
            // 
            this.futureSecondsLabel.AutoSize = true;
            this.futureSecondsLabel.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.futureSecondsLabel.Location = new System.Drawing.Point(40, 113);
            this.futureSecondsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.futureSecondsLabel.Name = "futureSecondsLabel";
            this.futureSecondsLabel.Size = new System.Drawing.Size(175, 29);
            this.futureSecondsLabel.TabIndex = 11;
            this.futureSecondsLabel.Text = "Future seconds";
            // 
            // pastSecondsTextBox
            // 
            this.pastSecondsTextBox.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pastSecondsTextBox.Location = new System.Drawing.Point(300, 44);
            this.pastSecondsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pastSecondsTextBox.MaxLength = 2;
            this.pastSecondsTextBox.Name = "pastSecondsTextBox";
            this.pastSecondsTextBox.Size = new System.Drawing.Size(79, 34);
            this.pastSecondsTextBox.TabIndex = 8;
            this.pastSecondsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pastSecondsTextBox_KeyPress);
            // 
            // saveChallangeSettingsButton
            // 
            this.saveChallangeSettingsButton.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveChallangeSettingsButton.Location = new System.Drawing.Point(300, 185);
            this.saveChallangeSettingsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveChallangeSettingsButton.Name = "saveChallangeSettingsButton";
            this.saveChallangeSettingsButton.Size = new System.Drawing.Size(80, 62);
            this.saveChallangeSettingsButton.TabIndex = 12;
            this.saveChallangeSettingsButton.Text = "Save";
            this.saveChallangeSettingsButton.UseVisualStyleBackColor = true;
            // 
            // ChallangeSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(443, 250);
            this.Controls.Add(this.saveChallangeSettingsButton);
            this.Controls.Add(this.futureSecondsTextBox);
            this.Controls.Add(this.pastSecondsLabel);
            this.Controls.Add(this.futureSecondsLabel);
            this.Controls.Add(this.pastSecondsTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(461, 297);
            this.MinimumSize = new System.Drawing.Size(461, 297);
            this.Name = "ChallangeSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Challange Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox futureSecondsTextBox;
        private System.Windows.Forms.Label pastSecondsLabel;
        private System.Windows.Forms.Label futureSecondsLabel;
        private System.Windows.Forms.TextBox pastSecondsTextBox;
        private System.Windows.Forms.Button saveChallangeSettingsButton;
    }
}