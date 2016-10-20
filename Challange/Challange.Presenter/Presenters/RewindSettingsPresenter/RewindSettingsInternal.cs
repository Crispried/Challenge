using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Presenter.Presenters.RewindSettingsPresenter
{
    public partial class RewindSettingsPresenter
    {
        private void SetRewindSettings(RewindSettings rewindSettings)
        {
            View.SetRewindSettings(rewindSettings);
        }

        private void SaveSettings(RewindSettings newSettings)
        {
            var rewindSettingsParser =
                new SettingsService<RewindSettings>(
                new RewindSettingsParser());
            rewindSettingsParser.SaveSetting(newSettings);
        }
    }
}
