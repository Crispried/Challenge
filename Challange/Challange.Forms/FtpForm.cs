using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Forms
{
    public partial class FtpForm : Form, IFtpView 
    {
        public FtpForm()
        {
            InitializeComponent();
            saveFtpSettingsButton.Click += (sender, args)
                                => Invoke(ChangeFtpSettings);
        }

        public FtpSettings FtpSettings
        {
            get
            {
                return GetSettings();
            }
            set
            {
                var ftpSettings = value;
            }
        }

        private FtpSettings GetSettings()
        {
            var ftpSettings = new FtpSettings()
            {
                FtpAddress = ftpAddressTextBox.Text,
                UserName = userNameTextBox.Text,
                Password = passwordTextBox.Text
            };
            return ftpSettings;
        }

        public event Action ChangeFtpSettings;

        public void SetFtpSettings(FtpSettings ftpSettings)
        {
            ftpAddressTextBox.Text =
                ftpSettings.FtpAddress.ToString();
            userNameTextBox.Text =
                ftpSettings.UserName.ToString();
            passwordTextBox.Text =
                ftpSettings.Password.ToString();
        }

        public bool ValidateForm()
        {
            if (FormFieldsAreValid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool FormFieldsAreValid()
        {
            return (!string.IsNullOrWhiteSpace(ftpAddressTextBox.Text)) &&
                   (!string.IsNullOrWhiteSpace(userNameTextBox.Text)) &&
                   (!string.IsNullOrWhiteSpace(passwordTextBox.Text));
        }

        public void ShowValidationErrorMessage()
        {
            string caption = "Form is not valid";
            string text = "Please, fill all fields";
            MessageBox.Show(text, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ftpTestConnectionButton_Click(object sender, EventArgs e)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAddressTextBox.Text);
                request.Proxy = null;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(userNameTextBox.Text, passwordTextBox.Text);
                request.GetResponse();
                request.Abort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
