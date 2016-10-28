using Challange.Domain.Services.Message;
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
    public partial class FtpSettingsForm : Form, IFtpSettingsView 
    {
        public FtpSettingsForm()
        {
            InitializeComponent();
            saveFtpSettingsButton.Click += (sender, args)
                                => Invoke(ChangeFtpSettings, GetSettings());
            ftpTestConnectionButton.Click += (sender, args)
                                => Invoke(TestFtpConnection, GetSettings());
        }

        public new void Show()
        {
            ShowDialog();
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

        public event Action<FtpSettings> ChangeFtpSettings;

        public event Action<FtpSettings> TestFtpConnection;

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

        public void ShowMessage(ChallengeMessage message)
        {
            string caption = message.Caption;
            string text = message.Text;
            MessageBox.Show(text, caption,
                message.MessageButtons, message.MessageIcon);
        }
    }
}
