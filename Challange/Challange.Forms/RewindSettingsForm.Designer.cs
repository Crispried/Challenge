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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveRewindSettings = new System.Windows.Forms.Button();
            this.rewindForwardTextBox = new System.Windows.Forms.TextBox();
            this.rewindBackwardTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rewind forward on";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rewind backward on";
            // 
            // saveRewindSettings
            // 
            this.saveRewindSettings.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveRewindSettings.Location = new System.Drawing.Point(339, 171);
            this.saveRewindSettings.Name = "saveRewindSettings";
            this.saveRewindSettings.Size = new System.Drawing.Size(81, 70);
            this.saveRewindSettings.TabIndex = 2;
            this.saveRewindSettings.Text = "Save";
            this.saveRewindSettings.UseVisualStyleBackColor = true;
            // 
            // rewindForwardTextBox
            // 
            this.rewindForwardTextBox.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rewindForwardTextBox.Location = new System.Drawing.Point(260, 29);
            this.rewindForwardTextBox.MaxLength = 2;
            this.rewindForwardTextBox.Name = "rewindForwardTextBox";
            this.rewindForwardTextBox.Size = new System.Drawing.Size(60, 34);
            this.rewindForwardTextBox.TabIndex = 3;
            this.rewindForwardTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rewindForwardTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rewindForwardTextBox_KeyPress);
            // 
            // rewindBackwardTextBox
            // 
            this.rewindBackwardTextBox.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rewindBackwardTextBox.Location = new System.Drawing.Point(260, 97);
            this.rewindBackwardTextBox.MaxLength = 2;
            this.rewindBackwardTextBox.Name = "rewindBackwardTextBox";
            this.rewindBackwardTextBox.Size = new System.Drawing.Size(60, 34);
            this.rewindBackwardTextBox.TabIndex = 4;
            this.rewindBackwardTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rewindBackwardTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rewindBackwardTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(334, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "frames";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Georgia", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(334, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "frames";
            // 
            // RewindSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(432, 253);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rewindBackwardTextBox);
            this.Controls.Add(this.rewindForwardTextBox);
            this.Controls.Add(this.saveRewindSettings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(450, 300);
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "RewindSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RewindSettingsForm";
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
    }
}