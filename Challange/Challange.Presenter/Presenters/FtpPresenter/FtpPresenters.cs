using Challange.Domain.Entities;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.Presenter.Presenters.FtpPresenter
{
    public partial class FtpPresenter
    {
        public override void Run(FtpSettings argument)
        {
            ftpSettings = argument;
            SetFtpSettings();
            View.Show();
        }

        /// <summary>
        /// Changes challenge settings (number of past and future FPSes)
        /// </summary>
        public void ChangeFtpSettings()
        {
            if (View.ValidateForm())
            {
                // ChallengeSettingsFormIsValid = true;

                var newSettings = View.FtpSettings;
                SaveSettings(newSettings);
                ftpSettings.FtpAddress = newSettings.FtpAddress;
                ftpSettings.UserName = newSettings.UserName;
                ftpSettings.Password = newSettings.Password;
                View.Close();

                // ChallengeSettingsAreSaved = true;
            }
            else
            {
                // ChallengeSettingsFormIsValid = false;
                View.ShowValidationErrorMessage();
            }
        }
    }
}
