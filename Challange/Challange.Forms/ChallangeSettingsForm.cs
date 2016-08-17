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
    }
}
