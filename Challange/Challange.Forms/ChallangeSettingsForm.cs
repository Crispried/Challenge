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

namespace Challange.Forms
{
    public partial class ChallangeSettingsForm : 
                                Form, IChallengeSettingsView
    {
        private bool isFormValid;

        public ChallangeSettingsForm()
        {
            InitializeComponent();
            saveChallangeSettingsButton.Click += (sender, args)
                                => Invoke(ChangeChallengeSettings);
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
        }

        public bool IsFormValid
        {
            get
            {
                return isFormValid;
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

        public event Action ChangeChallengeSettings;

        public void SetChallengeSettings(ChallengeSettings challengeSettings)
        {
            pastSecondsTextBox.Text = 
                challengeSettings.NumberOfPastFPS.ToString();
            futureSecondsTextBox.Text = 
                challengeSettings.NumberOfFutureFPS.ToString();
        }

        public void ValidateForm()
        {
            if (FormFieldsAreValid())
            {
                isFormValid = true;
            }
            else
            {
                isFormValid = false;
            }
        }

        private bool FormFieldsAreValid()
        {
            return (!string.IsNullOrWhiteSpace(pastSecondsTextBox.Text)) &&
                   (!string.IsNullOrWhiteSpace(futureSecondsTextBox.Text));
        }

        public void ShowValidationErrorMessage()
        {
            string caption = "Form is not valid";
            string text = "Please, fill all fields";
            MessageBox.Show(text, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
