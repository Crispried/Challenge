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
        private void ChangeChallengeSettings()
        {
            View.ValidateForm();
            if (View.IsFormValid)
            {
                var newSettings = View.ChallengeSettings;
                SaveSettings(newSettings);
                challengeSettings.NumberOfPastFPS =
                                newSettings.NumberOfPastFPS;
                challengeSettings.NumberOfFutureFPS =
                                newSettings.NumberOfFutureFPS;
                View.Close();
            }
            else
            {
                View.ShowErrorMessage();
            }
        }
    }
}
