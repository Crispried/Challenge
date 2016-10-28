using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.ChallengeSettingsPresenter
{
    public partial class ChallengeSettingsPresenter
    {
        public override void Run(ChallengeSettings argument)
        {
            challengeSettings = argument;
            SetChallengeSettings();
            View.Show();
        }

        /// <summary>
        /// Changes challenge settings (number of past and future FPSes)
        /// </summary>
        public void ChangeChallengeSettings(ChallengeSettings newSettings)
        {
            if (View.ValidateForm())
            {
                SaveSettings(newSettings);
                challengeSettings.SetSettings(newSettings);
                View.Close();
            }
            else
            {
                ChallengeMessage message =
                    messageParser.GetMessage(MessageType.ChallengeSettingsInvalid);
                View.ShowMessage(message);
            }
        }
    }
}
