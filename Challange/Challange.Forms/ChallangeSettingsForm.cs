using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Message;

namespace Challange.Forms
{
    public partial class ChallangeSettingsForm : 
                                Form, IChallengeSettingsView
    {
        public ChallangeSettingsForm()
        {
            InitializeComponent();
            saveChallangeSettingsButton.Click += (sender, args)
                                => Invoke(ChangeChallengeSettings, GetSettings());
        }

        public new void Show()
        {
            ShowDialog();
        }

        public ChallengeSettings ChallengeSettings
        {
            get
            {
                return GetSettings();
            }
            set
            {
                var challengeSettings = value;
            }
        }
                
        private ChallengeSettings GetSettings()
        {
            var challengeSettings = new ChallengeSettings()
            {
                NumberOfPastFPS =
                    Convert.ToInt32(pastSecondsTextBox.Text),
                NumberOfFutureFPS =
                    Convert.ToInt32(futureSecondsTextBox.Text)
            };
            return challengeSettings;
        }

        public event Action<ChallengeSettings> ChangeChallengeSettings;

        public void SetChallengeSettings(ChallengeSettings challengeSettings)
        {
            SetChallengeView(challengeSettings);
        }

        public void SetChallengeView(ChallengeSettings challengeSettings)
        {
            pastSecondsTextBox.Text = 
                challengeSettings.NumberOfPastFPS.ToString();
            futureSecondsTextBox.Text = 
                challengeSettings.NumberOfFutureFPS.ToString();
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
            return (!string.IsNullOrWhiteSpace(pastSecondsTextBox.Text)) &&
                   (!string.IsNullOrWhiteSpace(futureSecondsTextBox.Text));
        }

        public void ShowMessage(ChallengeMessage message)
        {
            string caption = message.Caption;
            string text = message.Text;
            MessageBox.Show(text, caption,
                message.MessageButtons, message.MessageIcon);
        }

        private void pastSecondsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void futureSecondsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }
    }
}
