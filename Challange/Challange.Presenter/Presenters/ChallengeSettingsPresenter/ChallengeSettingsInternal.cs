using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Presenter.Presenters.ChallengeSettingsPresenter
{
    public partial class ChallengeSettingsPresenter
    {
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
            settingsContext.SaveChallengeSetting(newSettings);
        }
    }
}
