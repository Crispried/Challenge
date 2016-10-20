namespace Challange.Forms
{
    partial class FtpForm
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
            this.ftpAddressTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ftpTestConnectionButton = new System.Windows.Forms.Button();
            this.saveFtpSettingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ftpAddressTextBox
            // 
            this.ftpAddressTextBox.Location = new System.Drawing.Point(100, 28);
            this.ftpAddressTextBox.Name = "ftpAddressTextBox";
            this.ftpAddressTextBox.Size = new System.Drawing.Size(185, 20);
            this.ftpAddressTextBox.TabIndex = 0;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(100, 65);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(185, 20);
            this.userNameTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(100, 101);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(185, 20);
            this.passwordTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FTP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "User name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // ftpTestConnectionButton
            // 
            this.ftpTestConnectionButton.Location = new System.Drawing.Point(164, 146);
            this.ftpTestConnectionButton.Name = "ftpTestConnectionButton";
            this.ftpTestConnectionButton.Size = new System.Drawing.Size(121, 23);
            this.ftpTestConnectionButton.TabIndex = 7;
            this.ftpTestConnectionButton.Text = "FTP Test Connection";
            this.ftpTestConnectionButton.UseVisualStyleBackColor = true;
            this.ftpTestConnectionButton.Click += new System.EventHandler(this.ftpTestConnectionButton_Click);
            // 
            // saveFtpSettingsButton
            // 
            this.saveFtpSettingsButton.Location = new System.Drawing.Point(28, 145);
            this.saveFtpSettingsButton.Name = "saveFtpSettingsButton";
            this.saveFtpSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.saveFtpSettingsButton.TabIndex = 8;
            this.saveFtpSettingsButton.Text = "Save";
            this.saveFtpSettingsButton.UseVisualStyleBackColor = true;
            // 
            // FtpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 196);
            this.Controls.Add(this.saveFtpSettingsButton);
            this.Controls.Add(this.ftpTestConnectionButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.ftpAddressTextBox);
            this.Name = "FtpForm";
            this.Text = "FtpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ftpAddressTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ftpTestConnectionButton;
        private System.Windows.Forms.Button saveFtpSettingsButton;
    }
}