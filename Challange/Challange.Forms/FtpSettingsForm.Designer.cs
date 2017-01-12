namespace Challange.Forms
{
    partial class FtpSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FtpSettingsForm));
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
            this.ftpAddressTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.ftpAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ftpAddressTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ftpAddressTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(224)))));
            this.ftpAddressTextBox.Location = new System.Drawing.Point(269, 75);
            this.ftpAddressTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ftpAddressTextBox.Name = "ftpAddressTextBox";
            this.ftpAddressTextBox.Size = new System.Drawing.Size(290, 33);
            this.ftpAddressTextBox.TabIndex = 0;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.userNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userNameTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(224)))));
            this.userNameTextBox.Location = new System.Drawing.Point(269, 125);
            this.userNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(290, 33);
            this.userNameTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(107)))), ((int)(((byte)(118)))));
            this.passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordTextBox.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(224)))));
            this.passwordTextBox.Location = new System.Drawing.Point(269, 175);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(290, 33);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.label1.Location = new System.Drawing.Point(73, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 34);
            this.label1.TabIndex = 4;
            this.label1.Text = "FTP address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.label2.Location = new System.Drawing.Point(73, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 34);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(171)))));
            this.label3.Location = new System.Drawing.Point(73, 175);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 34);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // ftpTestConnectionButton
            // 
            this.ftpTestConnectionButton.BackColor = System.Drawing.Color.Transparent;
            this.ftpTestConnectionButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ftpTestConnectionButton.BackgroundImage")));
            this.ftpTestConnectionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ftpTestConnectionButton.FlatAppearance.BorderSize = 0;
            this.ftpTestConnectionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ftpTestConnectionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ftpTestConnectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ftpTestConnectionButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ftpTestConnectionButton.ForeColor = System.Drawing.Color.White;
            this.ftpTestConnectionButton.Location = new System.Drawing.Point(-1, 310);
            this.ftpTestConnectionButton.Margin = new System.Windows.Forms.Padding(4);
            this.ftpTestConnectionButton.Name = "ftpTestConnectionButton";
            this.ftpTestConnectionButton.Size = new System.Drawing.Size(260, 65);
            this.ftpTestConnectionButton.TabIndex = 7;
            this.ftpTestConnectionButton.UseVisualStyleBackColor = false;
            // 
            // saveFtpSettingsButton
            // 
            this.saveFtpSettingsButton.BackColor = System.Drawing.Color.Transparent;
            this.saveFtpSettingsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveFtpSettingsButton.BackgroundImage")));
            this.saveFtpSettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveFtpSettingsButton.FlatAppearance.BorderSize = 0;
            this.saveFtpSettingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.saveFtpSettingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.saveFtpSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveFtpSettingsButton.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveFtpSettingsButton.ForeColor = System.Drawing.Color.White;
            this.saveFtpSettingsButton.Location = new System.Drawing.Point(312, 310);
            this.saveFtpSettingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveFtpSettingsButton.Name = "saveFtpSettingsButton";
            this.saveFtpSettingsButton.Size = new System.Drawing.Size(270, 65);
            this.saveFtpSettingsButton.TabIndex = 8;
            this.saveFtpSettingsButton.UseVisualStyleBackColor = false;
            // 
            // FtpSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(582, 378);
            this.Controls.Add(this.saveFtpSettingsButton);
            this.Controls.Add(this.ftpTestConnectionButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.ftpAddressTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(600, 425);
            this.MinimumSize = new System.Drawing.Size(600, 425);
            this.Name = "FtpSettingsForm";
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