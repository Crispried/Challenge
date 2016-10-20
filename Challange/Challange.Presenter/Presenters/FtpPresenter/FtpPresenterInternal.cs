using Challange.Domain.Entities;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using System;

namespace Challange.Presenter.Presenters.FtpPresenter
{
    public partial class FtpPresenter
    {
        /// <summary>
        /// fill the fields with current ftp settings in view
        /// </summary>
        private void SetFtpSettings()
        {
            View.SetFtpSettings(ftpSettings);
        }

        /// <summary>
        /// save new ftp settings into file with settings
        /// </summary>
        /// <param name="newSettings"></param>
        private void SaveSettings(FtpSettings newSettings)
        {
            var ftpSettingsService =
                    new SettingsService<FtpSettings>(
                    new FtpSettingsParser());
            ftpSettingsService.SaveSetting(newSettings);
        }
    }
}
