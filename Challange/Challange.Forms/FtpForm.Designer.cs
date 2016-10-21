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
            this.ftpAddressTextBox.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ftpAddressTextBox.Location = new System.Drawing.Point(181, 6);
            this.ftpAddressTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ftpAddressTextBox.Name = "ftpAddressTextBox";
            this.ftpAddressTextBox.Size = new System.Drawing.Size(288, 34);
            this.ftpAddressTextBox.TabIndex = 0;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameTextBox.Location = new System.Drawing.Point(181, 55);
            this.userNameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(288, 34);
            this.userNameTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTextBox.Location = new System.Drawing.Point(181, 102);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(288, 34);
            this.passwordTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "FTP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "User name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // ftpTestConnectionButton
            // 
            this.ftpTestConnectionButton.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ftpTestConnectionButton.Location = new System.Drawing.Point(18, 154);
            this.ftpTestConnectionButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ftpTestConnectionButton.Name = "ftpTestConnectionButton";
            this.ftpTestConnectionButton.Size = new System.Drawing.Size(161, 45);
            this.ftpTestConnectionButton.TabIndex = 7;
            this.ftpTestConnectionButton.Text = "FTP Test Connection";
            this.ftpTestConnectionButton.UseVisualStyleBackColor = true;
            // 
            // saveFtpSettingsButton
            // 
            this.saveFtpSettingsButton.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveFtpSettingsButton.Location = new System.Drawing.Point(369, 144);
            this.saveFtpSettingsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveFtpSettingsButton.Name = "saveFtpSettingsButton";
            this.saveFtpSettingsButton.Size = new System.Drawing.Size(100, 57);
            this.saveFtpSettingsButton.TabIndex = 8;
            this.saveFtpSettingsButton.Text = "Save";
            this.saveFtpSettingsButton.UseVisualStyleBackColor = true;
            // 
            // FtpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(482, 203);
            this.Controls.Add(this.saveFtpSettingsButton);
            this.Controls.Add(this.ftpTestConnectionButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.ftpAddressTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(500, 250);
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "FtpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ftp settings";
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