using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
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
                    new ChallengeSettingsParser(new FileWorker()));
            challengeSettingsService.SaveSetting(newSettings);
        }
    }
}
