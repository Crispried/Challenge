using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters
{
    public class ChallengeSettingsPresenter :
                    BasePresenter<IChallengeSettingsView, ChallengeSettings>
    {
        private ChallengeSettings challengeSettings;

        public ChallengeSettingsPresenter(
        IApplicationController controller,
        IChallengeSettingsView challengeSettingsView) :
                base(controller, challengeSettingsView)
        {
            View.ChangeChallengeSettings += () =>
                         ChangeChallengeSettings();
        }

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

        public override void Run(ChallengeSettings argument)
        {
            challengeSettings = argument;
            SetChallengeSettings();
            View.Show();
        }

        /// <summary>
        /// fill the fields with current settings in view
        /// </summary>
        private void SetChallengeSettings()
        {
            View.SetChallengeSettings(challengeSettings);
        }

        /// <summary>
        /// save new settings into file with settings
        /// </summary>
        /// <param name="newSettings"></param>
        private void SaveSettings(ChallengeSettings newSettings)
        {
            var challengeSettingsService =
                    new SettingsService<ChallengeSettings>(
                    new ChallengeSettingsParser());
            challengeSettingsService.SaveSetting(newSettings);
        }
    }
}
