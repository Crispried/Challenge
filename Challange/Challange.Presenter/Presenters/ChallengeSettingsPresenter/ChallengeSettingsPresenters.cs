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
            ChallengeSettingsAreOpened = true;
        }

        /// <summary>
        /// Changes challenge settings (number of past and future FPSes)
        /// </summary>
        public void ChangeChallengeSettings()
        {
            View.ValidateForm();
            if (View.IsFormValid)
            {
                ChallengeSettingsFormIsValid = true;

                var newSettings = View.ChallengeSettings;
                SaveSettings(newSettings);
                challengeSettings.NumberOfPastFPS =
                                newSettings.NumberOfPastFPS;
                challengeSettings.NumberOfFutureFPS =
                                newSettings.NumberOfFutureFPS;
                View.Close();

                ChallengeSettingsAreSaved = true;
            }
            else
            {
                ChallengeSettingsFormIsInvalid = true;
                View.ShowValidationErrorMessage();
            }
        }
    }
}
